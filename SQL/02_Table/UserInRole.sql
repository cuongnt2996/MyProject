Use WebStore
GO
CREATE TABLE UserInRole(
    UserId VARCHAR(32) NOT NULL,
    RoleId INT NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    PRIMARY KEY (UserId, RoleId)
);