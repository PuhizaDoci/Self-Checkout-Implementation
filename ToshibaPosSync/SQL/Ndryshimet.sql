
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='Sinkronizuar')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD [Sinkronizuar] [BIT] NOT NULL CONSTRAINT [DF_DaljaMallit_Sinkronizuar] DEFAULT ((0))
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='OperatoriAutomatikId')
BEGIN
ALTER TABLE dbo.Arkat ADD OperatoriAutomatikId INT NULL 
End
Go --
IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_Arkat_Mxh_Operatoret_OperatoriAutomatikId')
BEGIN
ALTER TABLE dbo.Arkat     
ADD CONSTRAINT FK_Arkat_Mxh_Operatoret_OperatoriAutomatikId 
	FOREIGN KEY (OperatoriAutomatikId)     
    REFERENCES dbo.Mxh_Operatoret(Id)     
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='Grupi')
BEGIN
ALTER TABLE dbo.Artikujt ADD Grupi varchar(400)
End
Go --
if exists (select 1 from sys.tables where name ='Arkat')
Begin
alter table Arkat 
alter column [RegjimiPunesOffline]                     BIT                            NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('CCPKompanite') AND name='AplikoCmiminShumices')
BEGIN
ALTER TABLE dbo.CCPKompanite ADD AplikoCmiminShumices bit null 
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='RFID')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD RFID varchar(30)
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='RFIDCCP')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD RFIDCCP varchar(30)
End 
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('CCPKompanite') AND name='AutorizoBlerjetNePOS')
BEGIN
ALTER TABLE dbo.CCPKompanite ADD AutorizoBlerjetNePOS bit not null default (0)
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='K1')
BEGIN
ALTER TABLE dbo.Artikujt ADD K1 INT
End 
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='K2')
BEGIN
ALTER TABLE dbo.Artikujt ADD K2 INT
End 
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='K3')
BEGIN
ALTER TABLE dbo.Artikujt ADD K3 INT
End 
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='K4')
BEGIN
ALTER TABLE dbo.Artikujt ADD K4 INT
End 
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='K5')
BEGIN
ALTER TABLE dbo.Artikujt ADD K5 INT
End 
Go --

Delete B from Barkodat B
inner join 
(
select Barkodi from barkodat group by Barkodi having count(Barkodi)>1
) A on B.Barkodi = A.Barkodi 

Go --

IF EXISTS
(
SELECT 1 
FROM sys.indexes 
WHERE name='UQ_Barkodat_Barkodi' AND object_id = OBJECT_ID('dbo.Barkodat')
) 
BEGIN 
		ALTER INDEX UQ_Barkodat_Barkodi ON dbo.Barkodat
		REBUILD
END
ELSE
BEGIN 
		ALTER TABLE [dbo].[Barkodat] ADD  CONSTRAINT [UQ_Barkodat_Barkodi] UNIQUE NONCLUSTERED 
		(
			[Barkodi] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF , IGNORE_DUP_KEY = OFF , ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90)
End
Go --

UPDATE D 
SET D.IdLokal=
				CONVERT(nchar(4),YEAR(DataERegjistrimit)) 
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),MONTH(DataERegjistrimit))  AS VARCHAR(2)),2)
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),DAY(DataERegjistrimit))  AS VARCHAR(2)),2)
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),DATEPART(hour, DataERegjistrimit))  AS VARCHAR(2)),2)
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),DATEPART(MINUTE, DataERegjistrimit))  AS VARCHAR(2)),2)
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),DATEPART(SECOND, DataERegjistrimit))  AS VARCHAR(2)),2)
				+RIGHT('00'+CAST(CONVERT(VARCHAR(2),NumriArkes)  AS VARCHAR(2)),2)
FROM dbo.DaljaMallit D WHERE IdLokal IS NULL OR IdLokal=''

Go --

UPDATE D SET Validuar = 1 from DaljaMallit D
where TavolinaId is null and (Validuar is null or Validuar=0)

Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Mxh_Operatoret') AND name='Tel')
BEGIN
ALTER TABLE dbo.Mxh_Operatoret ADD [Tel] VARCHAR (50)  NULL
End 
Go -- 

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('MenyratEPageses') AND name='ParaqitetNePos')
BEGIN
ALTER TABLE dbo.MenyratEPageses ADD [ParaqitetNePos] bit default (1)
End 
Go -- 

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('EkzekutimiPageses') AND name='DhenjeKesh')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses ADD [DhenjeKesh] DECIMAL (18, 2) DEFAULT ((0)) NOT NULL
End 
Go --

BEGIN TRY
DECLARE @TableName varchar(255) 

DECLARE TableCursor CURSOR FOR 
SELECT TABLE_SCHEMA+'.'+table_name FROM information_schema.tables 
WHERE table_type = 'base table' 
OPEN TableCursor
	FETCH NEXT FROM TableCursor INTO @TableName 
		WHILE @@FETCH_STATUS = 0 
				BEGIN 
				DBCC DBREINDEX(@TableName,' ',90) 
				print @TableName
				FETCH NEXT FROM TableCursor INTO @TableName 
				END 
		CLOSE TableCursor 
	DEALLOCATE TableCursor
END TRY 
BEGIN CATCH 
PRINT 'Error te rebuild index'
END CATCH
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='BarazimiId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD [BarazimiId] bigint NULL
End 
Go --

If exists(select 1 from sys.tables where Name = 'NumratEDokumenteve') 
Begin 
		DROP TABLE NumratEDokumenteve  
END 

Go --  

IF NOT EXISTS(select 1 from sys.tables where Name = 'CCPKartelatArtikujt') 
BEGIN
CREATE TABLE [dbo].[CCPKartelatArtikujt]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [CCPKompaniaId] INT NOT NULL,     
    [ArtikulliId] INT NOT NULL,
	[Statusi] BIT NOT NULL, 
	CONSTRAINT [FK_CCPKartelatArtikujt_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id]),
	CONSTRAINT uq_CCPKartelatArtikujt UNIQUE([CCPKompaniaId], [ArtikulliId])
)
end
Go -- 

IF EXISTS (
SELECT 1 FROM sys.foreign_keys WHERE name ='FK_POSAktivitetetKuponat_AktivitetiId'
) BEGIN 
ALTER TABLE POSAktivitetetKuponat DROP CONSTRAINT [FK_POSAktivitetetKuponat_AktivitetiId]
End
Go --

IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='DaljaMallitIdServer')
BEGIN
ALTER TABLE dbo.DaljaMallit DROP column DaljaMallitIdServer  
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='LimitiSasi')
BEGIN 
ALTER TABLE dbo.Subjektet ADD [LimitiSasi] [decimal] (18, 2) NOT NULL DEFAULT(0)
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='LimitiVlere')
BEGIN  
ALTER TABLE dbo.Subjektet ADD [LimitiVlere] [decimal] (18, 2) NOT NULL DEFAULT(0)
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='PrindiId')
BEGIN  
ALTER TABLE dbo.Subjektet ADD [PrindiId] Int
End
Go --


IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='ServerId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD ServerId bigint
END

Go -- 
begin try
			Drop table [TOSHIBA].[CCPSubjektetGrupet]
end try
begin catch

end  catch
Go -- 
IF EXISTS(select 1 from sys.tables where Name = 'CCPSubjektetGrupet') 
BEGIN
			Drop table [CCPSubjektetGrupet]
End
Go -- 
IF NOT EXISTS(select 1 from sys.tables where Name = 'CCPSubjektetGrupet') 
BEGIN
		CREATE TABLE [dbo].[CCPSubjektetGrupet]
		(
		[Id] [INT] NOT NULL PRIMARY KEY,
		[CCPGrupiId] [int] NOT NULL,
		[CCPSubjektiId] [int] NOT NULL,
		[LimitiSasi] [decimal] (18, 2) NOT NULL,
		[LimitiVlere] [decimal] (18, 2) NOT NULL,
		[VleraEShpenzuar] [decimal] (18,2)
		)
End


IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='ServerId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD ServerId bigint
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='ReferencaID')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD ReferencaID varchar(50) NULL 
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='IdImportimit')
BEGIN
ALTER TABLE dbo.Artikujt ADD IdImportimit varchar(50) NULL  
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='IdImportimit')
BEGIN
ALTER TABLE dbo.Subjektet ADD IdImportimit varchar(50) NULL  
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='VleraEShpenzuar')
BEGIN
ALTER TABLE dbo.Subjektet ADD VleraEShpenzuar decimal(18,4) NULL 
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('CCPSubjektetGrupet') AND name='VleraEShpenzuar')
BEGIN
ALTER TABLE dbo.CCPSubjektetGrupet ADD VleraEShpenzuar decimal(18,4) NULL 
End
Go --


IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='PershkrimiTiketa')
BEGIN
ALTER TABLE dbo.Artikujt ALTER COLUMN PershkrimiTiketa VARCHAR(100) NOT NULL
End 
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'DaljeInternePersonat') 
BEGIN
		CREATE TABLE [dbo].[DaljeInternePersonat] (
		[Id]           INT          NOT NULL,
		[Emri]         VARCHAR (30) NOT NULL,
		[Mbiemri]      VARCHAR (30) NOT NULL,
		[OrganizataId] INT          NOT NULL,
		CONSTRAINT [PK_DaljeInternePersonat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End

Go --

DECLARE @table_name NVARCHAR(256)  
DECLARE @col_name NVARCHAR(256)  
DECLARE @Command  NVARCHAR(1000)  

SET @table_name = N'DaljaMallit'
SET @col_name = N'IdLokal'

SELECT @Command = 'ALTER TABLE ' + @table_name + ' drop constraint ' + d.name
    FROM sys.tables t 
    JOIN sys.indexes d ON d.object_id = t.object_id  AND d.type=2 AND d.is_unique=1
    JOIN sys.index_columns ic ON d.index_id=ic.index_id AND ic.object_id=t.object_id
    JOIN sys.columns c ON ic.column_id = c.column_id  AND c.object_id=t.object_id
    WHERE t.name = @table_name AND c.name=@col_name

EXECUTE (@Command)

ALTER TABLE dbo.DaljaMallit ALTER COLUMN IdLokal VARCHAR(50)

ALTER TABLE DaljaMallit
ADD UNIQUE (IdLokal);

Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'DaljaMallitArtikujtEPerber') 
BEGIN
	CREATE TABLE [dbo].[DaljaMallitArtikujtEPerber] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [DaljaMallitId]       BIGINT          NOT NULL,
    [ArtikulliPerberId]   INT             NOT NULL,
    [ArtikulliPerberesId] INT             NOT NULL,
    [Sasia]               DECIMAL (18, 8) NOT NULL,
    [KostoMesatare]       DECIMAL (18, 8) NOT NULL,
    [QmimiIShitjes]       DECIMAL (18, 8) NOT NULL,
    [DaljaMallitDetaleId] INT             NOT NULL,
    [HyrjaDetaleId]       INT             NULL,
    [ArtikujtEProdhuarId] INT             NULL,
    CONSTRAINT [PK_DaljaMallitArtikujtEPerber_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_DaljaMallitArtikujtEPerber_ArtikulliPerberesId] FOREIGN KEY ([ArtikulliPerberesId]) REFERENCES [dbo].[Artikujt] ([Id]),
    CONSTRAINT [FK_DaljaMallitArtikujtEPerber_ArtikulliPerberId] FOREIGN KEY ([ArtikulliPerberId]) REFERENCES [dbo].[Artikujt] ([Id]),
    CONSTRAINT [FK_DaljaMallitArtikujtEPerber_DaljaMallitDetaleId] FOREIGN KEY ([DaljaMallitDetaleId]) REFERENCES [dbo].[DaljaMallitDetale] ([Id]),
    CONSTRAINT [FK_DaljaMallitArtikujtEPerber_DaljaMallitId] FOREIGN KEY ([DaljaMallitId]) REFERENCES [dbo].[DaljaMallit] ([Id])
)
End

Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'FinKontrolliPeriodes') 
BEGIN
	CREATE TABLE [dbo].[FinKontrolliPeriodes] (
    [Id]                     INT  NOT NULL,
    [PrejDates]              DATE NULL,
    [DeriMeDaten]            DATE NULL,
    [StatusiMbyllur]         BIT  NULL,
    [PjesaFinanciareMbyllur] BIT  NULL,
    CONSTRAINT [PK_FinKontrolliPeriodes_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End

Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('RaportetFajllat') AND name='EmriPrinterit')
BEGIN
ALTER TABLE dbo.RaportetFajllat ADD [EmriPrinterit] varchar(200)  
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('RaportetFajllat') AND name='NumriKopjeve')
BEGIN
ALTER TABLE dbo.RaportetFajllat ADD [NumriKopjeve] int
End
Go --

IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('SubjektiBankat') AND name='DataERegjistrimit')
BEGIN
					DECLARE @sql NVARCHAR(MAX)
					  SELECT @sql = N'alter table SubjektiBankat drop constraint ['+dc.NAME+N']'
						from sys.default_constraints dc
						JOIN sys.columns c
							ON c.default_object_id = dc.object_id
						WHERE 
							dc.parent_object_id = OBJECT_ID('SubjektiBankat')
					IF(@sql IS NOT NULL)
					begin
					EXEC (@sql)
					End
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('SubjektiBankat') AND name='Koment')
BEGIN
ALTER TABLE dbo.SubjektiBankat drop column Koment
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('SubjektiBankat') AND name='DataERegjistrimit')
BEGIN
ALTER TABLE dbo.SubjektiBankat drop column DataERegjistrimit
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Bankat') AND name='DataERegjistrimit')
BEGIN
					DECLARE @sql NVARCHAR(MAX)
					  SELECT @sql = N'alter table Bankat drop constraint ['+dc.NAME+N']'
						from sys.default_constraints dc
						JOIN sys.columns c
							ON c.default_object_id = dc.object_id
						WHERE 
							dc.parent_object_id = OBJECT_ID('Bankat')
					IF(@sql IS NOT NULL)
					begin
					EXEC (@sql)
					End
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Bankat') AND name='DataERegjistrimit')
BEGIN
ALTER TABLE dbo.Bankat drop column DataERegjistrimit
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Bankat') AND name='FinBankaId')
BEGIN
ALTER TABLE dbo.Bankat drop column FinBankaId
End
Go -- 
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='KartelaId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD [KartelaId] [int]
End
Go --  

IF NOT EXISTS(select 1 from sys.tables where Name = 'ArtikujtFoto') 
BEGIN
	CREATE TABLE [dbo].[ArtikujtFoto] (
    [Id]          INT           NOT NULL,
    [Foto]        IMAGE         NOT NULL,
    [ArtikulliId] INT           NOT NULL,
    CONSTRAINT [PK_ArtikujtFoto_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_ArtikujtFoto_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id])
)
End
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetFilterLlojet') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetFilterLlojet] (
    [Id]                  INT            NOT NULL,
    [Pershkrimi]          VARCHAR (50)   NOT NULL,
    [QueryDinamik]        VARCHAR (8000) NOT NULL,
    [EmriFushesPerFilter] VARCHAR (50)   NULL,
    [Tipi]                VARCHAR (50)   NULL,
    CONSTRAINT [PK_POSAktivitetetFilterLlojet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetLlojetEZbritjes') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetLlojetEZbritjes] (
    [Id]         INT          NOT NULL,
    [Pershkrimi] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_POSAktivitetetLlojetEZbritjes_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetet') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetet] (
    [Id]                   BIGINT       NOT NULL,
    [PrejDates]            DATE         NOT NULL,
    [DeriMeDaten]          DATE         NOT NULL,
    [PrejOres]             DATETIME     NULL,
    [DeriMeOren]           DATETIME     NULL,
    [Pershkrimi]           VARCHAR (50) NULL,
    [Statusi]              BIT          CONSTRAINT [DF_POSAktivitetet_Statusi] DEFAULT ((1)) NOT NULL,
    [OrganizataId]         INT          NOT NULL,
    [LlojiIDokumentitId]   INT          NOT NULL,
    [NrDokumentit]         INT          NOT NULL,
    [Viti]                 INT          NOT NULL,
    [Data]                 DATE         NOT NULL,
    [PerfshinKatallogun]   BIT          CONSTRAINT [DF_POSAktivitetet_PerfshinKatallogun] DEFAULT ((0)) NOT NULL,
    [Dhe]                  BIT          CONSTRAINT [DF_POSAktivitetet_Dhe] DEFAULT ((1)) NOT NULL,
    [AplikoZbritjenNeGrup] BIT          CONSTRAINT [DF_POSAktivitetet_AplikoZbritjenNeGrup] DEFAULT ((0)) NOT NULL,
    [Mbishkruaj]           BIT          CONSTRAINT [DF_POSAktivitetet_Mbishkruaj] DEFAULT ((0)) NOT NULL,
    [LlojiAktivitetitId]   INT          NULL,
    [Validuar]             BIT          CONSTRAINT [DF_POSAktivitetet_Validuar] DEFAULT ((0)) NOT NULL,
    [GratisFix] BIT NULL, 
	[AplikoNeRabat] BIT DEFAULT 0 NOT NULL,
	[AplikoNeEkstraRabat] BIT DEFAULT 0 NOT NULL,
    CONSTRAINT [PK_POSAktivitetet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    --CONSTRAINT [FK_POSAktivitetet_LlojiAktivitetitId] FOREIGN KEY ([LlojiAktivitetitId]) REFERENCES [dbo].[POSAktivitetetLlojet] ([Id]),
    CONSTRAINT [FK_POSAktivitetet_LlojiIDokumentitId] FOREIGN KEY ([LlojiIDokumentitId]) REFERENCES [dbo].[LlojetEDokumenteve] ([Id]),
    CONSTRAINT [FK_POSAktivitetet_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id])
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetOrganizatat') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetOrganizatat] (
    [Id]           INT    NOT NULL,
    [AktivitetiId] BIGINT NOT NULL,
    [OrganizataId] INT    NOT NULL,
    [Statusi]      BIT    CONSTRAINT [DF_POSAktivitetetOrganizatat_Statusi] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_POSAktivitetetOrganizatat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_POSAktivitetetOrganizatat_AktivitetiId] FOREIGN KEY ([AktivitetiId]) REFERENCES [dbo].[POSAktivitetet] ([Id]),
    CONSTRAINT [FK_POSAktivitetetOrganizatat_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id])
)
END
Go --
 
IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetZbritjaNeVlere') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetZbritjaNeVlere] (
    [Id]               INT             NOT NULL,
    [AktivitetiId]     BIGINT          NOT NULL,
    [LlojiIZbrijtesId] INT             NOT NULL,
    [Vlera]            DECIMAL (18, 2) NOT NULL,
    [Zbritja]          DECIMAL (18, 2) NOT NULL,
    [Komenti]          VARCHAR (200)   NULL,
    CONSTRAINT [PK_POSAktivitetetZbritjaNeVlere_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_POSAktivitetetZbritjaNeVlere_AktivitetiId] FOREIGN KEY ([AktivitetiId]) REFERENCES [dbo].[POSAktivitetet] ([Id]),
    CONSTRAINT [FK_POSAktivitetetZbritjaNeVlere_LlojiIZbrijtesId] FOREIGN KEY ([LlojiIZbrijtesId]) REFERENCES [dbo].[POSAktivitetetLlojetEZbritjes] ([Id])
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetFilter') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetFilter] (
    [Id]                  INT             NOT NULL,
    [AktivitetiId]        BIGINT          NOT NULL,
    [FilteriId]           INT             NOT NULL,
    [Shifra]              VARCHAR (50)    NOT NULL,
    [EmriFushesPerFilter] VARCHAR (50)    NULL,
    [AplikoZbritjen]      BIT             CONSTRAINT [DF_POSAktivitetetFilter_AplikoZbritjen] DEFAULT ((1)) NOT NULL,
    [Pershkrimi]          VARCHAR (50)    NULL,
    [LlojiZbritjesId]     INT             NULL,
    [Zbritja]             DECIMAL (18, 2) NULL,
    [Vlera]               DECIMAL (18, 2) NULL,
    [Gratis]              BIT             CONSTRAINT [DF_POSAktivitetetFilter_Gratis] DEFAULT ((0)) NOT NULL,
    [Perjashto]           BIT             CONSTRAINT [DF_POSAktivitetetFilter_Perjashto] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_POSAktivitetetFilter_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_POSAktivitetetFilter_AktivitetiId] FOREIGN KEY ([AktivitetiId]) REFERENCES [dbo].[POSAktivitetet] ([Id]),
    CONSTRAINT [FK_POSAktivitetetFilter_FilteriId] FOREIGN KEY ([FilteriId]) REFERENCES [dbo].[POSAktivitetetFilterLlojet] ([Id]),
    CONSTRAINT [FK_POSAktivitetetFilter_LlojiZbritjesId] FOREIGN KEY ([LlojiZbritjesId]) REFERENCES [dbo].[POSAktivitetetLlojetEZbritjes] ([Id])
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetLlojet') 
BEGIN
	CREATE TABLE [dbo].[POSAktivitetetLlojet] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Pershkrimi] VARCHAR (50) NOT NULL,
    [Kodi]       VARCHAR (50) NULL,
    CONSTRAINT [PK_POSAktivitetetLlojet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'DaljaMallitDetaleAktivitetet') 
BEGIN
	CREATE TABLE [dbo].[DaljaMallitDetaleAktivitetet] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [DaljaMallitDetaleId] INT             NULL,
    [AktivitetiId]        BIGINT          NULL,
    [Zbritja]             DECIMAL (23, 8) NULL,
    [DaljaMallitId]       BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
END
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.table_constraints 
    WHERE CONSTRAINT_NAME ='UQ_GrupetEMallrave_Shifra')
Begin
ALTER TABLE dbo.GrupetEMallrave DROP CONSTRAINT UQ_GrupetEMallrave_Shifra
End
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
    WHERE CONSTRAINT_NAME ='FK_CCPKartelatArtikujt_CCPKompaniaId')
Begin
ALTER TABLE dbo.CCPKartelatArtikujt DROP CONSTRAINT FK_CCPKartelatArtikujt_CCPKompaniaId
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='MeVete')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD [MeVete] [BIT] DEFAULT 0 NOT NULL 
End
Go --
IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_EkzekutimiPageses_MenyraEPagesesId')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses     
ADD CONSTRAINT FK_EkzekutimiPageses_MenyraEPagesesId 
	FOREIGN KEY (MenyraEPagesesId)     
    REFERENCES dbo.MenyratEPageses(Id)     
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSZbritjaMeKupon') AND name='KodiKuponit')
BEGIN
ALTER TABLE dbo.POSZbritjaMeKupon ADD [KodiKuponit] VARCHAR(50)
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallitDetale') AND name='AplikimiVoucherit')
BEGIN
ALTER TABLE dbo.DaljaMallitDetale ADD [AplikimiVoucherit] BIT
End
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'Mxh_OperatoretGrupetEMallrave') 
BEGIN
	CREATE TABLE [dbo].[Mxh_OperatoretGrupetEMallrave] (
    [Id]            INT    NOT NULL,
    [OperatoriId]   INT    NOT NULL,
    [GrupiMallitId] BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 100),
    CONSTRAINT [FK_Mxh_OperatoretGrupetEMallrave_GrupetEMallrave_Id] FOREIGN KEY ([GrupiMallitId]) REFERENCES [dbo].[GrupetEMallrave] ([Id]),
    CONSTRAINT [FK_Mxh_OperatoretGrupetEMallrave_Mxh_Operatoret_Id] FOREIGN KEY ([OperatoriId]) REFERENCES [dbo].[Mxh_Operatoret] ([Id]),
    CONSTRAINT [UQ_OperatoriId_GrupiMallitId] UNIQUE NONCLUSTERED ([OperatoriId] ASC, [GrupiMallitId] ASC)
)
END
Go --

DELETE FROM dbo.EkzekutimiPageses WHERE daljamallitid=-1

Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'CCPGrupet') 
BEGIN
CREATE TABLE [dbo].[CCPGrupet]
(
    [Id]         INT           NOT NULL,
    [Pershkrimi] VARCHAR (200) NULL,
    CONSTRAINT [PK_CCPGrupet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
END

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetFilterCCPGrupet') 
BEGIN
CREATE TABLE [dbo].[POSAktivitetetFilterCCPGrupet]
(
	[Id]           INT    NOT NULL,
	[AktivitetiId] BIGINT NOT NULL,
	[CCPGrupiId]   INT    NOT NULL,
	CONSTRAINT [PK_POSAktivitetetFilterCCPGrupet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
	CONSTRAINT [FK_POSAktivitetetFilterCCPGrupet_AktivitetiId] FOREIGN KEY ([AktivitetiId]) REFERENCES [dbo].[POSAktivitetet] ([Id]),
	CONSTRAINT [FK_POSAktivitetetFilterCCPGrupet_CCPGrupiId] FOREIGN KEY ([CCPGrupiId]) REFERENCES [dbo].[CCPGrupet] ([Id])
)
END
Go --



IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'LeximiSondave') AND type in (N'U')) 
			  begin
CREATE TABLE dbo.LeximiSondave (
		[Id]            INT IDENTITY(1,1)             NOT NULL,
		[TankNumber]    int not null,
		[FuelLevel] int not null default((0)),
		[FuelVolume] bigint not null default((0)),
		[WaterLevel] int not null default((0)),
		[WaterVolume] bigint not null default((0)),
		[Temperature] int not null default((0)),
		[Volume] int not null default((0)),
		[DateInserted] datetime default((getdate()))  not null
	)
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('LeximiSondave') AND name='ServerId')
BEGIN
ALTER TABLE dbo.LeximiSondave ADD ServerId INT NULL
End  

Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'Parapagimet') 
BEGIN
CREATE TABLE [dbo].[Parapagimet]
(
	[ParapagimiId]           BIGINT           NOT NULL,
	[SubjektiId]			 INT  not null,
	[Vlera]					 DECIMAL(18,2) not null,
	CONSTRAINT [PK_Parapagimet_ParapagimiId] PRIMARY KEY CLUSTERED ([ParapagimiId] ASC) WITH (FILLFACTOR = 90)
)

END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'ArtikujtPerberesOrganizatat') 
BEGIN
CREATE TABLE [dbo].[ArtikujtPerberesOrganizatat]
(
	[Id]           INT IDENTITY (1, 1) NOT NULL,
    [OrganizataId] INT                 NOT NULL,
    CONSTRAINT [FK_ArtikujtPerberesOrganizatat_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id])
)
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.indexes	WHERE name = 'IX_DaljaMallit_Data')
begin
		   create nonclustered INDEX [IX_DaljaMallit_Data] ON [dbo].[DaljaMallit] (Data) 
End
else
Begin 
ALTER INDEX [IX_DaljaMallit_Data] ON dbo.[DaljaMallit]
REBUILD
End
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'AktivitetetDokument') 
BEGIN
CREATE TABLE [dbo].[AktivitetetDokument]
(
	[Id]               BIGINT                     NOT NULL,
    [Pershkrimi]       VARCHAR(100)               NOT NULL,
    [OrganizataId]     INT                        NOT NULL,
    [NrDokumentit]     INT                        NOT NULL,
    [Viti]             INT                        NOT NULL,
    [Data]             DATE                       NOT NULL,
    [DataRegjistrimit] DATETIME DEFAULT GETDATE() NOT NULL,
    [RegjistruarNga]   INT                        NOT NULL,
    CONSTRAINT [PK_AktivitetetDokument_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    --CONSTRAINT [FK_AktivitetetDokument_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id]),
    CONSTRAINT [FK_AktivitetetDokument_RegjistruarNga] FOREIGN KEY ([RegjistruarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id])
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'AktivitetetDokumentDetale') 
BEGIN
CREATE TABLE [dbo].[AktivitetetDokumentDetale]
(
	[Id]                    INT    NOT NULL,
    [AktivitetetDokumentId] BIGINT NOT NULL,
    [ArtikulliId]           INT    NOT NULL,
    CONSTRAINT [PK_AktivitetetDokumentDetale_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AktivitetetDokumentDetale_AktivitetetDokumentId] FOREIGN KEY ([AktivitetetDokumentId]) REFERENCES [dbo].[AktivitetetDokument] ([Id]),
    CONSTRAINT [FK_AktivitetetDokumentDetale_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id])
)
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='SaldoFikse')
BEGIN
ALTER TABLE dbo.Subjektet ADD SaldoFikse decimal(18, 2) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='LimitiObligimit')
BEGIN
ALTER TABLE dbo.Subjektet ADD LimitiObligimit decimal(18, 2) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Valutat') AND name='Kursi')
BEGIN
ALTER TABLE dbo.Valutat ADD [Kursi] DECIMAL(18,10) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('EkzekutimiPageses') AND name='ValutaId')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses ADD [ValutaId] INT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('EkzekutimiPageses') AND name='Kursi')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses ADD [Kursi] DECIMAL(18,10) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('EkzekutimiPageses') AND name='KodiVoucherit')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses ADD KodiVoucherit varchar(200) 
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('EkzekutimiPageses') AND name='LlojiVoucherit')
BEGIN
ALTER TABLE dbo.EkzekutimiPageses ADD LlojiVoucherit varchar(200) 
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Valutat') AND name='ValutaAktive')
BEGIN
ALTER TABLE dbo.Valutat ADD ValutaAktive BIT DEFAULT 0 NOT NULL
End 
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSKuponatPerZbritjeKodetEKuponave') 
BEGIN
CREATE TABLE [dbo].[POSKuponatPerZbritjeKodetEKuponave]
(
	[Id] INT NOT NULL,
    [KuponatPerZbritjeId] INT NOT NULL,
    [KodiKuponit] VARCHAR(25) NOT NULL,
    [Aktivizuar] BIT DEFAULT 0 NOT NULL,
    [Aplikuar] BIT DEFAULT 0 NOT NULL,
    CONSTRAINT [PK_POSKuponatPerZbritjeKodetEKuponave_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSKuponatPerZbritjeKodetEKuponave') AND name='DaljaMallitIdGjeneruar')
BEGIN
ALTER TABLE dbo.POSKuponatPerZbritjeKodetEKuponave ADD DaljaMallitIdGjeneruar BIGINT NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSKuponatPerZbritjeKodetEKuponave') AND name='DaljaMallitIdAplikuar')
BEGIN
ALTER TABLE dbo.POSKuponatPerZbritjeKodetEKuponave ADD DaljaMallitIdAplikuar BIGINT NULL
End 
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSAktivitetetKuponat') 
BEGIN
CREATE TABLE [dbo].[POSAktivitetetKuponat]
(
	[Id] INT NOT NULL,
	[AktivitetiId] BIGINT NOT NULL,
	[KuponatPerZbritjeId] INT NOT NULL,
	[Sasia] DECIMAL(18,2) NOT NULL,
	CONSTRAINT [PK_POSAktivitetetKuponat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR=90)
)
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSAktivitetetFilter') AND name='Cmimi')
BEGIN
ALTER TABLE dbo.POSAktivitetetFilter ADD Cmimi DECIMAL(18,2) NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='ShteguFiskal')
BEGIN
ALTER TABLE dbo.Arkat ADD ShteguFiskal VARCHAR(250) NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='TipiPrinterit')
BEGIN
ALTER TABLE dbo.Arkat ADD TipiPrinterit VARCHAR(250) NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='Porti')
BEGIN
ALTER TABLE dbo.Arkat ADD Porti VARCHAR(250) NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='PrintonDirekt')
BEGIN
ALTER TABLE dbo.Arkat ADD PrintonDirekt BIT NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='NumriSerik')
BEGIN
ALTER TABLE dbo.Artikujt ADD NumriSerik VARCHAR(80) NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Mxh_Filialet') AND name='PrindiId')
BEGIN
ALTER TABLE dbo.Mxh_Filialet ADD PrindiId VARCHAR(100) NULL
End 
Go --


IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallitDetale') AND name='KujtesaOrderId')
BEGIN
ALTER TABLE dbo.DaljaMallitDetale ADD [KujtesaOrderId] INT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='NumriATK')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD NumriATK VARCHAR(150) NULL
End
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'CCPAlokimiBonuseve') 
BEGIN
CREATE TABLE [dbo].[CCPAlokimiBonuseve] (
    [Id]                INT              NOT NULL,
    [CCPKompaniaId]     INT              NOT NULL,
    [VleraBonusit]      DECIMAL (18, 10) NOT NULL,
    [DaljaMallitId]     BIGINT           NULL,
    [DataERegjistrimit] DATETIME         CONSTRAINT [DF_CCPAlokimiBonuseve_DataERegjistrimit] DEFAULT (getdate()) NOT NULL,
    [Koment]            VARCHAR (600)    NULL,
    [DataBonusit]       DATE             NULL,
    [RegjistruarNga]    INT              NULL,
    CONSTRAINT [PK_CCPAlokimiBonuseve_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PMPBonuset_RegjistruarNga] FOREIGN KEY ([RegjistruarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id])
)
END
Go --

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE Name = 'CCPArtikujtEPerjashtuar') 
BEGIN
CREATE TABLE [dbo].[CCPArtikujtEPerjashtuar](
    [Id]           INT  NOT NULL,
    [ArtikulliID]  INT  NOT NULL,
    [Prejdates]    DATE NULL,
    [DeriMeDaten]  DATE NULL,
	[Perkohesisht] BIT  NOT NULL DEFAULT(0),
    CONSTRAINT [PK_CCPArtikujtEPerjashtuar_ArtikulliID] PRIMARY KEY CLUSTERED ([ArtikulliID] ASC)
)
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'CCPShperblimetArtikujt') 
BEGIN
CREATE TABLE [dbo].[CCPShperblimetArtikujt] (
   [Id]          INT             NOT NULL,
   [ArtikulliId] INT             NOT NULL,
   [NrPikeve]    DECIMAL (18, 2) NOT NULL,
   CONSTRAINT [PK_CCPShperblimetArtikujt_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
   CONSTRAINT [FK_CCPShperblimetArtikujt_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id])
)
END
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='NumriArkesGK')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD NumriArkesGK varchar(50)
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='Personi')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD Personi VARCHAR(300) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='Adresa')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD Adresa VARCHAR(800) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='NumriPersonal')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD NumriPersonal VARCHAR(50) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='NrTel')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD NrTel VARCHAR(50) NULL
End
Go --
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallitDetale') AND name='Barkodi')
BEGIN
ALTER TABLE dbo.DaljaMallitDetale ADD [Barkodi] varchar(150)
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='ShenimeShtesePOS')
BEGIN
ALTER TABLE dbo.Subjektet ADD ShenimeShtesePOS BIT NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='PrindiId')
BEGIN
ALTER TABLE dbo.Subjektet ADD PrindiId int NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('CCPFaturat') AND name='Shtypur')
BEGIN
ALTER TABLE dbo.CCPFaturat ADD [Shtypur] BIT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('CCPFaturat') AND name='VleraPikeve')
BEGIN
ALTER TABLE dbo.CCPFaturat ADD [VleraPikeve] DECIMAL (18,2) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.indexes	WHERE name = 'IX_Artikujt_Shifra')
begin
		   create nonclustered INDEX [IX_Artikujt_Shifra] ON dbo.Artikujt (Shifra) 
End
else
Begin 
ALTER INDEX [IX_Artikujt_Shifra] ON dbo.Artikujt
REBUILD
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.indexes	WHERE name = 'IX_Artikujt_ShifraProdhuesit')
begin
		   create nonclustered INDEX [IX_Artikujt_ShifraProdhuesit] ON dbo.Artikujt (ShifraProdhuesit) 
End
else
Begin 
ALTER INDEX [IX_Artikujt_ShifraProdhuesit] ON dbo.Artikujt
REBUILD
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSAktivitetetFilterCCPGrupet') AND name='KodiKarteles')
BEGIN
ALTER TABLE dbo.POSAktivitetetFilterCCPGrupet ADD KodiKarteles VARCHAR(80) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Barkodat') AND name='Cmimi')
BEGIN
ALTER TABLE dbo.Barkodat ADD Cmimi DECIMAL(18,3) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='NeTestim')
BEGIN
ALTER TABLE dbo.Arkat ADD NeTestim INT NULL 
End
Go --

IF EXISTS(select 1 from sys.tables where Name = 'POSKuponatPerZbritje')
BEGIN
	DROP TABLE POSKuponatPerZbritje
END
Go --

IF NOT EXISTS(select 1 from sys.tables where Name = 'POSKuponatPerZbritje') 
BEGIN
CREATE TABLE [dbo].[POSKuponatPerZbritje] (
    [Id]                                 INT             NOT NULL,
    [Vlera]                              DECIMAL (18, 2) NOT NULL,
    [Aplikuar]                           BIT             CONSTRAINT [DF_POSKuponatPerZbritje_Aplikuar] DEFAULT ((0)) NOT NULL,
    [DataERegjistrimit]                  DATETIME        CONSTRAINT [DF_POSKuponatPerZbritje_DataERegjistrimit] DEFAULT (getdate()) NULL,
    [RegjistruarNga]                     INT             NOT NULL,
    [KuponatPerZbritjeLlojetEZbritjesId] INT             NOT NULL,
    [Dhe]                                BIT             NULL,
    [PrejDates]                          DATE            NULL,
    [DeriMeDaten]                        DATE            NULL,
    CONSTRAINT [PK_POSKuponatPerZbritje_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 100),
    CONSTRAINT [FK_POSKuponatPerZbritje_RegjistruarNga] FOREIGN KEY ([RegjistruarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id])
)
END
Go --
IF NOT EXISTS(select 1 from sys.tables where Name = 'DaljaMallitVaucherat') 
BEGIN
CREATE TABLE [dbo].[DaljaMallitVaucherat] 
(
Id int identity(1,1) not null,
[DaljaMallitID] [BIGINT],
[Vlera] [DECIMAL](18,2),
[KodiVaucherit] Varchar(150),
[Emri] Varchar(150),
[Mbiemri] Varchar(150),
[Lloji] Varchar(350),
[DerguarNeServer] BIT NOT NULL DEFAULT ((0)),
CONSTRAINT [PK_DaljaMallitVaucherat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Artikujt') AND name='ArtikulliNukPeshohet')
BEGIN
ALTER TABLE dbo.Artikujt ADD ArtikulliNukPeshohet bit not null default(0) 
End
Go --
IF EXISTS (
SELECT 1 FROM sys.objects WHERE name ='CK__PLUPeshoret__PLU__6497E884'
) BEGIN 
ALTER TABLE PLUPeshoret DROP CONSTRAINT [CK__PLUPeshoret__PLU__6497E884]
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('PLUPeshoret') AND name='PLU')
BEGIN
ALTER TABLE dbo.PLUPeshoret alter column PLU varchar(50)
End
Go --

IF EXISTS (
SELECT 1 FROM sys.objects WHERE name ='FK_AktivitetetDokument_OrganizataId'
) BEGIN 
ALTER TABLE AktivitetetDokument DROP CONSTRAINT [FK_AktivitetetDokument_OrganizataId]
End
Go --

IF EXISTS(select 1 from sys.objects where Name = 'PK_CCPArtikujtEPerjashtuar_ArtikulliID')
BEGIN
	ALTER TABLE dbo.CCPArtikujtEPerjashtuar
	DROP CONSTRAINT [PK_CCPArtikujtEPerjashtuar_ArtikulliID]
END
Go --
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='DataERegjistrimit')
BEGIN
					DECLARE @sql NVARCHAR(MAX)
					  SELECT @sql = N'alter table Cmimorja drop constraint ['+dc.NAME+N']'
						from sys.default_constraints dc
						JOIN sys.columns c
							ON c.default_object_id = dc.object_id
						WHERE 
							dc.parent_object_id = OBJECT_ID('Cmimorja')
					IF(@sql IS NOT NULL)
					begin
					EXEC (@sql)
					End
END
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='EkstraRabati')
BEGIN
					DECLARE @sql NVARCHAR(MAX)
					  SELECT @sql = N'alter table Cmimorja drop constraint ['+dc.NAME+N']'
						from sys.default_constraints dc
						JOIN sys.columns c
							ON c.default_object_id = dc.object_id
						WHERE 
							dc.parent_object_id = OBJECT_ID('Cmimorja')
					IF(@sql IS NOT NULL)
					begin
					EXEC (@sql)
					End
END
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='Kosto')
BEGIN
ALTER TABLE dbo.Cmimorja drop column Kosto
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='Mazha')
BEGIN
ALTER TABLE dbo.Cmimorja drop column Mazha
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='BazaFurnizuese')
BEGIN
ALTER TABLE dbo.Cmimorja drop column BazaFurnizuese
End
Go --

IF EXISTS(select 1 from sys.objects where Name = 'DF__Cmimorja__DataER__0638D371')
BEGIN
	ALTER TABLE dbo.Cmimorja
	DROP CONSTRAINT [DF__Cmimorja__DataER__0638D371]
END
Go --

IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='DataERegjistrimit')
BEGIN
ALTER TABLE dbo.Cmimorja drop column DataERegjistrimit
End
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.table_constraints 
    WHERE CONSTRAINT_NAME ='Kontrollo_EkstraRabati_0_99')
Begin
ALTER TABLE dbo.Cmimorja DROP CONSTRAINT Kontrollo_EkstraRabati_0_99
End
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.table_constraints 
    WHERE CONSTRAINT_NAME ='CK_KontrolloQmiminFurnizuesMeIMadhSeZero')
Begin
ALTER TABLE dbo.Cmimorja DROP CONSTRAINT CK_KontrolloQmiminFurnizuesMeIMadhSeZero
End
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.table_constraints 
    WHERE CONSTRAINT_NAME ='Kontrollo_Rabati_0_99')
Begin
ALTER TABLE dbo.Cmimorja DROP CONSTRAINT Kontrollo_Rabati_0_99
END
Go --
if exists(SELECT
    top 1 1
    FROM INFORMATION_SCHEMA.table_constraints 
    WHERE CONSTRAINT_NAME ='Kontrollo_ShpenzimetVarese_MaEMadheSeZero')
Begin
ALTER TABLE dbo.Cmimorja DROP CONSTRAINT Kontrollo_ShpenzimetVarese_MaEMadheSeZero
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='EkstraRabati')
BEGIN
ALTER TABLE dbo.Cmimorja drop column EkstraRabati
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='KostoMesatare')
BEGIN
ALTER TABLE dbo.Cmimorja drop column KostoMesatare
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='NjesiaId')
BEGIN
ALTER TABLE dbo.Cmimorja drop column NjesiaId
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='QmimiFurnizues')
BEGIN
ALTER TABLE dbo.Cmimorja drop column QmimiFurnizues
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='Rabati')
BEGIN
ALTER TABLE dbo.Cmimorja drop column Rabati
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='ShpenzimetVarese')
BEGIN
ALTER TABLE dbo.Cmimorja drop column ShpenzimetVarese
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='StokuMaksimal')
BEGIN
ALTER TABLE dbo.Cmimorja drop column StokuMaksimal
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='StokuMinimal')
BEGIN
ALTER TABLE dbo.Cmimorja drop column StokuMinimal
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='StokuOptimal')
BEGIN
ALTER TABLE dbo.Cmimorja drop column StokuOptimal
End
Go --
IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='Zona')
BEGIN
ALTER TABLE dbo.Cmimorja drop column Zona
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='ZbritjaAktive')
BEGIN
ALTER TABLE dbo.Cmimorja ADD [ZbritjaAktive]      DECIMAL (18, 3) NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='CmimiPermanentPakice')
BEGIN
ALTER TABLE dbo.Cmimorja ADD [CmimiPermanentPakice]      DECIMAL (18, 3) NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Cmimorja') AND name='CmimiPermanentShumice')
BEGIN
ALTER TABLE dbo.Cmimorja ADD [CmimiPermanentShumice]      DECIMAL (18, 3) NULL
End
Go --
IF EXISTS (SELECT 1 FROM SYS.key_constraints	WHERE name = 'UQ_DaljaMallitVaucherat_KodiVaucherit')
begin
		  ALTER TABLE [dbo].[DaljaMallitVaucherat] DROP CONSTRAINT [UQ_DaljaMallitVaucherat_KodiVaucherit] 
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallitVaucherat') AND name='KuponatPerZbritjeLlojetEZbritjesId')
BEGIN
ALTER TABLE dbo.DaljaMallitVaucherat ADD [KuponatPerZbritjeLlojetEZbritjesId] INT NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Subjektet') AND name='GjejKilometratOnline')
BEGIN
ALTER TABLE dbo.Subjektet ADD GjejKilometratOnline BIT DEFAULT((0)) NOT NULL
End
Go --
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='TouchScreen')
BEGIN
ALTER TABLE Arkat ADD TouchScreen BIT DEFAULT 0
End
Go --
IF NOT EXISTS (select 1 from sys.columns where object_id = object_id ('Konfigurimet') and Name='Vlera')
Begin
ALTER TABLE Konfigurimet
ADD Vlera VARCHAR(200) NULL
End
ELSE
Begin
ALTER TABLE Konfigurimet
alter column Vlera VARCHAR(200) NULL
End
Go --

IF EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Barkodat') AND name='Barkodi')
BEGIN
ALTER TABLE dbo.Barkodat 
ALTER COLUMN [Barkodi] [VARCHAR](30) NOT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Mxh_Filialet') AND name='NrTerminalit')
BEGIN
ALTER TABLE dbo.Mxh_Filialet ADD [NrTerminalit]  [VARCHAR](255) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSKuponatPerZbritje') AND name='VLFMeEVogelApliko')
BEGIN
ALTER TABLE dbo.POSKuponatPerZbritje ADD VLFMeEVogelApliko BIT DEFAULT(1) NOT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSKuponatPerZbritje') AND name='VleraFatures')
BEGIN
ALTER TABLE dbo.POSKuponatPerZbritje ADD VleraFatures DECIMAL(18, 2) NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('POSKuponatPerZbritje') AND name='VleraFatures')
BEGIN
ALTER TABLE dbo.POSKuponatPerZbritje ADD CONSTRAINT [FK_POSKuponatPerZbritje_KuponatPerZbritjeLlojetEZbritjesId] FOREIGN KEY ([KuponatPerZbritjeLlojetEZbritjesId]) REFERENCES [dbo].[POSKuponatPerZbritjeLlojetEZbritjes] ([Id])
ALTER TABLE dbo.POSKuponatPerZbritje ADD CONSTRAINT [FK_POSKuponatPerZbritje_RegjistruarNga] FOREIGN KEY ([RegjistruarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id])
End
Go --

alter table Konfigurimet alter column [Vlera] [VARCHAR](500)  NULL
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='AplikacioniId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD [AplikacioniId] [int]
END

Go --


IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('DaljaMallit') AND name='KthimiMallitArsyejaId')
BEGIN
ALTER TABLE dbo.DaljaMallit ADD KthimiMallitArsyejaId INT
End
Go --
 
IF NOT EXISTS(select 1 from sys.tables where Name = 'KthimiMallitArsyet') 
BEGIN
CREATE TABLE [dbo].[KthimiMallitArsyet]
(
	[Id] [int] NOT NULL ,
 	[Pershkrimi] [varchar] (100) ,
	CONSTRAINT [PK_KthimiMallitArsyet_Id] PRIMARY KEY CLUSTERED ([Id])
	)
END
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='HapPortinNjeHere')
BEGIN
ALTER TABLE dbo.Arkat ADD HapPortinNjeHere BIT NULL
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='LejoNdryshiminESasise')
BEGIN
ALTER TABLE dbo.Arkat ADD LejoNdryshiminESasise bit default 0 not null
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='LejoFshirjenEArtikujve')
BEGIN
ALTER TABLE dbo.Arkat ADD LejoFshirjenEArtikujve bit default 0 not null
End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallitKushteEBleresitArtikujt') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[DaljaMallitKushteEBleresitArtikujt] (
    [Id]            INT             NOT NULL,
    [SubjektiId]    INT             NOT NULL,
    [ArtikulliId]   INT             NOT NULL,
    [Rabati]        DECIMAL (18, 2) NOT NULL,
    [EkstraRabati]  DECIMAL (18, 2) NOT NULL,
    [Qmimi]         DECIMAL (18, 3) NOT NULL,
    [Marzha]        DECIMAL (18, 6) CONSTRAINT [DF_DaljaMallitKushteEBleresitArtikujt_Marzha] DEFAULT ((0.00)) NULL,
    [ZbritjaNeQmim] DECIMAL (18, 2) CONSTRAINT [DF_DaljaMallitKushteEBleresitArtikujt_ZbritjaNeQmim] DEFAULT ((0.00)) NULL,
    [InsertedDate]  DATETIME        CONSTRAINT [DF_DaljaMallitKushteEBleresitArtikujt_InsertedDate] DEFAULT (getdate()) NULL,
	[DataERegjistrimit]       DATETIME        CONSTRAINT [DF_DaljaMallitKushteEBleresitArtikujt_DataERegjistrimit] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DaljaMallitKushteEBleresitArtikujt_SubjektiId_ArtikulliId] PRIMARY KEY CLUSTERED ([SubjektiId] ASC, [ArtikulliId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [CK_KontrolloEkstraRabati_DaljaMallitKushteEBleresitArtikujt] CHECK ([EkstraRabati]>=(0) AND [EkstraRabati]<=(99.99)),
    CONSTRAINT [CK_KontrolloQmimin_DaljaMallitKushteEBleresitArtikujt] CHECK ([Qmimi]>=(0)),
    CONSTRAINT [CK_KontrolloRabati_DaljaMallitKushteEBleresitArtikujt] CHECK ([Rabati]>=(0) AND [Rabati]<=(99.99)),
    CONSTRAINT [FK_DaljaMallitKushteEBleresitArtikujt_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id]),
    CONSTRAINT [FK_DaljaMallitKushteEBleresitArtikujt_SubjektiId] FOREIGN KEY ([SubjektiId]) REFERENCES [dbo].[Subjektet] ([Id])
)
End
Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallitKushteEBleresit') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[DaljaMallitKushteEBleresit] (
    [Id]                    INT             NOT NULL,
    [SubjektiId]            INT             NOT NULL,
    [Rabati]                DECIMAL (18, 2) CONSTRAINT [DF_DaljaMallitKushteEBleresit_Rabati] DEFAULT ((0)) NOT NULL,
    [EkstraRabati]          DECIMAL (18, 2) CONSTRAINT [DF_DaljaMallitKushteEBleresit_EkstraRabati] DEFAULT ((0)) NOT NULL,
    [LimitiObligimit]       DECIMAL (18, 2) CONSTRAINT [DF_DaljaMallitKushteEBleresit_LimitiObligimit] DEFAULT ((0)) NOT NULL,
    [DataPrej]              DATE            NOT NULL,
    [DataDeri]              DATE            NOT NULL,
    [NrDokumentit]          INT             NOT NULL,
    [DataRegjistrimit]      DATETIME        CONSTRAINT [DF_DaljaMallitKushteEBleresit_DataRegjistrimit] DEFAULT (getdate()) NOT NULL,
    [RegjistruarNga]        INT             NOT NULL,
    [AfatiPageses]          INT             CONSTRAINT [DF_DaljaMallitKushteEBleresit_AfatiPageses] DEFAULT ((0)) NOT NULL,
    [KategoriaID]           INT             NOT NULL,
    [Marzha]                DECIMAL (18, 6) CONSTRAINT [DF_DaljaMallitKushteEBleresit_Marzha] DEFAULT ((0.00)) NULL,    
    [AplikoCmimetEShumices] BIT             CONSTRAINT [DF_DaljaMallitKushteEBleresit_AplikoCmimetEShumices] DEFAULT ((0)) NOT NULL,
    [VleresimiSubjektitId]  INT             NULL,
    [Klasifikatori]         VARCHAR (50)    NULL,
    [KlasifikatoriDate]     DATETIME        NULL,
    CONSTRAINT [PK_DaljaMallitKushteEBleresit_SubjektiId] PRIMARY KEY CLUSTERED ([SubjektiId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [CK_KontrolloEkstraRabati_DaljaMallitKushteEBleresit] CHECK ([EkstraRabati]>=(0) AND [EkstraRabati]<=(99.99)),
    CONSTRAINT [CK_KontrolloLimiti1_DaljaMallitKushteEBleresit] CHECK ([LimitiObligimit]>=(0)),
    CONSTRAINT [CK_KontrolloRabati_DaljaMallitKushteEBleresit] CHECK ([Rabati]>=(0) AND [Rabati]<=(99.99)),
    CONSTRAINT [FK_DaljaMallitKushteEBleresit_RegjistruarNga] FOREIGN KEY ([RegjistruarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id]),
    CONSTRAINT [FK_DaljaMallitKushteEBleresit_SubjektiId] FOREIGN KEY ([SubjektiId]) REFERENCES [dbo].[Subjektet] ([Id]),
)
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('MenyratEPageses') AND name='PagesMeBonus')
BEGIN
ALTER TABLE dbo.MenyratEPageses ADD [PagesMeBonus]       BIT             CONSTRAINT [DF_MenyratEPageses_PagesMeBonus] DEFAULT ((0)) NOT NULL
End 
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='TransferetEnable')
BEGIN
ALTER TABLE dbo.Arkat ADD TransferetEnable bit default 0 not null
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='ShperblimetEnable')
BEGIN
ALTER TABLE dbo.Arkat ADD ShperblimetEnable bit default 0 not null
End
Go --

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE OBJECT_ID=OBJECT_ID('Arkat') AND name='SubjektiDetaleEnable')
BEGIN
ALTER TABLE dbo.Arkat ADD SubjektiDetaleEnable bit default 0 not null
End
Go --
