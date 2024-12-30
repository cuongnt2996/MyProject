Use WebStore
go
IF OBJECT_ID('dbo.User', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.[User]
END
Go
CREATE TABLE dbo.[User]
(
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
    PasswordSalt NVARCHAR(256) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
	RoleId INT NULL,
    FirstName NVARCHAR(250),
    LastName NVARCHAR(250),
    DateOfBirth DATE,
    CreatedDate DATETIME DEFAULT(GETDATE())
);
