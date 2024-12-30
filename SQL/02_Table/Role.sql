USE WebStore
GO
IF OBJECT_ID('dbo.Role', 'U') IS NOT NULL
DROP TABLE dbo.Role
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Role
(
    RoleId INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    RoleCode [NVARCHAR](50) NOT NULL,
    RoleName [NVARCHAR](50) NOT NULL,
    IsActived BIT DEFAULT(1),
    IsDeleted BIT DEFAULT(0),
    CreatedUserId INT NULL,
    CreatedDate DATETIME DEFAULT(GETDATE()),
    DeletedUserId INT NULL,
    DeletedDate DATETIME NULL,
    -- specify more columns here
);
GO

