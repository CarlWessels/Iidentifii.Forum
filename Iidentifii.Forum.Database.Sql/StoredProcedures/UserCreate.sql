CREATE PROC UserCreate
(
	@Name VARCHAR(100),
	@Email NVARCHAR(255),
	@Password VARCHAR(255),
	@Id INT NULL OUT
)
AS 
BEGIN
    -- Check for existing user with the same name
    IF EXISTS (SELECT 1 FROM [User] WHERE Name = @Name)
    BEGIN
        RAISERROR('A user with this name already exists.', 16, 1);
        RETURN;
    END

    -- Check for existing user with the same email
    IF EXISTS (SELECT 1 FROM [User] WHERE Email = @Email)
    BEGIN
        RAISERROR('A user with this email already exists.', 16, 1);
        RETURN;
    END

    DECLARE @HashedPassword VARBINARY(64);
    DECLARE @Salt UNIQUEIDENTIFIER;
    
    SELECT @Salt = NEWID();
    
    SET @HashedPassword = HASHBYTES('SHA2_512', @Password + CONVERT(VARCHAR(100), @Salt));
    

	DECLARE @Role VARCHAR(10)
	IF (SELECT COUNT(1) FROM [User]) < 1
	BEGIN
		SET @Role = 'Owner'
	END
	ELSE
	BEGIN
		SET @Role = 'User'
	END

    INSERT INTO [User] (Name, Email, PasswordHash, Salt, [Role])
    VALUES (@Name, @Email, @HashedPassword, @Salt, @Role);
    
    SELECT @Id = SCOPE_IDENTITY();
END
