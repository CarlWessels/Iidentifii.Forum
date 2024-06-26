﻿CREATE TABLE [dbo].[Post]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[SubforumId] INT NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
	[Content] NVARCHAR(4000), -- MAX
	[UserId] INT NOT NULL,
	[CreationDate] DATETIME NOT NULL CONSTRAINT DF_Post_CreationDate DEFAULT GETDATE()

	CONSTRAINT [PK_Post] PRIMARY KEY (Id)
	, CONSTRAINT FK_Post_Subforum FOREIGN KEY ([SubforumId]) REFERENCES Subforum (Id)
	, CONSTRAINT FK_Post_UserId FOREIGN KEY ([UserId]) REFERENCES [User] (Id)
)
