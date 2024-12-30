Use WebStore
GO
CREATE OR ALTER PROCEDURE GetCarts
    @UserId INT = NULL,
    @ClientId VARCHAR(50) = NULL
AS
	SELECT		C.*, P.ProductName, P.Price, P.ImageUrl, Cat.CategoryName
	FROM		Cart C WITH(NOLOCK)
	INNER JOIN	Product P WITH(NOLOCK)
		ON		c.ProductId = p.ProductId
	INNER JOIN	Category Cat WITH(NOLOCK)
		ON		Cat.CategoryID = P.CategoryId
	WHERE		UserId = @UserId
	UNION
	SELECT		C.*, P.ProductName, P.Price, P.ImageUrl, Cat.CategoryName
	FROM		Cart C WITH(NOLOCK)
	INNER JOIN	Product P WITH(NOLOCK)
		ON		c.ProductId = p.ProductId
	INNER JOIN	Category Cat WITH(NOLOCK)
		ON		Cat.CategoryID = P.CategoryId
	WHERE 	ClientId = @ClientId AND UserId =0

GO
