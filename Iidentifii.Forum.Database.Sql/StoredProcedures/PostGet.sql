CREATE PROCEDURE dbo.PostGet
(
    @PostId INT,
    @PageNumber INT,
    @PageSize INT,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @AuthorId INT = NULL,
    @TagId INT = NULL,
    @SortBy NVARCHAR(50) = 'PostCreationDate',
    @SortOrder NVARCHAR(4) = 'Ascending',
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

    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow INT = @PageNumber * @PageSize;

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
				ORDER BY c.CreationDate
				OFFSET @StartRow - 1 ROWS
				FETCH NEXT @PageSize ROWS ONLY
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
			AND (@StartDate IS NULL OR p.CreationDate >= @StartDate)
			AND (@EndDate IS NULL OR p.CreationDate <= @EndDate)
			AND (@AuthorId IS NULL OR p.UserId = @AuthorId)
			AND (@TagId IS NULL OR EXISTS (SELECT 1 FROM Tag WHERE PostId = p.Id AND TagId = @TagId))
		ORDER BY 
			CASE WHEN @SortOrder = 'Descending' AND @SortBy = 'PostCreationDate' THEN p.CreationDate END DESC,
			CASE WHEN @SortOrder = 'Descending' AND @SortBy = 'LikeCount' THEN (SELECT COUNT(1) FROM dbo.Comment c WHERE c.PostId = p.Id) END DESC,
			CASE WHEN @SortOrder = 'Ascending' AND @SortBy = 'PostCreationDate' THEN p.CreationDate END ASC,
			CASE WHEN @SortOrder = 'Ascending' AND @SortBy = 'LikeCount' THEN (SELECT COUNT(1) FROM dbo.Comment c WHERE c.PostId = p.Id) END ASC
		OFFSET @StartRow - 1 ROWS
		FETCH NEXT @PageSize ROWS ONLY
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	);
END;
