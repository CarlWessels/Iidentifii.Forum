﻿CREATE PROCEDURE dbo.GetPost
(
    @PostId INT,
    @PageNumber INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow INT = @PageNumber * @PageSize;

    SELECT 
        p.Id AS PostId,
        sf.Name AS SubforumName,
        p.Title,
        p.Content,
        pu.Email AS PostUser,
        p.CreationDate AS PostCreationDate,
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
        ) AS Comments
    FROM dbo.Post p
    INNER JOIN dbo.Subforum sf ON p.SubforumId = sf.Id
    INNER JOIN dbo.[User] pu ON p.UserId = pu.Id
    WHERE p.Id = @PostId
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
END;
