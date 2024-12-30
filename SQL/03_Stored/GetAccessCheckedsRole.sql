use WebStore;
go
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetAccessCheckedsRole'
)
DROP PROCEDURE dbo.GetAccessCheckedsRole
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.GetAccessCheckedsRole
    @RoleId INT
AS
    SELECT      A.*, IIF(A.AccessId IS NULL,0,1) AS Checked 
    FROM        Access A WITH(NOLOCK)
    LEFT JOIN   AccessRole R WITH(NOLOCK)
        ON      A.AccessId = R.AccessId
        AND     RoleId = @RoleId
GO
-- -- example to execute the stored procedure we just created
-- EXECUTE dbo.GetAccessCheckedsRole 1 /*value_for_param1*/, 2 /*value_for_param2*/
-- GO