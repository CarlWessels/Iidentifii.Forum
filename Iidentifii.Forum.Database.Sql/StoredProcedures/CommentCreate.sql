CREATE PROC CommentCreate
(
	@PostId INT,
	@Comment NVARCHAR(4000),
	@UserId INT,
	@Id INT NULL OUT
)
AS 
BEGIN
    INSERT INTO [Comment] (PostId, Comment,UserId, CreationDate)
    VALUES (@PostId, @Comment,@UserId, GETDATE())
    
    SELECT @Id = SCOPE_IDENTITY();
END
