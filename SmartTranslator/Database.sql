USE [master]
GO

-- create logins
IF NOT EXISTS 
(
	SELECT NULL FROM sys.sql_logins WHERE name = 'ST_Admin' AND default_database_name = 'master'
)
BEGIN
	CREATE LOGIN [ST_Admin] WITH PASSWORD = N'ST_admin911', DEFAULT_DATABASE = [master], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF;
END;


--create database SmartTranslator
IF NOT EXISTS
(
	SELECT NULL FROM sys.databases WHERE name = 'SmartTranslator'
)
BEGIN
	CREATE DATABASE [SmartTranslator]
END;
GO


USE [SmartTranslator]
GO

-- create users
IF NOT EXISTS
(
	SELECT NULL FROM sys.database_principals WHERE type_desc = 'SQL_USER' AND name = 'ST_Admin'
)
BEGIN
	--ALTER USER [AIt_Admin] WITH LOGIN = [AIt_Admin]
	CREATE USER [ST_Admin] FOR LOGIN [ST_Admin]
END;
GO

-- create schemas AuditLog, Monitoring, Document, WBS, HR
IF NOT EXISTS
(
	SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'Translate'
)
BEGIN 
	EXEC sp_executesql N'CREATE SCHEMA [Translate]'
END;
GO

--granting rights on schemas for ST_Admin
EXEC sp_addrolemember 'db_owner', 'ST_Admin'

--create tables
IF NOT EXISTS 
(
	SELECT NULL FROM sys.tables WHERE name = 'Dictionary'
)
BEGIN
	CREATE TABLE [Translate].[Dictionary]
	(
		[StringID]			[int] NOT NULL IDENTITY(1, 1),
		[VendorCode] 		[varchar](100) NULL UNIQUE,
		[ItalianString]		[varchar](900) NULL,
		[UkrainianString]	[varchar](900) NULL,
		[DimensionSuffix]	[varchar](100) NULL,
		CONSTRAINT [PK_Translate.Dictionary] PRIMARY KEY CLUSTERED ([StringID] ASC)
	)
END;
GO

CREATE NONCLUSTERED INDEX it_str_index ON [Translate].[Dictionary]([ItalianString])
CREATE NONCLUSTERED INDEX ukr_str_index ON [Translate].[Dictionary]([UkrainianString])
CREATE NONCLUSTERED INDEX vcode_index ON [Translate].[Dictionary]([VendorCode])