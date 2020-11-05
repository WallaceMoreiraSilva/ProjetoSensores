USE ProjetoModeloDDD

GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProjetoModeloDDD.Paises') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Paises(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NULL,
	[IsoDuasLetras] [varchar](2) NULL,
	[IsoTresLetras] [varchar](3) NULL,
	[NumeroCodigoIso] [varchar](250) NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NOT NULL,
	CONSTRAINT [PK_Paises] PRIMARY KEY CLUSTERED (	[Id] ASC)
)

	PRINT('Tabela nova Paises criada em - Database: ProjetoModeloDDD.');

END
ELSE
	PRINT('Tabela Paises já existe no - Database: ProjetoModeloDDD.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProjetoModeloDDD.Regioes') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Regioes(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NOT NULL,
	CONSTRAINT [PK_Regioes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
	PRINT('Tabela nova Regioes criada em - Database: ProjetoModeloDDD.');

END
ELSE
	PRINT('Tabela Regioes já existe no - Database: ProjetoModeloDDD.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProjetoModeloDDD.Sensores') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE Sensores(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NULL,
	[Numero] [int] NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NOT NULL,
	[RegiaoId] [int] NOT NULL,
	[PaisId] [int] NOT NULL,
	[StatusSensor] [int] NOT NULL,
	CONSTRAINT [PK_Sensores] PRIMARY KEY CLUSTERED ([Id] ASC)
 )
	PRINT('Tabela nova Sensores criada em - Database: ProjetoModeloDDD.');

END
ELSE
	PRINT('Tabela Sensores já existe no - Database: ProjetoModeloDDD.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProjetoModeloDDD.EventoDisparados') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE EventoDisparados(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NULL,
	[Valor] [int] NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[SensorId] [int] NOT NULL,
	[StatusEventoDisparado] [int] NOT NULL,
	CONSTRAINT [PK_EventoDisparados] PRIMARY KEY CLUSTERED ([Id] ASC)
)
	PRINT('Tabela nova EventoDisparados criada em - Database: ProjetoModeloDDD.');

END
ELSE
	PRINT('Tabela EventoDisparados já existe no - Database: ProjetoModeloDDD.');
GO

IF NOT EXISTS (SELECT TOP 1 * 
                 FROM sys.objects 
                WHERE object_id = OBJECT_ID('ProjetoModeloDDD.LogAuditorias') 
                  AND type IN ('U'))

BEGIN

CREATE TABLE LogAuditorias(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DetalhesAuditoria] [nvarchar](max) NULL,
	CONSTRAINT [PK_LogAuditorias] PRIMARY KEY CLUSTERED ([Id] ASC)
 )
	PRINT('Tabela nova LogAuditorias criada em - Database: ProjetoModeloDDD.');

END
ELSE
	PRINT('Tabela LogAuditorias já existe no - Database: ProjetoModeloDDD.');
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

GO

ALTER TABLE [dbo].[EventoDisparados]  WITH CHECK ADD  CONSTRAINT [FK_EventoDisparados_Sensores_SensorId] FOREIGN KEY([SensorId])
 REFERENCES [dbo].[Sensores] ([Id]) ON DELETE CASCADE

GO

ALTER TABLE [dbo].[EventoDisparados] CHECK CONSTRAINT [FK_EventoDisparados_Sensores_SensorId]




