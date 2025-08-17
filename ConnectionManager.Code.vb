
Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Microsoft.Data.SqlClient
'Imports Velocity.Core
Imports Velocity.Core

Partial Class ConnectionManager
    ' === TWS connectivity state ===

    ' Helper: find a control by name safely
    Private Function FindCtl(Of T As Control)(name As String) As T
        Dim arr = Me.Controls.Find(name, True)
        If arr IsNot Nothing AndAlso arr.Length > 0 Then
            Return TryCast(arr(0), T)
        End If
        Return Nothing
    End Function

    Private Sub ConnectionManager_Wire_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Wire buttons dynamically so this file works regardless of designer field names.
        Dim btnTest = FindCtl(Of Button)("btnTestDb")
        If btnTest IsNot Nothing Then AddHandler btnTest.Click, AddressOf BtnTestDb_Click

        Dim btnSave = FindCtl(Of Button)("btnSaveDb")
        If btnSave IsNot Nothing Then AddHandler btnSave.Click, AddressOf BtnSaveDb_Click

        Dim btnLoad = FindCtl(Of Button)("btnLoadDb")
        If btnLoad IsNot Nothing Then AddHandler btnLoad.Click, AddressOf BtnLoadDb_Click
    End Sub

    Private Function GetConnString() As String
        Dim txt = FindCtl(Of TextBox)("txtDbConnection")
        If txt Is Nothing Then Return Nothing
        Return txt.Text.Trim()
    End Function

    Private Sub SetConnString(value As String)
        Dim txt = FindCtl(Of TextBox)("txtDbConnection")
        If txt IsNot Nothing Then txt.Text = value
    End Sub

    Private Sub SetStatus(msg As String, Optional ok As Boolean? = Nothing)
        Dim lbl = FindCtl(Of Label)("lblDbStatus")
        If lbl IsNot Nothing Then
            lbl.Text = msg
            If ok.HasValue Then
                lbl.ForeColor = If(ok.Value, Color.ForestGreen, Color.Firebrick)
            End If
        Else
            ' Fallback: show a toast
            If ok.HasValue AndAlso Not ok.Value Then
                MessageBox.Show(msg, "Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub BtnTestDb_Click(sender As Object, e As EventArgs)
        Dim cs = GetConnString()
        If String.IsNullOrWhiteSpace(cs) Then
            MessageBox.Show("Enter a SQL Server connection string first.")
            Return
        End If

        Cursor.Current = Cursors.WaitCursor
        Try
            Using c As New SqlConnection(cs)
                c.Open()

                ' Basic info
                Dim server As String = Nothing
                Dim db As String = Nothing
                Using cmd As New SqlCommand("SELECT @@SERVERNAME AS [server], DB_NAME() AS [db]", c)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            server = Convert.ToString(r("server"))
                            db = Convert.ToString(r("db"))
                        End If
                    End Using
                End Using

                ' Round-trip temp table
                Using cmd As New SqlCommand("CREATE TABLE #ping(v int); INSERT INTO #ping VALUES (1); SELECT SUM(v) FROM #ping;", c)
                    Dim sumObj = cmd.ExecuteScalar()
                    Dim sum As Integer = If(sumObj Is Nothing OrElse sumObj Is DBNull.Value, 0, Convert.ToInt32(sumObj))
                    If sum <> 1 Then Throw New Exception("Temp table round-trip failed.")
                End Using

                ' Schema check
                Dim hasSettings As Boolean
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM sys.tables WHERE name='settings' AND schema_id=SCHEMA_ID('dbo');", c)
                    hasSettings = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using

                If hasSettings Then
                    SetStatus(String.Format("Connected to {0} / {1}. Schema OK.", server, db), True)
                Else
                    SetStatus(String.Format("Connected to {0} / {1}. WARNING: dbo.settings missing. Run Schema_VelocityDB.sql.", server, db), False)
                End If
            End Using
        Catch ex As Exception
            SetStatus("DB error: " & ex.Message, False)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub BtnSaveDb_Click(sender As Object, e As EventArgs)
        Dim cs = GetConnString()
        If String.IsNullOrWhiteSpace(cs) Then
            MessageBox.Show("Enter a SQL Server connection string first.")
            Return
        End If

        Cursor.Current = Cursors.WaitCursor
        Try
            ' Verify schema presence and write into dbo.settings
            Using c As New SqlConnection(cs)
                c.Open()

                Dim hasSettings As Boolean
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM sys.tables WHERE name='settings' AND schema_id=SCHEMA_ID('dbo');", c)
                    hasSettings = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
                If Not hasSettings Then
                    Throw New InvalidOperationException("dbo.settings not found. Please run Schema_VelocityDB.sql in this database first.")
                End If
            End Using

            ' Save to settings using our repository
            Dim cf As IConnectionFactory = New SqlConnectionFactory(cs)
            Dim repo As ISettingsRepository = New SqlSettingsRepository(cf)
            repo.SetValue("app.db.connection", cs)
            AppServices.Initialize(cs)
            'UpdateDbStatus(GetDbStatusText(cs))
            SetStatus("Connection string saved to dbo.settings (key = 'app.db.connection').", True)
        Catch ex As Exception
            SetStatus("Save failed: " & ex.Message, False)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub BtnLoadDb_Click(sender As Object, e As EventArgs)
        Dim cs = GetConnString()
        If String.IsNullOrWhiteSpace(cs) Then
            MessageBox.Show("Enter a SQL Server connection string (to the VelocityDB database) so I can load settings from it.")
            Return
        End If

        Cursor.Current = Cursors.WaitCursor
        Try
            Dim cf As IConnectionFactory = New SqlConnectionFactory(cs)
            Dim repo As ISettingsRepository = New SqlSettingsRepository(cf)
            Dim saved = repo.GetValue("app.db.connection")
            If String.IsNullOrWhiteSpace(saved) Then
                SetStatus("No saved value for 'app.db.connection' in dbo.settings.", False)
            Else
                SetConnString(saved)
                SetStatus("Loaded saved connection string from dbo.settings.", True)
                AppServices.Initialize(saved)
            End If
        Catch ex As Exception
            SetStatus("Load failed: " & ex.Message, False)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

End Class
