USE WebStore
GO
CREATE OR ALTER PROCEDURE dbo.GetAccessesByUser
    @UserId CHAR(32) 
AS
    SELECT      A.*, IIF(tbl.AccessId IS NULL, 0, 1) AS Checked
    FROM        Access A WITH(NOLOCK)
    LEFT JOIN   (SELECT AccessId FROM UserInRole INNER JOIN AccessRole ON UserInRole.RoleId = AccessRole.RoleId AND UserId = @UserId) tbl
        ON      A.AccessId = tbl.AccessId
GO
