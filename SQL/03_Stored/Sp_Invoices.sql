USE [WebStore]
GO
/****** Object:  StoredProcedure [dbo].[Invoices]    Script Date: 09/12/2024 2:37:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   PROCEDURE [dbo].[Invoices]
	@Type VARCHAR(50) = 'GetInvoice',
    @InvoiceId VARCHAR(50)
AS
BEGIN
	IF @Type = 'GetInvoice'
	BEGIN
		SELECT * FROM Invoice WHERE InvoiceId = @InvoiceId
		SELECT * FROM InvoiceItem WHERE InvoiceId = @InvoiceId
	END
END
