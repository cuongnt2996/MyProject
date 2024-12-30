Use WebStore
GO
IF OBJECT_ID('dbo.Access', 'U') IS NOT NULL
DROP TABLE dbo.Access
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Access
(
    AccessId INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
    AccessName [NVARCHAR](64) NOT NULL,
    AccessUrl [VARCHAR](64)  NULL,
    ParentId INT REFERENCES   dbo.Access(AccessId)
    -- specify more columns here
);
GO

IF OBJECT_ID('dbo.AccessRole', 'U') IS NOT NULL
DROP TABLE dbo.AccessRole
GO
-- Create the table in the specified schema
CREATE TABLE dbo.AccessRole
(
    AccessId INT NOT NULL , -- primary key column
    RoleId INT NOT null,
    PRIMARY key (AccessId, RoleId)
);