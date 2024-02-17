IF NOT EXISTS(SELECT * FROM [sys].[database_principals] WHERE [name] = 'app-toggleon-api-dev')
BEGIN
	CREATE USER [app-toggleon-api-dev] FROM  EXTERNAL PROVIDER  WITH DEFAULT_SCHEMA=[dbo]
	ALTER ROLE db_datareader ADD MEMBER [app-toggleon-api-dev];
	ALTER ROLE db_datawriter ADD MEMBER [app-toggleon-api-dev];
END
GO