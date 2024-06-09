CREATE PROC LikeOrUnlike 
(
	@postId INT
	,@UserId INT
)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM [Post] WHERE Id = @PostId)
    BEGIN
        RAISERROR('Unable to find post', 16, 1);
        RETURN;
    END

	IF ((SELECT UserId FROM dbo.Post p WHERE p.Id = @postId) = @UserId)
	BEGIN
        RAISERROR('User cannot like own post', 16, 1);
        RETURN;
	END

	DECLARE @LikeId INT 
	select @LikeId = ID 
	from [Like] l
	WHERE	l.PostId = @postId
			AND l.UserId = @UserId

	IF (@LikeId IS NOT NULL)
	BEGIN
		DELETE FROM [Like] 
		WHERE	PostId = @postId
				AND UserId = @UserId
	END
	ELSE
	BEGIN
		INSERT INTO [Like] (PostId, UserId)
		VALUES (@PostId, @UserId)
	END
END


