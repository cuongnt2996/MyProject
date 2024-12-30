SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE GenarateInvoiceCode
-- =============================================
-- Author:		<Tấn Cường>
-- Create date: <22/08/2024>
-- Description:	<Description,,>
/*
EXEC dbo.SCM_MM_RepairingRequest_GetKey
*/
-- =============================================
AS
BEGIN
	DECLARE @Prifix VARCHAR(50);
	DECLARE @InvoiceCode VARCHAR(50);

	DECLARE @Now DATETIME = GETDATE();
	SET @Prifix = 'INV' + CONVERT(VARCHAR(8), @Now, 12);
	SELECT @InvoiceCode
		= @Prifix + (CASE
						 WHEN (CAST(MAX(RIGHT(inv.InvoiceCode, 5)) AS BIGINT) + 1) > 1 THEN
							 RIGHT(REPLICATE('0', 5) + CAST(MAX(RIGHT(inv.InvoiceCode, 5)) + 1 AS VARCHAR(5)), 5)
						 ELSE
							 CAST('00001' AS VARCHAR(5))
					 END
					)
	FROM [Invoice] inv	WITH(NOLOCK)
	WHERE inv.CreatedDate > CAST((CAST(DATEPART(YEAR, @Now) AS VARCHAR(20)) + '-'
								   + CAST(DATEPART(MONTH, @Now) AS VARCHAR(20)) + '-'
								   + CAST(DATEPART(DAY, @Now) AS VARCHAR(20))
								  ) AS DATETIME);
	
	SELECT @InvoiceCode AS InvoiceCode

END
GO
