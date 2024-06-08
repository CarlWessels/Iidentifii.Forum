﻿CREATE TABLE [dbo].[Subforum]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NOT NULL

	CONSTRAINT [PK_Subforum] PRIMARY KEY (Id)
)