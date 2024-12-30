USE [WebStore]
Go
CREATE TABLE [dbo].[VnPayment](
	[VnPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [bigint] NULL,
	[BankCode] [varchar](256) NULL,
	[BankTranNo] [varchar](256) NULL,
	[CardType] [varchar](256) NULL,
	[OrderInfo] [nvarchar](256) NULL,
	[PayDate] [bigint] NULL,
	[ResponseCode] [varchar](256) NULL,
	[TmnCode] [varchar](256) NULL,
	[TransactionNo] [varchar](256) NULL,
	[TxnRef] [varchar](256) NULL,
	[TransactionStatus] [varchar](256) NULL,
	[SecureHash] [varchar](256) NULL
)
