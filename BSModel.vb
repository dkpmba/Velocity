Imports System

Public Class BSModel
    Implements IDisposable

    ' ===== Backing fields =====
    Private _S As Double          ' Underlying price
    Private _K As Double          ' Strike
    Private _sigma As Double      ' Volatility (annualized, e.g., 0.22)
    Private _T As Double          ' Time to expiry in YEARS
    Private _r As Double          ' Risk-free (annualized, cont. comp)
    Private _q As Double          ' Dividend yield (annualized, cont. comp)

    Private _callMkt As Double    ' Market call price for IV solving
    Private _putMkt As Double     ' Market put price for IV solving

    Private disposedValue As Boolean

    ' ===== Public API (properties) =====
    Public Property UnderlyingPrice As Double
        Get
            Return _S
        End Get
        Set(value As Double)
            _S = value
        End Set
    End Property

    Public Property ExercisePrice As Double
        Get
            Return _K
        End Get
        Set(value As Double)
            _K = value
        End Set
    End Property

    Public Property Volatility As Double
        Get
            Return _sigma
        End Get
        Set(value As Double)
            If value < 0 Then
                _sigma = 0
            Else
                _sigma = value
            End If
        End Set
    End Property

    ' Externally: set/get DAYS; internally we store YEARS
    Public Property DaysToExpire As Double
        Get
            Return _T * 365.0R
        End Get
        Set(value As Double)
            Dim d As Double = value
            If d < 0 Then d = 0
            _T = d / 365.0R
        End Set
    End Property

    Public Property InterestRate As Double
        Get
            Return _r
        End Get
        Set(value As Double)
            _r = value
        End Set
    End Property

    Public Property Dividend As Double
        Get
            Return _q
        End Get
        Set(value As Double)
            _q = value
        End Set
    End Property

    Public Property CallValue As Double
        Get
            Return _callMkt
        End Get
        Set(value As Double)
            _callMkt = value
        End Set
    End Property

    Public Property PutValue As Double
        Get
            Return _putMkt
        End Get
        Set(value As Double)
            _putMkt = value
        End Set
    End Property

    ' ===== Math helpers =====
    Private Shared Function Phi(x As Double) As Double
        ' Standard normal PDF φ(x)
        Return Math.Exp(-0.5R * x * x) / Math.Sqrt(2.0R * Math.PI)
    End Function

    Private Shared Function CND(x As Double) As Double
        ' Standard normal CDF Φ(x) via Abramowitz–Stegun 7.1.26 approximation
        Dim L As Double = Math.Abs(x)
        Dim k As Double = 1.0R / (1.0R + 0.2316419R * L)
        Dim ksum As Double = (((((1.330274429R * k - 1.821255978R) * k) + 1.781477937R) * k - 0.356563782R) * k + 0.31938153R) * k
        Dim w As Double = 1.0R - Phi(L) * ksum
        If x < 0.0R Then
            Return 1.0R - w
        Else
            Return w
        End If
    End Function

    Private Function d1() As Double
        If _S <= 0 OrElse _K <= 0 OrElse _sigma <= 0 OrElse _T <= 0 Then
            Return 0.0R
        End If
        Return (Math.Log(_S / _K) + (_r - _q + 0.5R * _sigma * _sigma) * _T) / (_sigma * Math.Sqrt(_T))
    End Function

    Private Function d2() As Double
        Return d1() - _sigma * Math.Sqrt(_T)
    End Function

    ' ===== Prices =====
    Private Function CallPrice() As Double
        If _T <= 0 Then
            Return Math.Max(0.0R, _S - _K)
        End If
        If _sigma <= 0 Then
            Dim fwd As Double = _S * Math.Exp((_r - _q) * _T)
            Return Math.Exp(-_r * _T) * Math.Max(0.0R, fwd - _K)
        End If
        Dim d_1 As Double = d1()
        Dim d_2 As Double = d2()
        Return _S * Math.Exp(-_q * _T) * CND(d_1) - _K * Math.Exp(-_r * _T) * CND(d_2)
    End Function

    Private Function PutPrice() As Double
        If _T <= 0 Then
            Return Math.Max(0.0R, _K - _S)
        End If
        If _sigma <= 0 Then
            Dim fwd As Double = _S * Math.Exp((_r - _q) * _T)
            Return Math.Exp(-_r * _T) * Math.Max(0.0R, _K - fwd)
        End If
        Dim d_1 As Double = d1()
        Dim d_2 As Double = d2()
        Return _K * Math.Exp(-_r * _T) * CND(-d_2) - _S * Math.Exp(-_q * _T) * CND(-d_1)
    End Function

    Public ReadOnly Property CallOption As Double
        Get
            Return CallPrice()
        End Get
    End Property

    Public ReadOnly Property PutOption As Double
        Get
            Return PutPrice()
        End Get
    End Property

    ' ===== Greeks =====
    Public ReadOnly Property CallDelta As Double
        Get
            If _T <= 0 Then
                If _S > _K Then
                    Return 1.0R
                Else
                    Return 0.0R
                End If
            End If
            Return Math.Exp(-_q * _T) * CND(d1())
        End Get
    End Property

    Public ReadOnly Property PutDelta As Double
        Get
            If _T <= 0 Then
                If _S < _K Then
                    Return -1.0R
                Else
                    Return 0.0R
                End If
            End If
            Return Math.Exp(-_q * _T) * (CND(d1()) - 1.0R)
        End Get
    End Property

    Public ReadOnly Property Gamma As Double
        Get
            If _S <= 0 OrElse _sigma <= 0 OrElse _T <= 0 Then
                Return 0.0R
            End If
            Return Math.Exp(-_q * _T) * Phi(d1()) / (_S * _sigma * Math.Sqrt(_T))
        End Get
    End Property

    Public ReadOnly Property Vega As Double
        Get
            If _S <= 0 OrElse _T <= 0 Then
                Return 0.0R
            End If
            ' Per 1 vol point (1%) convention
            Return 0.01R * _S * Math.Exp(-_q * _T) * Phi(d1()) * Math.Sqrt(_T)
        End Get
    End Property

    Public ReadOnly Property CallTheta As Double
        Get
            If _T <= 0 OrElse _sigma <= 0 Then
                Return 0.0R
            End If
            Dim term1 As Double = -_S * Math.Exp(-_q * _T) * Phi(d1()) * _sigma / (2.0R * Math.Sqrt(_T))
            Dim term2 As Double = _q * _S * Math.Exp(-_q * _T) * CND(d1())
            Dim term3 As Double = -_r * _K * Math.Exp(-_r * _T) * CND(d2())
            ' Return per DAY
            Return (term1 + term2 + term3) / 365.0R
        End Get
    End Property

    Public ReadOnly Property PutTheta As Double
        Get
            If _T <= 0 OrElse _sigma <= 0 Then
                Return 0.0R
            End If
            Dim term1 As Double = -_S * Math.Exp(-_q * _T) * Phi(d1()) * _sigma / (2.0R * Math.Sqrt(_T))
            Dim term2 As Double = -_q * _S * Math.Exp(-_q * _T) * CND(-d1())
            Dim term3 As Double = _r * _K * Math.Exp(-_r * _T) * CND(-d2())
            ' Return per DAY
            Return (term1 + term2 + term3) / 365.0R
        End Get
    End Property

    Public ReadOnly Property CallRho As Double
        Get
            If _T <= 0 Then
                Return 0.0R
            End If
            Return 0.01R * _K * _T * Math.Exp(-_r * _T) * CND(d2())
        End Get
    End Property

    Public ReadOnly Property PutRho As Double
        Get
            If _T <= 0 Then
                Return 0.0R
            End If
            Return -0.01R * _K * _T * Math.Exp(-_r * _T) * CND(-d2())
        End Get
    End Property

    ' ===== Implied vol (Newton–Raphson w/ bisection fallback) =====
    Private Function ImpliedVol(targetPrice As Double, isCall As Boolean,
                                Optional tol As Double = 0.0001,
                                Optional maxIter As Integer = 100) As Double
        If targetPrice <= 0 Then
            Return 0.0R
        End If

        Dim lo As Double = 0.0001R   ' 1bp vol
        Dim hi As Double = 5.0R      ' 500% cap
        Dim sigma As Double = 0.2R   ' initial guess

        Dim priceAt As Func(Of Double, Double) =
            Function(sig As Double) As Double
                Dim keep = _sigma
                _sigma = sig
                Dim p As Double = If(isCall, CallPrice(), PutPrice())
                _sigma = keep
                Return p
            End Function

        Dim vegaAt As Func(Of Double, Double) =
            Function(sig As Double) As Double
                Dim keep = _sigma
                _sigma = sig
                Dim v As Double = Vega ' Vega property returns per 1% vol
                _sigma = keep
                Return v
            End Function

        ' Ensure bracket contains the target
        Dim plo As Double = priceAt(lo)
        If plo > targetPrice Then
            Return lo
        End If
        Dim phi As Double = priceAt(hi)
        If phi < targetPrice Then
            Return hi
        End If

        For i As Integer = 1 To maxIter
            Dim p As Double = priceAt(sigma)
            Dim diff As Double = p - targetPrice
            If Math.Abs(diff) < tol Then
                Return Math.Max(lo, Math.Min(hi, sigma))
            End If

            Dim v As Double = vegaAt(sigma)
            If v > 0.00000001 Then
                ' Vega is per 1% vol → divide by (v/0.01) to step in sigma units
                Dim nextSigma As Double = sigma - diff / (v / 0.01R)
                If nextSigma > lo AndAlso nextSigma < hi Then
                    sigma = nextSigma
                Else
                    sigma = 0.5R * (lo + hi)
                End If
            Else
                sigma = 0.5R * (lo + hi)
            End If

            ' Update bracket
            Dim pmid As Double = priceAt(sigma)
            If pmid > targetPrice Then
                hi = sigma
            Else
                lo = sigma
            End If
        Next

        Return Math.Max(lo, Math.Min(hi, sigma))
    End Function

    Public ReadOnly Property CallIV As Double
        Get
            Return ImpliedVol(_callMkt, True)
        End Get
    End Property

    Public ReadOnly Property PutIV As Double
        Get
            Return ImpliedVol(_putMkt, False)
        End Get
    End Property

    ' ===== IDisposable =====
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
