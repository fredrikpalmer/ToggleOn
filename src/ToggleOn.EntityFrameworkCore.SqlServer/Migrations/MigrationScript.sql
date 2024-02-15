IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE TABLE [FeatureGroup] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [UserIds] nvarchar(max) NOT NULL,
        [IpAddresses] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FeatureGroup] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE TABLE [FeatureToggle] (
        [Id] uniqueidentifier NOT NULL,
        [ProjectId] uniqueidentifier NOT NULL,
        [EnvironmentId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Enabled] bit NOT NULL,
        [FeatureGroups] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FeatureToggle] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE TABLE [Project] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Project] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE TABLE [FeatureRule] (
        [Id] uniqueidentifier NOT NULL,
        [FeatureToggleId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Value] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FeatureRule] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FeatureRule_FeatureToggle_FeatureToggleId] FOREIGN KEY ([FeatureToggleId]) REFERENCES [FeatureToggle] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE TABLE [Environment] (
        [Id] uniqueidentifier NOT NULL,
        [ProjectId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Environment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Environment_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE INDEX [IX_Environment_ProjectId] ON [Environment] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    CREATE INDEX [IX_FeatureRule_FeatureToggleId] ON [FeatureRule] ([FeatureToggleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240113230010_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240113230010_InitialCreate', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    ALTER TABLE [Environment] DROP CONSTRAINT [FK_Environment_Project_ProjectId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    DROP TABLE [FeatureRule];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureToggle]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FeatureToggle] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [FeatureToggle] DROP COLUMN [Name];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    EXEC sp_rename N'[FeatureToggle].[ProjectId]', N'FeatureId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureToggle]') AND [c].[name] = N'FeatureGroups');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FeatureToggle] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [FeatureToggle] ALTER COLUMN [FeatureGroups] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureGroup]') AND [c].[name] = N'UserIds');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [FeatureGroup] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [FeatureGroup] ALTER COLUMN [UserIds] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureGroup]') AND [c].[name] = N'IpAddresses');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [FeatureGroup] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [FeatureGroup] ALTER COLUMN [IpAddresses] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE TABLE [Feature] (
        [Id] uniqueidentifier NOT NULL,
        [ProjectId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Feature] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Feature_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE TABLE [FeatureFilter] (
        [Id] uniqueidentifier NOT NULL,
        [FeatureToggleId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_FeatureFilter] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FeatureFilter_FeatureToggle_FeatureToggleId] FOREIGN KEY ([FeatureToggleId]) REFERENCES [FeatureToggle] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE INDEX [IX_FeatureToggle_EnvironmentId] ON [FeatureToggle] ([EnvironmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE INDEX [IX_FeatureToggle_FeatureId] ON [FeatureToggle] ([FeatureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE INDEX [IX_Feature_ProjectId] ON [Feature] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    CREATE INDEX [IX_FeatureFilter_FeatureToggleId] ON [FeatureFilter] ([FeatureToggleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    ALTER TABLE [Environment] ADD CONSTRAINT [FK_Environment_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    ALTER TABLE [FeatureToggle] ADD CONSTRAINT [FK_FeatureToggle_Environment_EnvironmentId] FOREIGN KEY ([EnvironmentId]) REFERENCES [Environment] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    ALTER TABLE [FeatureToggle] ADD CONSTRAINT [FK_FeatureToggle_Feature_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Feature] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240117101320_Refactoring')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240117101320_Refactoring', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureFilter]') AND [c].[name] = N'Value');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [FeatureFilter] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [FeatureFilter] DROP COLUMN [Value];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Project]') AND [c].[name] = N'Name');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Project] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Project] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureGroup]') AND [c].[name] = N'Name');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [FeatureGroup] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [FeatureGroup] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feature]') AND [c].[name] = N'Name');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Feature] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Feature] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Environment]') AND [c].[name] = N'Name');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Environment] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Environment] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    ALTER TABLE [Project] ADD CONSTRAINT [AK_Project_Name] UNIQUE ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    ALTER TABLE [FeatureGroup] ADD CONSTRAINT [AK_FeatureGroup_Name] UNIQUE ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    ALTER TABLE [Feature] ADD CONSTRAINT [AK_Feature_Name] UNIQUE ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    ALTER TABLE [Environment] ADD CONSTRAINT [AK_Environment_Name] UNIQUE ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    CREATE TABLE [FeatureFilterParameter] (
        [Id] uniqueidentifier NOT NULL,
        [FeatureFilterId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Value] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FeatureFilterParameter] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId] FOREIGN KEY ([FeatureFilterId]) REFERENCES [FeatureFilter] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    CREATE INDEX [IX_FeatureFilterParameter_FeatureFilterId] ON [FeatureFilterParameter] ([FeatureFilterId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240120081200_FilterParameters')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240120081200_FilterParameters', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    ALTER TABLE [Feature] DROP CONSTRAINT [AK_Feature_Name];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    DROP INDEX [IX_Feature_ProjectId] ON [Feature];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    ALTER TABLE [Environment] DROP CONSTRAINT [AK_Environment_Name];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    DROP INDEX [IX_Environment_ProjectId] ON [Environment];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    ALTER TABLE [Feature] ADD CONSTRAINT [AK_Feature_ProjectId_Name] UNIQUE ([ProjectId], [Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    ALTER TABLE [Environment] ADD CONSTRAINT [AK_Environment_ProjectId_Name] UNIQUE ([ProjectId], [Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127160321_NameConstraint')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240127160321_NameConstraint', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127162232_CascadeEnvironments')
BEGIN
    ALTER TABLE [Environment] DROP CONSTRAINT [FK_Environment_Project_ProjectId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127162232_CascadeEnvironments')
BEGIN
    ALTER TABLE [Environment] ADD CONSTRAINT [FK_Environment_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127162232_CascadeEnvironments')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240127162232_CascadeEnvironments', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureFilter] DROP CONSTRAINT [FK_FeatureFilter_FeatureToggle_FeatureToggleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureFilterParameter] DROP CONSTRAINT [FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureToggle] DROP CONSTRAINT [FK_FeatureToggle_Feature_FeatureId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureFilter] ADD CONSTRAINT [FK_FeatureFilter_FeatureToggle_FeatureToggleId] FOREIGN KEY ([FeatureToggleId]) REFERENCES [FeatureToggle] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureFilterParameter] ADD CONSTRAINT [FK_FeatureFilterParameter_FeatureFilter_FeatureFilterId] FOREIGN KEY ([FeatureFilterId]) REFERENCES [FeatureFilter] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    ALTER TABLE [FeatureToggle] ADD CONSTRAINT [FK_FeatureToggle_Feature_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Feature] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240127170356_CascadeFeature')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240127170356_CascadeFeature', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240128125637_ToggleAlternateKey')
BEGIN
    DROP INDEX [IX_FeatureToggle_FeatureId] ON [FeatureToggle];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240128125637_ToggleAlternateKey')
BEGIN
    ALTER TABLE [FeatureToggle] ADD CONSTRAINT [AK_FeatureToggle_FeatureId_EnvironmentId] UNIQUE ([FeatureId], [EnvironmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240128125637_ToggleAlternateKey')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240128125637_ToggleAlternateKey', N'7.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209111227_DropFeatureToggleFeatureGroups')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureToggle]') AND [c].[name] = N'FeatureGroups');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [FeatureToggle] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [FeatureToggle] DROP COLUMN [FeatureGroups];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209111227_DropFeatureToggleFeatureGroups')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240209111227_DropFeatureToggleFeatureGroups', N'7.0.15');
END;
GO

COMMIT;
GO


