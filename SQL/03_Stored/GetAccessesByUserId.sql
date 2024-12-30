USE WebStore
GO
CREATE OR ALTER PROCEDURE dbo.GetAccessesByUserId
    @Id CHAR(32) 
AS
    SELECT      A.*
    FROM        Access A WITH(NOLOCK)
    INNER JOIN  AccessRole R WITH(NOLOCK)
        ON      A.AccessId = r.AccessId
    INNER JOIN  UserInRole M WITH(NOLOCK)
        ON      R.RoleId = M.RoleId
    AND         M.UserId=@Id   ;
GO
