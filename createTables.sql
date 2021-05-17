IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Monitoramento')
  BEGIN
    CREATE DATABASE Monitoramento

END

GO
    USE Monitoramento
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('Monitoramento.Paises') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Paises(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NOT NULL,
	[IsoDuasLetras] [varchar](2) NULL,
	[IsoTresLetras] [varchar](3) NULL,
	[NumeroCodigoIso] [varchar](250) NULL,	
	CONSTRAINT [PK_Paises] PRIMARY KEY CLUSTERED (	[Id] ASC)
)

	PRINT('Tabela nova Paises criada em - Database: Monitoramento.');

END
ELSE
	PRINT('Tabela Paises já existe no - Database: Monitoramento.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('Monitoramento.Regioes') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Regioes(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NOT NULL,	
	CONSTRAINT [PK_Regioes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
	PRINT('Tabela nova Regioes criada em - Database: Monitoramento.');

END
ELSE
	PRINT('Tabela Regioes já existe no - Database: Monitoramento.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('Monitoramento.Sensores') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Sensores(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NOT NULL,	
	[DataCadastro] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NOT NULL,
	[RegiaoId] [int] NOT NULL,
	[PaisId] [int] NOT NULL,
	[StatusSensor] [int] NOT NULL,
	CONSTRAINT [PK_Sensores] PRIMARY KEY CLUSTERED ([Id] ASC)
 )
	PRINT('Tabela nova Sensores criada em - Database: Monitoramento.');

END
ELSE
	PRINT('Tabela Sensores já existe no - Database: Monitoramento.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('Monitoramento.Logs') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Logs(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Detalhes] [nvarchar](max) NOT NULL,	
	CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
 )
	PRINT('Tabela nova Logs criada em - Database: Monitoramento.');

END
ELSE
	PRINT('Tabela Logs já existe no - Database: Monitoramento.');
GO

ALTER TABLE [dbo].[Sensores]  WITH CHECK ADD  CONSTRAINT [FK_Sensores_Paises_PaisId] FOREIGN KEY([PaisId])
 REFERENCES [dbo].[Paises] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Sensores] CHECK CONSTRAINT [FK_Sensores_Paises_PaisId]

GO

ALTER TABLE [dbo].[Sensores]  WITH CHECK ADD  CONSTRAINT [FK_Sensores_Regioes_RegiaoId] FOREIGN KEY([RegiaoId])
 REFERENCES [dbo].[Regioes] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[Sensores] CHECK CONSTRAINT [FK_Sensores_Regioes_RegiaoId]






