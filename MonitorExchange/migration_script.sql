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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531073736_mssql.onprem_migration_818'
)
BEGIN
    CREATE TABLE [FileExchanges] (
        [Id] uniqueidentifier NOT NULL,
        [DataCreate] datetime2 NOT NULL,
        [StrId] nvarchar(max) NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Item] int NOT NULL,
        [InAll] int NOT NULL,
        CONSTRAINT [PK_FileExchanges] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531073736_mssql.onprem_migration_818'
)
BEGIN
    CREATE TABLE [Goodses] (
        [Id] uniqueidentifier NOT NULL,
        [strId] nvarchar(max) NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Artikul] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [NameUkr] nvarchar(max) NOT NULL,
        [NameFull] nvarchar(max) NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [View] nvarchar(max) NOT NULL,
        [Manufacturer] nvarchar(max) NOT NULL,
        [Country] nvarchar(max) NOT NULL,
        [CountryUkr] nvarchar(max) NOT NULL,
        [Material] nvarchar(max) NOT NULL,
        [MaterialUkr] nvarchar(max) NOT NULL,
        [Season] nvarchar(max) NOT NULL,
        [Color] nvarchar(max) NOT NULL,
        [Categoria] nvarchar(max) NOT NULL,
        [CategoriaSite] nvarchar(max) NOT NULL,
        [CategoriaSiteUkr] nvarchar(max) NOT NULL,
        [Sex] nvarchar(max) NOT NULL,
        [Marke] nvarchar(max) NOT NULL,
        [Brend] nvarchar(max) NOT NULL,
        [DataSeason] datetime2 NOT NULL,
        CONSTRAINT [PK_Goodses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531073736_mssql.onprem_migration_818'
)
BEGIN
    CREATE TABLE [GoodsSizes] (
        [Id] uniqueidentifier NOT NULL,
        [strId] nvarchar(max) NOT NULL,
        [GoodsId] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_GoodsSizes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531073736_mssql.onprem_migration_818'
)
BEGIN
    CREATE TABLE [Stocks] (
        [Id] uniqueidentifier NOT NULL,
        [StrId] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Stocks] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531073736_mssql.onprem_migration_818'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240531073736_mssql.onprem_migration_818', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531074758_mssql.onprem_migration_792'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240531074758_mssql.onprem_migration_792', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531202832_mssql.onprem_migration_820'
)
BEGIN
    ALTER TABLE [FileExchanges] DROP CONSTRAINT [PK_FileExchanges];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531202832_mssql.onprem_migration_820'
)
BEGIN
    EXEC sp_rename N'[FileExchanges]', N'FilesExchange';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531202832_mssql.onprem_migration_820'
)
BEGIN
    ALTER TABLE [FilesExchange] ADD CONSTRAINT [PK_FilesExchange] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240531202832_mssql.onprem_migration_820'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240531202832_mssql.onprem_migration_820', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240601082627_mssql.onprem_migration_596'
)
BEGIN
    ALTER TABLE [FilesExchange] DROP CONSTRAINT [PK_FilesExchange];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240601082627_mssql.onprem_migration_596'
)
BEGIN
    EXEC sp_rename N'[FilesExchange]', N'FileExchanges';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240601082627_mssql.onprem_migration_596'
)
BEGIN
    ALTER TABLE [FileExchanges] ADD CONSTRAINT [PK_FileExchanges] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240601082627_mssql.onprem_migration_596'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [PasswordHash] varbinary(max) NOT NULL,
        [PasswordSalt] varbinary(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240601082627_mssql.onprem_migration_596'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240601082627_mssql.onprem_migration_596', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602121637_mssql.onprem_migration_775'
)
BEGIN
    ALTER TABLE [GoodsSizes] ADD [GoodsId1] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602121637_mssql.onprem_migration_775'
)
BEGIN
    CREATE INDEX [IX_GoodsSizes_GoodsId1] ON [GoodsSizes] ([GoodsId1]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602121637_mssql.onprem_migration_775'
)
BEGIN
    ALTER TABLE [GoodsSizes] ADD CONSTRAINT [FK_GoodsSizes_Goodses_GoodsId1] FOREIGN KEY ([GoodsId1]) REFERENCES [Goodses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602121637_mssql.onprem_migration_775'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240602121637_mssql.onprem_migration_775', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    ALTER TABLE [GoodsSizes] DROP CONSTRAINT [FK_GoodsSizes_Goodses_GoodsId1];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    DROP INDEX [IX_GoodsSizes_GoodsId1] ON [GoodsSizes];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GoodsSizes]') AND [c].[name] = N'GoodsId1');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [GoodsSizes] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [GoodsSizes] DROP COLUMN [GoodsId1];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GoodsSizes]') AND [c].[name] = N'GoodsId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [GoodsSizes] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [GoodsSizes] ALTER COLUMN [GoodsId] uniqueidentifier NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    CREATE INDEX [IX_GoodsSizes_GoodsId] ON [GoodsSizes] ([GoodsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    ALTER TABLE [GoodsSizes] ADD CONSTRAINT [FK_GoodsSizes_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602122000_mssql.onprem_migration_292'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240602122000_mssql.onprem_migration_292', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240602164658_mssql.onprem_migration_824'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240602164658_mssql.onprem_migration_824', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614212708_mssql.onprem_migration_125'
)
BEGIN
    CREATE TABLE [FeImport] (
        [Id] uniqueidentifier NOT NULL,
        [FileExchangeId] uniqueidentifier NOT NULL,
        [GoodsId] uniqueidentifier NOT NULL,
        [GoodsSizeId] uniqueidentifier NOT NULL,
        [DateTime] datetime2 NOT NULL,
        CONSTRAINT [PK_FeImport] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FeImport_FileExchanges_FileExchangeId] FOREIGN KEY ([FileExchangeId]) REFERENCES [FileExchanges] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FeImport_GoodsSizes_GoodsSizeId] FOREIGN KEY ([GoodsSizeId]) REFERENCES [GoodsSizes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FeImport_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614212708_mssql.onprem_migration_125'
)
BEGIN
    CREATE INDEX [IX_FeImport_FileExchangeId] ON [FeImport] ([FileExchangeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614212708_mssql.onprem_migration_125'
)
BEGIN
    CREATE INDEX [IX_FeImport_GoodsId] ON [FeImport] ([GoodsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614212708_mssql.onprem_migration_125'
)
BEGIN
    CREATE INDEX [IX_FeImport_GoodsSizeId] ON [FeImport] ([GoodsSizeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614212708_mssql.onprem_migration_125'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614212708_mssql.onprem_migration_125', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614213639_mssql.onprem_migration_923'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614213639_mssql.onprem_migration_923', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614215226_mssql.onprem_migration_563'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_GoodsSizes_GoodsSizeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614215226_mssql.onprem_migration_563'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeImport]') AND [c].[name] = N'GoodsSizeId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [FeImport] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [FeImport] ALTER COLUMN [GoodsSizeId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614215226_mssql.onprem_migration_563'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_GoodsSizes_GoodsSizeId] FOREIGN KEY ([GoodsSizeId]) REFERENCES [GoodsSizes] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614215226_mssql.onprem_migration_563'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614215226_mssql.onprem_migration_563', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614215245_mssql.onprem_migration_982'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614215245_mssql.onprem_migration_982', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222248_mssql.onprem_migration_987'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222248_mssql.onprem_migration_987'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeImport]') AND [c].[name] = N'GoodsId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [FeImport] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [FeImport] ALTER COLUMN [GoodsId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222248_mssql.onprem_migration_987'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222248_mssql.onprem_migration_987'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614222248_mssql.onprem_migration_987', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222506_mssql.onprem_migration_787'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222506_mssql.onprem_migration_787'
)
BEGIN
    DROP INDEX [IX_FeImport_GoodsId] ON [FeImport];
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeImport]') AND [c].[name] = N'GoodsId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [FeImport] DROP CONSTRAINT [' + @var4 + '];');
    EXEC(N'UPDATE [FeImport] SET [GoodsId] = ''00000000-0000-0000-0000-000000000000'' WHERE [GoodsId] IS NULL');
    ALTER TABLE [FeImport] ALTER COLUMN [GoodsId] uniqueidentifier NOT NULL;
    ALTER TABLE [FeImport] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [GoodsId];
    CREATE INDEX [IX_FeImport_GoodsId] ON [FeImport] ([GoodsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222506_mssql.onprem_migration_787'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222506_mssql.onprem_migration_787'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614222506_mssql.onprem_migration_787', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222707_mssql.onprem_migration_821'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222707_mssql.onprem_migration_821'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614222707_mssql.onprem_migration_821'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614222707_mssql.onprem_migration_821', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614223849_mssql.onprem_migration_834'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_FileExchanges_FileExchangeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614223849_mssql.onprem_migration_834'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614223849_mssql.onprem_migration_834'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_FileExchanges_FileExchangeId] FOREIGN KEY ([FileExchangeId]) REFERENCES [FileExchanges] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614223849_mssql.onprem_migration_834'
)
BEGIN
    ALTER TABLE [FeImport] ADD CONSTRAINT [FK_FeImport_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614223849_mssql.onprem_migration_834'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614223849_mssql.onprem_migration_834', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_FileExchanges_FileExchangeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_GoodsSizes_GoodsSizeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [FK_FeImport_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FeImport] DROP CONSTRAINT [PK_FeImport];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    EXEC sp_rename N'[FeImport]', N'FEImports';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    EXEC sp_rename N'[FEImports].[IX_FeImport_GoodsSizeId]', N'IX_FEImports_GoodsSizeId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    EXEC sp_rename N'[FEImports].[IX_FeImport_GoodsId]', N'IX_FEImports_GoodsId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    EXEC sp_rename N'[FEImports].[IX_FeImport_FileExchangeId]', N'IX_FEImports_FileExchangeId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [PK_FEImports] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_FileExchanges_FileExchangeId] FOREIGN KEY ([FileExchangeId]) REFERENCES [FileExchanges] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_GoodsSizes_GoodsSizeId] FOREIGN KEY ([GoodsSizeId]) REFERENCES [GoodsSizes] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224053_mssql.onprem_migration_698'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614224053_mssql.onprem_migration_698', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224222_mssql.onprem_migration_663'
)
BEGIN
    ALTER TABLE [FEImports] DROP CONSTRAINT [FK_FEImports_FileExchanges_FileExchangeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224222_mssql.onprem_migration_663'
)
BEGIN
    ALTER TABLE [FEImports] DROP CONSTRAINT [FK_FEImports_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224222_mssql.onprem_migration_663'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_FileExchanges_FileExchangeId] FOREIGN KEY ([FileExchangeId]) REFERENCES [FileExchanges] ([Id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224222_mssql.onprem_migration_663'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE SET NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240614224222_mssql.onprem_migration_663'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240614224222_mssql.onprem_migration_663', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615192319_mssql.onprem_migration_723'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615192319_mssql.onprem_migration_723', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193247_mssql.onprem_migration_539'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615193247_mssql.onprem_migration_539', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193944_mssql.onprem_migration_196'
)
BEGIN
    ALTER TABLE [FEImports] DROP CONSTRAINT [FK_FEImports_FileExchanges_FileExchangeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193944_mssql.onprem_migration_196'
)
BEGIN
    ALTER TABLE [FEImports] DROP CONSTRAINT [FK_FEImports_Goodses_GoodsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193944_mssql.onprem_migration_196'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_FileExchanges_FileExchangeId] FOREIGN KEY ([FileExchangeId]) REFERENCES [FileExchanges] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193944_mssql.onprem_migration_196'
)
BEGIN
    ALTER TABLE [FEImports] ADD CONSTRAINT [FK_FEImports_Goodses_GoodsId] FOREIGN KEY ([GoodsId]) REFERENCES [Goodses] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615193944_mssql.onprem_migration_196'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615193944_mssql.onprem_migration_196', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615194917_mssql.onprem_migration_244'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615194917_mssql.onprem_migration_244', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615200143_mssql.onprem_migration_203'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615200143_mssql.onprem_migration_203', N'8.0.6');
END;
GO

COMMIT;
GO

