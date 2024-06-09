CREATE PROC ModeratorCreate
(
	@userId INT
)
AS
BEGIN

	UPDATE [User]
	SET Role = 'Moderator'
	WHERE Id = @userId

END