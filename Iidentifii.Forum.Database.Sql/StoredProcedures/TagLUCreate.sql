CREATE PROCEDURE [dbo].[TagLUCreate]	
(
	@Name VARCHAR(100),
	@Description VARCHAR(1000),
	@Id INT NULL OUT
)
AS 
BEGIN
	IF EXISTS (SELECT 1 FROM TagLU WHERE Name = @Name)
    BEGIN
        RAISERROR('Tag already exists', 16, 1);
        RETURN;
    END

    INSERT INTO [TagLU] (Name, Description)
    VALUES (@Name, @Description)
    
    SELECT @Id = SCOPE_IDENTITY();
END
