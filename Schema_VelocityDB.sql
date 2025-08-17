/* VelocityDB schema for Microsoft SQL Server
   Target: SQL Server 2019+ (your instance is v16)
   Run as a user with CREATE permissions.
*/

IF DB_ID('VelocityDB') IS NULL
BEGIN
    PRINT 'Creating database VelocityDB...'
    EXEC('CREATE DATABASE VelocityDB');
END
GO

USE VelocityDB;
GO

-- 1) Core reference tables -----------------------------------------------------
IF OBJECT_ID('dbo.symbols','U') IS NULL
BEGIN
CREATE TABLE dbo.symbols(
    cid             BIGINT      NOT NULL PRIMARY KEY,
    symbol          NVARCHAR(64) NOT NULL,
    sectype         NVARCHAR(16) NOT NULL,
    exchange        NVARCHAR(64) NULL,
    mult            INT          NULL,
    increment       DECIMAL(18,8) NULL,
    iv              DECIMAL(18,6) NULL,
    liquidity_score DECIMAL(18,6) NULL,
    enabled         BIT          NOT NULL DEFAULT(1),
    created_utc     DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    updated_utc     DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    rv              ROWVERSION
);
CREATE INDEX IX_symbols_symbol ON dbo.symbols(symbol);
END
GO

-- 2) Trades + Legs -------------------------------------------------------------
IF OBJECT_ID('dbo.trades','U') IS NULL
BEGIN
CREATE TABLE dbo.trades(
    tid         BIGINT IDENTITY(1000,1) NOT NULL PRIMARY KEY,
    symbol      NVARCHAR(64) NOT NULL,
    structure   NVARCHAR(32) NOT NULL,
    size        INT NOT NULL,
    strike_mid  DECIMAL(18,6) NULL,
    credit      DECIMAL(18,6) NULL,
    status      NVARCHAR(32) NOT NULL,
    start_time  DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    last_update DATETIME2(3) NULL,
    atr3        DECIMAL(18,6) NULL,
    ema_state   NVARCHAR(16) NULL,
    time_value  DECIMAL(18,6) NULL,
    urgl        DECIMAL(18,6) NULL,
    rgl         DECIMAL(18,6) NULL,
    net_delta   DECIMAL(18,8) NULL,
    net_gamma   DECIMAL(18,8) NULL,
    net_theta   DECIMAL(18,8) NULL,
    rv          ROWVERSION
);
CREATE INDEX IX_trades_status ON dbo.trades(status);
CREATE INDEX IX_trades_symbol ON dbo.trades(symbol);
END
GO

IF OBJECT_ID('dbo.trade_legs','U') IS NULL
BEGIN
CREATE TABLE dbo.trade_legs(
    tid        BIGINT NOT NULL,
    leg_id     INT    NOT NULL,
    cid        BIGINT NOT NULL,
    sectype    NVARCHAR(16) NOT NULL,
    symbol     NVARCHAR(64) NOT NULL,
    [right]    NVARCHAR(8)  NULL,
    strike     DECIMAL(18,6) NULL,
    expiry     DATE NULL,
    qty        INT NOT NULL,
    avg_cost   DECIMAL(18,6) NULL,
    mark       DECIMAL(18,6) NULL,
    time_value DECIMAL(18,6) NULL,
    delta      DECIMAL(18,8) NULL,
    gamma      DECIMAL(18,8) NULL,
    theta      DECIMAL(18,8) NULL,
    vega       DECIMAL(18,8) NULL,
    exchange   NVARCHAR(64) NULL,
    PRIMARY KEY(tid, leg_id),
    CONSTRAINT FK_trade_legs_trades FOREIGN KEY(tid) REFERENCES dbo.trades(tid),
    CONSTRAINT FK_trade_legs_symbols FOREIGN KEY(cid) REFERENCES dbo.symbols(cid)
);
CREATE INDEX IX_trade_legs_cid ON dbo.trade_legs(cid);
END
GO

-- 3) Orders + Fills ------------------------------------------------------------
IF OBJECT_ID('dbo.orders','U') IS NULL
BEGIN
CREATE TABLE dbo.orders(
    oid        BIGINT NOT NULL PRIMARY KEY,
    tid        BIGINT NULL,
    account    NVARCHAR(32) NULL,
    symbol     NVARCHAR(64) NULL,
    side       NVARCHAR(8)  NULL,
    sectype    NVARCHAR(16) NULL,
    qty        INT NULL,
    order_type NVARCHAR(16) NULL,
    price      DECIMAL(18,6) NULL,
    status     NVARCHAR(32) NULL,
    fills      INT NULL,
    remaining  INT NULL,
    exchange   NVARCHAR(64) NULL,
    [error]    NVARCHAR(256) NULL,
    [time]     DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    CONSTRAINT FK_orders_trades FOREIGN KEY(tid) REFERENCES dbo.trades(tid)
);
CREATE INDEX IX_orders_tid ON dbo.orders(tid);
CREATE INDEX IX_orders_time ON dbo.orders([time]);
END
GO

IF OBJECT_ID('dbo.fills','U') IS NULL
BEGIN
CREATE TABLE dbo.fills(
    exec_id    NVARCHAR(64) NOT NULL PRIMARY KEY,
    oid        BIGINT NOT NULL,
    tid        BIGINT NULL,
    [time]     DATETIME2(3) NOT NULL,
    qty        INT NOT NULL,
    price      DECIMAL(18,6) NOT NULL,
    commission DECIMAL(18,6) NULL,
    fees       DECIMAL(18,6) NULL,
    exchange   NVARCHAR(64) NULL,
    liquidity  NVARCHAR(16) NULL,
    CONSTRAINT FK_fills_orders FOREIGN KEY(oid) REFERENCES dbo.orders(oid)
);
CREATE INDEX IX_fills_oid ON dbo.fills(oid);
CREATE INDEX IX_fills_tid ON dbo.fills(tid);
END
GO

-- 4) Market data ---------------------------------------------------------------
IF OBJECT_ID('dbo.bars_1m','U') IS NULL
BEGIN
CREATE TABLE dbo.bars_1m(
    cid   BIGINT NOT NULL,
    [time] DATETIME2(0) NOT NULL,
    [open]  DECIMAL(18,6) NOT NULL,
    [high]  DECIMAL(18,6) NOT NULL,
    [low]   DECIMAL(18,6) NOT NULL,
    [close] DECIMAL(18,6) NOT NULL,
    vol     BIGINT NOT NULL,
    PRIMARY KEY(cid, [time]),
    CONSTRAINT FK_bars_symbols FOREIGN KEY(cid) REFERENCES dbo.symbols(cid)
);
END
GO

IF OBJECT_ID('dbo.quotes','U') IS NULL
BEGIN
CREATE TABLE dbo.quotes(
    cid   BIGINT NOT NULL PRIMARY KEY,
    [time] DATETIME2(3) NOT NULL,
    bid  DECIMAL(18,6) NULL,
    ask  DECIMAL(18,6) NULL,
    last DECIMAL(18,6) NULL,
    iv   DECIMAL(18,6) NULL,
    CONSTRAINT FK_quotes_symbols FOREIGN KEY(cid) REFERENCES dbo.symbols(cid)
);
END
GO

IF OBJECT_ID('dbo.greeks','U') IS NULL
BEGIN
CREATE TABLE dbo.greeks(
    cid    BIGINT NOT NULL PRIMARY KEY,
    [time] DATETIME2(3) NOT NULL,
    delta  DECIMAL(18,8) NULL,
    gamma  DECIMAL(18,8) NULL,
    theta  DECIMAL(18,8) NULL,
    vega   DECIMAL(18,8) NULL,
    iv     DECIMAL(18,6) NULL,
    CONSTRAINT FK_greeks_symbols FOREIGN KEY(cid) REFERENCES dbo.symbols(cid)
);
END
GO

-- 5) Alerts + Activity + Settings ---------------------------------------------
IF OBJECT_ID('dbo.alerts','U') IS NULL
BEGIN
CREATE TABLE dbo.alerts(
    id           BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [time]       DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    severity     NVARCHAR(16) NOT NULL,
    [type]       NVARCHAR(32) NOT NULL,
    code         NVARCHAR(32) NULL,
    message      NVARCHAR(512) NOT NULL,
    symbol       NVARCHAR(64) NULL,
    tid          BIGINT NULL,
    ack          BIT NOT NULL DEFAULT(0),
    snooze_until DATETIME2(3) NULL
);
CREATE INDEX IX_alerts_time ON dbo.alerts([time]);
CREATE INDEX IX_alerts_ack ON dbo.alerts(ack);
END
GO

IF OBJECT_ID('dbo.activity','U') IS NULL
BEGIN
CREATE TABLE dbo.activity(
    id      BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [time]  DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME()),
    source  NVARCHAR(64) NOT NULL,
    level   NVARCHAR(16) NOT NULL,
    message NVARCHAR(512) NOT NULL,
    context NVARCHAR(512) NULL
);
CREATE INDEX IX_activity_time ON dbo.activity([time]);
END
GO

IF OBJECT_ID('dbo.settings','U') IS NULL
BEGIN
CREATE TABLE dbo.settings(
    [key]  NVARCHAR(128) NOT NULL PRIMARY KEY,
    json   NVARCHAR(MAX) NOT NULL,
    updated_utc DATETIME2(3) NOT NULL DEFAULT(SYSUTCDATETIME())
);
END
GO

PRINT 'VelocityDB schema ready.'
