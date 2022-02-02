USE NerdStoreEnterpriseDB
GO
INSERT INTO [dbo].[Vouchers] 
([Id], [Codigo], [Percentual], [ValorDesconto], [Quantidade], [TipoDesconto], [DataCriacao],[DataValidade], [Ativo], [Utilizado]) VALUES 
	(NEWID(), N'150-OFF-GERAL', NULL, 150.00, 50, 1, GETDATE(), DATEADD(MONTH, 4, GETDATE()), 1, 0),
	(NEWID(), N'50-OFF-GERAL', 50.00, NULL, 50, 0, GETDATE(), DATEADD(MONTH, 4, GETDATE()), 1, 0),
	(NEWID(), N'10-OFF-GERAL', 10.00, NULL, 50, 0, GETDATE(), DATEADD(MONTH, 4, GETDATE()), 1, 0)