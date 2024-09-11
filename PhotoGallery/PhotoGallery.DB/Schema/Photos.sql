CREATE TABLE [dbo].[Photos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UserId] INT NOT NULL,
	[AlbumId] INT NOT NULL,
	[PhotoUrl] NVARCHAR(512) NOT NULL,
	[UploadedOn] DATETIME NOT NULL,
	CONSTRAINT [FK_Photos_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
	CONSTRAINT [FK_Photos_Albums] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[Albums] ([Id])
)
