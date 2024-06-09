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
	IF NOT EXISTS (SELECT 1 FROM Subforum WHERE Id = @SubforumId)
    BEGIN
        RAISERROR('Unable to find subforum', 16, 1);
        RETURN;
    END

    INSERT INTO [Post] (SubforumId, Title, Content, UserId)
    VALUES (@SubforumId, @Title, @Content, @UserId)
    
    SELECT @Id = SCOPE_IDENTITY();
END
