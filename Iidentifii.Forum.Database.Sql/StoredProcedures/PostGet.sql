CREATE PROCEDURE dbo.PostGet
(
    @PostId INT,
	@Output NVARCHAR(MAX) OUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [Post] WHERE Id = @PostId)
    BEGIN
        RAISERROR('Unable to find post', 16, 1);
        RETURN;
    END

    SELECT @Output = (

		SELECT 
			p.Id AS PostId,
			sf.Name AS SubforumName,
			p.Title,
			p.Content,
			pu.Email AS PostUser,
			p.CreationDate AS PostCreationDate,
			LikeCount = (SELECT COUNT(1) FROM dbo.[Like] c WHERE c.PostId = p.Id),
			(
				SELECT 
					c.Id AS CommentId,
					c.Comment,
					cu.Name AS CommentUser,
					c.CreationDate AS CommentCreationDate
				FROM dbo.Comment c
				INNER JOIN dbo.[User] cu ON c.UserId = cu.Id
				WHERE c.PostId = p.Id
				FOR JSON PATH
			) AS Comments,
			(
				SELECT tlu.Name, tlu.Id, tlu.Description
				FROM dbo.Tag t
				INNER JOIN dbo.TagLU tlu ON t.TagId = tlu.Id
				WHERE t.PostId = p.Id
				FOR JSON PATH
			) AS Tags
		FROM dbo.Post p
		INNER JOIN dbo.Subforum sf ON p.SubforumId = sf.Id
		INNER JOIN dbo.[User] pu ON p.UserId = pu.Id
		WHERE 
			p.Id = @PostId
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	);
END;
