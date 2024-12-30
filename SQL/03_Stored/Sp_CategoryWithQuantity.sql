IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Sp_CategoryWithQuantity'
)
DROP PROCEDURE dbo.Sp_CategoryWithQuantity
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Sp_CategoryWithQuantity
    @KeyWord NVARCHAR(250)
AS
    SELECT      C.*,COUNT(distinct P.ProductId) AS ProductQuantity
    FROM        Category C WITH(NOLOCK) 
    INNER JOIN  Product P WITH(NOLOCK) 
        ON      P.CategoryId = C.CategoryId 
    WHERE       P.ProductName LIKE N'%' + @keyword + '%' and P.IsDeleted = 0 AND P.IsActived = 1
    GROUP BY    C.CategoryID, c.CategoryName, c.CreatedDate, C.CreatedUserId, C.DeletedDate, C.DeletedUserId, C.ImageUrl, C.IsActived, C.IsDeleted
GO
