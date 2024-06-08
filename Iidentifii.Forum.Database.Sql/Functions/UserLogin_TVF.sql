CREATE FUNCTION UserLogin_TVF
(
    @Email VARCHAR(50),
    @Password VARCHAR(100)
)
RETURNS TABLE
AS
    RETURN
    (
        SELECT TOP 1 Id, Name, Email, Role
        FROM [User] u
        WHERE UPPER(u.Email) = UPPER(@Email)
        AND HASHBYTES('SHA2_512', @Password + CONVERT(VARCHAR(100), u.Salt)) = u.PasswordHash
    );

