﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(512) NOT NULL,
	[Email] NVARCHAR(200) NOT NULL
)
