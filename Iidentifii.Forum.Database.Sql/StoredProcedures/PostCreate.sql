CREATE PROC PostCreate
(
	@SubforumId INT,
	@Title NVARCHAR(255),
	@Content NVARCHAR(4000),
	@UserId INT,
	@Id INT NULL OUT
)
AS 
BEGIN
    INSERT INTO [Post] (SubforumId, Title, Content, UserId)
    VALUES (@SubforumId, @Title, @Content, @UserId)
    
    SELECT @Id = SCOPE_IDENTITY();
END
