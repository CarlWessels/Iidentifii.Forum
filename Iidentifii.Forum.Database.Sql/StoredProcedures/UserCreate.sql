CREATE PROC UserCreate
(
	@Name VARCHAR(100)
	,@Email NVARCHAR(255)
	,@Password VARCHAR(255)
)
AS 
BEGIN

	DECLARE @HashedPassword VARBINARY(64);

	DECLARE @Salt UNIQUEIDENTIFIER 
	SELECT @Salt = NEWID()

	SET @HashedPassword = HASHBYTES('SHA2_512', @Password + CONVERT(VARCHAR(100),@Salt));

	INSERT INTO [User] (Name, Email, PasswordHash, Salt, [Role])
	VALUES (@Name, @Email, @HashedPassword, @Salt, 'User')
	SELECT SCOPE_IDENTITY();
END