Use WebStore
GO
IF OBJECT_ID('Invoice', 'U') IS NOT NULL
DROP TABLE Invoice
GO
-- Create the table in the specified schema
CREATE TABLE Invoice
(
	InvoiceId VARCHAR(50) primary key,
	InvoiceCode VARCHAR(50),
	CreatedUserId int,
	CreatedDate DATETIME DEFAULT(GETDATE()),
	CustomerName NVARCHAR(500),
	Email VARCHAR(250) ,
	[Address]  NVARCHAR(500),
	Phone VARCHAR(50),
	Amount DECIMAL(28,8),
	IsPaid BIT DEFAULT(0)
);
GO
IF OBJECT_ID('InvoiceItem', 'U') IS NOT NULL
DROP TABLE InvoiceItem
GO
-- Create the table in the specified schema
CREATE TABLE InvoiceItem
(
	InvoiceItemId int primary key identity(1,1),
	InvoiceId VARCHAR(50),
	CartId int,
	ProductId int,
	ProductName NVARCHAR(250),
	ImageUrl VARCHAR(250),
	Price DECIMAL(28,8),
	Quantity INT
);
GO

CREATE TYPE dbo.InvoiceItemType AS TABLE
(
    InvoiceId VARCHAR(50),
    CartId INT,
    ProductId INT,
    ProductName NVARCHAR(250),
    ImageUrl VARCHAR(250),
    Price DECIMAL(28, 8),
    Quantity INT
)
GO
