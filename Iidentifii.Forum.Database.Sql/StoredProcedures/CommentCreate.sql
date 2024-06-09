CREATE PROC CommentCreate
(
	@PostId INT,
	@Comment NVARCHAR(4000),
	@UserId INT,
	@Id INT NULL OUT
)
AS 
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [Post] WHERE Id = @PostId)
    BEGIN
        RAISERROR('Unable to find post', 16, 1);
        RETURN;
    END


    INSERT INTO [Comment] (PostId, Comment,UserId, CreationDate)
    VALUES (@PostId, @Comment,@UserId, GETDATE())
    
    SELECT @Id = SCOPE_IDENTITY();
END
