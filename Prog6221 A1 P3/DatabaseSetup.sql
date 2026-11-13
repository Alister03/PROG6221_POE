USE CyberBotDB;
GO
IF OBJECT_ID(N'dbo.CyberTask', N'U') IS NULL
CREATE TABLE dbo.CyberTask (
    TaskId       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserName     NVARCHAR(50) NOT NULL,
    Title        NVARCHAR(200) NOT NULL,
    Description  NVARCHAR(500) NULL,
    ReminderDate DATETIME2 NULL,
    IsCompleted  BIT NOT NULL DEFAULT 0,
    DateCreated  DATETIME2 DEFAULT SYSDATETIME()
);
GO