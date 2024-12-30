Use WebStore
GO
IF OBJECT_ID('Cart', 'U') IS NOT NULL
DROP TABLE Cart
GO
-- Create the table in the specified schema
CREATE TABLE Cart
(
	CartId INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- primary key column
	-- CartCode CHAR(32),
	ProductId INT NOT NULL REFERENCES Product(ProductId),
	Quantity SMALLINT not NULL,
	UserId INT NULL,
	ClientId VARCHAR(50)
);
GO