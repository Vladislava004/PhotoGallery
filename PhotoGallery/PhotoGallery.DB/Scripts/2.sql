GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

GO
USE [PhotoGallery];

GO
/*
The column [dbo].[Users].[UserName] is being dropped, data loss could occur.

The column [dbo].[Users].[Name] on table [dbo].[Users] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Users])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping Foreign Key [dbo].[FK_Albums_Users]...';


GO
ALTER TABLE [dbo].[Albums] DROP CONSTRAINT [FK_Albums_Users];


GO
PRINT N'Dropping Foreign Key [dbo].[FK_Photos_Users]...';


GO
ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [FK_Photos_Users];


GO
PRINT N'Starting rebuilding table [dbo].[Users]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Password] NVARCHAR (512) NOT NULL,
    [Email]    NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Users])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Users] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Users] ([Id], [Password], [Email])
        SELECT   [Id],
                 [Password],
                 [Email]
        FROM     [dbo].[Users]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Users] OFF;
    END

DROP TABLE [dbo].[Users];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Users]', N'Users';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


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
PRINT N'Checking existing data against newly created constraints';


GO
USE [PhotoGallery];


GO
ALTER TABLE [dbo].[Albums] WITH CHECK CHECK CONSTRAINT [FK_Albums_Users];

ALTER TABLE [dbo].[Photos] WITH CHECK CHECK CONSTRAINT [FK_Photos_Users];


GO
PRINT N'Update complete.';


GO
