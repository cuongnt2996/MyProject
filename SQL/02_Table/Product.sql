Use WebStore
go
-- Kiểm tra xem bảng Product có tồn tại không, nếu có thì xóa
IF OBJECT_ID('dbo.Product', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Product;
END

-- Tạo bảng Product
CREATE TABLE dbo.Product
(
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    CategoryId INT NOT NULL,
    ProductName NVARCHAR(250) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    Price DECIMAL(28,2) NULL,
    IsActived BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    ImageUrl NVARCHAR(255),
    CreatedUserId INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DeletedUserId INT NULL,
    DeletedDate DATETIME
    );
GO
-- Chèn các dòng dữ liệu mẫu vào bảng Product
INSERT INTO dbo.Product (CategoryId, ProductName, Description, Unit, IsActived, IsDeleted, ImageUrl, CreatedUserId, CreatedDate, DeletedUserId, DeletedDate)
VALUES 
(1, N'Intel Core i9', N'CPU Intel thế hệ 10', N'Cái', 1, 0, 'https://example.com/images/intel_core_i9.jpg', 1, GETDATE(), NULL, NULL),
(2, N'Corsair Vengeance 16GB', N'RAM DDR4 16GB', N'Cái', 1, 0, 'https://example.com/images/corsair_vengeance.jpg', 1, GETDATE(), NULL, NULL),
(3, N'ASUS ROG Strix Z490-E', N'Mainboard ASUS', N'Cái', 1, 0, 'https://example.com/images/asus_rog_strix.jpg', 1, GETDATE(), NULL, NULL),
(4, N'Samsung 970 EVO 1TB', N'SSD Samsung 1TB', N'Cái', 1, 0, 'https://example.com/images/samsung_970_evo.jpg', 1, GETDATE(), NULL, NULL),
(5, N'Seagate Barracuda 2TB', N'HDD Seagate 2TB', N'Cái', 1, 0, 'https://example.com/images/seagate_barracuda.jpg', 1, GETDATE(), NULL, NULL),
(6, N'NVIDIA GeForce RTX 3080', N'Card đồ họa NVIDIA', N'Cái', 1, 0, 'https://example.com/images/nvidia_rtx_3080.jpg', 1, GETDATE(), NULL, NULL),
(7, N'Corsair RM850x', N'Nguồn máy tính Corsair 850W', N'Cái', 1, 0, 'https://example.com/images/corsair_rm850x.jpg', 1, GETDATE(), NULL, NULL),
(8, N'NZXT H510', N'Case máy tính NZXT', N'Cái', 1, 0, 'https://example.com/images/nzxt_h510.jpg', 1, GETDATE(), NULL, NULL),
(9, N'Cooler Master Hyper 212', N'Tản nhiệt Cooler Master', N'Cái', 1, 0, 'https://example.com/images/cooler_master_hyper_212.jpg', 1, GETDATE(), NULL, NULL),
(10, N'APC Back-UPS 600VA', N'Bộ lưu điện APC', N'Cái', 1, 0, 'https://example.com/images/apc_back_ups.jpg', 1, GETDATE(), NULL, NULL);
