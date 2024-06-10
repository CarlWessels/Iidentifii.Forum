CREATE PROC SubforumGet
(
    @PageNumber INT,
    @PageSize INT,
	@Output NVARCHAR(MAX) OUT
)
AS 
BEGIN

    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow INT = @PageNumber * @PageSize;

	SELECT @Output = (
		SELECT Id, Name, Description
		FROM Subforum sf
		ORDER BY sf.Name
		OFFSET @StartRow - 1 ROWS
		FETCH NEXT @PageSize ROWS ONLY
		FOR JSON PATH
	)
END