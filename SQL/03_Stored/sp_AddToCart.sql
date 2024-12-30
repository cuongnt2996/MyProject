Use WebStore
GO
CREATE OR ALTER PROCEDURE AddToCart
    @UserId INT = 0,
    @ProductId INT = NULL,
    @Quantity INT = NULL,
    @ClientId VARCHAR(50) = NULL
AS

    IF EXISTS(SELECT TOP 1 1 FROM Cart WHERE ProductId = @ProductId AND (UserId = @UserId OR ClientId = @ClientId))
    BEGIN
        UPDATE  Cart SET Quantity = Quantity + @Quantity
        WHERE   ProductId = @ProductId AND (UserId = @UserId OR ClientId = @ClientId)
    END
    ELSE
    BEGIN
        INSERT INTO Cart(ProductId, Quantity, UserId, ClientId) VALUES (@ProductId, @Quantity, @UserId, @ClientId)
    END
GO
