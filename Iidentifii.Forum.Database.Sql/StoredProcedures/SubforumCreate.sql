CREATE PROC SubforumCreate
(
	@Name VARCHAR(100),
	@Description NVARCHAR(255),
	@Id INT NULL OUT
)
AS 
BEGIN
    IF EXISTS (SELECT 1 FROM [Subforum] WHERE Name = @Name)
    BEGIN
        RAISERROR('A subforum with this name already exists.', 16, 1);
        RETURN;
    END

    INSERT INTO [Subforum] (Name, Description)
    VALUES (@Name, @Description)
    
    SELECT @Id = SCOPE_IDENTITY();
END
