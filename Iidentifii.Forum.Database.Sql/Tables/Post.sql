CREATE TABLE [dbo].[Post]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[SubformumId] INT NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
	[Content] NVARCHAR(4000) -- MAX


	CONSTRAINT [PK_Post] PRIMARY KEY (Id)
	, CONSTRAINT FK_Post_Subforum FOREIGN KEY ([SubformumId]) REFERENCES Subforum (Id)
)
