CREATE PROC LikeOrUnlike 
(
	@postId INT
	,@UserId INT
)
AS
BEGIN
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


