USE WebStore
GO
CREATE OR ALTER PROCEDURE dbo.sp_Product
    @Type VARCHAR(20) = '',
    @CategoryId int =0,
    @Page int = 1,
    @Size int = 0,
    @KeyWord NVARCHAR(500) = null
AS
    SELECT      *
    INTO        #TempData 
    FROM        Product WITH (NOLOCK) 
    WHERE       IsDeleted = 0
    AND         (@CategoryId = 0 OR CategoryId = @CategoryId)
    AND         (ISNULL(@KeyWord,'')='' OR ProductName LIKE N'%' + @KeyWord +'%')
    

    IF(@Type='LoadList')
    BEGIN
        SELECT * FROM #TempData ORDER BY  ProductId DESC OFFSET (@Page -1) * @Size ROWS FETCH NEXT @Size ROWS ONLY
    END
    IF(@Type = 'CountProduct')
    BEGIN
        SELECT COUNT(*) FROM #TempData
    END
    DROP TABLE #TempData
GO
