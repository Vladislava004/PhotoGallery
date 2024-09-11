GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO
USE [PhotoGallery];


GO
PRINT N'Creating Table [dbo].[Albums]...';


GO
CREATE TABLE [dbo].[Albums] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    INT            NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [CreatedOn] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Photos]...';


GO
CREATE TABLE [dbo].[Photos] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [AlbumId]    INT            NOT NULL,
    [PhotoUrl]   NVARCHAR (512) NOT NULL,
    [UploadedOn] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (512) NOT NULL,
    [Email]    NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Albums_Users]...';


GO
ALTER TABLE [dbo].[Albums] WITH NOCHECK
    ADD CONSTRAINT [FK_Albums_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Photos_Users]...';


GO
ALTER TABLE [dbo].[Photos] WITH NOCHECK
    ADD CONSTRAINT [FK_Photos_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Photos_Albums]...';


GO
ALTER TABLE [dbo].[Photos] WITH NOCHECK
    ADD CONSTRAINT [FK_Photos_Albums] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[Albums] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [PhotoGallery];


GO
ALTER TABLE [dbo].[Albums] WITH CHECK CHECK CONSTRAINT [FK_Albums_Users];

ALTER TABLE [dbo].[Photos] WITH CHECK CHECK CONSTRAINT [FK_Photos_Users];

ALTER TABLE [dbo].[Photos] WITH CHECK CHECK CONSTRAINT [FK_Photos_Albums];


GO
PRINT N'Update complete.';


GO
