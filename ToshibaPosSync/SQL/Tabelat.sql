	  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Tatimet') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[Tatimet]
(
      [Id] [TINYINT] NOT NULL
    , [Pershkrimi] [VARCHAR](50)  NOT NULL
    , [Vlera] [DECIMAL](18,2) NOT NULL
    , [Statusi] [BIT] NOT NULL CONSTRAINT [DF_Tatimet_Statusi] DEFAULT ((0))
    , [Kategoria] [VARCHAR](2)  NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Tatimet__DataERe__08AB2BC8] DEFAULT (getdate())  
				, CONSTRAINT [PK_Tatimet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'SubjektiLloji') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[SubjektiLloji] (
    [Id]                        INT          NOT NULL,
    [Shkurtesa]                 VARCHAR (5)  NOT NULL,
    [Pershkrimi]                VARCHAR (50) NOT NULL,
    [DataERegjistrimit]         DATETIME     DEFAULT (getdate()) NOT NULL,
    [ValidoPraniminETransferit] BIT          NULL,
    CONSTRAINT [PK_SubjektiLloji_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End
     Go --
	 
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Brendet') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Brendet]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](30)  NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Brendet__DataERe__6B84DD35] DEFAULT (getdate())  
				, CONSTRAINT [PK_Brendet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'LlojetEArtikullit') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[LlojetEArtikullit]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](70)  NOT NULL
    , [Statusi] [BIT] NOT NULL CONSTRAINT [DF_LlojetEArtikullit_Statusi] DEFAULT ((1))
    , [LejonStokunNegative] [BIT] NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__LlojetEAr__DataE__7E62A77F] DEFAULT (getdate())  
				, CONSTRAINT [PK_LlojetEArtikullit_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Njesit') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Njesit]
(
      [Id] [TINYINT] NOT NULL
    , [Pershkrimi] [VARCHAR](50)  NOT NULL
    , [Njesia] [VARCHAR](10)  NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Njesit__DataEReg__135DC465] DEFAULT (getdate())  
				, CONSTRAINT [PK_Njesit_Id] PRIMARY KEY CLUSTERED ([Id])
)
             End	
    Go -- 

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Artikujt') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Artikujt]
(
      [Id] [INT] NOT NULL
    , [Shifra] [VARCHAR](15)  NOT NULL
    , [BrendId] [INT] NULL
    , [Pershkrimi] [VARCHAR](500)  NOT NULL
    , [PershkrimiTiketa] [VARCHAR](25)  NOT NULL
	, [PershkrimiFiskal] [VARCHAR](500)  NOT NULL
    , [LlojiIArtikullitId] [INT] NOT NULL
    , [NjesiaID] [TINYINT] NOT NULL
    , [TatimetID] [TINYINT] NOT NULL
    , [GrupiMallitID] [BIGINT] NOT NULL
    , [DoganaID] [TINYINT] NULL
    , [Pesha] [DECIMAL](18,3) NOT NULL
    , [Lartesia] [DECIMAL](18,2) NOT NULL
    , [Gjersia] [DECIMAL](18,2) NOT NULL
    , [Gjatesia] [DECIMAL](18,2) NOT NULL
    , [Paketimi] [DECIMAL](18,3) NOT NULL
    , [Statusi] [BIT] NOT NULL
    , [SektoriId] [INT] NULL CONSTRAINT [DF__Artikujt__Sektor__492FC531] DEFAULT (NULL)
    , [ArtikulliPerbereId] [INT] NULL  
	, [ShifraProdhuesit]     VARCHAR (200)   NULL
	, [DataERegjistrimit]    DATETIME        CONSTRAINT [DF_Artikujt_DataERegjistrimit] DEFAULT (getdate()) NOT NULL
	, [Grupi] VARCHAR (400)   NULL,
	  [K1]                   INT             DEFAULT (NULL) NULL,
      [K2]                   INT             DEFAULT (NULL) NULL,
      [K3]                   INT             DEFAULT (NULL) NULL,
      [K4]                   INT             DEFAULT (NULL) NULL,
      [K5]                   INT             DEFAULT (NULL) NULL
				, CONSTRAINT [PK_Artikujt_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Subjektet') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Subjektet]
(
      [Id] [INT] NOT NULL
    , [Shifra] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](80)  NOT NULL
	, [PershkrimiShkurter] [VARCHAR](80)  NOT NULL
    , [NRB] [VARCHAR](200)  NULL
    , [NrTVSH] [VARCHAR](20)  NULL
    , [NumriFiskal] [VARCHAR](50)  NULL
    , [Email] [VARCHAR](500)  NULL
    , [Telefoni] [VARCHAR](200)  NULL
    , [Fax] [VARCHAR](200)  NULL
    , [Teleporosia] [VARCHAR](200)  NULL
    , [Adresa] [VARCHAR](200)  NULL
    , [VendiId] [INT] NULL
	, [Vendi] [VARCHAR](120) NULL 
    , [NumriPostar] [VARCHAR](30)  NULL
    , [Shteti] [VARCHAR](30)  NULL
    , [Komuna] [VARCHAR](30)  NULL
    , [PersoniKontaktues] [VARCHAR](60)  NULL
    , [LlojiISubjektitID] [INT] NOT NULL
	, [LlojiISubjektit] [VARCHAR](150) NULL
    , [Koment] [VARCHAR](2000)  NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_Subjektet_DataERegjistrimit] DEFAULT (getdate())
    , [RegjistruarNga] [INT] NOT NULL CONSTRAINT [DF_Subjektet_RegjistruarNga] DEFAULT (suser_sid())
    , [PranimiFaturaveEmail] [VARCHAR](100)  NULL
    , [FinancatEmail] [VARCHAR](100)  NULL
    , [Statusi] [BIT] NULL CONSTRAINT [DF_Subjektet_Statusi] DEFAULT ((1))
    , [MenaxhuesiSubjektitId] [INT] NULL
	, [MenaxheriPershrimi] [VARCHAR](100) NULL
	, [MenaxheriEmail] [VARCHAR](100) NULL
	, [MenaxheriSubjektitTel] [VARCHAR](100) NULL
    , [KategoriaId] [INT] NULL
    , [NrLicenses] [VARCHAR](100)  NULL
	, [DataESkadences] [DATETIME] NULL
    , [K16] [INT] NULL CONSTRAINT [DF__Subjektet__K16__7B5130AA] DEFAULT (NULL)
    , [K17] [INT] NULL CONSTRAINT [DF__Subjektet__K17__7C4554E3] DEFAULT (NULL)
    , [K18] [INT] NULL CONSTRAINT [DF__Subjektet__K18__7D39791C] DEFAULT (NULL)
    , [K19] [INT] NULL CONSTRAINT [DF__Subjektet__K19__7E2D9D55] DEFAULT (NULL)
    , [K20] [INT] NULL CONSTRAINT [DF__Subjektet__K20__7F21C18E] DEFAULT (NULL)
    , [MenyraEPagesesId] [INT] NULL
	, [MenyraEPageses] [VARCHAR](100) NULL
	, [AfatiPageses] [INT] NULL 
	, [AfatiPagesesShitje] [INT] NULL
	, [SubjektiEshteKompani] [BIT] NULL
	, [Shkurtesa] [VARCHAR](100) NULL
	, [KushtetEDergeses] [VARCHAR](2000) NULL
	, [ReferencaEKontrates] [VARCHAR](400) NULL
    , [PershkrimiWEB] [VARCHAR](100)  NULL  
				, CONSTRAINT [PK_Subjektet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Mxh_Filialet') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Mxh_Filialet]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](100)  NOT NULL
    , [LogoBardhEZi] [IMAGE] NULL
    , [Logo] [IMAGE] NULL
    , [OrganizataId] [INT] NOT NULL
    , [Tipi] [INT] NULL
    , [EmriServerit] [VARCHAR](50)  NULL
    , [EmriDatabazes] [VARCHAR](50)  NULL
    , [Statusi] [BIT] NULL
    , [FilialaVepruese] [BIT] NULL
    , [LinkServeri] [VARCHAR](50)  NULL
    , [Sinkronizohet] [BIT] NOT NULL CONSTRAINT [DF_Mxh_Filialet_Sinkronizohet] DEFAULT ((1))
    , [SinkronizohetNga] [INT] NULL
    , [Lloji] [CHAR](1)  NULL CONSTRAINT [CK_Filiala_DepoMarket] CHECK ([Lloji]='R' OR [Lloji]='D')
    , [MundesoTavolinatEHapura] [BIT] NULL
    , [StatusiProjektit] [BIT] NULL
    , [TvshPerProjekt] [DECIMAL](18,2) NOT NULL CONSTRAINT [DF__Mxh_Filia__TvshP__03275C9C] DEFAULT ((0)) CONSTRAINT [CK_Filiala_KontrolloTVSH] CHECK ([TvshPerProjekt]>=(0.00) OR [TvshPerProjekt]<=(100))
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Mxh_Filia__DataE__041B80D5] DEFAULT (getdate())
    , [KaCmime] [BIT] NOT NULL CONSTRAINT [DF__Mxh_Filia__KaCmi__050FA50E] DEFAULT ((1))
    , [PrefixNeNrTeFatures] [VARCHAR](50)  NULL
    , [Renditja] [INT] NULL
    , [PershkrimiShkurter] [VARCHAR](25)  NULL
    , [LogoWEB] [IMAGE] NULL
    , [SMTP] [VARCHAR](50)  NULL
    , [Password] [VARCHAR](50)  NULL
    , [PORT] [VARCHAR](50)  NULL
    , [FromEmail] [VARCHAR](50)  NULL  
				, CONSTRAINT [PK_Mxh_Filialet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Mxh_GrupetEShfrytezuesve') AND type in (N'U')) 
			  begin
			  			  CREATE TABLE [dbo].[Mxh_GrupetEShfrytezuesve]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](50)  NOT NULL
    , [Tipi] [VARCHAR](10)  NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Mxh_Grupe__DataE__0603C947] DEFAULT (getdate())  
				, CONSTRAINT [PK_Mxh_GrupetEShfrytezuesve_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Mxh_Operatoret') AND type in (N'U')) 
			  begin
 CREATE TABLE [dbo].[Mxh_Operatoret] (
    [Id]                    INT           NOT NULL,
    [Emri]                  VARCHAR (50)  NOT NULL,
    [Mbiemri]               VARCHAR (50)  NOT NULL,
    [GrupiId]               INT           NOT NULL,
    [Operatori]             VARCHAR (100) NULL,
    [Statusi]               BIT           NOT NULL,
    [OrganizataId]          INT           NOT NULL,
    [Pass]                  VARCHAR (100) NULL,
    [DataEKrijimit]         DATETIME      NOT NULL,
    [Email]                 VARCHAR (100) CONSTRAINT [DF_Mxh_Operatoret_Email] DEFAULT ('X') NULL,
    [FjalekalimiPerZbritje] VARCHAR (100) NULL,
    [ShifraOperatorit]      VARCHAR (100) NULL,
    [DataERegjistrimit]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [HapjaEDokumenteve]     BIT           DEFAULT ((0)) NOT NULL,
    [SektoriId]             INT           NULL,
    [ShfaqNeWeb]            BIT           DEFAULT ((0)) NULL,
    [SubjektiID]            INT           NULL,
    [MenaxhimiKolonave]     BIT           DEFAULT ((0)) NOT NULL,
	[Tel]                   VARCHAR (50)  NULL,
    CONSTRAINT [PK_Mxh_Operatoret_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90) 
)
End
Go --
		  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'ArtikujtMeLirim') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[ArtikujtMeLirim]
(
      [Id] [INT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [Sasia] [DECIMAL](18,3) NOT NULL CONSTRAINT [CK__ArtikujtM__Sasia__6FDF7DFE] CHECK ([Sasia]<(1000) AND [Sasia]>(0))
    , [Zbritja] [DECIMAL](18,3) NOT NULL CONSTRAINT [CK__ArtikujtM__Zbrit__71C7C670] CHECK ([Zbritja]<(100) AND [Zbritja]>(0))
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_ArtikujtMeLirim_DataERegjistrimit] DEFAULT (getdate())
    , [RegjistruarNga] [INT] NOT NULL  
				, CONSTRAINT [PK_ArtikujtMeLirim_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Barkodat') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Barkodat]
(
      [Id] [INT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [Barkodi] [VARCHAR](20)  NOT NULL
    , [SasiaPako] [SMALLINT] NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Barkodat__DataER__60132A89] DEFAULT (getdate())  
				, CONSTRAINT [PK_Barkodat_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'StandardetEBarkodave') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[StandardetEBarkodave]
(
      [Id] [INT] NOT NULL
    , [Tipi] [VARCHAR](20)  NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Standarde__DataE__77809FC6] DEFAULT (getdate())  
				, CONSTRAINT [PK_StandardetEBarkodave_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Peshoret') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Peshoret]
(
      [Id] [INT] NOT NULL
    , [Shifra] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](70)  NOT NULL
    , [Statusi] [BIT] NOT NULL CONSTRAINT [DF_Peshoret_Statusi] DEFAULT ((1))
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Peshoret__DataER__2E11BAA1] DEFAULT (getdate())  
				, CONSTRAINT [PK_Peshoret_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'PLUPeshoret') AND type in (N'U')) 
			  begin
			 CREATE TABLE [dbo].[PLUPeshoret]
(
      [Id] [BIGINT] NOT NULL
    , [PeshoretId] [INT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [PLU] varchar(50)
    , [AfatiDite] [INT] NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__PLUPeshor__DataE__30EE274C] DEFAULT (getdate())  
				, CONSTRAINT [PK_PLUPeshoret_ArtikulliId] PRIMARY KEY CLUSTERED ([ArtikulliId])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Cmimorja') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Cmimorja]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [OrganizataId] [INT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [NjesiaId] [TINYINT] NOT NULL
    , [Stoku] [DECIMAL](18,3) NOT NULL
    , [QmimiFurnizues] [DECIMAL](18,6) NOT NULL CONSTRAINT [CK_KontrolloQmiminFurnizuesMeIMadhSeZero] CHECK ([QmimiFurnizues]>(0))
    , [Rabati] [DECIMAL](18,2) NOT NULL CONSTRAINT [Kontrollo_Rabati_0_99] CHECK ([Rabati]>=(0) AND [Rabati]<(100))
    , [EkstraRabati] [DECIMAL](18,2) NOT NULL CONSTRAINT [Kontrollo_EkstraRabati_0_99] CHECK ([EkstraRabati]>=(0) AND [EkstraRabati]<(100))
    , [BazaFurnizuese] AS (([QmimiFurnizues]*((1)-[Rabati]/(100)))*((1)-[EkstraRabati]/(100)))
    , [ShpenzimetVarese] [DECIMAL](18,6) NOT NULL CONSTRAINT [Kontrollo_ShpenzimetVarese_MaEMadheSeZero] CHECK ([ShpenzimetVarese]>=(0))
    , [Kosto] AS ((([QmimiFurnizues]*((1)-[Rabati]/(100)))*((1)-[EkstraRabati]/(100)))*((1)+[ShpenzimetVarese]/(100)))
    , [Tvsh] [DECIMAL](18,2) NOT NULL CONSTRAINT [Kontrollo_Tvsh_0_99] CHECK ([Tvsh]>=(0) AND [Tvsh]<(100))
    , [QmimiIShitjes] [DECIMAL](18,3) NOT NULL
    , [KostoMesatare] [DECIMAL](18,6) NOT NULL
    , [StatusiQmimitId] [INT] NULL CONSTRAINT [DF_Cmimorja_StatusiQmimitId] DEFAULT ((10))
    , [Mazha] AS ((([QmimiIShitjes]/((1)+[Tvsh]/(100))-(([QmimiFurnizues]*((1)-[Rabati]/(100)))*((1)-[EkstraRabati]/(100)))*((1)+[ShpenzimetVarese]/(100)))/((([QmimiFurnizues]*((1)-[Rabati]/(100)))*((1)-[EkstraRabati]/(100)))*((1)+[ShpenzimetVarese]/(100))))*(100))
    , [QmimiShumice] [DECIMAL](18,3) NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Cmimorja__DataER__0638D371] DEFAULT (getdate())
    , [Zona] [VARCHAR](200)  NULL
    , [StokuMinimal] [DECIMAL](18,2) NULL
    , [StokuOptimal] [DECIMAL](18,2) NULL
    , [StokuMaksimal] [DECIMAL](18,2) NULL  
	, CONSTRAINT [PK_Cmimorja_ArtikulliId] PRIMARY KEY CLUSTERED ([OrganizataId], [ArtikulliId])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'MenyratEPageses') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[MenyratEPageses]
(
      [Id] [INT] NOT NULL
    , [Shkurtesa] [VARCHAR](15)  NOT NULL
    , [Pershkrimi] [VARCHAR](40)  NOT NULL
    , [Provizioni] [DECIMAL](18,2) NULL
    , [Tipi] [VARCHAR](20)  NULL
    , [Renditja] [INT] NULL
    , [PershkrimiAnglisht] [VARCHAR](40)  NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__MenyratEP__DataE__013F142A] DEFAULT (getdate())
	, [ParaqitetNePos]    BIT              NOT NULL DEFAULT ((1))
	, CONSTRAINT [PK_MenyratEPageses_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'LlojetEDokumenteve') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[LlojetEDokumenteve]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](80)  NOT NULL
    , [Tipi] [VARCHAR](20)  NOT NULL
    , [PrindiID] [INT] NULL
    , [Shkurtesa] [VARCHAR](6)  NULL
    , [Shenja] [INT] NULL CONSTRAINT [CK_KontrolloShenjenMatematikorePozitiveOseNegative] CHECK ([Shenja]=(1) OR [Shenja]=(-1))
    , [DokumentIJashtem] [BIT] NULL
    , [Tatimi] [DECIMAL](18,2) NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [LlojetEDokumenteve_DataERegjistrimit_GetDate] DEFAULT (getdate())
    , [TrackingTipi] [VARCHAR](30)  NULL
    , [KaKurs] [BIT] NOT NULL CONSTRAINT [LlojetEDokumenteve_KaKurs_False] DEFAULT ((0))  
				, CONSTRAINT [PK_LlojetEDokumenteve_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Departamentet') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[Departamentet] (
    [Id]                INT          NOT NULL,
    [Pershkrimi]        VARCHAR (50) NOT NULL,
    [Plani]             IMAGE        NULL,
    [DataERegjistrimit] DATETIME     DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Departamentet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
			  End
     Go --		


	 IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'CCPLlojetEKartelave') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[CCPLlojetEKartelave] (
    [Id]                INT          NOT NULL,
    [Pershkrimi]        VARCHAR (50) NOT NULL,
    [TipiKarteles]      VARCHAR (30) NOT NULL,
    [Statusi]           BIT          DEFAULT ((1)) NOT NULL,
    [DataERegjistrimit] DATETIME     DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_CCPLlojetEKartelave_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
    
)
			  End
     Go --	

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallit') AND type in (N'U')) 
			  begin
			 CREATE TABLE [dbo].[DaljaMallit]
(
      [Id] [BIGINT] NOT NULL
    , [OrganizataId] [INT] NOT NULL
    , [Viti] [INT] NOT NULL
    , [Data] [DATE] NOT NULL CONSTRAINT [DF_DaljaMallit_Data] DEFAULT (getdate())
    , [NrFatures] [INT] NOT NULL
    , [DokumentiId] [INT] NOT NULL
    , [RegjistruarNga] [INT] NOT NULL CONSTRAINT [DF_DaljaMallit_RegjistruarNga] DEFAULT (suser_sid())
    , [NumriArkes] [INT] NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_DaljaMallit_DataERegjistrimit] DEFAULT (getdate())
    , [SubjektiId] [INT] NULL
    , [ShitjeEPerjashtuar] [BIT] NOT NULL CONSTRAINT [DF_DaljaMallit_ShitjeEPerjashtuar] DEFAULT ((0))
    , [Koment] [VARCHAR](500)  NULL
    , [Xhirollogari] [BIT] NULL CONSTRAINT [DF_DaljaMallit_Xhirollogari] DEFAULT ((0))
    , [Sinkronizuar] [BIT] NOT NULL CONSTRAINT [DF_DaljaMallit_BartjaFin] DEFAULT ((0))
    , [NeTransfer] [BIT] NOT NULL CONSTRAINT [DF_DaljaMallit_NeTransfer] DEFAULT ((0))
    , [DepartamentiId] [INT] NOT NULL
    , [Validuar] [BIT] NULL
    , [AfatiPageses] [INT] NOT NULL CONSTRAINT [DF_DaljaMallit_AfatiPageses] DEFAULT ((0))
    , [DaljaMallitKorrektuarId] [BIGINT] NULL
    , [TavolinaId] [INT] NULL
    , [NrDuditX3] [NVARCHAR](50)  NULL
    , [DataFatures] [DATE] NULL
    , [RaportDoganor] [BIT] NOT NULL CONSTRAINT [DF__DaljaMall__Rapor__0FC23DAB] DEFAULT ((1))
    , [Kursi] [DECIMAL](18,8) NOT NULL CONSTRAINT [DF_DaljaMallit_ValutaDefault_1] DEFAULT ((1))
    , [ValutaId] [INT] NULL
    , [KuponiFiskalShtypur] [BIT] NOT NULL CONSTRAINT [DF__DaljaMall__Kupon__11AA861D] DEFAULT ((0))
    , [K6] [INT] NULL
    , [K7] [INT] NULL
    , [K8] [INT] NULL
    , [K9] [INT] NULL
    , [K10] [INT] NULL
    , [DataValidimit] [DATE] NULL
    , [DaljaMallitImportuarId] [VARCHAR](50)  NULL
    , [IdLokal] [BIGINT] NULL unique
    , [FaturaKomulativeId] [BIGINT] NULL
    , [TrackingId] [INT] NULL
    , [NumriFaturesManual] [VARCHAR](50)  NULL
    , [ZbritjeNgaOperatori] [INT] NULL  
				, CONSTRAINT [PK_DaljaMallit_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallitDetale') AND type in (N'U')) 
			  begin
			 CREATE TABLE [dbo].[DaljaMallitDetale]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [DaljaMallitID] [BIGINT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [NjesiaID] [TINYINT] NOT NULL
    , [NR] [SMALLINT] NOT NULL
    , [Sasia] [DECIMAL](18,3) NOT NULL CONSTRAINT [CK_DaljaMallitDetale_Sasia] CHECK ([Sasia]<>(0))
    , [BazaFurnizuese] [DECIMAL](18,6) NOT NULL
    , [Kostoja] [DECIMAL](18,6) NOT NULL
    , [KostoMesatare] [DECIMAL](18,6) NOT NULL
    , [Tvsh] [DECIMAL](18,2) NOT NULL
    , [QmimiShitjes] [DECIMAL](18,8) NOT NULL
    , [Rabati] [DECIMAL](18,2) NOT NULL CONSTRAINT [DF_DaljaMallitDetale_Rabati] DEFAULT ((0)) CONSTRAINT [CK_DaljaMallitDetale_Rabati] CHECK ([Rabati]>=(0) OR [Rabati]<=(100))
    , [EkstraRabati] [DECIMAL](18,2) NOT NULL CONSTRAINT [DF_DaljaMallitDetale_EkstraRabati] DEFAULT ((0))
    , [Stoku] [DECIMAL](18,3) NOT NULL
    , [StatusiArtikullit] [TINYINT] NULL
    , [HyrjaDetaleId] [INT] NULL
    , [ArtikujtEProdhuarId] [INT] NULL
    , [QmimiRregullt] [DECIMAL](18,4) NOT NULL CONSTRAINT [DF_DaljaMallitDetale_QmimiRregullt] DEFAULT ((0.00))
    , [QmimiShumice] [DECIMAL](18,4) NOT NULL CONSTRAINT [DF_DaljaMallitDetale_QmimiShumice] DEFAULT ((0.00))
    , [ProjektiLinjatId] [INT] NULL
    , [DaljaMallitDetaleKthimiId] [INT] NULL
	, [KujtesaOrderId] [INT] NULL
				, CONSTRAINT [PK_DaljaMallitDetale_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallitDetaleHistori') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[DaljaMallitDetaleHistori]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [DaljaMallitID] [BIGINT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [NjesiaID] [TINYINT] NOT NULL
    , [NR] [SMALLINT] NOT NULL
    , [Sasia] [DECIMAL](18,3) NOT NULL
    , [BazaFurnizuese] [DECIMAL](18,6) NOT NULL
    , [Kostoja] [DECIMAL](18,6) NOT NULL
    , [KostoMesatare] [DECIMAL](18,6) NOT NULL
    , [Tvsh] [DECIMAL](18,2) NOT NULL
    , [QmimiShitjes] [DECIMAL](18,2) NOT NULL
    , [Rabati] [DECIMAL](18,2) NOT NULL CONSTRAINT [DF_DaljaMallitDetaleHistori_Rabati] DEFAULT ((0))
    , [EkstraRabati] [DECIMAL](18,2) NOT NULL
    , [Stoku] [DECIMAL](18,3) NOT NULL  
				, CONSTRAINT [PK_DaljaMallitDetaleHistori_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'EkzekutimiPageses') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[EkzekutimiPageses]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [MenyraEPagesesId] [INT] NOT NULL
    , [DaljaMallitID] [BIGINT] NOT NULL
    , [Vlera] [DECIMAL](18,2) NOT NULL
    , [Paguar] [DECIMAL](18,2) NOT NULL
    , [ShifraOperatorit] [INT] NULL  
	, [DhenjeKesh] [INT] NULL  
				, CONSTRAINT [PK_EkzekutimiPageses_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
		  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'BleresitMeKartela') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[BleresitMeKartela]
(
      [Id] [INT] NOT NULL
    , [Kodi] [VARCHAR](50)  NOT NULL
    , [Zbritja] [DECIMAL](18,2) NOT NULL
    , [SubjektiId] [INT] NOT NULL
    , [Emri] [VARCHAR](50)  NOT NULL
    , [Mbiemri] [VARCHAR](50)  NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL
    , [RegjistruarNga] [INT] NOT NULL  
				, CONSTRAINT [PK_BleresitMeKartela_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'ArtikujtEPerber') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[ArtikujtEPerber]
(
      [Id] [INT] NOT NULL
    , [ArtikulliIPerberId] [INT] NOT NULL
    , [ArtikulliId] [INT] NOT NULL
    , [KoeficientiSasi] [NUMERIC] NOT NULL
    , [Lloji] [VARCHAR](20)  NOT NULL
    , [RegjistruarNga] [INT] NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_ArtikujtEPerber_DataERegjistrimit] DEFAULT (getdate())  
				, CONSTRAINT [PK_ArtikujtEPerber_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'BleresitMeKartelaDetale') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[BleresitMeKartelaDetale]
(
      [Id] [INT] NOT NULL
    , [SubjektiId] [INT] NOT NULL
    , [BleresitMeKartelaId] [INT] NOT NULL
    , [DaljaMallitId] [BIGINT] NOT NULL  
				, CONSTRAINT [PK_BleresitMeKartelaDetale_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Konfigurimet') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Konfigurimet]
(
      [Id] [INT] NOT NULL
    , [Pershkrimi] [VARCHAR](1000)  NOT NULL
    , [Statusi] [BIT] NOT NULL
    , [Tipi] [VARCHAR](100)  NULL
    , [Vlera] [VARCHAR](50)  NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Konfiguri__DataE__799DF262] DEFAULT (getdate())
    , [Mxh_ObjektetAplikacionId] [INT] NULL  
				, CONSTRAINT [PK_Konfigurimet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'KonfigurimetOrganizatat') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[KonfigurimetOrganizatat]
(
      [KonfigurimiId] [INT] NOT NULL
    , [OrganizataId] [INT] NOT NULL  
				, CONSTRAINT [PK_KonfigurimetOrganizatat_OrganizataId] PRIMARY KEY CLUSTERED ([KonfigurimiId], [OrganizataId])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'dbo.ArtikujtPerberes') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[ArtikujtPerberes]
(
      [Id] [INT] NOT NULL IDENTITY(10000,5)
    , [ArtikulliIPerberId] [INT] NOT NULL
    , [ArtikulliPerberesId] [INT] NOT NULL
    , [NjesiaId] [TINYINT] NOT NULL
    , [Sasia] [DECIMAL](18,9) NOT NULL
    , [RegjistruarNga] [INT] NOT NULL
    , [DataRegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_ArtikujtPerberes_DataRegjistrimit] DEFAULT (getdate())  
				, CONSTRAINT [PK_ArtikujtPerberes_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Mxh_OrganizataDetalet') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Mxh_OrganizataDetalet]
(
      [Id] [INT] NOT NULL
    , [Gjuha] [VARCHAR](2)  NOT NULL CONSTRAINT [CK__Mxh_Organ__Gjuha__719DA93E] CHECK ([Gjuha]='Sr' OR [Gjuha]='Al')
    , [Email] [VARCHAR](100)  NULL
    , [Smtp] [VARCHAR](50)  NULL
    , [Porti] [VARCHAR](50)  NULL
    , [UserName] [VARCHAR](50)  NULL
    , [Pass] [VARCHAR](200)  NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__Mxh_Organ__DataE__108157BA] DEFAULT (getdate())
    , [EmailDisplayName] [VARCHAR](100)  NULL  
				, CONSTRAINT [PK_Mxh_OrganizataDetalet_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'dbo.Valutat') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[Valutat]
(
      [Id] [INT] NOT NULL
    , [Shifra] [VARCHAR](5)  NOT NULL
    , [Pershkrimi] [VARCHAR](50)  NOT NULL
				, CONSTRAINT [PK_Valutat_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'CCPFaturat') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[CCPFaturat]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [CCPKompaniaId] [INT] NOT NULL
    , [DaljaMallitId] [BIGINT] NOT NULL
    , [CCPBonusiId] [INT] NULL
    , [DataRegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__CCPFatura__DataR__6D6D25A7] DEFAULT (getdate())
    , [RegjistruarNga] [INT] NOT NULL  
				, CONSTRAINT [PK_CCPFaturat_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'LokalDBKohaSinkronizimit') AND type in (N'U')) 
			  begin
			   CREATE TABLE [dbo].[LokalDBKohaSinkronizimit]
(
      [Data] [DATETIME] NOT NULL  
				, CONSTRAINT [PK_LokalDBKohaSinkronizimit_Data] PRIMARY KEY CLUSTERED ([Data])
)
			  End
     Go --
			 
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'CCPKompanite') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[CCPKompanite]
(
      [Id] [INT] NOT NULL
    , [EmriKompanise] [VARCHAR](80)  NOT NULL
    , [KodiKarteles] [VARCHAR](25)  NOT NULL CONSTRAINT [DF_CCPKompania_KodiKarteles] DEFAULT ('')
    , [ZbritjaBonus] [DECIMAL](18,2) NOT NULL CONSTRAINT [CK_CCPKompanite_ZbritjaBonus_0_100] CHECK ([ZbritjaBonus]>=(0) AND [ZbritjaBonus]<(100))
    , [ZbritjaNeArke] [DECIMAL](18,2) NOT NULL
    , [Statusi] [BIT] NOT NULL CONSTRAINT [DF__CCPKompan__Statu__6F556E19] DEFAULT ((1))
    , [RegjistruarNga] [INT] NOT NULL
    , [SubjektiId] [INT] NULL
    , [LlojiKartelesId] [INT] NOT NULL
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF__CCPKompan__DataE__70499252] DEFAULT (getdate())
    , [ArkaOperatoriId] [INT] NULL
    , [ZbritjaNeArkeVlere] [DECIMAL](18,2) NOT NULL CONSTRAINT [CK_CCPKompanite_ZbritjaPerNjesi_0] CHECK ([ZbritjaNeArkeVlere]>=(0))
    , [Vendi] [VARCHAR](80)  NULL
    , [Telefoni] [VARCHAR](30)  NULL
    , [Email] [VARCHAR](50)  NULL
    , [Komenti] [VARCHAR](200)  NULL
    , [PersoniAutorizuar] [VARCHAR](200)  NULL
    , [ZbritjaPerNjesi] [DECIMAL](18,2) NOT NULL CONSTRAINT [DF__CCPKompan__Zbrit__781667FA] DEFAULT ((0))  
	, [LlojiILimitit] VARCHAR(50) NULL 
    , [VleraELimitit] DECIMAL(18, 2) NULL 
	, [AplikoCmiminShumices] BIT NULL
	, CONSTRAINT [PK_CCPKompanite_Id] PRIMARY KEY CLUSTERED ([Id])
)

End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'BarazimiArkatareve') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[BarazimiArkatareve] (
    [Id]                BIGINT          NOT NULL,
    [OrganizataId]      INT             NOT NULL,
    [OperatoriId]       INT             NOT NULL,
    [NrDokumentit]      INT             NOT NULL,
    [DataEFillimit]     DATETIME        NOT NULL,
    [DataEPerfundimit]  DATETIME        NOT NULL,
    [BarazuarNga]       INT             NOT NULL,
    [DataERegjistrimit] DATETIME        NOT NULL,
    [Komenti]           VARCHAR (200)   NULL,
    [NderrimiMbyllur]   BIT             NOT NULL,
    [LlojiDokumentitId] INT             NOT NULL,
    [Barazuar]          BIT             DEFAULT ((0)) NOT NULL,
    [GjendjaFillestare] DECIMAL (18, 2) DEFAULT ((0.00)) NOT NULL,
    [XRaport]           DECIMAL (18, 2) NULL,
    [Data]              DATE            DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BarazimiArkatareve_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_BarazimiArkatareve_BarazuarNga] FOREIGN KEY ([BarazuarNga]) REFERENCES [dbo].[Mxh_Operatoret] ([Id]),
    CONSTRAINT [FK_BarazimiArkatareve_LlojiDokumentitId] FOREIGN KEY ([LlojiDokumentitId]) REFERENCES [dbo].[LlojetEDokumenteve] ([Id]),
    CONSTRAINT [FK_BarazimiArkatareve_OperatoriId] FOREIGN KEY ([OperatoriId]) REFERENCES [dbo].[Mxh_Operatoret] ([Id]),
    CONSTRAINT [FK_BarazimiArkatareve_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id])
)
End

Go --
			 		  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'POSZbritjaMeKupon') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[POSZbritjaMeKupon]
(
      [Id] [INT] NOT NULL IDENTITY(1,1)
    , [POSKuponatPerZbritjeId] [INT] NOT NULL
    , [Vlera] [DECIMAL](18,2) NOT NULL
    , [DaljaMallitId] [BIGINT] NOT NULL
)
			  End
     Go --
			  
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'ArtikujtMeLirimOrganizatat') AND type in (N'U')) 
			  begin
			  CREATE TABLE [dbo].[ArtikujtMeLirimOrganizatat]
(
      [Id] [INT] NOT NULL
    , [ArtikujtMeLirimId] [INT] NOT NULL
    , [OrganizataId] [INT] NOT NULL
    , [Statusi] [BIT] NOT NULL CONSTRAINT [DF__tmp_ms_xx__Statu__7C7108C3] DEFAULT ((0))
    , [DataERegjistrimit] [DATETIME] NOT NULL CONSTRAINT [DF_ArtikujtMeLirimOrganizatat_DataERegjistrimit] DEFAULT (getdate())  
				, CONSTRAINT [PK_ArtikujtMeLirimOrganizatat_Id] PRIMARY KEY CLUSTERED ([Id])
)
			  End

Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Bankat') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[Bankat] (
    [Id]          INT           NOT NULL,
    [EmriIBankes] VARCHAR (100) NOT NULL,
    [Shkurtesa]   VARCHAR (20)  NOT NULL,
    [SwiftCode]   VARCHAR (20)  NULL,
    [FinBankaId]  INT           NULL,
    [IBAN]        VARCHAR (100) NULL,
    [Renditja]    INT           NULL,
	[DataERegjistrimit] datetime not null default (getdate()),
    CONSTRAINT [PK_Bankat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90) ON [PRIMARY] 
)
			  End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'SubjektiBankat') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[SubjektiBankat] (
    [Id]         INT           NOT NULL,
    [SubjektiId] INT           NOT NULL,
    [BankaId]    INT           NOT NULL,
    [NrLlogaris] VARCHAR (25)  NOT NULL,
    [Koment]     VARCHAR (128) NULL,
    [DataERegjistrimit]  DATETIME     DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SubjektiBankat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_SubjektiBankat_BankaId] FOREIGN KEY ([BankaId]) REFERENCES [dbo].[Bankat] ([Id]),
    CONSTRAINT [FK_SubjektiBankat_SubjektiId] FOREIGN KEY ([SubjektiId]) REFERENCES [dbo].[Subjektet] ([Id]),
	CONSTRAINT [UQ_SubjektiId_BankaId_rLlogaris] unique ([SubjektiId],[BankaId],[NrLlogaris])
)
			  End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DaljaMallitKushtetEBleresitLlojiZbritjes') AND type in (N'U')) 
			  begin
					CREATE TABLE [dbo].[DaljaMallitKushtetEBleresitLlojiZbritjes] (
						[DaljaMallitDetaleId]  BIGINT        NULL, 
						[Zbritja]              VARCHAR (100) NOT NULL
					)
			  End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'ArtikujtPaZbritje') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[ArtikujtPaZbritje] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ArtikulliId] INT          NOT NULL,
    [Statusi]     BIT          DEFAULT ((0)) NOT NULL,
    [Tipi]        VARCHAR (20) DEFAULT ('Kubit') NOT NULL,
	[DataERegjistrimit]  DATETIME     DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ArtikujtPaZbritje_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 100),
    CONSTRAINT [FK_ArtikujtPaZbritje_ArtikulliId] FOREIGN KEY ([ArtikulliId]) REFERENCES [dbo].[Artikujt] ([Id])
)
			  End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'POS_PlaniZbritjeve') AND type in (N'U')) 
			  begin
	CREATE TABLE [dbo].[POS_PlaniZbritjeve] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [DataPrej]          DATE            NOT NULL,
    [DataDeri]          DATE            NOT NULL,
    [ZbritjaPerqindje]  DECIMAL (18, 2) NOT NULL,
    [RegjistruarNga]    INT             NOT NULL,
    [DataERegjistrimit] DATETIME        DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_POS_PlaniZbritjeve_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 100) 
)
			  End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Arkat') AND type in (N'U')) 
			  begin
				  CREATE TABLE [dbo].[Arkat] (
						[Id]                       INT          ,
						[OrganizataId]             INT          NOT NULL,
						[NrArkes]                  VARCHAR (3)  NOT NULL,
						[HostName]                 VARCHAR (80) NULL,
						[FreskimIPlote]            BIT          DEFAULT ((0)) NOT NULL,
						[FLinkCode]                VARCHAR (80) NULL,
						[PGMCode]                  VARCHAR (80) NULL,
						[NumriArkesGK]             VARCHAR (80) NULL,
						[VerzioniIArkes]           VARCHAR (50) NULL,
						[DataERegjistrimit]        DATETIME     DEFAULT (getdate()) NULL,
						[ShtypjaAutomatikeZRaport] BIT          DEFAULT ((0)) NULL,
						[KohaEShtypjesSeZRaportit] DATETIME     NULL,
						[LejoKerkiminmeEmer] BIT NOT NULL DEFAULT 0, 
						[AplikocmiminMeShumiceKurarrihetPaketimi] BIT NOT NULL DEFAULT 0, 
						[LejoStokunNegative] BIT NOT NULL DEFAULT 0, 
						[LejoZbritjenNeArke] BIT NOT NULL DEFAULT 0, 
						[LejoNDerrimineCmimit] BIT NOT NULL DEFAULT 0,  
						[ShtypKopjenEKuponitFiskal] BIT NOT NULL DEFAULT 0,  
						[KerkoPassWordPerAplikiminEZbritjes] BIT NOT NULL DEFAULT 0,
						[LejoRabatPerTeGjitheArtikujt] BIT NOT NULL DEFAULT 0,
						[LejoZbritjeNeTotalVler] BIT NOT NULL DEFAULT 0,
						[RegjimiPunesOffline] BIT NULL ,
						[IntervaliImportimitSekonda]              int          DEFAULT ((360)) NOT NULL,
						[IntervaliDergimitSekonda]                int          DEFAULT ((100)) NOT NULL,
						[KaTeDrejtTePunojOffline]				  Bit		   DEFAULT ((1)) NOT NULL,
						OperatoriAutomatikId					  int 
						PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 100),
						CONSTRAINT [FK_Arkat_OrganizataId] FOREIGN KEY ([OrganizataId]) REFERENCES [dbo].[Mxh_Filialet] ([Id]),
						CONSTRAINT [UQ_Arkat_NrArkes_OrganizataId] UNIQUE NONCLUSTERED ([OrganizataId] ASC, [NrArkes] ASC) WITH (FILLFACTOR = 100)
					)
			  End
Go -- 

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'DataBazeLokale') AND type in (N'U')) 
			  begin
	CREATE TABLE [dbo].[DataBazeLokale] (
    [Databaza] varchar(50)
)
End
Go --


IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'GrupetEMallrave') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[GrupetEMallrave] (
    [Id]                 BIGINT        NOT NULL,
    [Shifra]             VARCHAR (20)  NOT NULL,
    [Pershkrimi]         VARCHAR (200) NOT NULL,
    [Shkalla]            VARCHAR (20)  NULL,
    [Statusi]            BIT           CONSTRAINT [DF_GrupetEMallrave_Statusi] DEFAULT ((0)) NULL,
    [FushaIdentifikuese] VARCHAR (50)  NULL,
    [DataERegjistrimit]  DATETIME      DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_GrupetEMallrave_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End
Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'Klasifikatoret') AND type in (N'U')) 
			  begin
						CREATE TABLE [dbo].[Klasifikatoret] (
							[Id]               INT          NOT NULL,
							[Pershkrimi]       VARCHAR (50) NOT NULL,
							[PershkrimiKlient] VARCHAR (50) NOT NULL,
							[Tipi]             VARCHAR (50) NOT NULL,
							[Statusi]          BIT          DEFAULT ((0)) NULL,
							[Obligativ]        BIT          DEFAULT ((0)) NOT NULL,
							CONSTRAINT [PK_Klasifikatoret_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
						)
End
Go --

IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'RaportetFajllat') AND type in (N'U')) 
			  begin
CREATE TABLE [dbo].[RaportetFajllat] (
    [Id]                       INT             NOT NULL,
    [Raporti]                  VARBINARY (MAX) NOT NULL,
    [Freskuar]                 BIT             DEFAULT ((0)) NULL,
    [Pershkrimi]               VARCHAR (200)   NOT NULL,
    [EmriRaportit]             VARCHAR (200)   NULL
    CONSTRAINT [PK_RaportetFajllat_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
)
End
Go --
IF NOT EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'CCPSubjektetGrupet') AND type in (N'U')) 
			  begin
CREATE TABLE dbo.CCPSubjektetGrupet (
		[Id]            INT             NOT NULL,
		[CCPGrupiId]    INT             NOT NULL,
		[CCPSubjektiId] INT             NOT NULL,
		[LimitiSasi]    DECIMAL (18, 2) NOT NULL,
		[LimitiVlere]   DECIMAL (18, 2) NOT NULL,
		CONSTRAINT [PK_CCPSubjektetGrupet_Id] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 90)
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
