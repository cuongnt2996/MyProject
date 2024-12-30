CREATE OR ALTER PROCEDURE InsertInvoiceAndItems
    @InvoiceId VARCHAR(50),
    @InvoiceCode VARCHAR(50),
    @CreatedUserId INT,
    @CreatedDate DATETIME,
    @CustomerName NVARCHAR(500),
    @Email VARCHAR(250),
    @Address NVARCHAR(500),
    @Phone VARCHAR(50),
    @Amount DECIMAL(28, 8),
    @InvoiceItems dbo.InvoiceItemType READONLY,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @Result = 0; -- Mặc định là 0 (thất bại)

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert into Invoice table
        INSERT INTO Invoice (InvoiceId, InvoiceCode, CreatedUserId, CreatedDate, CustomerName, Email, [Address], Phone, Amount)
        VALUES (@InvoiceId, @InvoiceCode, @CreatedUserId, @CreatedDate, @CustomerName, @Email, @Address, @Phone, @Amount);

        -- Insert into InvoiceItem table
        INSERT INTO InvoiceItem (InvoiceId, CartId, ProductId, ProductName, ImageUrl, Price, Quantity)
        SELECT InvoiceId, CartId, ProductId, ProductName, ImageUrl, Price, Quantity
        FROM @InvoiceItems;

        COMMIT TRANSACTION;

        SET @Result = 1; -- Thành công
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Result = 0; -- Thất bại
    END CATCH
END
GO


select * from invoiceitem