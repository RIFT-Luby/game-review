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

CREATE TABLE [GameGenders] (
    [Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_GameGenders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Games] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Summary] nvarchar(200) NOT NULL,
    [Developer] nvarchar(100) NOT NULL,
    [GameGenderId] int NOT NULL,
    [Score] decimal(18,2) NOT NULL,
    [Console] nvarchar(100) NOT NULL,
    [ImgPath] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Games_GameGenders_GameGenderId] FOREIGN KEY ([GameGenderId]) REFERENCES [GameGenders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [UserName] nvarchar(450) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [UserRoleId] int NOT NULL,
    [ImgPath] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_UserRoles_UserRoleId] FOREIGN KEY ([UserRoleId]) REFERENCES [UserRoles] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [GameId] int NOT NULL,
    [UserId] int NOT NULL,
    [UserReview] nvarchar(255) NOT NULL,
    [Score] int NOT NULL,
    [UserId1] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Games_GameId] FOREIGN KEY ([GameId]) REFERENCES [Games] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [Users] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[GameGenders]'))
    SET IDENTITY_INSERT [GameGenders] ON;
INSERT INTO [GameGenders] ([Id], [Name])
VALUES (1, N'Action'),
(2, N'Race'),
(3, N'FPS'),
(4, N'RPG'),
(5, N'Strategy');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[GameGenders]'))
    SET IDENTITY_INSERT [GameGenders] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] ON;
INSERT INTO [UserRoles] ([Id], [Name])
VALUES (1, N'Admin'),
(2, N'Common');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[UserRoles]'))
    SET IDENTITY_INSERT [UserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Email', N'ImgPath', N'Name', N'Password', N'UpdatedAt', N'UserName', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [CreatedAt], [Email], [ImgPath], [Name], [Password], [UpdatedAt], [UserName], [UserRoleId])
VALUES (1, '2012-12-12T00:00:00.0000000', N'admin@api.com', NULL, N'Admin Root Application', N'AQAAAAEAAAPoAAAAEPVzQgwXqBvMOyniUSp5g3qM111Cv3/Wujcl45Hi0nQy5tPOc+cqDH5D5suSO9Ub7w==', NULL, N'admin', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Email', N'ImgPath', N'Name', N'Password', N'UpdatedAt', N'UserName', N'UserRoleId') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_Games_GameGenderId] ON [Games] ([GameGenderId]);
GO

CREATE INDEX [IX_Reviews_GameId] ON [Reviews] ([GameId]);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

CREATE INDEX [IX_Reviews_UserId1] ON [Reviews] ([UserId1]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]);
GO

CREATE INDEX [IX_Users_UserRoleId] ON [Users] ([UserRoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220620193003_Initial-Create', N'6.0.6');
GO

COMMIT;
GO

