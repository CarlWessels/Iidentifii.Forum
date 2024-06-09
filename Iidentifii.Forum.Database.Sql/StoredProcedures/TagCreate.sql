CREATE PROCEDURE [dbo].[TagCreate]
(
	@PostId INT,
    @TagId INT,
	@UserId INT
)
AS 
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Post WHERE Id = @PostId)
    BEGIN
        RAISERROR('Unable to find post', 16, 1);
        RETURN;
    END


    IF EXISTS (SELECT 1 FROM Tag WHERE Id = @PostId AND TagId = @TagId)
    BEGIN
        RAISERROR('Post already tagged', 16, 1);
        RETURN;
    END


    INSERT INTO [Tag] (PostId, TagId, UserId, CreationDate)
    VALUES (@PostId, @TagId, @UserId, GETDATE())
END
