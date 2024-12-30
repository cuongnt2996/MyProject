Use WebStore
IF OBJECT_ID('dbo.Category', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Category;
END

CREATE TABLE dbo.Category
(
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(250) NOT NULL,
    IsActived BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedUserId INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DeletedUserId INT NULL,
    DeletedDate DATETIME NULL
);
Go
TRUNCATE TABLE dbo.Category
-- Chèn các dòng dữ liệu mẫu bổ sung
INSERT INTO dbo.Category (CategoryName, IsActived, IsDeleted, ImageUrl, CreatedUserId, CreatedDate, DeletedUserId, DeletedDate)
VALUES 
(N'CPU', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'RAM', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Mainboard', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Ổ cứng SSD', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Ổ cứng HDD', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Card đồ họa', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Nguồn máy tính', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Case máy tính', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Tản nhiệt', 1, 0, '', NULL, GETDATE(), NULL, NULL),
(N'Bộ lưu điện UPS', 1, 0, '', NULL, GETDATE(), NULL, NULL);
