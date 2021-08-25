if not exists(
select 1 from sys.schemas where name ='TOSHIBA'
)
Begin 
 exec ('CREATE SCHEMA TOSHIBA AUTHORIZATION dbo')
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.Mxh_OperatoretQasjaSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.Mxh_OperatoretQasjaSelect_sp
			  End
Go --

CREATE PROCEDURE TOSHIBA.Mxh_OperatoretQasjaSelect_sp
(@Operatori varchar (100) =null,
@Pass varchar (100)=null
) 
as begin 
 
Select O.Id,Emri,Mbiemri,GrupiId,Operatori,Statusi
,(SELECT TOP 1 Id FROM dbo.Mxh_Filialet F WHERE Id=F.OrganizataId ) OrganizataId
,(Select Pershkrimi from Mxh_Filialet where Id=O.OrganizataId) PershkrimiOrganizates
,Email , G.Pershkrimi Grupi ,SubjektiID
,SektoriId
,case when g.Pershkrimi='Admin' then convert(bit,1) else convert(bit,0) end Admin
From Mxh_Operatoret O
inner join Mxh_GrupetEShfrytezuesve G on G.Id = O.GrupiId
where (Operatori=@Operatori) and  (O.ShifraOperatorit=@Pass) And Statusi=1
end
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.Mxh_OperatoretSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.Mxh_OperatoretSelect_sp
			  End
Go --
CREATE Procedure TOSHIBA.Mxh_OperatoretSelect_sp
(
@Id int  =null,
@Emri varchar (50) =null,
@Mbiemri varchar (50) =null,
@GrupiId int  =null,
@SektoriId int  =null,
@Operatori varchar (100) =null,
@Statusi bit  =null,
@OrganizataId int=null,
@Pass varchar (100)=null,
@FjalekalimiPerZbritje varchar(100) = NULL,
@RreshtiIIdPerSHfletim VARCHAR(2000) = NULL,
@ShifraOperatorit varchar(100) = NULL,
@HapjaEDokumenteve BIT = NULL,
@MenaxhimiKolonave BIT = NULL,
@ShfaqNeWeb BIT = NULL
) 
		
as begin 
 
Select M.Id, M.Emri, M.Mbiemri,M.GrupiId, G.Pershkrimi Grupi,M.Operatori,M.Statusi
,OrganizataId
,(Select Pershkrimi from Mxh_Filialet with(Nolock) where Id=M.OrganizataId) PershkrimiOrganizates
, Pass
,m.Email 
,FjalekalimiPerZbritje
,ShifraOperatorit
,HapjaEDokumenteve
,MenaxhimiKolonave,
M.Emri + ' ' + M.Mbiemri EmriMbiemri,
M.ShfaqNeWeb
,m.SubjektiID
,Sub.Pershkrimi Subjekti
,M.Tel
From dbo.Mxh_Operatoret  M					 with(Nolock)
inner join  Mxh_GrupetEShfrytezuesve G		 with(Nolock) ON G.Id = M.GrupiId 
LEFT OUTER JOIN Subjektet Sub				 with(Nolock) ON M.SubjektiID = Sub.Id
where  (M.Id=@Id or @Id is null ) 
and  (Emri=@Emri or @Emri is null ) 
and  (Mbiemri=@Mbiemri or @Mbiemri is null ) 
and  (GrupiId=@GrupiId or @GrupiId is null ) 
and  (Operatori=@Operatori or @Operatori is null ) 
and  (M.Statusi=@Statusi or @Statusi is null ) 
and  (OrganizataId=@OrganizataId or @OrganizataId is null) 
and  (Pass=@Pass or @Pass is null) 
and  (FjalekalimiPerZbritje=@FjalekalimiPerZbritje or @FjalekalimiPerZbritje is null) 
AND  (ShifraOperatorit=@ShifraOperatorit OR @ShifraOperatorit IS NULL)
AND (HapjaEDokumenteve = @HapjaEDokumenteve OR @HapjaEDokumenteve IS NULL)
AND (MenaxhimiKolonave = @MenaxhimiKolonave OR @MenaxhimiKolonave IS NULL)
AND (ShfaqNeWeb = @ShfaqNeWeb OR @ShfaqNeWeb IS NULL)
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DataKohaSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DataKohaSelect_Sp
			  End
Go --

Create PROCEDURE TOSHIBA.DataKohaSelect_Sp 
AS
SELECT getdate () AS DataKoha
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtSelect_sp
			  End

Go --
CREATE Procedure TOSHIBA.POS_ArtikujtSelect_sp --@Shifra='2604797',@OrganizataId=4
(
@Id int  =null,
@Shifra varchar(30),
@OrganizataId int
		) 
as begin 
		Declare @DyNumratEParaTeBarkodit int,@Pershorja int,@Plu int,@Pesha decimal(18,3),@Sasia decimal(18,3),@Tipi varchar(20),@Cmimi DECIMAL(18,3)
		set @Sasia=1
		if (LEN(@Shifra)=13 AND ISNUMERIC(@Shifra)=1)
			begin
			   select @DyNumratEParaTeBarkodit=Id,@Tipi=Tipi from StandardetEBarkodave  with(nolock) where Id=CONVERT(int,(LEFT(@Shifra,2)))
			   if (@DyNumratEParaTeBarkodit is not null)
			   begin
			   if (@Tipi='FIX')
				   begin
				   Select @Id=TOSHIBA.GjejeArtikulliId(LEFT(@Shifra,7)),@Pesha=convert(Decimal(18,3),SUBSTRING(@Shifra,8,5))
				   if exists (select id from Konfigurimet  with(nolock) where id=203 and Statusi=1)
				   begin
				    set @Sasia=@Pesha
				   end
				   else
				   begin
				   set @Sasia=@Pesha/ 1000.00
				   end
				   end
			   else
				   begin
							Select @Id=ArtikulliId from dbo.PLUPeshoret  with(nolock) where PLU=SUBSTRING(@Shifra,3,5) 
							if (@Id is not null)
							begin											 
							Select @Pershorja=SUBSTRING(@Shifra,3,2),@Plu=SUBSTRING(@Shifra,3,5),@Pesha=convert(Decimal(18,3),SUBSTRING(@Shifra,8,5))
								  if exists (select id from Konfigurimet  with(nolock) where id=203 and Statusi=1)
									   begin
											 set @Sasia=@Pesha
									   end
									   else
									   begin

											set @Sasia=@Pesha/ 1000.00

									   end
								  end
				   End
			   end
			   else
			   Begin
			   Select @Id=TOSHIBA.GjejeArtikulliId(@Shifra)
			   end
			end
		Else
			Begin
			Select @Id=TOSHIBA.GjejeArtikulliId(@Shifra)
			end
			
	    if 	@Id =-1
		begin
			set @Id=isnull((select artikulliid from Barkodat where Barkodi=@Shifra),-1)
			set @Sasia=1
		end


			Select @Sasia=SasiaPako, @Cmimi=ISNULL(Cmimi,0) From Barkodat with(nolock) Where Barkodi=@Shifra
			If (@Sasia is null) Begin Set @Sasia=1 End
select A.Id
,A.Shifra
,A.Pershkrimi
,A.PershkrimiTiketa
,A.NjesiaID
,(Select Pershkrimi From dbo.Njesit  with(nolock) Where Id= A.NjesiaID) Njesia 
,@Sasia Sasia
,B.Tvsh TVSH
,CASE WHEN @Cmimi>0 THEN @Cmimi ELSE B.QmimiIShitjes END QmimiIShitjes 
,B.QmimiIShitjes QmimiFinal
,Case when B.QmimiShumice is null then B.QmimiIShitjes
	 When  B.QmimiShumice  <=0	  then B.QmimiIShitjes
	 When  B.QmimiShumice  >B.QmimiIShitjes	  then B.QmimiIShitjes
	 when  A.paketimi in (0,1) then B.QmimiIShitjes
	 when  B.QmimiShumice  > 0 and A.paketimi not in (0,1)	  then B.QmimiShumice 
	 else B.QmimiShumice end QmimiShumice
,IsNull((Select ZbritjaPerqindje from POS_PlaniZbritjeve  with(nolock) where Convert(Date,GETDATE()) Between DataPrej and DataDeri ),0.00) Rabati
,0.00 EkstraRabati
,0.00 Vlera
,B.Stoku
,IsNull(A.Paketimi ,0) Paketimi
,cast(isnull((Select top 1 Case when ArtikulliId Is null then 0
			  when ArtikulliId IS not null then 1 
			  else 0 end from dbo.ArtikujtMeLirim  with(nolock) where ArtikulliId=A.Id and OrganizataId =@OrganizataId),0) as Bit) KaLirim
,cast(isnull((select top 1 1 from ArtikujtPaZbritje where ArtikulliId = A.Id),0) as Bit) ArtikullPaZbritje
,B.StatusiQmimitId
,A.ShifraProdhuesit
,(SELECT TOP 1 Barkodi FROM Barkodat WHERE ArtikulliId=A.Id) Barkodi
,A.NumriSerik
,(SELECT TOP 1 Pershkrimi FROM Brendet WHERE Id=A.BrendId) Brendi
,B.ZbritjaAktive
From Artikujt A  with(nolock),dbo.Cmimorja B  with(nolock),dbo.LlojetEArtikullit L  with(nolock)
Where A.Id=B.ArtikulliId 
And A.LlojiIArtikullitId =L.Id
And (A.Id=@Id)  
And B.OrganizataId=@OrganizataId
And B.QmimiIShitjes >0
And @Sasia*B.QmimiIShitjes >0
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArkaOperatoretSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArkaOperatoretSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_ArkaOperatoretSelect_sp
(
	@Id int=null,
	@Shifra int=null,
	@Pershkrimi varchar(100)=null,
	@DataERegjistrimit datetime=null,
	@RegjistruarNga int=null,
	@Statusi bit=null,
	@OrganizataId int,
	@Pass VARCHAR(50)=NULL,
	@ShifraOperatorit varchar(100)=NULL
) 
as begin 
	SELECT 
		Id,Id Shifra,
		(Emri+ ' ' + Mbiemri) Pershkrimi,
		DataEKrijimit DataERegjistrimit,
		'' RegjistruarNga,
		Statusi,
		OrganizataId,
		Pass, 
		ShifraOperatorit,
		GrupiId
	FROM Mxh_Operatoret with(nolock)
	WHERE  
		(@Id is NULL OR Id=@Id) 
	AND	(@Shifra is NULL OR Id=@Shifra) 
	AND	(@ShifraOperatorit IS NULL OR (@ShifraOperatorit is not null and @ShifraOperatorit !='' and ShifraOperatorit=@ShifraOperatorit)) 
	AND	(@Statusi is NULL OR Statusi=@Statusi)  
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_MenyratEPagesesSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_MenyratEPagesesSelect_sp
			  End
Go --

CREATE Procedure TOSHIBA.POS_MenyratEPagesesSelect_sp  
(
@Id int  =null,
@Shkurtesa varchar (15) =null,
@Pershkrimi varchar (40) =null,
@ParaqitetNePos bit = null
		) 
as begin 
Select 
	Id,
    Shkurtesa,
    Pershkrimi,
    Provizioni,
    Tipi,
    Renditja,
    PershkrimiAnglisht,
    DataERegjistrimit,
    ParaqitetNePos,
	PagesMeBonus
FROM dbo.MenyratEPageses with(nolock)
where  (Id=@Id or @Id is null ) 
and  (Shkurtesa=@Shkurtesa or @Shkurtesa is null ) 
and  (Pershkrimi=@Pershkrimi or @Pershkrimi is null ) 
and (ParaqitetNePos = @ParaqitetNePos or @ParaqitetNePos is null)
Order by Renditja ASC 
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitSelect_sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[POS_DaljaMallitSelect_sp] 
(
@Id BIGINT  =NULL,
@Sinkronizuar bit = null,
@TavolinaId int = null,
@Validuar bit = null,  
@DepartamentiId int = null,
@ShitjeEPerjashtuar bit  =null,
@DataERegjistrimit datetime  =null
)  
AS BEGIN 
SELECT A.Id,
A.OrganizataId,
A.Viti,
A.Data
,convert(bigint,A.NrFatures) NrFatures,
A.DokumentiId,
A.RegjistruarNga,
A.NumriArkes,
A.DataERegjistrimit,
A.SubjektiId BleresiId ,
A.SubjektiId ,
A.Validuar,
A.Koment,
A.AfatiPageses,
IdLokal,
ZbritjeNgaOperatori ,
A.RFID,
A.RFIDCCP,
A.TavolinaId,
A.Kursi,
DepartamentiId,  
(select Id from Departamentet where Id=A.DepartamentiId) Departamenti,
A.ShitjeEPerjashtuar,
ISNULL(A.KuponiFiskalShtypur,0) KuponiFiskalShtypur,
A.BarazimiId,
A.NumriATK,
A.NumriArkesGK,
A.NumriFaturesManual,
A.ServerId,
DaljaMallitKorrektuarId,
A.AplikacioniId,
A.KthimiMallitArsyejaId,
       Personi,
       Adresa,
       NumriPersonal,
       NrTel
From DaljaMallit A  
where  (A.Id=@Id or @Id is null ) 
and (@Sinkronizuar is null or (@Sinkronizuar=0 AND ServerId IS Null ))
and (@TavolinaId is null or A.TavolinaId =@TavolinaId)
and (@Validuar is null or A.Validuar =@Validuar)
and (@DepartamentiId is null or A.DepartamentiId=@DepartamentiId)
and (@ShitjeEPerjashtuar is null or A.ShitjeEPerjashtuar=@ShitjeEPerjashtuar) 
and (@DataERegjistrimit is null or CONVERT(DATE,(A.DataERegjistrimit)) =CONVERT(DATE,@DataERegjistrimit) or A.DataERegjistrimit=@DataERegjistrimit ) 
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitDetaleSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitDetaleSelect_sp
			  End
Go --
Create Procedure [TOSHIBA].[POS_DaljaMallitDetaleSelect_sp] 
(
@Id INT  =NULL,
@DaljaMallitID BIGINT  =NULL,
@KujtesaOrderId INT = NULL
)   
AS 
BEGIN   
SELECT  Nr Nr,
		DD.Id,
		Nr Nr1,
		A.Shifra,
		DD.Barkodi,
		A.Pershkrimi AS Emertimi,
		A.ShifraProdhuesit ,
		A.PershkrimiFiskal AS PershkrimiFiskal,
		'' NumriKatallogut,
		DaljaMallitID,
		DD.ArtikulliId,
		DD.NjesiaID,
		NJ.Njesia AS Njesia, 
		A.BrendId,
		B.Pershkrimi Brendi,
		T.Kategoria TvshKategoria,
		DD.Sasia, 
		DD.QmimiShitjes QmimiRregullt,
		DD.QmimiShitjes QmimiShumice,
		DD.QmimiShitjes, 
		DD.Rabati,
		DD.EkstraRabati,
	    DD.Tvsh,
		(DD.QmimiShitjes /(1+DD.Tvsh/ 100.00)) QmimiPaTvsh,
		((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00)) QmimiFinal,		
		DD.Sasia*(((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00))/(1+DD.Tvsh/ 100.00)) VleraPaTvsh,
		CONVERT(DECIMAL(18,2),(DD.Sasia*Convert(decimal(25,8),((((Convert(decimal(25,8),DD.QmimiShitjes *Convert(decimal(25,8),(1-DD.Rabati / 100.00))))*Convert(decimal(25,8),(1-DD.EkstraRabati/ 100.00)))))-(((Convert(decimal(25,8),DD.QmimiShitjes *Convert(decimal(25,8),(1-DD.Rabati / 100.00))))*Convert(decimal(25,8),(1-DD.EkstraRabati/ 100.00)))/CONVERT(decimal(25,8),(1+DD.Tvsh/ 100.00)))))) VleraTvsh,
		DD.Sasia*((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00)) VleraMeTvsh,
		CONVERT(BIT,0) KaLirim
		,CAST(0 AS BIT) NdryshuarNgaShfrytezuesi
 		,'' LlojiZbritjes
		,CAST(0 AS BIT) ArtikullPaZbritje
		,'' NumriSerik
		,getdate() DataESkadences
		,0.00 AS SasiaMaksimale
		,DD.ArtikujtEProdhuarId
		,DD.HyrjaDetaleId
		,cast('' as varchar(50)) IdUnikeProdhimHyrje
		,1 Paketimi
		,DaljaMallitDetaleKthimiId
		,1 SasiaPaketave
		,C.StatusiQmimitId,
		d.Kursi,
		d.NumriArkes,
		DD.AplikimiVoucherit
FROM DaljaMallitDetale DD with(nolock)
INNER JOIN DaljaMallit d with(nolock) ON d.id=dd.DaljaMallitID
INNER JOIN dbo.Artikujt A with(nolock) ON DD.ArtikulliId =A.Id 
inner join dbo.Tatimet T on A.TatimetID = T.Id 
INNER JOIN dbo.Njesit NJ with(nolock) ON A.NjesiaID =NJ.Id
LEFT OUTER JOIN Brendet B ON A.BrendId = B.Id
LEFT OUTER JOIN Cmimorja C ON d.OrganizataId=C.OrganizataId  AND DD.ArtikulliId=C.ArtikulliId
WHERE  (DD.Id=@Id OR @Id IS NULL )  
AND (DD.DaljaMallitID = @DaljaMallitID OR @DaljaMallitID IS NULL )
AND (@KujtesaOrderId IS NULL OR (DD.KujtesaOrderId = @KujtesaOrderId and not exists(select * from DaljaMallitDetale
												where KujtesaOrderId = @KujtesaOrderId and Sasia < 0)))
ORDER BY DD.NR 

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitDetaleInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitDetaleInsert_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_DaljaMallitDetaleInsert_sp
(
  @DaljaMallitID BIGINT ,
  @OrganizataId INT ,
  @ArtikulliId INT ,
  @NjesiaID TINYINT ,
  @Sasia DECIMAL(18, 3) ,
  @Tvsh DECIMAL(18, 2) ,
  @QmimiShitjes DECIMAL(18, 8) ,
  @Rabati DECIMAL(18, 2) ,
  @EkstraRabati DECIMAL(18, 2) =NULL,
  @NR SMALLINT,
  @HyrjaDetaleId INT=NULL,
  @ArtikujtEProdhuarId INT =NULL,
  @DaljaMallitDetaleKthimiId Int = NULL ,
  @ProjektiLinjatId INT = NULL	,
  @QmimiFinal DECIMAL(18, 2) =NULL,
  @LlojiZbritjes VARCHAR(100) = NULL,
  @AplikimiVoucherit bit=null,
  @KujtesaOrderId int = null ,
  @Barkodi varchar(50)=null
    )
AS 
    BEGIN 

if (IsNull(@Rabati,0.00)<0 Or IsNull(@Rabati,0.00)>100) 
begin
raiserror('Rabati duhet te jet brenda limitit 0 deri 100!',
11,
1,
'Vrejtje'
)
return
END

declare @ms varchar(50)
set @ms='Nuk lejohet rabati per artikullin ' + convert(varchar(50),(SELECT ShifraProdhuesit  from Artikujt with(nolock) where Id = @ArtikulliId)) + '!'

if exists(Select ArtikulliId from ArtikujtPaZbritje with(nolock) Where ArtikulliId=@ArtikulliId)
begin
	if (IsNull(@Rabati,0.00)>0)
	begin
	raiserror(@ms,
	11,
	1,
	'Vrejtje'
	)
	return
	END
end
 
if (IsNull(@EkstraRabati,0.00)<0 Or IsNull(@EkstraRabati,0.00)>100) 
begin
raiserror('Ekstra rabati duhet te jet brenda limitit 0 deri 100!',
11,
1,
'Vrejtje'
)
return
END
 
set @EkstraRabati=(isnull(@EkstraRabati,0.00)) 

	Declare @ArtikulliIPerberID int
	set @ArtikulliIPerberID =(select ArtikulliIPerberId from ArtikujtEPerber with(nolock) where ArtikulliId=@ArtikulliId)

    DECLARE @BazaFurnizuese DECIMAL(18, 5)=0.00
    DECLARE @Kostoja DECIMAL(18, 5)=0.00
    DECLARE @KostoMesatare DECIMAL(18, 5)=0.00
    DECLARE @Stoku DECIMAL(18, 5)=0.00
    DECLARE @LlojiArtikullit INT
    DECLARE @StatusiArtikullit INT

		DECLARE @DokumentiId INT,@DataDokumentit date
		SELECT @DokumentiId=DokumentiId,@DataDokumentit=Data FROM DaljaMallit WITH(NOLOCK) WHERE Id=@DaljaMallitID

		IF (@DokumentiId in (211,25,26))
		BEGIN 
		SET @Sasia=-ABS(@sasia)
		End
 

				Declare @LlojiOrganizates int
				select @LlojiOrganizates=LlojiISubjektitID from Subjektet with(nolock) where  id=@OrganizataId

				
-------------Merr Qmimi Aktual
Declare @QmimiRregullt decimal(18,4),@QmimiShumice decimal(18,4)
select @QmimiRregullt=QmimiIShitjes,@QmimiShumice=QmimiShumice From Cmimorja with(nolock) where ArtikulliId = @ArtikulliId and OrganizataId =@OrganizataId 
-------------Update i Cmimores
Update C set C.Stoku=C.Stoku - @Sasia 
from Cmimorja C where OrganizataId = @OrganizataId and ArtikulliId = @ArtikulliId  

-----------------------------------
    INSERT  INTO DaljaMallitDetale
    ( DaljaMallitID ,
      ArtikulliId ,
      NjesiaID ,
      NR ,
      Sasia ,
      BazaFurnizuese ,
      Kostoja ,
      KostoMesatare ,
      Tvsh ,
      QmimiShitjes ,
      Rabati ,
      EkstraRabati ,
      Stoku ,
      StatusiArtikullit,
      HyrjaDetaleId,
	  ArtikujtEProdhuarId,
	  QmimiRregullt,
	  QmimiShumice,
	  DaljaMallitDetaleKthimiId,
	  ProjektiLinjatId,
	  AplikimiVoucherit,
	  KujtesaOrderId ,
	  Barkodi
    )
    VALUES  
	( @DaljaMallitID ,
      @ArtikulliId ,
      @NjesiaID ,
      @NR ,
      @Sasia ,
      @BazaFurnizuese ,
      @Kostoja ,
      @KostoMesatare ,
      @Tvsh ,
      @QmimiShitjes ,
      @Rabati ,
      Isnull(@EkstraRabati,0) ,
      @Stoku ,
      @StatusiArtikullit,
      @HyrjaDetaleId,
	  @ArtikujtEProdhuarId,
	  Isnull(@QmimiRregullt,0.00),
	  Isnull(@QmimiShumice,0.00),
	  @DaljaMallitDetaleKthimiId,
	  @ProjektiLinjatId,
	  @AplikimiVoucherit,
	  @KujtesaOrderId ,
	  @Barkodi
    )
	 
				Declare @DaljaDetaleId int 
				 SELECT  @DaljaDetaleId=SCOPE_IDENTITY()

   SELECT @DaljaDetaleId

    END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitDetaleHistoriInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitDetaleHistoriInsert_sp
			  End
Go --
CREATE procedure TOSHIBA.POS_DaljaMallitDetaleHistoriInsert_sp  
(
	@DaljaMallitID bigint ,
	@OrganizataId int,
	@ArtikulliId int ,
	@NjesiaID tinyint ,
	@Sasia decimal (18,3),
	@Tvsh decimal (18,2),
	@QmimiShitjes decimal (18,2),
	@Rabati decimal (18,2),
	@EkstraRabati decimal (18,2)=null,
	@NR smallint		,
	  @QmimiFinal DECIMAL(18, 2) =Null						
)
 
as begin 
Declare @BazaFurnizuese   decimal (18,5)
Declare @Kostoja   decimal (18,5)
Declare @KostoMesatare   decimal (18,5)
Declare @Stoku   decimal (18,5)

set @OrganizataId = (Select OrganizataId from DaljaMallit with(nolock) where id = @DaljaMallitID)

Select  
@Stoku=isnull(Stoku,0)  
From Cmimorja with(nolock)
Where ArtikulliId=@ArtikulliId and OrganizataId=@OrganizataId

Insert into DaljaMallitDetaleHistori 
(
DaljaMallitID,ArtikulliId,NjesiaID,NR,Sasia,BazaFurnizuese,Kostoja,KostoMesatare,Tvsh,QmimiShitjes,Rabati,EkstraRabati,Stoku ) 
Values 
( @DaljaMallitID,@ArtikulliId,@NjesiaID,@NR,@Sasia,isnull(@BazaFurnizuese,0),isnull(@Kostoja,0),isnull(@KostoMesatare,0),@Tvsh,@QmimiShitjes,@Rabati,isnull(@EkstraRabati,0),isnull(@Stoku,0) ) 
Select scope_identity() as Id
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_EkzekutimiPagesesInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_EkzekutimiPagesesInsert_sp
			  End
Go --
CREATE Procedure TOSHIBA.POS_EkzekutimiPagesesInsert_sp 
(
@MenyraEPagesesId int ,
@DaljaMallitID bigint ,
@Vlera decimal (18,2),
@Paguar decimal (18,2),
@ShifraOperatorit int,
@DhenjeKesh decimal (18,2),
@ValutaId int=NULL,
@Kursi decimal(18,10)=null,
@KodiVoucherit varchar(250)=null,
@LlojiVoucherit varchar(250)=null
) 
as begin  
		Declare @LlojiIDokumentit int
		select @LlojiIDokumentit=DokumentiId from dbo.DaljaMallit with(nolock) where Id=@DaljaMallitID

		IF (@LlojiIDokumentit=211)
		BEGIN 
		SET @Paguar=-ABS(@Paguar)
		SET @Vlera=-ABS(@Vlera)
		END
    
	 --   if (@LlojiIDokumentit=20 and @Vlera<0)
		--Begin
		--RAISERROR ('Paratë e dhëna nuk janë të mjaftueshme!', -- Message text.
  --     11, -- Severity,
  --     1, -- State,
  --     N'Paratë e dhëna nuk janë të mjaftueshme!')
		--End

  --  if (@LlojiIDokumentit=20 and @MenyraEPagesesId=12)
		--Begin
		--RAISERROR ('Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!', -- Message text.
  -- 11, -- Severity,
  -- 1, -- State,
  -- N'Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!')
		--End

		--if (@LlojiIDokumentit=20 and @MenyraEPagesesId=18)
		--Begin
		--RAISERROR ('Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!', -- Message text.
  -- 11, -- Severity,
  -- 1, -- State,
  -- N'Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!')
		--End

		--if (@LlojiIDokumentit in (30,40,215,217) and @MenyraEPagesesId<>12)
		--Begin
		--RAISERROR ('Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!', -- Message text.
  -- 11, -- Severity,
  -- 1, -- State,
  -- N'Mënyra e Pagesës është e pa pranueshme për këtë lloj të shitjes!')
		--End
		
    Insert into EkzekutimiPageses 
		(MenyraEPagesesId,DaljaMallitID,Vlera,Paguar,ShifraOperatorit,DhenjeKesh,ValutaId,Kursi,KodiVoucherit,LlojiVoucherit)
		Values 
		(@MenyraEPagesesId,@DaljaMallitID,@Vlera,@Paguar,@ShifraOperatorit,isnull(@DhenjeKesh,0.00),@ValutaId,@Kursi,@KodiVoucherit,@LlojiVoucherit)
		Return scope_identity()

end


Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitInsert_sp
			  End
Go --
CREATE Procedure TOSHIBA.POS_DaljaMallitInsert_sp  
(
	@OrganizataId int ,
	@Viti int ,
	@Data date,
	@NrFatures int =null,
	@DokumentiId int ,
	@RegjistruarNga int ,
	@NumriArkes int ,
	@SubjektiId int =null,
	@Bleresi int=null,
	@ShitjeEPerjashtuar bit=null , 
	@DepartamentiId INT=NULL,
	@Validuar bit=null,
	@Koment varchar(1000)=null,
	@AfatiPageses INT = NULL ,
	@DaljaMallitKorrektuarId BIGINT=NULL,
	@TavolinaId int=null,
	@NrDuditX3 varchar(50)=null,
	@DataFatures date = null ,
	@DaljaMallitPorosiaId bigint = null ,
	@ValutaId int = null ,
	@Kursi decimal(18,8) = null,
	@DaljaMallitImportuarId varchar(50)=null,
	@IdLokal varchar(50)=null,
	@OfertaId BIGINT = NULL,
	@TrackingId INT = NULL,
	@NumriFaturesManual varchar(50) =null,
	@ZbritjeNgaOperatori INT = NULL,
	@RFID varchar(50) =null,
	@RFIDCCP varchar(50) =NULL,
	@NumriATK varchar(150)=null,
	@NumriArkesGK varchar(50) = NULL,
	@Personi varchar(300)=NULL,
	@Adresa varchar(800)=NULL,
	@NumriPersonal varchar(50)=NULL,
	@NrTel varchar(50)=null,
	@AplikacioniId int=null,
	@KthimiMallitArsyejaId int=null
)
 
as begin 

declare @Kom varchar(50)=''
set @Kom =(SELECT IdLokal FROM dbo.DaljaMallit WHERE @IdLokal is not null and IdLokal =@IdLokal AND OrganizataId = @OrganizataId and IdLokal is not null and IdLokal <> '')
if  (len(isnull(@Kom,''))>2)
begin 
		Declare @IIII varchar(200)='Ky numër '+ @Kom +' i faturës lokale është regjistruar njëherë!'
		Raiserror(
					@IIII,
					11,
					1,
					'Vrejtje!'
				 )
End

if exists (SELECT 1 FROM dbo.DaljaMallit WHERE @IdLokal is not null and IdLokal =@IdLokal AND OrganizataId = @OrganizataId)
begin 
		Raiserror(
					'Ky numër i faturës lokale është regjistruar njëherë!',
					11,
					1,
					'Vrejtje!'
				 )
END


set @Viti = Year(Isnull(@Data,getdate()))

If(@TavolinaId IS NULL)
Begin 
Set @Validuar = 1
End

IF @Bleresi IS NULL AND @SubjektiId IS NOT NULL
BEGIN
SET @Bleresi=@SubjektiId
end

if @Bleresi is not null and @SubjektiId is null
begin
set @SubjektiId=@Bleresi
end

 
set @Data=getdate()
 

Declare @ID bigint
 
IF(@SubjektiId<>0)
Begin
	if exists (SELECT 1 FROM dbo.Konfigurimet WHERE Id=432 AND statusi=1)
	begin 
		IF(@NumriATK IS NULL OR LEN(@NumriATK)=0 OR @NumriATK='' OR @NumriATK=' ')
			Raiserror(
						'Nuk mund te regjistroni fatura pa numër serik!',
						11,
						1,
						'Dalja Mallit DB!'
					 )
	END
END
/* Cakto numrin e fatures */
set @NrFatures = IsNull(
							(
								select max(MaxNrFatures) from 
											(
											select max(NrFatures) MaxNrFatures,DokumentiId,OrganizataId
											from DaljaMallit 
											where 
											OrganizataId=@OrganizataId
											and DokumentiId=@DokumentiId
											and Year(DataERegjistrimit)=Year(getdate())
											Group by DokumentiId,OrganizataId,NumriArkes
											) nrF
							),
0)+1


if @Kursi=0
begin 
set @Kursi=1
end


Select @ID=TOSHIBA.GjeneroDokumentiId(@OrganizataId,@DokumentiId,YEAR(@Data),@NrFatures)
while (1=(select 1 from DaljaMallit where Id=@ID))
		Begin
		set @NrFatures=@NrFatures+1
		Select @ID=TOSHIBA.GjeneroDokumentiId(@OrganizataId,@DokumentiId,YEAR(@Data),@NrFatures)
		END

DECLARE @ServerName VARCHAR(100)
		SET @ServerName=@@SERVERNAME

SELECT @NumriFaturesManual=
	  RIGHT(CONVERT(VARCHAR(50),YEAR(@Data)) ,2)
+'-'+ISNULL(CONVERT(VARCHAR(50),@OrganizataId),'')
+'-'+ISNULL(CONVERT(VARCHAR(50),@DokumentiId),'')
+'-'+ISNULL(CONVERT(VARCHAR(50),@NumriArkes),'')
+'-'+ISNULL(CONVERT(VARCHAR(50),@NrFatures),'')

Insert into dbo.DaljaMallit 
(
Id,OrganizataId,Viti,Data,NrFatures,DokumentiId,RegjistruarNga,NumriArkes,DataERegjistrimit,SubjektiId ,DepartamentiId,Validuar,Koment,AfatiPageses ,
DaljaMallitKorrektuarId,TavolinaId,NrDuditX3,DataFatures, RaportDoganor,ValutaId , Kursi ,KuponiFiskalShtypur,
DaljaMallitImportuarId,IdLokal, TrackingId,NumriFaturesManual, ZbritjeNgaOperatori,RFID,RFIDCCP,NumriATK, NumriArkesGK,
Personi,Adresa,NumriPersonal,NrTel
,AplikacioniId,
KthimiMallitArsyejaId
)
Values 
( 
@Id,@OrganizataId,@Viti,@Data,@NrFatures,@DokumentiId,@RegjistruarNga,@NumriArkes,GETDATE(),@SubjektiId ,ISNULL(@DepartamentiId,10),isnull(@Validuar,0),@Koment,ISNULL(@AfatiPageses,0)
,@DaljaMallitKorrektuarId,@TavolinaId,@NrDuditX3,@DataFatures,0,@ValutaId,IsNull(@Kursi,1),1,
@DaljaMallitImportuarId,@IdLokal, @TrackingId,@NumriFaturesManual, @ZbritjeNgaOperatori,@RFID,@RFIDCCP,@NumriATK, @NumriArkesGK,
@Personi,@Adresa,@NumriPersonal,@NrTel
,@AplikacioniId,
@KthimiMallitArsyejaId
)

UPDATE DaljaMallitDetaleHistori
SET DaljaMallitId=@ID
WHERE DaljaMallitId=-1

Select @ID as Id
END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikulliSelectKontrolloCmiminPOS_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikulliSelectKontrolloCmiminPOS_sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_ArtikulliSelectKontrolloCmiminPOS_sp
(																
  @Barkodi VARCHAR(50),  @OrganizataId INT 
)
as
begin

	DECLARE @ArtikulliId INT 

	If @Barkodi='-1'
	begin
	   Select 
				0 Id,
				'' Shifra,
				'' Barkodi,
				'' Pershkrimi,
				'' NumriSerik,
				0.00 Stoku ,
				0.00 QmimiIShitjes 
	end
	else
	begin
			SET @ArtikulliId = TOSHIBA.GjejeArtikulliId(@Barkodi)

			Select 
				A.Id,
				A.Shifra,
				(Select top 1 Barkodi From Barkodat  with(nolock) Where ArtikulliId =A.Id) AS Barkodi,
				A.Pershkrimi,
				A.NumriSerik,
				N.Njesia ,
				s.Stoku ,
				C.QmimiIShitjes 
			From Artikujt A  with(nolock) INNER JOIN dbo.Njesit N  with(nolock) ON A.NjesiaID = N.Id
			Inner join Cmimorja C  with(nolock) on A.Id=C.ArtikulliId  inner join StokuSipasArtikullit_TabFN(@OrganizataId,'', NULL) S on C.ArtikulliId=S.ArtikulliId 
			Where (C.OrganizataId=@OrganizataId) and A.LlojiIArtikullitId in (10,11,12,13,14,16)
			and a.id=@ArtikulliId
	end
End 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ZbritjaNeArka_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ZbritjaNeArka_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_ZbritjaNeArka_Sp ( @Pass VARCHAR(50) )
AS 
    BEGIN
    DECLARE @Egziston VARCHAR(20)
    IF ( @Pass IS NULL ) 
    GoTO NukEgziston 
    IF ( @Pass = '' ) 
    GoTO NukEgziston 
    SELECT  @Egziston = ID
    FROM    dbo.Mxh_Operatoret with(nolock)
    WHERE   FjalekalimiPerZbritje = @Pass 
    IF ( @Egziston IS NOT NULL ) 
    BEGIN
    SET @Egziston = 1 
    END
    ELSE 
    BEGIN
    NukEgziston:
    SET @Egziston = 0 
    END
    SELECT  @Egziston
    END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresitMeKartelaDetaleSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresitMeKartelaDetaleSelect_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_BleresitMeKartelaDetaleSelect_Sp
	(
		@Id int=Null,
		@SubjektiId int=Null,
		@BleresitMeKartelaId int=Null,
		@DaljaMallitId bigint=Null
	)
As
Begin
Select 
			A.Id,
			A.SubjektiId,
			A.BleresitMeKartelaId,
			A.DaljaMallitId,
			S.Shifra ,
			S.Pershkrimi ,
			BK.Emri ,
			BK.Mbiemri 
From 
dbo.BleresitMeKartelaDetale A with(nolock) INNER JOIN dbo.Subjektet S with(nolock) ON A.SubjektiId = S.Id 
INNER JOIN dbo.BleresitMeKartela BK with(nolock) ON A.BleresitMeKartelaId = BK.Id
Where 
 
		(A.Id=@Id Or @Id Is Null )
		And (A.SubjektiId=@SubjektiId Or @SubjektiId Is Null )
		And (A.BleresitMeKartelaId=@BleresitMeKartelaId Or @BleresitMeKartelaId Is Null )
		And (A.DaljaMallitId=@DaljaMallitId Or @DaljaMallitId Is Null )
End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresitMeKartelaDetaleInsert_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresitMeKartelaDetaleInsert_Sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_BleresitMeKartelaDetaleInsert_Sp
	(
		@Id INT=NULL,
		@SubjektiId int,
		@BleresitMeKartelaId int,
		@DaljaMallitId bigint
	)
As
BEGIN
set @Id=Isnull((select Max(Id) from dbo.BleresitMeKartelaDetale with(nolock)),100000)+1
Insert into dbo.BleresitMeKartelaDetale
		(
			Id,
			SubjektiId,
			BleresitMeKartelaId,
			DaljaMallitId
		)
		Values
		(
			@Id,
			@SubjektiId,
			@BleresitMeKartelaId,
			@DaljaMallitId
		)
		SELECT @Id
End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresitMeKartelaSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresitMeKartelaSelect_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_BleresitMeKartelaSelect_Sp
	(
		@Id int=Null,
		@SubjektiId int=Null,
		@ShifraSubjektit int = null 
	)
As
Begin
Select 
			A.Id,
			A.Kodi,
			A.Zbritja,
			A.SubjektiId,
			A.Emri ,
			A.Mbiemri,
			S.Shifra,
			S.Pershkrimi,
			A.DataERegjistrimit,
			A.RegjistruarNga
From 
dbo.BleresitMeKartela A with(nolock) Inner join dbo.Subjektet S with(nolock) on A.SubjektiId=S.Id
Where 
 
		(A.Id=@Id Or @Id Is Null )
		And (A.SubjektiId=@SubjektiId Or @SubjektiId Is Null )
		And (S.Shifra=@ShifraSubjektit Or @ShifraSubjektit Is Null )
End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresitMeKartelaInsert_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresitMeKartelaInsert_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_BleresitMeKartelaInsert_Sp
	(
		@Id int=null,
		@Kodi varchar(50),
		@Zbritja Decimal(18,2),
		@SubjektiId int,
		@DataERegjistrimit datetime=null,
		@RegjistruarNga int,
		@Emri Varchar(50),
		@Mbiemri Varchar(50)
	)
As
Begin
set @Id=Isnull((select Max(Id) from dbo.BleresitMeKartela with(nolock)),1000)+19
set @DataERegjistrimit=getdate()
if exists (Select 1 from dbo.BleresitMeKartela with(nolock) where Kodi=@Kodi)
Begin
raiserror('Kjo kartel është e regjistruar njeher!'
,11,1,'Vrejtje!')
Return
End
if (@Zbritja<0.00 or @Zbritja>99.99)
Begin
raiserror('Limitet e lejuara per zbritje janë prej 0.00 deri 99.99!'
,11,1,'Vrejtje!')
Return
End
Insert into dbo.BleresitMeKartela
		(
			Id,
			Kodi,
			Zbritja ,
			SubjektiId,
			DataERegjistrimit,
			RegjistruarNga,
			Emri,
			Mbiemri
		)
		Values
		(
			@Id,
			@Kodi,
			@Zbritja,
			@SubjektiId,
			@DataERegjistrimit,
			@RegjistruarNga,
			@Emri,
			@Mbiemri
		)
		SELECT @Id AS Id
End

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresitMeKartela_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresitMeKartela_Sp
			  End
Go --
			  Create Procedure TOSHIBA.POS_BleresitMeKartela_Sp
	(
		@KodiKarteles varchar(50)
	)
As
Begin
Select 
			A.Id,
			A.Kodi,
			A.Zbritja,
			A.SubjektiId,
			S.Shifra,
			S.Pershkrimi,
			A.Emri,
			A.Mbiemri,
			A.DataERegjistrimit,
			A.RegjistruarNga
From 
dbo.BleresitMeKartela A Inner join dbo.Subjektet S on A.SubjektiId=S.Id
Where 
 
		(A.Kodi = @KodiKarteles)
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_KonfigurimetGetId_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_KonfigurimetGetId_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_KonfigurimetGetId_Sp
(
@Id int ,
@OrganizataId int=null

		) 
as begin 

	if (select isnull(count(KonfigurimiId),0)  from KonfigurimetOrganizatat with(nolock) Where KonfigurimiId=@Id)=0
	begin
		Select Statusi  
		From Konfigurimet K  with(nolock)
		where  (Id=@Id) 
	end
	else
	begin
		Select isnull(Statusi ,0)  Statusi
		From Konfigurimet K with(nolock) left outer join KonfigurimetOrganizatat KO with(nolock) on K.Id=KO.KonfigurimiId 
		where  (Id=@Id) 
		and (KO.OrganizataId = @OrganizataId or @OrganizataId is null )
	end
	end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikulliSelectF1_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikulliSelectF1_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_ArtikulliSelectF1_sp
(																
@OrganizataId int=null
)
as
begin
Select 
    A.Id,
    A.Shifra,
    A.Pershkrimi,
    N.Njesia ,
    C.Stoku ,
	C.QmimiIShitjes  Çmimi,
	C.Tvsh
From Artikujt A INNER JOIN dbo.Njesit N ON A.NjesiaID = N.Id
Inner join Cmimorja C on A.Id=C.ArtikulliId  
Where (C.OrganizataId=@OrganizataId) and A.LlojiIArtikullitId in (10,11,12,13,14,16)
and QmimiIShitjes >0
End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitKontimiGjenero_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitKontimiGjenero_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_DaljaMallitKontimiGjenero_sp
	@DaljaMallitID bigint=null
	AS
	BEGIN

	if ((select statusi from Konfigurimet where id=263)=1)
	begin
	return
	end

	delete from dbo.DaljaMallitKontimi  where (@DaljaMallitId is null or  DaljaMallitID=@DaljaMallitId)

 
	begin


	INSERT INTO dbo.DaljaMallitKontimi 
			( DaljaMallitID, KontoId, Vlera, DK )
	select DaljaMallitId,JV.KontoID,H.Vlera ,J.DK  From 
	(
	select OrganizataId,DaljaMallitId,LlojiDokumentitId,Vlera,LlojiVleres From 
	(
							Select  convert(decimal(18,2),SUM(t1.KostoMesatare)) KMSH,
									convert(decimal(18,2),SUM(t1.VleraMP)-(SUM(t1.TotalVlera)-SUM(t1.VleraPaTVSH))) TeHyra,
									convert(decimal(18,2),(SUM(t1.TotalVlera)-SUM(t1.VleraPaTVSH))) TVSH,
									LlojiDokumentitId,DaljaMallitId,OrganizataId
									From (

											Select round((DD.Sasia * DD.KostoMesatare),2) AS KostoMesatare,
												   round((DD.Sasia * ((DD.QmimiShitjes*(1-Rabati/ 100.00)*(1-EkstraRabati/ 100.00))/(1+TVSH/ 100.00))),2) AS VleraPaTVSH,
												   round((DD.Sasia * (DD.QmimiShitjes*(1-Rabati/ 100.00)*(1-EkstraRabati/ 100.00))),2) AS TotalVlera,
												   0.00 VleraMP,D.DokumentiId LlojiDokumentitId,D.Id DaljaMallitId,D.OrganizataId
											FROM dbo.DaljaMallit D with(nolock) inner join DaljaMallitDetale DD with(nolock) on  D.id=DD.DaljaMallitID
											where  D.DokumentiId <>225 and  (@DaljaMallitId is null or D.Id=@DaljaMallitId )
											Union all 
											Select 0.00,0.00,0.00,isnull(vlera,0) VleraMP,D.DokumentiId LlojiDokumentitId,D.Id DaljaMallitId,OrganizataId 
											from DaljaMallit D  with(nolock)  inner join EkzekutimiPageses E  with(nolock)  on D.Id=E.DaljaMallitID
											where D.DokumentiId <>225 and  (@DaljaMallitId is null or D.Id=@DaljaMallitId )
										

										) t1
										group by LlojiDokumentitId,DaljaMallitId,OrganizataId
	) A1
	unpivot
	(Vlera for LlojiVleres in (KMSH,TeHyra,TVSH)) A2

	)  H Inner join FinKonfiguroKontiminEDokumenteve J with(nolock) on H.LlojiVleres = J.Vlera 
	Inner join FinKonfiguroKontiminEDokumenteveDetale JV with(nolock) on J.Id = JV.FinKonfiguroKontiminEDokumenteveID 
	and (H.LlojiDokumentitId = JV.LlojiDokumentitID  or JV.LlojiDokumentitId is null)
	  Union all
	  select DaljaMallitId,JV.KontoID,H.Vlera ,J.DK  From 
	(
	select OrganizataId,DaljaMallitId,LlojiDokumentitId,Vlera,LlojiVleres,MenyraEPagesesId  From 
	(
							Select 'VleraMP' LlojiVleres,convert(decimal(18,2),SUM(t1.VleraMP)) Vlera,
									MenyraEPagesesId,
									LlojiDokumentitId,
									DaljaMallitId,
									OrganizataId
									From (
											Select isnull(vlera,0) VleraMP,MenyraEPagesesId,D.DokumentiId LlojiDokumentitId,DaljaMallitId,OrganizataId 
											from DaljaMallit D with(nolock) inner join EkzekutimiPageses  E with(nolock) on D.Id=E.DaljaMallitID
											where  D.DokumentiId <>225 and  (@DaljaMallitId is null or D.Id=@DaljaMallitId)  
										) t1
										group by MenyraEPagesesId,LlojiDokumentitId,DaljaMallitId,OrganizataId
	) A1


	)  H Inner join FinKonfiguroKontiminEDokumenteve J with(nolock) on H.LlojiVleres = J.Vlera 
	Inner join FinKonfiguroKontiminEDokumenteveDetale JV with(nolock) on J.Id = JV.FinKonfiguroKontiminEDokumenteveID AND (ISNULL(JV.OrganizataId ,H.OrganizataId)=H.OrganizataId )
	and (H.LlojiDokumentitId = JV.LlojiDokumentitID  or JV.LlojiDokumentitId is null) 
	and H.MenyraEPagesesId= JV.Parametri  

	--INSERT INTO dbo.DaljaMallitKontimi 
	--		( DaljaMallitID, KontoId, Vlera, DK )
	--select DaljaMallitID,KontoId, Vlera,DK from dbo.KontimiIShitjeveTeAseteveTABFnc(@DaljaMallitID) 
	--where Vlera<>0
	end
	End


Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_NdaloStokunNegative_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_NdaloStokunNegative_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_NdaloStokunNegative_Sp
(
@ArtikulliId int,
@OrganizataId int,
@SasiaDalese decimal(18,6),
@Id BIGINT = NULL
)
AS
begin


declare @LlojiArtikullit int, @LlojiDokumentitId int
set @LlojiArtikullit=(select LlojiIArtikullitId from Artikujt with(nolock) where id=@ArtikulliId)

set @LlojiDokumentitId = (SELECT LlojiDokumentitId FROM DaljeInterne with(nolock) where Id = @Id)

    IF exists (select 1 from dbo.Konfigurimet with(nolock) where Id =127 and Statusi = 1) 
			Begin 
			Declare @Stoku decimal(18,3)=IsNull((select C.Stoku from dbo.StokuSipasArtikullit_TabFN(@OrganizataId,null,null) C where ArtikulliId = @ArtikulliId),0)
				IF ((@Stoku-@SasiaDalese)<0 and @LlojiArtikullit<>16 AND @LlojiDokumentitId <> 64 AND @LlojiDokumentitId <> 65)
					BEGIN
					Declare @Com varchar(200)=''
					select @Com='Per artikullin '+Convert(varchar(50),Shifra)+'  '+Pershkrimi+' nuk keni stok! Sasia maksimale per shitje eshte '+Convert(varchar(50),@Stoku)+'!' from dbo.Artikujt with(nolock) where Id=@ArtikulliId
							RAISERROR (
											@Com,
											11,
											1,
											'Vrejtje!'
											)
							RETURN
					END 
			End

End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_OrganizataDetaletSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_Mxh_OrganizataDetaletSelect_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_Mxh_OrganizataDetaletSelect_sp  
(
@Id INT = NULL 
,@Gjuha CHAR(2)= NULL 
,@Email VARCHAR(100) = NULL 
,@Smtp VARCHAR(50) = NULL 
,@Porti VARCHAR(50)= NULL 
,@UserName VARCHAR(50)= NULL 
,@Pass VARCHAR(200)= NULL 
)
AS
begin
SELECT Id,Gjuha,Email,Smtp,Porti,UserName,Pass FROM Mxh_OrganizataDetalet with(nolock)
End

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_KonfigurimetGetVleren_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_KonfigurimetGetVleren_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_KonfigurimetGetVleren_Sp
(
@Id int ,
@OrganizataId int=null
		) 
as begin 
Select isnull(Vlera ,2 ) Vlera
From Konfigurimet K with(nolock) left outer join KonfigurimetOrganizatat KO with(nolock) on K.Id=KO.KonfigurimiId 
where  (Id=@Id) 
and (KO.OrganizataId = @OrganizataId or @OrganizataId is null )
end

 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CCPFaturatInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CCPFaturatInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_CCPFaturatInsert_sp  
(
	@CCPKompaniaId int ,
	@DaljaMallitId bigint ,
	@RegjistruarNga int
)
 
as begin 

Declare @nrPikeve decimal(18,2)

if exists(select id from Konfigurimet where id=399 and Statusi=1)
begin
Select @nrPikeve=sum(dd.Sasia*dd.QmimiShitjes*(1-rabati/100.00)*(1-ekstrarabati/100.00))
from DaljaMallit d with(nolock) inner join DaljaMallitDetale dd with(nolock) on d.id=dd.DaljaMallitID
where d.id=@DaljaMallitId and dd.ArtikulliId not in (select ArtikulliId from CCPArtikujtEPerjashtuar) and rabati=0
end


Insert into dbo.CCPFaturat (CCPKompaniaId,DaljaMallitId,DataRegjistrimit,RegjistruarNga ,VleraPikeve) 
Values (@CCPKompaniaId,@DaljaMallitId,GETDATE(),@RegjistruarNga ,isnull(@nrPikeve,0))

if exists(select id from Konfigurimet where id=399 and Statusi=1)
begin
		if not exists(select top 1 id from [CCPAlokimiBonuseve] where CCPKompaniaId=@CCPKompaniaId)
		begin
			INSERT INTO [dbo].[CCPAlokimiBonuseve]
					   ([Id]
					   ,[CCPKompaniaId]
					   ,[VleraBonusit]
					   ,[DaljaMallitId]
					   ,[DataERegjistrimit]
					   ,[Koment]
					   ,[DataBonusit]
					   ,[RegjistruarNga])
		values(isnull((select max(id) from [dbo].[CCPAlokimiBonuseve]),0)+1
					   ,@CCPKompaniaId
					   ,0.00
					   ,null
					   ,getdate()
					   ,''
					   ,getdate()
					   ,@RegjistruarNga)

		end

		update b
		set b.[VleraBonusit]=b.[VleraBonusit]+isnull(f.VleraPikeve,0) 
		from CCPFaturat f inner join [CCPAlokimiBonuseve] b on f.CCPKompaniaId=b.CCPKompaniaId
		where f.CCPKompaniaId=@CCPKompaniaId and f.DaljaMallitId=@DaljaMallitId
		end
Select scope_identity() as Id 

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CCPFaturatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CCPFaturatSelect_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_CCPFaturatSelect_sp 
(
@Id int  =null,
@CCPKompaniaId int  =null,
@DaljaMallitId bigint  =null,
@CCPBonusiId int  =null,
@RegjistruarNga int  =null
		) 
as begin 
Select A.Id,
	   A.CCPKompaniaId,
	   A.DaljaMallitId,
	   A.CCPBonusiId,
	   A.DataRegjistrimit,
	   A.RegjistruarNga 
From dbo.CCPFaturat as A with(nolock)
where  (A.Id=@Id or @Id is null ) 
and  (A.CCPKompaniaId=@CCPKompaniaId or @CCPKompaniaId is null ) 
and  (A.DaljaMallitId=@DaljaMallitId or @DaljaMallitId is null ) 
and  (A.CCPBonusiId=@CCPBonusiId or @CCPBonusiId is null ) 
and  (A.RegjistruarNga=@RegjistruarNga or @RegjistruarNga is null ) 
 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CCPKompaniteSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CCPKompaniteSelect_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_CCPKompaniteSelect_sp  
(
@Id int  =null,
@EmriKompanise varchar (80) =null,
@KodiKarteles varchar (25) =null,
@ZbritjaBonus decimal (18,2) =null,
@ZbritjaNeArke decimal (18,2) =null,
@Statusi bit  =null,
@DataERegjistrimit datetime  =null,
@RegjistruarNga int  =null,
@SubjektiId int  =null,
@LlojiKartelesId int  =null, 
@ArkaOperatoriId int = null,
@Operatori varchar(200) = null,
@Subjekti varchar(200) = null,
@LlojiKarteles varchar(200) = null,
@ZbritjaNeArkeVlere decimal(18,2) = null,
@Vendi VARCHAR(80) = NULL,
@Telefoni VARCHAR(30) = NULL,
@Email VARCHAR(50) = NULL,
@Komenti VARCHAR(200) = NULL,
@Gjenerofature bit=0,
@GjeneroFleteDergese bit=0,
@TipiKarteles VARCHAR(80) = NULL

		) 
as begin 
Select A.Id,
A.EmriKompanise,
A.KodiKarteles,
A.ZbritjaBonus,
A.ZbritjaNeArke,
A.ZbritjaNeArkeVlere,
A.Statusi,
A.DataERegjistrimit,
A.RegjistruarNga,
A.SubjektiId,
S.Pershkrimi Subjekti,
A.LlojiKartelesId,
K.Pershkrimi LlojiKarteles,
A.ArkaOperatoriId,
AO.Emri + ' ' + AO.Mbiemri Operatori,
A.Vendi,
A.Telefoni,
A.Email,
A.Komenti,
K.TipiKarteles,
'Regjistruar Nga: ' + O.Emri + ' ' + O.Mbiemri + '  Data e Regjistrimit: ' + Convert(VARCHAR(20), A.DataERegjistrimit, 104) +  ' ' + Convert(VARCHAR(20), A.DataERegjistrimit, 8)   RegjistruarPershkrimi
From dbo.CCPKompanite as A with(nolock)
LEFT OUTER JOIN Subjektet S with(nolock) ON A.SubjektiId = S.Id
INNER JOIN CCPLlojetEKartelave K with(nolock) ON K.Id = A.LlojiKartelesId
LEFT OUTER JOIN Mxh_Operatoret O with(nolock) ON A.RegjistruarNga = O.Id
LEFT OUTER JOIN Mxh_Operatoret AO with(nolock) ON AO.Id = A.ArkaOperatoriId
where  (A.Id=@Id or @Id is null ) 
and  (A.EmriKompanise=@EmriKompanise or @EmriKompanise is null ) 
and  (A.KodiKarteles=@KodiKarteles or @KodiKarteles is null ) 
and  (A.ZbritjaBonus=@ZbritjaBonus or @ZbritjaBonus is null ) 
and  (A.ZbritjaNeArke=@ZbritjaNeArke or @ZbritjaNeArke is null ) 
and  (A.Statusi=@Statusi or @Statusi is null ) 
and  (A.DataERegjistrimit=@DataERegjistrimit or @DataERegjistrimit is null ) 
and  (A.RegjistruarNga=@RegjistruarNga or @RegjistruarNga is null ) 
and  (A.SubjektiId=@SubjektiId or @SubjektiId is null ) 
and  (A.LlojiKartelesId=@LlojiKartelesId or @LlojiKartelesId is null ) 
and	 (A.ArkaOperatoriId = @ArkaOperatoriId OR @ArkaOperatoriId is null)
and (A.ZbritjaNeArkeVlere = @ZbritjaNeArkeVlere or @ZbritjaNeArkeVlere is null) 
AND (A.Vendi = @Vendi OR @Vendi IS NULL)
AND (A.Telefoni = @Telefoni OR @Telefoni IS NULL)
AND (A.Email = @Email OR @Email IS NULL)
AND (A.Komenti = @Komenti OR @Komenti IS NULL)
AND (K.TipiKarteles = @TipiKarteles OR @TipiKarteles IS NULL)
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitDetaleHistoriSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitDetaleHistoriSelect_sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_DaljaMallitDetaleHistoriSelect_sp
(
@DaljaMallitID bigint=null
)
AS 
SELECT DH.Id ,
   DH.DaljaMallitID ,
   DH.ArtikulliId ,
   DH.NjesiaID ,
   DH.NR ,
   DH.Sasia ,
   DH.BazaFurnizuese ,
   DH.Kostoja ,
   DH.KostoMesatare ,
   DH.Tvsh ,
   DH.QmimiShitjes ,
   DH.Rabati ,
   DH.EkstraRabati ,
   DH.Stoku
FROM DaljaMallitDetaleHistori DH with(nolock)
Where (DH.DaljaMallitID = @DaljaMallitID or @DaljaMallitID is null )
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDerguarNeServer_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDerguarNeServer_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitDerguarNeServer_Sp
	@DaljaMallitId bigint
AS
Update dbo.DaljaMallit set Sinkronizuar=1
where Id = @DaljaMallitId
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_LokalDBKohaSinkronizimitInsert_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_LokalDBKohaSinkronizimitInsert_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_LokalDBKohaSinkronizimitInsert_Sp
@DataKoha datetime
AS
Insert into LokalDBKohaSinkronizimit (Data) values (@DataKoha )


DELETE TOP (20) from dbo.LokalDBKohaSinkronizimit
WHERE Data in ( SELECT Data FROM dbo.LokalDBKohaSinkronizimit 
				GROUP BY Data
				having (SELECT COUNT(*) 
				FROM dbo.LokalDBKohaSinkronizimit  with(nolock)) > 50)

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_LokalDBKohaSinkronizimitMaxSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_LokalDBKohaSinkronizimitMaxSelect_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_LokalDBKohaSinkronizimitMaxSelect_Sp
AS
	SELECT max(Data) from LokalDBKohaSinkronizimit with(nolock)

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_LokalDBArtikujtMaxIdSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_LokalDBArtikujtMaxIdSelect_sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_LokalDBArtikujtMaxIdSelect_sp
AS
	SELECT max(Id) as Id from dbo.Artikujt  with(nolock)

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ZbritjaMeKuponInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ZbritjaMeKuponInsert_sp
			  End
Go --
Create Procedure TOSHIBA.POS_ZbritjaMeKuponInsert_sp  
(
	@POSKuponatPerZbritjeId int ,
	@Vlera decimal (18,2),
	@DaljaMallitId bigint ,
	@KodiKuponit varchar(50)
									
)
 
as begin 
Insert into dbo.POSZbritjaMeKupon 
(
POSKuponatPerZbritjeId,Vlera,DaljaMallitId ,KodiKuponit) 
Values 
( 
@POSKuponatPerZbritjeId,@Vlera,@DaljaMallitId ,@KodiKuponit) 
Select scope_identity() as Id 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSKuponiMeZbritjeApliko_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSKuponiMeZbritjeApliko_sp
			  End
Go --
Create Procedure TOSHIBA.POSKuponiMeZbritjeApliko_sp  
	@POSKuponatPerZbritjeId int
AS BEGIN

UPDATE dbo.POSKuponatPerZbritje
SET Aplikuar = 1
WHERE Id = @POSKuponatPerZbritjeId

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_KonfigurimetSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_KonfigurimetSelect_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_KonfigurimetSelect_sp  
(

@Id int  =null,
@Pershkrimi varchar (80) =null,
@Statusi bit  =null
		) 
as begin 
Select Id,Pershkrimi,Statusi , Vlera
From Konfigurimet with(nolock)
where  (Id=@Id or @Id is null ) 
and  (Pershkrimi=@Pershkrimi or @Pershkrimi is null ) 
and  (Statusi=@Statusi or @Statusi is null ) 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BarazimiArkatareveSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BarazimiArkatareveSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_BarazimiArkatareveSelect_sp
(
@Id bigint  =null,
@OrganizataId int  =null,
@OperatoriId int  =null,
@NrDokumentit int  =null,
@DataEFillimit datetime  =null,
@DataEPerfundimit datetime  =null,
@BarazuarNga int  =null,
@DataERegjistrimit datetime  =null,
@Komenti varchar (200) =null,
@Barazuar bit  =null,
@LlojiDokumentitId int  =null,
@NderrimiMbyllur bit = null,
@PershkrimiOperatorit varchar(100) = null,
--@RreshtiIIdPerSHfletim varchar(2000) = NULL,
@GjendjaFillestare DECIMAL (18,2) = NULL,
@XRaport decimal(18,2) = null,
@Data Date = null
) 
as begin 

SELECT
A.Id,
A.OrganizataId,
O.Pershkrimi PershkrimiOrganizates,
A.OperatoriId,
OP.Emri + ' ' + OP.Mbiemri PershkrimiOperatorit,
A.NrDokumentit,
A.Data,
A.DataEFillimit,
A.DataEPerfundimit,
A.BarazuarNga,
BN.Emri + ' ' + BN.Mbiemri PershkrimiBarazuarNga,
A.DataERegjistrimit,
A.NderrimiMbyllur,
A.GjendjaFillestare,
A.XRaport,
A.Komenti,
CASE WHEN A.Barazuar = 1 THEN 'Po' WHEN A.Barazuar = 0 THEN 'Jo' END Barazuar,
A.LlojiDokumentitId ,
L.Pershkrimi PershkrimiDokumentit,
			Case when ISNULL(A.Barazuar,0) = 1 then 0 else 1 End Enabled,
			ISNULL(Barazuar,0)  ReadOnly,
			Case when ISNULL(Barazuar,0) = 0 then 1 else 0 end as Ruaj,
			Case when ISNULL(Barazuar,0) = 0 then 1 else 0 end as Valido,
			Case when ISNULL(Barazuar,0) = 1 then 1 else 0 end as Printo,
			Case when ISNULL(Barazuar,0) = 1  then 1 else 0 end as Storno,
                ( CASE WHEN A.Barazuar = 1 THEN 0
                       WHEN A.Barazuar = 0 THEN 1
                 END ) Enable,				  
			 0  AS HapNderriminEnabled ,
			CASE WHEN (A.NderrimiMbyllur = 0 AND A.OperatoriId = OP.Id) THEN 1 ELSE 0 END AS MbyllNderriminEnabled,
			'Regjistruar Nga: ' + OP.Emri + ' ' + OP.Mbiemri + '  Data e Regjistrimit: ' + Convert(VARCHAR(20), A.DataERegjistrimit, 104) +  ' ' + Convert(VARCHAR(20), A.DataERegjistrimit, 8)   RegjistruarPershkrimi

From dbo.BarazimiArkatareve as A
INNER JOIN dbo.Mxh_Filialet O ON o.Id = A.OrganizataId
INNER JOIN dbo.Mxh_Operatoret OP ON OP.Id = A.OperatoriId
INNER JOIN dbo.Mxh_Operatoret BN ON BN.Id = A.BarazuarNga
INNER JOIN dbo.LlojetEDokumenteve L ON L.Id = A.LlojiDokumentitId
where  (A.Id=@Id or @Id is null ) 
--And ((A.Id in (select Id from dbo.KonvertoStringNeTabel(@RreshtiIIdPerSHfletim,','))) or @RreshtiIIdPerSHfletim is null )
and  (A.OrganizataId=@OrganizataId or @OrganizataId is null ) 
and  (A.OperatoriId=@OperatoriId or @OperatoriId is null ) 
and  (A.NrDokumentit=@NrDokumentit or @NrDokumentit is null )
and  (A.DataEFillimit=@DataEFillimit or @DataEFillimit is null ) 
and  (A.DataEPerfundimit=@DataEPerfundimit or @DataEPerfundimit is null ) 
and  (A.BarazuarNga=@BarazuarNga or @BarazuarNga is null ) 
and  (A.DataERegjistrimit=@DataERegjistrimit or @DataERegjistrimit is null ) 
and  (A.Komenti=@Komenti or @Komenti is null ) 
and  (A.Barazuar=@Barazuar or @Barazuar is null ) 
and  (A.LlojiDokumentitId=@LlojiDokumentitId or @LlojiDokumentitId is null ) 
and (A.NderrimiMbyllur = @NderrimiMbyllur or @NderrimiMbyllur is null) 
AND (A.GjendjaFillestare = @GjendjaFillestare OR @GjendjaFillestare IS NULL)
and (A.Data = @Data or @Data is null)
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp
(
@OrganizataId INT,
@OperatoriId INT,
@LlojiDokumentitId INT,
@Komenti varchar(200) = NULL,
@BarazuarNga int,
@GjendjaFillestare DECIMAL(18,2)=0
)
AS BEGIN

DECLARE @NrDokumentit INT

if exists( select 1 from BarazimiArkatareve B where OperatoriId = @OperatoriId and NderrimiMbyllur = 0) 
begin
RAISERROR('Nderrimi eshte hapur per kete operator!',11,1,'Verejtje')
return
end 


SET @NrDokumentit = isnull((SELECT MAX(NrDokumentit) FROM dbo.BarazimiArkatareve WHERE LlojiDokumentitId = 750),0) +1

DECLARE @Id BIGINT 
SET @Id = TOSHIBA.GjeneroDokumentiId(@OrganizataId,@LlojiDokumentitId,YEAR(GETDATE()),@NrDokumentit)


Insert into dbo.BarazimiArkatareve 
(
Id,OrganizataId,OperatoriId,NrDokumentit,DataEFillimit,DataEPerfundimit,BarazuarNga,DataERegjistrimit,Komenti,Barazuar,LlojiDokumentitId, NderrimiMbyllur, GjendjaFillestare, XRaport, Data ) 
Values 
( 
@Id,@OrganizataId,@OperatoriId,@NrDokumentit,getdate(),getdate(),@BarazuarNga,GETDATE(),@Komenti,0,@LlojiDokumentitId, 0, @GjendjaFillestare, 0.00,GETDATE()) 

SELECT @Id

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ZbritjaMeKuponSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ZbritjaMeKuponSelect_sp
			  End
Go --
CREATE Procedure TOSHIBA.POS_ZbritjaMeKuponSelect_sp 
(
@Id int  =null,
@POSKuponatPerZbritjeId int  =null,
@Vlera decimal (18,2) =null,
@DaljaMallitId bigint  =null
) 
as begin
Select A.Id,A.POSKuponatPerZbritjeId,A.Vlera,A.DaljaMallitId 
From dbo.POSZbritjaMeKupon as A
inner join POSKuponatPerZbritje K on K.Id = A.POSKuponatPerZbritjeId
where  (A.Id=@Id or @Id is null ) 
and  (A.POSKuponatPerZbritjeId=@POSKuponatPerZbritjeId or @POSKuponatPerZbritjeId is null ) 
and  (A.Vlera=@Vlera or @Vlera is null ) 
and  (A.DaljaMallitId=@DaljaMallitId or @DaljaMallitId is null ) 
 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSKuponatPerZbritjeSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSKuponatPerZbritjeSelect_sp
			  End
Go --
CREATE Procedure TOSHIBA.POSKuponatPerZbritjeSelect_sp 
(
@Id int  =null,
@Vlera decimal (18,2) =null,
@Aplikuar bit  =null,
@DataERegjistrimit datetime  =null,
@RegjistruarNga int  =null
		) 
as begin 
Select A.Id,A.Vlera,A.Aplikuar,A.DataERegjistrimit,A.RegjistruarNga 
From dbo.POSKuponatPerZbritje as A
where  (A.Id=@Id or @Id is null )
and  (A.Vlera=@Vlera or @Vlera is null ) 
and  (A.Aplikuar=@Aplikuar or @Aplikuar is null ) 
and  (A.DataERegjistrimit=@DataERegjistrimit or @DataERegjistrimit is null ) 
and  (A.RegjistruarNga=@RegjistruarNga or @RegjistruarNga is null ) 
 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitKrijimiIFaturesZyrtarePaBleres_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitKrijimiIFaturesZyrtarePaBleres_Sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_DaljaMallitKrijimiIFaturesZyrtarePaBleres_Sp
(	
@OrganizataId int,
@TopRreshta INT = NULL,
@NrFatures int = null ,
@Data date = null ,
@Viti int =null 
)
AS
Begin
If(@TopRreshta is not null )
Begin 
set rowcount @TopRreshta
End

SELECT   D.Id 
  ,Data
  ,D.Viti
  ,NrFatures NrDokumentit
  ,L.Pershkrimi Dokumenti
  ,Validuar
  ,round(sum(DD.Sasia*(DD.QmimiShitjes*(1-DD.Rabati/ 100.00)*(1-DD.EkstraRabati/ 100.00))),2) Vlera,
	  O.Emri + ' ' + O.Mbiemri AS Operatori
FROM dbo.DaljaMallit D with(nolock) inner join dbo.DaljaMallitDetale DD with(nolock) on D.Id=DD.DaljaMallitID 
  inner join LlojetEDokumenteve L with(nolock) on D.DokumentiId=L.Id 
  INNER JOIN dbo.Mxh_Operatoret O with(nolock) ON D.RegjistruarNga = O.Id
WHERE D.DokumentiId = 20 and 
    D.OrganizataId=@OrganizataId
	and (D.Data = @Data or @Data is null )
	and (D.NrFatures = @NrFatures or @NrFatures is null )
	and (D.Data<=convert(date,getdate()) )
group by D.Id
  ,Data
  ,NrFatures
  ,L.Pershkrimi 
  ,Validuar
  ,D.Viti,
	  O.Emri,
	  O.Mbiemri
Order by Data Desc,NrDokumentit desc 
set rowcount 0
End 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CmimorjaInsertUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CmimorjaInsertUpdate_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CmimorjaTP') 
			  begin
			  Drop  type  TOSHIBA.CmimorjaTP
			  End 
Go --
CREATE Type TOSHIBA.CmimorjaTP AS TABLE 
(
	[Id] [INT] NOT NULL,
	[OrganizataId] [INT] NOT NULL,
	[ArtikulliId] [INT] NOT NULL,
	[Stoku] [DECIMAL](18, 3) NOT NULL,
	[Tvsh] [DECIMAL](18, 2) NOT NULL,
	[QmimiIShitjes] [DECIMAL](18, 3) NOT NULL,
	[StatusiQmimitId] [INT] NULL,
	[QmimiShumice] [DECIMAL](18, 3) NULL,
	[ZbritjaAktive] [DECIMAL](18, 3) NULL,
	[CmimiPermanentPakice] [DECIMAL](18, 3) NULL,
	[CmimiPermanentShumice] [DECIMAL](18, 3) NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POS_CmimorjaInsertUpdate_sp
    (
      @CmimorjaTP TOSHIBA.CmimorjaTP READONLY 
    )
AS
    BEGIN

        UPDATE  C
        SET
                C.OrganizataId = CA.OrganizataId ,
                C.ArtikulliId = CA.ArtikulliId , 
                C.Stoku = CA.Stoku ,  
                C.Tvsh = CA.Tvsh ,
                C.QmimiIShitjes = CA.QmimiIShitjes ,
                C.StatusiQmimitId = CA.StatusiQmimitId ,
                C.QmimiShumice = CA.QmimiShumice ,
                C.ZbritjaAktive = CA.ZbritjaAktive ,
                C.CmimiPermanentPakice = CA.CmimiPermanentPakice ,
                C.CmimiPermanentShumice = CA.CmimiPermanentShumice 
        FROM    dbo.Cmimorja C
                INNER JOIN @CmimorjaTP CA ON C.OrganizataId = CA.OrganizataId AND C.ArtikulliId = CA.ArtikulliId 
		where  isnull(C.OrganizataId,0)=isnull(CA.OrganizataId,0) or 
                isnull(C.ArtikulliId,0)=isnull(CA.ArtikulliId,0) or  
                isnull(C.Stoku,0)=isnull(CA.Stoku,0) or   
                isnull(C.Tvsh,0)=isnull(CA.Tvsh,0) or 
                isnull(C.QmimiIShitjes,0)=isnull(CA.QmimiIShitjes,0) or 
                isnull(C.StatusiQmimitId,0)=isnull(CA.StatusiQmimitId,0) or 
                isnull(C.QmimiShumice,0)=isnull(CA.QmimiShumice,0) or 
                isnull(C.ZbritjaAktive,0)=isnull(CA.ZbritjaAktive,0) or 
                isnull(C.CmimiPermanentPakice,0)=isnull(CA.CmimiPermanentPakice,0) or 
                isnull(C.CmimiPermanentShumice,0)=isnull(CA.CmimiPermanentShumice,0)

        INSERT INTO dbo.Cmimorja 
        ( OrganizataId ,
          ArtikulliId ,
          Stoku ,
          Tvsh ,
          QmimiIShitjes ,
          StatusiQmimitId ,
          QmimiShumice ,
          ZbritjaAktive ,
          CmimiPermanentPakice ,
          CmimiPermanentShumice
        )
                SELECT  A.OrganizataId ,
                        A.ArtikulliId ,
                        A.Stoku ,
                        A.Tvsh ,
                        A.QmimiIShitjes ,
                        A.StatusiQmimitId ,
                        A.QmimiShumice ,
                        A.ZbritjaAktive ,
                        A.CmimiPermanentPakice ,
                        A.CmimiPermanentShumice
                FROM    @CmimorjaTP A
                        LEFT OUTER JOIN dbo.Cmimorja C ON A.OrganizataId = C.OrganizataId
                                                          AND A.ArtikulliId = C.ArtikulliId
                WHERE   C.ArtikulliId IS NULL 
    END 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArkatInsertUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArkatInsertUpdate_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ArkatTP') 
			  begin
			  Drop  type  TOSHIBA.ArkatTP
			  End 
Go --
CREATE TYPE TOSHIBA.ArkatTP as Table (
	   Id										  int NOT NULL 
      ,OrganizataId							  int NOT NULL 
      ,NrArkes								  varchar(3) NOT NULL
      ,HostName								  varchar(80) NULL
      ,FreskimIPlote							  bit NOT NULL
      ,FLinkCode								  varchar(80) NULL
      ,PGMCode								  varchar(80) NULL
      ,NumriArkesGK							  varchar(80) NULL
      ,VerzioniIArkes							  varchar(50) NULL
      ,DataERegjistrimit						  datetime NULL
      ,ShtypjaAutomatikeZRaport				  bit NULL
      ,KohaEShtypjesSeZRaportit				  datetime NULL
      ,LejoKerkiminmeEmer						  bit NOT NULL
      ,AplikocmiminMeShumiceKurarrihetPaketimi  bit NOT NULL
      ,LejoStokunNegative						  bit NOT NULL
      ,LejoZbritjenNeArke						  bit NOT NULL
      ,LejoNDerrimineCmimit					  bit NOT NULL
      ,ShtypKopjenEKuponitFiskal				  bit NOT NULL
      ,KerkoPassWordPerAplikiminEZbritjes		  bit NOT NULL
      ,LejoRabatPerTeGjitheArtikujt			  bit NOT NULL
      ,LejoZbritjeNeTotalVler					  bit NOT NULL
      ,RegjimiPunesOffline					  bit NOT NULL
	  ,IntervaliImportimitSekonda               int NOT NULL
	  ,IntervaliDergimitSekonda                 int NOT NULL
	  ,KaTeDrejtTePunojOffline				  bit NOT NULL
	  ,OperatoriAutomatikId				  int  NULL
	  ,ShteguFiskal				  VARCHAR(250)  NULL
	  ,TipiPrinterit				  VARCHAR(250)  NULL
	  ,Porti				  VARCHAR(250)  NULL
	  ,PrintonDirekt				  BIT  NULL
	  ,NeTestim		  BIT  NULL
	  ,TouchScreen		  BIT  NULL
	  ,HapPortinNjeHere		  BIT  NULL
	  ,LejoNdryshiminESasise bit NULL
	  ,LejoFshirjenEArtikujve BIT null
	  ,TransferetEnable BIT NULL
	  ,ShperblimetEnable BIT NULL
	  ,SubjektiDetaleEnable BIT NULL
	)
Go --
CREATE Procedure TOSHIBA.POS_ArkatInsertUpdate_sp  
(
	@ArkatTp TOSHIBA.ArkatTP readonly	
)
 
as begin 
 Update A set									   
       A.OrganizataId							   =B.OrganizataId
      ,A.NrArkes								   =B.NrArkes
      ,A.HostName								   =B.HostName
      ,A.FreskimIPlote						   =B.FreskimIPlote
      ,A.FLinkCode							   =B.FLinkCode
      ,A.PGMCode								   =B.PGMCode
      ,A.NumriArkesGK							   =B.NumriArkesGK
      ,A.VerzioniIArkes						   =B.VerzioniIArkes
      ,A.DataERegjistrimit					   =B.DataERegjistrimit
      ,A.ShtypjaAutomatikeZRaport				   =B.ShtypjaAutomatikeZRaport
      ,A.KohaEShtypjesSeZRaportit				   =B.KohaEShtypjesSeZRaportit
      ,A.LejoKerkiminmeEmer					   =B.LejoKerkiminmeEmer 
      ,A.AplikocmiminMeShumiceKurarrihetPaketimi =B.AplikocmiminMeShumiceKurarrihetPaketimi
      ,A.LejoStokunNegative					   =B.LejoStokunNegative 
      ,A.LejoZbritjenNeArke					   =B.LejoZbritjenNeArke
      ,A.LejoNDerrimineCmimit					   =B.LejoNDerrimineCmimit
      ,A.ShtypKopjenEKuponitFiskal			   =B.ShtypKopjenEKuponitFiskal
      ,A.KerkoPassWordPerAplikiminEZbritjes	   =B.KerkoPassWordPerAplikiminEZbritjes 
      ,A.LejoRabatPerTeGjitheArtikujt			   =B.LejoRabatPerTeGjitheArtikujt
      ,A.LejoZbritjeNeTotalVler				   =B.LejoZbritjeNeTotalVler
      ,A.RegjimiPunesOffline					   =B.RegjimiPunesOffline
	  ,A.IntervaliImportimitSekonda              =B.IntervaliImportimitSekonda
	  ,A.IntervaliDergimitSekonda                =B.IntervaliDergimitSekonda
	  ,A.KaTeDrejtTePunojOffline                =B.KaTeDrejtTePunojOffline
	  ,A.OperatoriAutomatikId                =B.OperatoriAutomatikId
	  ,A.ShteguFiskal                =B.ShteguFiskal
	  ,A.TipiPrinterit                =B.TipiPrinterit
	  ,A.Porti                =B.Porti
	  ,A.PrintonDirekt                =B.PrintonDirekt
	  ,A.NeTestim         =B.NeTestim
	  ,A.TouchScreen  = B.TouchScreen
	  ,A.HapPortinNjeHere  = B.HapPortinNjeHere
	  ,A.LejoNdryshiminESasise=B.LejoNdryshiminESasise 
	  ,A.LejoFshirjenEArtikujve=B.LejoFshirjenEArtikujve
	  ,A.TransferetEnable=B.TransferetEnable
	  ,A.ShperblimetEnable=B.ShperblimetEnable
	  ,A.SubjektiDetaleEnable=B.SubjektiDetaleEnable
From dbo.Arkat A inner join @ArkatTp B on A.OrganizataId = B.OrganizataId and A.NrArkes=B.NrArkes

insert into dbo.Arkat
      (Id
      ,OrganizataId
      ,NrArkes
      ,HostName
      ,FreskimIPlote
      ,FLinkCode
      ,PGMCode
      ,NumriArkesGK
      ,VerzioniIArkes
      ,DataERegjistrimit
      ,ShtypjaAutomatikeZRaport
      ,KohaEShtypjesSeZRaportit
      ,LejoKerkiminmeEmer
      ,AplikocmiminMeShumiceKurarrihetPaketimi
      ,LejoStokunNegative
      ,LejoZbritjenNeArke
      ,LejoNDerrimineCmimit
      ,ShtypKopjenEKuponitFiskal
      ,KerkoPassWordPerAplikiminEZbritjes
      ,LejoRabatPerTeGjitheArtikujt
      ,LejoZbritjeNeTotalVler
      ,RegjimiPunesOffline
	  ,IntervaliImportimitSekonda
	  ,IntervaliDergimitSekonda
	  ,KaTeDrejtTePunojOffline
	  ,OperatoriAutomatikId
	  ,ShteguFiskal
      ,TipiPrinterit
      ,Porti
      ,PrintonDirekt
	  ,NeTestim
	  ,TouchScreen
	  ,HapPortinNjeHere
	  ,LejoNdryshiminESasise
	  ,LejoFshirjenEArtikujve
	  ,TransferetEnable
	  ,ShperblimetEnable
	  ,SubjektiDetaleEnable
	  )
SELECT A.Id
      ,A.OrganizataId
      ,A.NrArkes
      ,A.HostName
      ,A.FreskimIPlote
      ,A.FLinkCode
      ,A.PGMCode
      ,A.NumriArkesGK
      ,A.VerzioniIArkes
      ,A.DataERegjistrimit
      ,A.ShtypjaAutomatikeZRaport
      ,A.KohaEShtypjesSeZRaportit
      ,A.LejoKerkiminmeEmer
      ,A.AplikocmiminMeShumiceKurarrihetPaketimi
      ,A.LejoStokunNegative
      ,A.LejoZbritjenNeArke
      ,A.LejoNDerrimineCmimit
      ,A.ShtypKopjenEKuponitFiskal
      ,A.KerkoPassWordPerAplikiminEZbritjes
      ,A.LejoRabatPerTeGjitheArtikujt
      ,A.LejoZbritjeNeTotalVler
      ,A.RegjimiPunesOffline
	  ,A.IntervaliDergimitSekonda
	  ,A.IntervaliImportimitSekonda
	  ,A.KaTeDrejtTePunojOffline
	  ,A.OperatoriAutomatikId
	  ,A.ShteguFiskal
      ,A.TipiPrinterit
      ,A.Porti
      ,A.PrintonDirekt
	  ,A.NeTestim
	  ,A.TouchScreen
      ,A.HapPortinNjeHere
	  ,A.LejoNdryshiminESasise
	  ,A.LejoFshirjenEArtikujve
	  ,A.TransferetEnable
	  ,A.ShperblimetEnable
	  ,A.SubjektiDetaleEnable
	  FROM @ArkatTp A left outer join dbo.Arkat B on A.OrganizataId = B.OrganizataId and A.NrArkes=B.NrArkes
	  Where B.OrganizataId is null 
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtMeLirimOrganizatatUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtMeLirimOrganizatatUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_ArtikujtMeLirimOrganizatatUpdateInsert_sp  
(

	@Id int ,
	@ArtikujtMeLirimId int ,
	@OrganizataId int ,
	@Statusi bit ,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.ArtikujtMeLirimOrganizatat with(nolock) WHERE Id = @Id)
BEGIN 
Insert into dbo.ArtikujtMeLirimOrganizatat 
(
Id,ArtikujtMeLirimId,OrganizataId,Statusi,DataERegjistrimit ) 
Values 
( 
@Id,@ArtikujtMeLirimId,@OrganizataId,@Statusi,@DataERegjistrimit ) 
 
end 
ELSE 
BEGIN 
 
Update dbo.ArtikujtMeLirimOrganizatat 
Set  
 ArtikujtMeLirimId=@ArtikujtMeLirimId,
 OrganizataId=@OrganizataId,
 Statusi=@Statusi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id   
END 

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtMeLirimUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtMeLirimUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ArtikujtMeLirimTP') 
			  begin
			  Drop  type  TOSHIBA.ArtikujtMeLirimTP
			  End 
Go --
CREATE TYPE TOSHIBA.ArtikujtMeLirimTP as Table (
	Id int NOT NULL,
	ArtikulliId int NOT NULL,
	Sasia decimal(18, 3) NOT NULL,
	Zbritja decimal(18, 3) NOT NULL,
	DataERegjistrimit datetime NOT NULL,
	RegjistruarNga int NOT NULL
	)
Go --
CREATE Procedure TOSHIBA.POS_ArtikujtMeLirimUpdateInsert_sp  
(
	@ArtikujtMeLirimTP TOSHIBA.ArtikujtMeLirimTP readonly	
)
 
as begin 
 Update A set
        A.ArtikulliId		  = B.ArtikulliId
       ,A.Sasia			  = B.Sasia
       ,A.Zbritja			  = B.Zbritja
       ,A.DataERegjistrimit = B.DataERegjistrimit
       ,A.RegjistruarNga	  = B.RegjistruarNga
      
From dbo.ArtikujtMeLirim A inner join @ArtikujtMeLirimTP B on A.id = B.id

--SET IDENTITY_INSERT dbo.ArtikujtMeLirim ON

insert into dbo.ArtikujtMeLirim
      (Id
      ,ArtikulliId
      ,Sasia
      ,Zbritja
      ,DataERegjistrimit
      ,RegjistruarNga)
SELECT Id
      ,ArtikulliId
      ,Sasia
      ,Zbritja
      ,DataERegjistrimit
      ,RegjistruarNga

  FROM @ArtikujtMeLirimTP WHERE Id NOT IN (SELECT Id FROM dbo.ArtikujtMeLirim)

end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ArtikujtTP') 
			  begin
			  Drop  type  TOSHIBA.ArtikujtTP
			  End 
Go --
CREATE Type TOSHIBA.ArtikujtTP AS TABLE (
	Id INT NOT NULL Primary Key,
	Shifra VARCHAR(15) NOT NULL,
	BrendId INT NULL,
	Pershkrimi VARCHAR(500) NOT NULL,
	PershkrimiTiketa VARCHAR(100) NOT NULL,
	PershkrimiFiskal VARCHAR(500) NOT NULL,
	LlojiIArtikullitId INT NOT NULL,
	NjesiaID TINYINT NOT NULL,
	TatimetID TINYINT NOT NULL,
	GrupiMallitID BIGINT NOT NULL,
	DoganaID TINYINT NULL,
	Pesha DECIMAL(18, 3) NOT NULL,
	Lartesia DECIMAL(18, 2) NOT NULL,
	Gjersia DECIMAL(18, 2) NOT NULL,
	Gjatesia DECIMAL(18, 2) NOT NULL,
	Paketimi DECIMAL(18, 3) NOT NULL,
	Statusi BIT NOT NULL,
	SektoriId INT NULL  ,
	ArtikulliPerbereId INT NULL,
	Grupi varchar(500) null,
	K1 INT NULL,
	K2 INT NULL,
	K3 INT NULL,
	K4 INT NULL,
	K5 INT NULL,
	IdImportimit varchar(50) NULL,
	NumriSerik VARCHAR(80) NULL,
	ShifraProdhuesit VARCHAR(200) NULL
	)
Go --
CREATE PROCEDURE TOSHIBA.POS_ArtikujtUpdateInsert_sp
(
@ArtikujtTP TOSHIBA.ArtikujtTP READONLY 
)
AS
BEGIN

Update A 
set
A.Id=S.Id,
A.Shifra=S.Shifra,
A.BrendId=S.BrendId,
A.Pershkrimi=S.Pershkrimi,
A.PershkrimiTiketa=S.PershkrimiTiketa,
A.PershkrimiFiskal=S.PershkrimiFiskal,
A.LlojiIArtikullitId=S.LlojiIArtikullitId,
A.NjesiaID=S.NjesiaID,
A.TatimetID=S.TatimetID,
A.GrupiMallitID=S.GrupiMallitID,
A.DoganaID=S.DoganaID,
A.Pesha=S.Pesha,
A.Lartesia=S.Lartesia,
A.Gjersia=S.Gjersia,
A.Gjatesia=S.Gjatesia,
A.Paketimi=S.Paketimi,
A.Statusi=S.Statusi,
A.SektoriId=S.SektoriId,
A.ArtikulliPerbereId=S.ArtikulliPerbereId,
A.Grupi=S.Grupi,
A.K1=S.K1,
A.K2=S.K2,
A.K3=S.K3,
A.K4=S.K4,
A.K5=S.K5,
A.IdImportimit =S.IdImportimit,
A.NumriSerik=S.NumriSerik,
A.ShifraProdhuesit=S.ShifraProdhuesit
From Artikujt A inner join @ArtikujtTP S on A.Id=S.Id 

INSERT INTO dbo.Artikujt 
    ( Id ,
  Shifra ,
  BrendId ,
  Pershkrimi ,
  PershkrimiTiketa ,
  PershkrimiFiskal ,
  LlojiIArtikullitId ,
  NjesiaID ,
  TatimetID ,
  GrupiMallitID ,
  DoganaID ,
  Pesha ,
  Lartesia ,
  Gjersia ,
  Gjatesia ,
  Paketimi ,
  Statusi ,
  SektoriId ,
  ArtikulliPerbereId,
  Grupi,
  K1,
  K2,
  K3,
  K4,
  K5,
  IdImportimit,
  NumriSerik,
  ShifraProdhuesit
    )
SELECT    Id ,
  Shifra ,
  BrendId ,
  Pershkrimi ,
  PershkrimiTiketa ,
  PershkrimiFiskal ,
  LlojiIArtikullitId ,
  NjesiaID ,
  TatimetID ,
  GrupiMallitID ,
  DoganaID ,
  Pesha ,
  Lartesia ,
  Gjersia ,
  Gjatesia ,
  Paketimi ,
  Statusi ,
  SektoriId ,
  ArtikulliPerbereId,
  Grupi,
  K1,
  K2,
  K3,
  K4,
  K5,
  IdImportimit,
  NumriSerik,
  ShifraProdhuesit
FROM @ArtikujtTP WHERE Id NOT IN (SELECT Id FROM dbo.Artikujt)
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BarkodatUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BarkodatUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'BarkodatTP') 
			  begin
			  Drop  type  TOSHIBA.BarkodatTP
			  End 
Go --
CREATE Type TOSHIBA.BarkodatTP AS TABLE 
(
	Id INT NOT NULL PRIMARY KEY ,
	ArtikulliId INT NOT NULL,
	Barkodi VARCHAR(30) NOT NULL,
	SasiaPako SMALLINT NOT NULL,
	DataERegjistrimit DATETIME NOT NULL,
	Cmimi DECIMAL(18,3) NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POS_BarkodatUpdateInsert_sp
(
@BarkodatTP TOSHIBA.BarkodatTP READONLY 
)
AS
BEGIN

declare @Id int = 0
set @Id=isnull((select Max(Id) from Barkodat ),0)+7

Delete A 
From dbo.Barkodat A inner join @BarkodatTP S on A.Barkodi=S.Barkodi
where S.Barkodi is not null and S.Barkodi<>''
and A.Barkodi is not null and A.Barkodi<>''

INSERT INTO dbo.Barkodat 
    ( 
Id ,
  ArtikulliId ,
  Barkodi ,
  SasiaPako ,
  DataERegjistrimit,
  Cmimi
    ) 
SELECT 
  ROW_NUMBER() OVER (ORDER BY Id ) + @Id Id ,
  ArtikulliId ,
  Barkodi ,
  SasiaPako ,
  DataERegjistrimit,
  Cmimi
FROM @BarkodatTP 

End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BrendetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BrendetUpdateInsert_sp
			  End
Go --

CREATE PROCEDURE TOSHIBA.POS_BrendetUpdateInsert_sp
( 
	@Id int ,
	@Pershkrimi varchar (30),
	@DataERegjistrimit datetime 
)
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Brendet with(nolock) WHERE Id = @id)
BEGIN 
Insert into dbo.Brendet 
(Id,Pershkrimi,DataERegjistrimit) 
Values 
(@Id,@Pershkrimi,@DataERegjistrimit)  
end 
ELSE
BEGIN 
Update dbo.Brendet 
Set  
 Pershkrimi=@Pershkrimi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
Select scope_identity() as id 
end
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CCPLlojetEKartelaveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CCPLlojetEKartelaveUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_CCPLlojetEKartelaveUpdateInsert_sp  
( 
	@Id int ,
	@Pershkrimi varchar (50),
	@TipiKarteles varchar (30),
	@Statusi bit ,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.CCPLlojetEKartelave with(nolock) WHERE Id = @Id)
BEGIN

Insert into dbo.CCPLlojetEKartelave 
(
Id,Pershkrimi,TipiKarteles,Statusi,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@TipiKarteles,@Statusi,@DataERegjistrimit ) 

end 
ELSE
BEGIN
 
Update dbo.CCPLlojetEKartelave 
Set  
 Pershkrimi=@Pershkrimi,
 TipiKarteles=@TipiKarteles,
 Statusi=@Statusi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id  
END

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CCPZbritjenNeBonusSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_CCPZbritjenNeBonusSelect_sp
			  End
Go --
CREATE Procedure TOSHIBA.POS_CCPZbritjenNeBonusSelect_sp  
(
 @CCPKompaniaId INT  ,
 @VleraFatures decimal(18,3)
)
WITH RECOMPILE
AS 

begin

DECLARE @Saldo DECIMAL(18,2),@SaldoPer3euro DECIMAL(18,2), @Vlera DECIMAL(18,2), @Zbritja DECIMAL(18,2)

select @Saldo=ISNULL(sum(dd.Sasia*(dd.QmimiShitjes*(1.00-dd.Rabati/100.00)*(1.00-dd.EkstraRabati/100.00))),0)
from dbo.CCPFaturat C with(nolock) inner join dbo.DaljaMallitDetale dd with(nolock) on c.DaljaMallitId=dd.DaljaMallitID
where c.CCPKompaniaId=@CCPKompaniaId

select @SaldoPer3euro=ISNULL(sum(dd.Sasia*(dd.QmimiShitjes)),0)
from dbo.CCPFaturat C with(nolock) inner join dbo.DaljaMallitDetale dd with(nolock) on c.DaljaMallitId=dd.DaljaMallitID
where c.CCPKompaniaId=@CCPKompaniaId

SET @Vlera = @Saldo + @VleraFatures
    
DECLARE @TotalVlera DECIMAL(18,6)=@Saldo+@VleraFatures,@BonusiFiks decimal(18,2)=0


if (@SaldoPer3euro<100 and (@Saldo+@VleraFatures)>=100)
begin
set @BonusiFiks=3
end
else
begin
set @BonusiFiks=0
end
 
  declare @AplikoZbritjenEuroPrejtabeles bit,@ZbritjaEuro decimal(18,2),@ZbritjaPerqindje decimal(18,2)
  set @AplikoZbritjenEuroPrejtabeles=isnull((select Statusi from Konfigurimet where id=193),0)
  select @ZbritjaEuro=ZbritjaNeArkeVlere,@ZbritjaPerqindje=ZbritjaNeArke from CCPKompanite where id=@CCPKompaniaId


  DECLARE @Radha INT = 1, @rangu INT = 6
  DECLARE @VleraEZbritur DECIMAL(18,2)
 
  --IF((@Saldo+@VleraFatures )>= 200)
  --BEGIN
	 --SET @rangu = 1
  --end
IF @Saldo<200 AND (@Saldo+@VleraFatures)>200
BEGIN
SET @Saldo=200
END
 
  IF(@Saldo >= 200 AND @Saldo < 500)
  BEGIN
	 SET @rangu = 1
  end
    ELSE IF(@Saldo >= 500 AND @Saldo < 1000)
  BEGIN
	 SET @rangu = 2
  END
    ELSE IF(@Saldo >= 1000 AND @Saldo < 2000)
  BEGIN
	 SET @rangu = 3
  END
    ELSE IF(@Saldo >= 2000 AND @Saldo < 5000)
  BEGIN
	 SET @rangu = 4
  END
   ELSE if (@Saldo >= 5000)
  BEGIN
	 SET @rangu = 5
  END

DECLARE @VleraERangut DECIMAL(18,2)



SET @VleraERangut=0

  WHILE @rangu <= 5
  BEGIN

		   IF(@rangu=1)
		  BEGIN  
			  IF @Vlera>=500
			  BEGIN
			   SET @VleraERangut =@VleraERangut+(500.00-@Saldo)*1.00/100.00
			   SET @Saldo=500
			  END
			  ELSE
			  BEGIN
			  SET @VleraERangut =@VleraERangut+((@Vlera-@Saldo)*(1.00/100.00))
			  break
			  end
		  END 
		  ELSE if (@rangu=2)
		  BEGIN
			 IF @Vlera>=1000
			  BEGIN
			   SET @VleraERangut =@VleraERangut+(1000.00-@Saldo)*2.00/100.00
				 SET @Saldo=1000
			  END
			  ELSE
			  BEGIN
			  SET @VleraERangut =@VleraERangut+((@Vlera-@Saldo)*2.00/100.00)
			  break
			  end
		  end
			ELSE IF(@rangu=3)
		  BEGIN
			 IF @Vlera>=2000
			  BEGIN
			   SET @VleraERangut =@VleraERangut+((2000.00-@Saldo)*3.00/100.00)
				 SET @Saldo=2000
			  END
			  ELSE
			  BEGIN
			  SET @VleraERangut =@VleraERangut+((@Vlera-@Saldo)*3.00/100.00)
			  break
			  end
		  END
			ELSE IF(@rangu=4)
		  BEGIN
			  IF @Vlera>=5000
			  BEGIN
			   SET @VleraERangut =@VleraERangut+((5000.00-@Saldo)*4.00/100.00)
				 SET @Saldo=5000
			  END
			  ELSE
			  BEGIN
			  SET @VleraERangut =@VleraERangut+((@Vlera-@Saldo)*4.00/100.00)
			  break
			  end
		  END
			ELSE IF(@rangu=5)
		  BEGIN
			 IF @Vlera>=5000
			  BEGIN
			   SET @VleraERangut =@VleraERangut+((@Vlera-@Saldo)*5.00/100.00   )
			   break
			  END	
		  END

		   SET @rangu = @rangu + 1
  END

   
SELECT CASE  when @AplikoZbritjenEuroPrejtabeles=1 then  @VleraERangut+@BonusiFiks else @ZbritjaEuro  end ZbritjaNeVlere,  @ZbritjaPerqindje TotalRabati


end  
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DepartamentetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DepartamentetUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_DepartamentetUpdateInsert_sp  
(
	@Id int ,
	@Pershkrimi varchar (50),
	@Plani image ,
	@DataERegjistrimit datetime 	
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Departamentet with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.Departamentet 
(
Id,Pershkrimi,Plani,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Plani,@DataERegjistrimit ) 
Select scope_identity() as Id 
end 
ELSE
BEGIN  
Update dbo.Departamentet 
Set  
 Pershkrimi=@Pershkrimi,
 Plani=@Plani,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DoganaUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DoganaUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_DoganaUpdateInsert_sp  
(
	@Id tinyint ,
	@Pershkrimi varchar (50),
	@Vlera decimal (18,2),
	@Statusi bit ,
	@DataERegjistrimit datetime 					
)
 
AS
begin 

IF NOT EXISTS (SELECT 1 FROM dbo.Dogana with(nolock) WHERE Id = @Id)
BEGIN 
Insert into dbo.Dogana 
(
Id,Pershkrimi,Vlera,Statusi,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Vlera,@Statusi,@DataERegjistrimit ) 
Select scope_identity() as Id 
end 
ELSE 
BEGIN 
 
  
Update dbo.Dogana 
Set  
 Pershkrimi=@Pershkrimi,
 Vlera=@Vlera,
 Statusi=@Statusi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id  
end
 
 END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_GrupetEMallraveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_GrupetEMallraveUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'GrupetEMallraveTP') 
			  begin
			  Drop  type  TOSHIBA.GrupetEMallraveTP
			  End 
Go --
CREATE Type TOSHIBA.GrupetEMallraveTP AS TABLE 
(
	Id BIGINT  NOT NULL PRIMARY KEY,
	Shifra VARCHAR(100) NOT NULL,
	Pershkrimi VARCHAR(200) NOT NULL,
	Shkalla VARCHAR(100) NULL,
	Statusi BIT NULL ,
	FushaIdentifikuese VARCHAR(100) NULL,
	DataERegjistrimit DATETIME  NOT NULL 
)
Go --
CREATE PROCEDURE TOSHIBA.POS_GrupetEMallraveUpdateInsert_sp
(
@GrupetEMallraveTP TOSHIBA.GrupetEMallraveTP READONLY 
)
AS
BEGIN
Update A 
set 
A.Shifra=S.Shifra,
A.Pershkrimi=S.Pershkrimi,
A.Shkalla=S.Shkalla,
A.Statusi=S.Statusi,
A.FushaIdentifikuese=S.FushaIdentifikuese,
A.DataERegjistrimit=S.DataERegjistrimit
From dbo.GrupetEMallrave A inner join @GrupetEMallraveTP S on A.Id=S.Id 

if (Convert(bit,(SELECT OBJECTPROPERTY(object_id('GrupetEMallrave'), 'TableHasIdentity')))=1)
Begin
SET IDENTITY_INSERT dbo.GrupetEMallrave off  
End
INSERT INTO dbo.GrupetEMallrave 
    ( Id,Shifra ,
  Pershkrimi ,
  Shkalla ,
  Statusi ,
  FushaIdentifikuese ,
  DataERegjistrimit
    )
SELECT 
	Id,Shifra ,
  Pershkrimi ,
  Shkalla ,
  Statusi ,
  FushaIdentifikuese ,
  DataERegjistrimit
FROM @GrupetEMallraveTP WHERE Id NOT IN (SELECT Id FROM dbo.GrupetEMallrave)
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_KonfigurimetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_KonfigurimetUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_KonfigurimetUpdateInsert_sp  
(

	@Id int ,
	@Pershkrimi varchar (1000),
	@Statusi bit ,
	@Tipi varchar (100)=null,
	@Vlera varchar (500)=null,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Konfigurimet with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.Konfigurimet 
(
Id,Pershkrimi,Statusi,Tipi,Vlera,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Statusi,@Tipi,@Vlera,@DataERegjistrimit ) 


end 
ELSE 
BEGIN
 
Update dbo.Konfigurimet 
Set 
 Pershkrimi=@Pershkrimi,
 Statusi=@Statusi,
 Tipi=@Tipi,
 Vlera=@Vlera,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id  
END
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_LlojetEArtikullitUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_LlojetEArtikullitUpdateInsert_sp
			  End
Go --
CREATE Procedure TOSHIBA.POS_LlojetEArtikullitUpdateInsert_sp  
( 
	@Id int ,
	@Pershkrimi varchar (70),
	@Statusi bit ,
	@LejonStokunNegative bit ,
	@DataERegjistrimit datetime 
									
)
 
as begin 

IF NOT EXISTS(SELECT 1 FROM dbo.LlojetEArtikullit with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.LlojetEArtikullit (Id,Pershkrimi,Statusi,LejonStokunNegative,DataERegjistrimit ) 
Values (@Id,@Pershkrimi,@Statusi,@LejonStokunNegative,@DataERegjistrimit ) 

end 
ELSE 
BEGIN 
 
UPDATE dbo.LlojetEArtikullit 
SET  Pershkrimi=@Pershkrimi,
	 Statusi=@Statusi,
	 LejonStokunNegative=@LejonStokunNegative,
	 DataERegjistrimit=@DataERegjistrimit 
WHERE Id=@Id 

END

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_LlojetEDokumenteveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_LlojetEDokumenteveUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_LlojetEDokumenteveUpdateInsert_sp  
( 
	@Id int ,
	@Pershkrimi varchar (80),
	@Tipi varchar (20),
	@PrindiID int ,
	@Shkurtesa varchar (6)=null,
	@Shenja int ,
	@DokumentIJashtem bit ,
	@Tatimi decimal (18,2),
	@DataERegjistrimit datetime ,
	@TrackingTipi varchar (30)=null
									
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.LlojetEDokumenteve with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.LlojetEDokumenteve 
(
Id,Pershkrimi,Tipi,PrindiID,Shkurtesa,Shenja,DokumentIJashtem,Tatimi,DataERegjistrimit,TrackingTipi ) 
Values 
( 
@Id,@Pershkrimi,@Tipi,@PrindiID,@Shkurtesa,@Shenja,@DokumentIJashtem,@Tatimi,@DataERegjistrimit,@TrackingTipi ) 

end 
ELSE 
BEGIN   

Update dbo.LlojetEDokumenteve 
Set 
 Pershkrimi=@Pershkrimi,
 Tipi=@Tipi,
 PrindiID=@PrindiID,
 Shkurtesa=@Shkurtesa,
 Shenja=@Shenja,
 DokumentIJashtem=@DokumentIJashtem,
 Tatimi=@Tatimi,
 DataERegjistrimit=@DataERegjistrimit,
 TrackingTipi=@TrackingTipi 
where Id=@Id 
END

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_MenyratEPagesesUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_MenyratEPagesesUpdateInsert_sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_MenyratEPagesesUpdateInsert_sp
( 
	@Id int ,
	@Shkurtesa varchar (15),
	@Pershkrimi varchar (40),
	@Provizioni decimal (18,2),
	@Tipi varchar (20)=null,
	@Renditja int ,
	@PershkrimiAnglisht varchar (40)=null,
	@DataERegjistrimit datetime  ,
	@ParaqitetNePos bit =null,
	@PagesMeBonus bit
)
 
as begin 
if(@ParaqitetNePos is null )
begin
set @ParaqitetNePos = 1;
end 

IF NOT EXISTS(SELECT 1 FROM dbo.MenyratEPageses with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.MenyratEPageses 
(
Id,Shkurtesa,Pershkrimi,Provizioni,Tipi,Renditja,PershkrimiAnglisht,DataERegjistrimit,ParaqitetNePos,PagesMeBonus ) 
Values 
( 
@Id,@Shkurtesa,@Pershkrimi,@Provizioni,@Tipi,@Renditja,@PershkrimiAnglisht,@DataERegjistrimit,isnull(@ParaqitetNePos,1),@PagesMeBonus ) 

end 
ELSE
BEGIN
 
Update dbo.MenyratEPageses 
Set 
 Shkurtesa=@Shkurtesa,
 Pershkrimi=@Pershkrimi,
 Provizioni=@Provizioni,
 Tipi=@Tipi,
 Renditja=@Renditja,
 PershkrimiAnglisht=@PershkrimiAnglisht,
 DataERegjistrimit=@DataERegjistrimit ,
 ParaqitetNePos=@ParaqitetNePos,
 PagesMeBonus= @PagesMeBonus
where Id=@Id 
END

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_FilialetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POS_Mxh_FilialetUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_Mxh_FilialetUpdateInsert_sp  
( 
	@Id int ,
	@Pershkrimi varchar (100),
	@OrganizataId int ,
	@Tipi int ,
	@EmriServerit varchar (50)=null,
	@EmriDatabazes varchar (50)=null,
	@Statusi bit ,
	@FilialaVepruese bit ,
	@LinkServeri varchar (50)=null,
	@Sinkronizohet bit ,
	@SinkronizohetNga int ,
	@Lloji char (1)=null,
	@MundesoTavolinatEHapura bit ,
	@StatusiProjektit bit ,
	@TvshPerProjekt decimal (18,2),
	@DataERegjistrimit datetime ,
	@KaCmime bit ,
	@PrefixNeNrTeFatures varchar (50)=null,
	@Renditja int ,
	@PershkrimiShkurter varchar (25)=null ,
	@Logo image=null,
	@LogoBardhEZi image,
	@PrindiId varchar(100)=null,
	@NrTerminalit varchar(255) = null

)
 
as begin 
IF NOT EXISTS(SELECT id FROM dbo.Mxh_Filialet with(nolock) WHERE Id = @Id)
BEGIN 
Insert into dbo.Mxh_Filialet 
(
Id,Pershkrimi,OrganizataId,Tipi,EmriServerit,EmriDatabazes,Statusi,FilialaVepruese,LinkServeri,Sinkronizohet
,SinkronizohetNga,Lloji,MundesoTavolinatEHapura,StatusiProjektit,TvshPerProjekt,DataERegjistrimit,KaCmime
,PrefixNeNrTeFatures,Renditja,PershkrimiShkurter,Logo,LogoBardhEZi,PrindiId,NrTerminalit) 
Values 
( 
@Id,@Pershkrimi,@OrganizataId,@Tipi,@EmriServerit,@EmriDatabazes,@Statusi,@FilialaVepruese,@LinkServeri,@Sinkronizohet
,@SinkronizohetNga,@Lloji,@MundesoTavolinatEHapura,@StatusiProjektit,@TvshPerProjekt,@DataERegjistrimit,@KaCmime
,@PrefixNeNrTeFatures,@Renditja,@PershkrimiShkurter,@Logo,@LogoBardhEZi,@PrindiId,@NrTerminalit) 
Select scope_identity() as Id 
end 
ELSE
BEGIN
 
Update dbo.Mxh_Filialet 
Set  
 Pershkrimi=@Pershkrimi,
 OrganizataId=@OrganizataId,
 Tipi=@Tipi,
 EmriServerit=@EmriServerit,
 EmriDatabazes=@EmriDatabazes,
 Statusi=@Statusi,
 FilialaVepruese=@FilialaVepruese,
 LinkServeri=@LinkServeri,
 Sinkronizohet=@Sinkronizohet,
 SinkronizohetNga=@SinkronizohetNga,
 Lloji=@Lloji,
 MundesoTavolinatEHapura=@MundesoTavolinatEHapura,
 StatusiProjektit=@StatusiProjektit,
 TvshPerProjekt=@TvshPerProjekt,
 DataERegjistrimit=@DataERegjistrimit,
 KaCmime=@KaCmime,
 PrefixNeNrTeFatures=@PrefixNeNrTeFatures,
 Renditja=@Renditja,
 PershkrimiShkurter=@PershkrimiShkurter ,
 Logo=@Logo,
 LogoBardhEZi=@LogoBardhEZi,
 PrindiId=@PrindiId,
 NrTerminalit = @NrTerminalit
where Id=@Id 
END

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_GrupetEShfrytezuesveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_Mxh_GrupetEShfrytezuesveUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_Mxh_GrupetEShfrytezuesveUpdateInsert_sp  
(	@Id int ,
	@Pershkrimi varchar (50),
	@Tipi varchar (10)=null,
	@DataERegjistrimit datetime 							
)
 
as begin 

IF NOT exists(SELECT 1 FROM dbo.Mxh_GrupetEShfrytezuesve with(nolock) WHERE Id = @Id)
BEGIN


Insert into dbo.Mxh_GrupetEShfrytezuesve 
(
Id,Pershkrimi,Tipi,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Tipi,@DataERegjistrimit ) 

end 
ELSE
BEGIN  

Update dbo.Mxh_GrupetEShfrytezuesve 
Set  
 Pershkrimi=@Pershkrimi,
 Tipi=@Tipi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_OperatoretUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_Mxh_OperatoretUpdateInsert_sp
			  End
Go --
			  CREATE PROCEDURE TOSHIBA.POS_Mxh_OperatoretUpdateInsert_sp
(
	@Id int ,
	@Emri varchar (50),
	@Mbiemri varchar (50),
	@GrupiId int ,
	@Operatori varchar (100)=null,
	@Statusi bit ,
	@OrganizataId int ,
	@Pass varchar (100)=null,
	@DataEKrijimit datetime ,
	@Email varchar (100)=null,
	@FjalekalimiPerZbritje varchar (100)=null,
	@ShifraOperatorit varchar (100)=null,
	@DataERegjistrimit datetime ,
	@HapjaEDokumenteve bit ,
	@SektoriId int ,
	@Tel varchar(250)=null				
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Mxh_Operatoret with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.Mxh_Operatoret 
(
Id,Emri,Mbiemri,GrupiId,Operatori,Statusi,OrganizataId,Pass,DataEKrijimit,Email,FjalekalimiPerZbritje,ShifraOperatorit,DataERegjistrimit,HapjaEDokumenteve,SektoriId,Tel ) 
Values 
( 
@Id,@Emri,@Mbiemri,@GrupiId,@Operatori,@Statusi,@OrganizataId,@Pass,@DataEKrijimit,@Email,@FjalekalimiPerZbritje,@ShifraOperatorit,@DataERegjistrimit,@HapjaEDokumenteve,@SektoriId,@Tel ) 

end 
ELSE
BEGIN  

Update dbo.Mxh_Operatoret 
Set 
 Emri=@Emri,
 Mbiemri=@Mbiemri,
 GrupiId=@GrupiId,
 Operatori=@Operatori,
 Statusi=@Statusi,
 OrganizataId=@OrganizataId,
 Pass=@Pass,
 DataEKrijimit=@DataEKrijimit,
 Email=@Email,
 FjalekalimiPerZbritje=@FjalekalimiPerZbritje,
 ShifraOperatorit=@ShifraOperatorit,
 DataERegjistrimit=@DataERegjistrimit,
 HapjaEDokumenteve=@HapjaEDokumenteve,
 SektoriId=@SektoriId ,
 Tel=@Tel
where Id=@Id 

END

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_OrganizataDetaletUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_Mxh_OrganizataDetaletUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_Mxh_OrganizataDetaletUpdateInsert_sp  
(

	@Id int ,
	@Gjuha varchar (2),
	@Email varchar (100)=null,
	@Smtp varchar (50)=null,
	@Porti varchar (50)=null,
	@UserName varchar (50)=null,
	@Pass varchar (200)=null,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT exists(SELECT 1 FROM dbo.Mxh_OrganizataDetalet with(nolock) WHERE Id = @Id)
BEGIN 
Insert into dbo.Mxh_OrganizataDetalet 
(
Id,Gjuha,Email,Smtp,Porti,UserName,Pass,DataERegjistrimit ) 
Values 
( 
@Id,@Gjuha,@Email,@Smtp,@Porti,@UserName,@Pass,@DataERegjistrimit ) 

end 
ELSE
BEGIN  
Update dbo.Mxh_OrganizataDetalet 
Set  
 Gjuha=@Gjuha,
 Email=@Email,
 Smtp=@Smtp,
 Porti=@Porti,
 UserName=@UserName,
 Pass=@Pass,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id  
END 

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_NjesitUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_NjesitUpdateInsert_sp
			  End
Go --

CREATE Procedure TOSHIBA.POS_NjesitUpdateInsert_sp  
( 
	@Id tinyint ,
	@Pershkrimi varchar (50),
	@Njesia varchar (10),
	@DataERegjistrimit datetime  						
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Njesit with(nolock) WHERE Id = @id)
BEGIN 
Insert into dbo.Njesit 
(
Id,Pershkrimi,Njesia,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Njesia,@DataERegjistrimit )  
end 
ELSE 
BEGIN  
Update dbo.Njesit 
Set 
 Pershkrimi=@Pershkrimi,
 Njesia=@Njesia,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_PeshoretUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_PeshoretUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_PeshoretUpdateInsert_sp  
( 
	@Id int ,
	@Shifra int ,
	@Pershkrimi varchar (70),
	@Statusi bit ,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS (SELECT 1 FROM dbo.Peshoret with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.Peshoret 
(
Id,Shifra,Pershkrimi,Statusi,DataERegjistrimit ) 
Values 
( 
@Id,@Shifra,@Pershkrimi,@Statusi,@DataERegjistrimit ) 

end 
ELSE
BEGIN 
 
Update dbo.Peshoret 
Set  
 Shifra=@Shifra,
 Pershkrimi=@Pershkrimi,
 Statusi=@Statusi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

end


Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_PlaniZbritjeveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POS_PlaniZbritjeveUpdateInsert_sp
			  End

Go --
			  CREATE Procedure TOSHIBA.POS_PlaniZbritjeveUpdateInsert_sp  
(

	@Id int ,
	@DataPrej date,
	@DataDeri date,
	@ZbritjaPerqindje decimal (18,2),
	@RegjistruarNga int ,
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS(SELECT id FROM dbo.POS_PlaniZbritjeve with(nolock) WHERE Id = @Id)
BEGIN 

Insert into dbo.POS_PlaniZbritjeve 
(
Id,DataPrej,DataDeri,ZbritjaPerqindje,RegjistruarNga,DataERegjistrimit ) 
Values 
( 
@Id,@DataPrej,@DataDeri,@ZbritjaPerqindje,@RegjistruarNga,@DataERegjistrimit ) 

end 
ELSE 
BEGIN
 
Update dbo.POS_PlaniZbritjeve 
Set  
 DataPrej=@DataPrej,
 DataDeri=@DataDeri,
 ZbritjaPerqindje=@ZbritjaPerqindje,
 RegjistruarNga=@RegjistruarNga,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id  
END

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_PLUPeshoretUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_PLUPeshoretUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'PLUPeshoretTP') 
			  begin
			  Drop  type  TOSHIBA.PLUPeshoretTP
			  End 
Go --
CREATE TYPE TOSHIBA.PLUPeshoretTP as Table (
	Id bigint NOT NULL,
	PeshoretId int NOT NULL,
	ArtikulliId int NOT NULL,
	PLU varchar(50) NOT NULL,
	AfatiDite int NULL,
	DataERegjistrimit datetime NOT NULL
	)
Go --
CREATE Procedure TOSHIBA.POS_PLUPeshoretUpdateInsert_sp  
(
	@PLUPeshoretTP TOSHIBA.PLUPeshoretTP readonly	
)
 
as begin 
 Update A set	
       A.PeshoretId=B.PeshoretId
      ,A.PLU= B.PLU
      ,A.AfatiDite=B.AfatiDite
      ,A.DataERegjistrimit=B.DataERegjistrimit
From dbo.PLUPeshoret A inner join @PLUPeshoretTP B on A.ArtikulliId=B.ArtikulliId

insert into dbo.PLUPeshoret
      (Id
      ,PeshoretId
      ,ArtikulliId
      ,PLU
      ,AfatiDite
      ,DataERegjistrimit
       )
SELECT Id
      ,PeshoretId
      ,ArtikulliId
      ,PLU
      ,AfatiDite
      ,DataERegjistrimit
  FROM @PLUPeshoretTP WHERE ArtikulliId NOT IN (SELECT ArtikulliId FROM dbo.PLUPeshoret)

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_StandardetEBarkodaveUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_StandardetEBarkodaveUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_StandardetEBarkodaveUpdateInsert_sp  
( 
	@Id int ,
	@Tipi varchar (20),
	@DataERegjistrimit datetime 
									
)
 
as begin 
IF NOT EXISTS	(SELECT 1 FROM dbo.StandardetEBarkodave with(nolock) WHERE Id = @id)
BEGIN 

Insert into dbo.StandardetEBarkodave 
(
Id,Tipi,DataERegjistrimit ) 
Values 
( 
@Id,@Tipi,@DataERegjistrimit ) 
Select scope_identity() as Id 
END
ELSE
BEGIN  
Update dbo.StandardetEBarkodave 
Set  
 Tipi=@Tipi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 

END 

END

Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_SubjektetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_SubjektetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'SubjektetTP') 
			  begin
			  Drop  type  TOSHIBA.SubjektetTP
			  End 
Go --
CREATE Type TOSHIBA.SubjektetTP AS TABLE 
(
	Id INT NOT NULL Primary Key,
	Shifra INT NOT NULL,
	Pershkrimi VARCHAR(200) NOT NULL,
	NRB VARCHAR(200) NULL,
	NrTVSH VARCHAR(200) NULL,
	NumriFiskal VARCHAR(200) NULL,
	Email VARCHAR(500) NULL,
	Telefoni VARCHAR(200) NULL,
	Fax VARCHAR(200) NULL,
	Teleporosia VARCHAR(200) NULL,
	Adresa VARCHAR(200) NULL,
	VendiId INT NULL,
	NumriPostar VARCHAR(200) NULL,
	Shteti VARCHAR(200) NULL,
	Komuna VARCHAR(200) NULL,
	PersoniKontaktues VARCHAR(200) NULL,
	LlojiISubjektitID INT NOT NULL,
	Koment VARCHAR(2000) NULL,
	DataERegjistrimit DATETIME NOT NULL ,
	RegjistruarNga INT NOT NULL ,
	PranimiFaturaveEmail VARCHAR(200) NULL,
	FinancatEmail VARCHAR(200) NULL,
	Statusi BIT NULL ,
	MenaxhuesiSubjektitId INT NULL,
	KategoriaId INT NULL,
	NrLicenses VARCHAR(200) NULL,
	K16 INT NULL  ,
	K17 INT NULL  ,
	K18 INT NULL  ,
	K19 INT NULL  ,
	K20 INT NULL  ,
	MenyraEPagesesId INT NULL,
	Vendi VARCHAR(200) NULL,
	LlojiISubjektit VARCHAR(200) NULL,
	DataESkadences DATETIME NULL,
	AfatiPageses INT NULL,
	AfatiPagesesShitje INT NULL,
	MenaxheriPershrimi VARCHAR(200) NULL,
	MenaxheriEmail VARCHAR(200) NULL,
	MenaxheriSubjektitTel VARCHAR(200) NULL,
	SubjektiEshteKompani BIT NULL,
	MenyraEPageses VARCHAR(200) NULL,
	PershkrimiShkurter VARCHAR(200) NOT NULL,
	Shkurtesa VARCHAR(200) NULL,
	KushtetEDergeses VARCHAR(2000) NULL,
	ReferencaEKontrates VARCHAR(200) NULL,
	[LimitiSasi] [decimal] (18, 2) NOT NULL,
	[LimitiVlere] [decimal] (18, 2) NOT NULL,
	IdImportimit varchar(50) null,
	VleraEShpenzuar decimal(18,2) null,
	SaldoFikse decimal(18,2) null,
	LimitiObligimit decimal(18,2) null,
	PrindiId INT NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POS_SubjektetUpdateInsert_sp
(
@SubjektetTP TOSHIBA.SubjektetTP READONLY 
)
AS
BEGIN
Update A 
set 
A.Shifra=S.Shifra,
A.Pershkrimi=S.Pershkrimi,
A.NRB=S.NRB,
A.NrTVSH=S.NrTVSH,
A.NumriFiskal=S.NumriFiskal,
A.Email=S.Email,
A.Telefoni=S.Telefoni,
A.Fax=S.Fax,
A.Teleporosia=S.Teleporosia,
A.Adresa=S.Adresa,
A.VendiId=S.VendiId,
A.NumriPostar=S.NumriPostar,
A.Shteti=S.Shteti,
A.Komuna=S.Komuna,
A.PersoniKontaktues=S.PersoniKontaktues,
A.LlojiISubjektitID=S.LlojiISubjektitID,
A.Koment=S.Koment,
A.DataERegjistrimit=S.DataERegjistrimit,
A.RegjistruarNga=S.RegjistruarNga,
A.PranimiFaturaveEmail=S.PranimiFaturaveEmail,
A.FinancatEmail=S.FinancatEmail,
A.Statusi=S.Statusi,
A.MenaxhuesiSubjektitId=S.MenaxhuesiSubjektitId,
A.KategoriaId=S.KategoriaId,
A.NrLicenses=S.NrLicenses,
A.K16=S.K16,
A.K17=S.K17,
A.K18=S.K18,
A.K19=S.K19,
A.K20=S.K20,
A.MenyraEPagesesId=S.MenyraEPagesesId,
A.Vendi=S.Vendi,
A.LlojiISubjektit=S.LlojiISubjektit,
A.DataESkadences=S.DataESkadences,
A.AfatiPageses=S.AfatiPageses,
A.AfatiPagesesShitje=S.AfatiPagesesShitje,
A.MenaxheriPershrimi=S.MenaxheriPershrimi,
A.MenaxheriEmail=S.MenaxheriEmail,
A.MenaxheriSubjektitTel=S.MenaxheriSubjektitTel,
A.SubjektiEshteKompani=S.SubjektiEshteKompani,
A.MenyraEPageses=S.MenyraEPageses,
A.PershkrimiShkurter=S.PershkrimiShkurter,
A.Shkurtesa=S.Shkurtesa,
A.KushtetEDergeses=S.KushtetEDergeses,
A.ReferencaEKontrates=S.ReferencaEKontrates,
A.LimitiSasi=S.LimitiSasi,
A.LimitiVlere=S.LimitiVlere,
A.IdImportimit=S.IdImportimit,
A.VleraEShpenzuar = S.VleraEShpenzuar,
A.SaldoFikse = S.SaldoFikse,
A.LimitiObligimit = S.LimitiObligimit,
A.PrindiId=S.PrindiId
From dbo.Subjektet A inner join @SubjektetTP S on A.Id=S.Id 
where 
   ISNULL(A.Shifra,0)<>isnull(S.Shifra,0)
or ISNULL(A.Pershkrimi,0)<>isnull(S.Pershkrimi,0)
or ISNULL(A.NRB,0)<>isnull(S.NRB,0)
or ISNULL(A.NrTVSH,0)<>isnull(S.NrTVSH,0)
or ISNULL(A.NumriFiskal,0)<>isnull(S.NumriFiskal,0)
or ISNULL(A.Email,0)<>isnull(S.Email,0)
or ISNULL(A.Telefoni,0)<>isnull(S.Telefoni,0)
or ISNULL(A.Fax,0)<>isnull(S.Fax,0)
or ISNULL(A.Teleporosia,0)<>isnull(S.Teleporosia,0)
or ISNULL(A.Adresa,0)<>isnull(S.Adresa,0)
or ISNULL(A.VendiId,0)<>isnull(S.VendiId,0)
or ISNULL(A.NumriPostar,0)<>isnull(S.NumriPostar,0)
or ISNULL(A.Shteti,0)<>isnull(S.Shteti,0)
or ISNULL(A.Komuna,0)<>isnull(S.Komuna,0)
or ISNULL(A.PersoniKontaktues,0)<>isnull(S.PersoniKontaktues,0)
or ISNULL(A.LlojiISubjektitID,0)<>isnull(S.LlojiISubjektitID,0)
or ISNULL(A.Koment,0)<>isnull(S.Koment,0)
or ISNULL(A.DataERegjistrimit,0)<>isnull(S.DataERegjistrimit,0)
or ISNULL(A.RegjistruarNga,0)<>isnull(S.RegjistruarNga,0)
or ISNULL(A.PranimiFaturaveEmail,0)<>isnull(S.PranimiFaturaveEmail,0)
or ISNULL(A.FinancatEmail,0)<>isnull(S.FinancatEmail,0)
or ISNULL(A.Statusi,0)<>isnull(S.Statusi,0)
or ISNULL(A.MenaxhuesiSubjektitId,0)<>isnull(S.MenaxhuesiSubjektitId,0)
or ISNULL(A.KategoriaId,0)<>isnull(S.KategoriaId,0)
or ISNULL(A.NrLicenses,0)<>isnull(S.NrLicenses,0)
or ISNULL(A.K16,0)<>isnull(S.K16,0)
or ISNULL(A.K17,0)<>isnull(S.K17,0)
or ISNULL(A.K18,0)<>isnull(S.K18,0)
or ISNULL(A.K19,0)<>isnull(S.K19,0)
or ISNULL(A.K20,0)<>isnull(S.K20,0)
or ISNULL(A.MenyraEPagesesId,0)<>isnull(S.MenyraEPagesesId,0)
or ISNULL(A.Vendi,0)<>isnull(S.Vendi,0)
or ISNULL(A.LlojiISubjektit,0)<>isnull(S.LlojiISubjektit,0)
or ISNULL(A.DataESkadences,0)<>isnull(S.DataESkadences,0)
or ISNULL(A.AfatiPageses,0)<>isnull(S.AfatiPageses,0)
or ISNULL(A.AfatiPagesesShitje,0)<>isnull(S.AfatiPagesesShitje,0)
or ISNULL(A.MenaxheriPershrimi,0)<>isnull(S.MenaxheriPershrimi,0)
or ISNULL(A.MenaxheriEmail,0)<>isnull(S.MenaxheriEmail,0)
or ISNULL(A.MenaxheriSubjektitTel,0)<>isnull(S.MenaxheriSubjektitTel,0)
or ISNULL(A.SubjektiEshteKompani,0)<>isnull(S.SubjektiEshteKompani,0)
or ISNULL(A.MenyraEPageses,0)<>isnull(S.MenyraEPageses,0)
or ISNULL(A.PershkrimiShkurter,0)<>isnull(S.PershkrimiShkurter,0)
or ISNULL(A.Shkurtesa,0)<>isnull(S.Shkurtesa,0)
or ISNULL(A.KushtetEDergeses,0)<>isnull(S.KushtetEDergeses,0)
or ISNULL(A.ReferencaEKontrates,0)<>isnull(S.ReferencaEKontrates,0)
or ISNULL(A.LimitiSasi,0)<>isnull(S.LimitiSasi,0)
or ISNULL(A.LimitiVlere,0)<>isnull(S.LimitiVlere,0)
or ISNULL(A.IdImportimit,0)<>isnull(S.IdImportimit,0)
or ISNULL(A.VleraEShpenzuar ,0)<>isnull( S.VleraEShpenzuar,0)
or ISNULL(A.SaldoFikse ,0)<>isnull( S.SaldoFikse,0)
or ISNULL(A.LimitiObligimit ,0)<>isnull( S.LimitiObligimit,0)

INSERT INTO dbo.Subjektet
    ( Id ,
  Shifra ,
  Pershkrimi ,
  NRB ,
  NrTVSH ,
  NumriFiskal ,
  Email ,
  Telefoni ,
  Fax ,
  Teleporosia ,
  Adresa ,
  VendiId ,
  NumriPostar ,
  Shteti ,
  Komuna ,
  PersoniKontaktues ,
  LlojiISubjektitID ,
  Koment ,
  DataERegjistrimit ,
  RegjistruarNga ,
  PranimiFaturaveEmail ,
  FinancatEmail ,
  Statusi ,
  MenaxhuesiSubjektitId ,
  KategoriaId ,
  NrLicenses ,
  K16 ,
  K17 ,
  K18 ,
  K19 ,
  K20 ,
  MenyraEPagesesId,
  Vendi,
  LlojiISubjektit,
  DataESkadences,
  AfatiPageses,
  AfatiPagesesShitje,
  MenaxheriPershrimi,
  MenaxheriEmail,
  MenaxheriSubjektitTel,
  SubjektiEshteKompani,
  MenyraEPageses,
  PershkrimiShkurter,
  Shkurtesa,
  KushtetEDergeses,
  ReferencaEKontrates,
  LimitiSasi,
  LimitiVlere,
  IdImportimit,
  VleraEShpenzuar,
  SaldoFikse,
  LimitiObligimit,
  PrindiId
    )
		SELECT
  Id ,
  Shifra ,
  Pershkrimi ,
  NRB ,
  NrTVSH ,
  NumriFiskal ,
  Email ,
  Telefoni ,
  Fax ,
  Teleporosia ,
  Adresa ,
  VendiId ,
  NumriPostar ,
  Shteti ,
  Komuna ,
  PersoniKontaktues ,
  LlojiISubjektitID ,
  Koment ,
  DataERegjistrimit ,
  RegjistruarNga ,
  PranimiFaturaveEmail ,
  FinancatEmail ,
  Statusi ,
  MenaxhuesiSubjektitId ,
  KategoriaId ,
  NrLicenses ,
  K16 ,
  K17 ,
  K18 ,
  K19 ,
  K20 ,
  MenyraEPagesesId,
  Vendi,
  LlojiISubjektit,
  DataESkadences,
  AfatiPageses,
  AfatiPagesesShitje,
  MenaxheriPershrimi,
  MenaxheriEmail,
  MenaxheriSubjektitTel,
  SubjektiEshteKompani,
  MenyraEPageses,
  PershkrimiShkurter,
  Shkurtesa,
  KushtetEDergeses,
  ReferencaEKontrates,
  LimitiSasi,
  LimitiVlere,
  IdImportimit,
  VleraEShpenzuar,
  SaldoFikse,
  LimitiObligimit,
  PrindiId
FROM @SubjektetTP WHERE Id NOT IN (SELECT Id FROM dbo.Subjektet with (nolock)) 
End
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_SubjektiLlojiUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_SubjektiLlojiUpdateInsert_sp
			  End
Go --
			  CREATE Procedure TOSHIBA.POS_SubjektiLlojiUpdateInsert_sp  
(
	@Id int ,
	@Shkurtesa varchar (5),
	@Pershkrimi varchar (50),
	@DataERegjistrimit datetime 	
)
 
as begin 
IF NOT EXISTS (SELECT 1 FROM SubjektiLloji with(nolock) WHERE Id = @Id)
BEGIN

Insert into dbo.SubjektiLloji 
(
Id,Shkurtesa,Pershkrimi,DataERegjistrimit ) 
Values 
( 
@Id,@Shkurtesa,@Pershkrimi,@DataERegjistrimit )  
end 
ELSE
BEGIN 
Update dbo.SubjektiLloji 
Set  
 Shkurtesa=@Shkurtesa,
 Pershkrimi=@Pershkrimi,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_TatimetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_TatimetUpdateInsert_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_TatimetUpdateInsert_sp
( 
	@Id tinyint ,
	@Pershkrimi varchar (50),
	@Vlera decimal (18,2),
	@Statusi bit ,
	@KateGoria varchar (2),
	@DataERegjistrimit datetime  	
)
 
as begin 
IF NOT EXISTS(SELECT 1 FROM dbo.Tatimet with(nolock) WHERE Id = @id)
begin
Insert into dbo.Tatimet 
(
Id,Pershkrimi,Vlera,Statusi,KateGoria,DataERegjistrimit ) 
Values 
( 
@Id,@Pershkrimi,@Vlera,@Statusi,@KateGoria,@DataERegjistrimit ) 
end 
ELSE
BEGIN 
Update dbo.Tatimet 
Set 
 Pershkrimi=@Pershkrimi,
 Vlera=@Vlera,
 Statusi=@Statusi,
 KateGoria=@KateGoria,
 DataERegjistrimit=@DataERegjistrimit 
where Id=@Id 
END

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_OrganizataSelect_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POS_OrganizataSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_OrganizataSelect_sp 
	(@FilialaIdi INT =NULL)
 
as 
begin 
 
	declare @Xhirollogarit varchar(1000)='',@OrganizataKryesore int
	select @Xhirollogarit +=B.Shkurtesa + ': ' + NrLlogaris + '  ' from SubjektiBankat SB with(nolock) inner join Bankat B with(nolock) on sb.BankaId=b.Id Where SubjektiId=@FilialaIdi
 
Select 
 FIL.Id
,Fil.Id FilialaId
,Fil.Pershkrimi Pershkrimi
,FIL.Pershkrimi PershkrimiOrganizates
,FIL.NRB
,FIL.NrTVSH
,FIL.NumriFiskal
,FIL.Email
,FIL.Telefoni
,FIL.Fax
,FIL.Teleporosia
,CASE WHEN Fil.Adresa='' THEN FIL.Adresa ELSE Fil.Adresa END Adresa
,NULL Vendi
,Fil.NumriPostar
,Fil.Shteti
,Fil.Komuna
,Fil.PersoniKontaktues
,Fil.LlojiISubjektitID
,Fil.Koment
,Fil.DataERegjistrimit
,Fil.RegjistruarNga 
,F.Logo 
,F.LogoBardhEZi LogoBardhEZi
,ORG.Logo LogoOrganizates
,ORG.LogoBardhEZi LogoBardhEZiOrganizates
,@Xhirollogarit  Xhirollogaria 
,'' SwiftCode
,'' IBAN
,Fil.Pershkrimi PershkrimiFiliales
,FIL.NumriFiskal + ' ' + FIL.NRB + ' ' + FIL.NrTVSH NIT
,FIL.NrLicenses 
,'' Qyteti,
F.NrTerminalit
From 
Subjektet FIL with(nolock) inner join 
Mxh_Filialet F with(nolock) on FIL.Id=F.Id
inner join Mxh_Filialet ORG with(nolock) on F.OrganizataId = ORG.Id
where (FIL.Id =@FilialaIdi OR @FilialaIdi IS NULL)
end 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSSubjektiBankatSelect_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POSSubjektiBankatSelect_sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[POSSubjektiBankatSelect_sp]
(
@Id int  =null,
@SubjektiId int  =null,
@BankaId int  =null,
@NrLlogaris varchar (25) = null,
@OrganizataId int  =null

) 
as begin  
  
Select A.Id
,A.SubjektiId
,A.BankaId
,EmriIBankes Emertimi
,EmriIBankes Pershkrimi
,A.NrLlogaris 
,Case when F.Id is not null then 'NJ' else 'SU' end FilialSubjekt
From SubjektiBankat A inner join Bankat B on A.BankaId = B.Id
inner join Subjektet S on A.SubjektiId = S.Id
left outer join Mxh_Filialet F on S.Id=F.Id

where  (A.Id=@Id or @Id is null ) 
and  (A.SubjektiId=@SubjektiId or @SubjektiId is null ) 
and  (A.BankaId=@BankaId or @BankaId is null ) 
and  (A.NrLlogaris=@NrLlogaris or @NrLlogaris is null )
and  (@OrganizataId is null or (SubjektiId=(select top 1 OrganizataId from Mxh_Filialet where Id=@OrganizataId)))

end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.OrganizataSelectBankat_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.OrganizataSelectBankat_sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[OrganizataSelectBankat_sp]
(@NjesiaID INT =NULL
)

as 
begin 
	DECLARE @paraqitja INT 
	SELECT B.EmriIBankes,B.Shkurtesa,isnull(b.SwiftCode,'') SwiftCode,isnull(b.IBAN,'') IBAN,sb.NrLlogaris,Renditja  
	FROM dbo.SubjektiBankat sb  with(nolock)
	INNER JOIN dbo.Bankat b with(nolock) ON b.Id = sb.BankaId
	WHERE sb.SubjektiId = @NjesiaID 
	order by Renditja asc
end
Go -- 
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_Mxh_FilialetSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_Mxh_FilialetSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_Mxh_FilialetSelect_sp
(  
	@DeriMeOren DATETIME = null,
	@Id int=null
)
 
as begin 
Select  Id ,
        Pershkrimi ,
        OrganizataId ,
        Tipi ,
        EmriServerit ,
        EmriDatabazes ,
        Statusi ,
        FilialaVepruese ,
        LinkServeri ,
        Sinkronizohet ,
        SinkronizohetNga ,
        Lloji ,
        MundesoTavolinatEHapura ,
        StatusiProjektit ,
        TvshPerProjekt ,
        DataERegjistrimit ,
        KaCmime ,
        PrefixNeNrTeFatures ,
        Renditja ,
        PershkrimiShkurter,
		Logo,
		LogoBardhEZi,
		PrindiId,
		NrTerminalit
from dbo.Mxh_Filialet with(nolock)
where 
	(@Id IS NULL OR Id=@Id)

END 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtMeLirimOrganizatatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtMeLirimOrganizatatSelect_sp
			  End
Go --
Create Procedure TOSHIBA.POS_ArtikujtMeLirimOrganizatatSelect_sp  
(
 
	@DeriMeOren datetime 
									
)
 
as begin 
 Select  Id ,
         ArtikujtMeLirimId ,
         OrganizataId ,
         Statusi ,
         DataERegjistrimit
FROM dbo.ArtikujtMeLirimOrganizatat  with(nolock)
where id in (
Select Id
From ArtikujtMeLirimOrganizatat  with(nolock)
where (DataERegjistrimit >= @DeriMeOren or @DeriMeOren is null) 
UNION ALL 
 Select Id
From dbo.Z_ArtikujtMeLirimOrganizatat with(nolock)
where (DataKoha >= @DeriMeOren or @DeriMeOren is null))
end
Go -- 
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtPaZbritjeSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtPaZbritjeSelect_sp
			  End
Go -- 
CREATE PROCEDURE TOSHIBA.POS_ArtikujtPaZbritjeSelect_sp
( 
@Id int  =null,
@ArtikulliId int  =null,
@Statusi bit  =null,
@Tipi varchar(20) = null
		) 
as begin 
Select ROW_NUMBER() OVER (ORDER BY A.Id) Nr,A.Id,A.ArtikulliId, AR.Shifra, AR.Pershkrimi, B.Pershkrimi Brendi, A.Statusi, A.Tipi
From dbo.ArtikujtPaZbritje as A
INNER JOIN Artikujt AR ON A.ArtikulliId = AR.Id
LEFT OUTER JOIN Brendet B ON AR.BrendId = B.Id
where  (A.Id=@Id or @Id is null ) 
and  (A.ArtikulliId=@ArtikulliId or @ArtikulliId is null ) 
and  (A.Statusi=@Statusi or @Statusi is null ) 
and (A.Tipi = @Tipi or @Tipi is null)
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArkatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArkatSelect_sp
			  End
Go -- 
Create Procedure TOSHIBA.POS_ArkatSelect_sp 
(
@Id int  =null,
@OrganizataId int  =null,
@NrArkes varchar (3) =null,
@HostName varchar (80) =null,
@FreskimIPlote bit  =null,
@FLinkCode varchar (80) =null,
@PGMCode varchar (80) =null,
@NumriArkesGK varchar (80) =null,
@VerzioniIArkes varchar (50) =null,
@DataERegjistrimit datetime  =null,
@ShtypjaAutomatikeZRaport bit  =null,
@KohaEShtypjesSeZRaportit datetime  =null,
@LejoKerkiminmeEmer bit  =null,
@AplikocmiminMeShumiceKurarrihetPaketimi bit  =null,
@LejoStokunNegative bit  =null,
@LejoZbritjenNeArke bit  =null,
@LejoNDerrimineCmimit bit  =null,
@IntervaliImportimitSekonda int=null,
@IntervaliDergimitSekonda int=null
		) 
as begin 
Select 
 A.Id,A.OrganizataId,A.NrArkes,A.HostName,A.FreskimIPlote,A.FLinkCode,A.PGMCode
,A.NumriArkesGK,A.VerzioniIArkes,A.DataERegjistrimit,A.ShtypjaAutomatikeZRaport,A.KohaEShtypjesSeZRaportit
,A.LejoKerkiminmeEmer,A.AplikocmiminMeShumiceKurarrihetPaketimi,A.LejoStokunNegative,A.LejoZbritjenNeArke,A.LejoNDerrimineCmimit 
,ShtypKopjenEKuponitFiskal,KerkoPassWordPerAplikiminEZbritjes
,LejoRabatPerTeGjitheArtikujt,LejoZbritjeNeTotalVler,IntervaliImportimitSekonda,IntervaliDergimitSekonda
,OperatoriAutomatikId
,RegjimiPunesOffline
,TouchScreen
,PrintonDirekt
,Porti
,TipiPrinterit
,HapPortinNjeHere
,A.LejoNdryshiminESasise,
 A.LejoFshirjenEArtikujve
,A.TransferetEnable
,A.ShperblimetEnable
,A.SubjektiDetaleEnable
From dbo.Arkat A
where  (A.Id=@Id or @Id is null ) 
and  (A.OrganizataId=@OrganizataId or @OrganizataId is null ) 
and  (A.NrArkes=@NrArkes or @NrArkes is null ) 
and  (A.HostName=@HostName or @HostName is null ) 
and  (A.FreskimIPlote=@FreskimIPlote or @FreskimIPlote is null ) 
and  (A.FLinkCode=@FLinkCode or @FLinkCode is null ) 
and  (A.PGMCode=@PGMCode or @PGMCode is null ) 
and  (A.NumriArkesGK=@NumriArkesGK or @NumriArkesGK is null ) 
and  (A.VerzioniIArkes=@VerzioniIArkes or @VerzioniIArkes is null ) 
and  (A.DataERegjistrimit=@DataERegjistrimit or @DataERegjistrimit is null ) 
and  (A.ShtypjaAutomatikeZRaport=@ShtypjaAutomatikeZRaport or @ShtypjaAutomatikeZRaport is null ) 
and  (A.KohaEShtypjesSeZRaportit=@KohaEShtypjesSeZRaportit or @KohaEShtypjesSeZRaportit is null ) 
and  (A.LejoKerkiminmeEmer=@LejoKerkiminmeEmer or @LejoKerkiminmeEmer is null ) 
and  (A.AplikocmiminMeShumiceKurarrihetPaketimi=@AplikocmiminMeShumiceKurarrihetPaketimi or @AplikocmiminMeShumiceKurarrihetPaketimi is null ) 
and  (A.LejoStokunNegative=@LejoStokunNegative or @LejoStokunNegative is null ) 
and  (A.LejoZbritjenNeArke=@LejoZbritjenNeArke or @LejoZbritjenNeArke is null ) 
and  (A.LejoNDerrimineCmimit=@LejoNDerrimineCmimit or @LejoNDerrimineCmimit is null )
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ArkatInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.ArkatInsert_sp
			  End
Go -- 
Create Procedure TOSHIBA.ArkatInsert_sp 
@OrganizataId int ,
@NrArkes varchar (3),
@HostName varchar (80)=null,
@FreskimIPlote bit =0,
@FLinkCode varchar (80)=null,
@PGMCode varchar (80)=null,
@NumriArkesGK varchar (80)=null,
@VerzioniIArkes varchar (50)=null,
@DataERegjistrimit datetime =null,
@ShtypjaAutomatikeZRaport bit =0,
@KohaEShtypjesSeZRaportit datetime =null,
@LejoKerkiminmeEmer bit =0,
@AplikocmiminMeShumiceKurarrihetPaketimi bit  =0,
@LejoStokunNegative bit =0 ,
@LejoZbritjenNeArke bit  =0,
@LejoNDerrimineCmimit bit  =0 ,
@ShtypKopjenEKuponitFiskal bit  =0 ,
@KerkoPassWordPerAplikiminEZbritjes bit  =0 ,
@LejoRabatPerTeGjitheArtikujt bit  =0 ,
@LejoZbritjeNeTotalVler bit  =0 ,
@RegjimiPunesOffline bit = 0,
@IntervaliImportimitSekonda int=null,
@IntervaliDergimitSekonda int=null,
@TouchScreen bit = null,
@HapPortinNjeHere bit = null
AS
 Begin
		declare @Id int 
		set @Id = convert(int,convert(varchar(50),@OrganizataId)+convert(varchar(50),@NrArkes))
if exists (select 1 from dbo.Arkat where Id=@Id)
Begin
Update dbo.Arkat 
Set  
 OrganizataId=@OrganizataId,
 NrArkes=@NrArkes,
 HostName=@HostName,
 FreskimIPlote=@FreskimIPlote,
 FLinkCode=@FLinkCode,
 PGMCode=@PGMCode,
 NumriArkesGK=@NumriArkesGK,
 VerzioniIArkes=@VerzioniIArkes,
 DataERegjistrimit=@DataERegjistrimit,
 ShtypjaAutomatikeZRaport=@ShtypjaAutomatikeZRaport,
 KohaEShtypjesSeZRaportit=@KohaEShtypjesSeZRaportit,
 LejoKerkiminmeEmer=@LejoKerkiminmeEmer,
 AplikocmiminMeShumiceKurarrihetPaketimi=@AplikocmiminMeShumiceKurarrihetPaketimi,
 LejoStokunNegative=@LejoStokunNegative,
 LejoZbritjenNeArke=@LejoZbritjenNeArke,
 LejoNDerrimineCmimit=@LejoNDerrimineCmimit ,
 RegjimiPunesOffline=@RegjimiPunesOffline,
 IntervaliImportimitSekonda=@IntervaliImportimitSekonda,
 IntervaliDergimitSekonda=@IntervaliDergimitSekonda,
 TouchScreen = @TouchScreen,
 HapPortinNjeHere = @HapPortinNjeHere
where Id=@Id 
End
Else
Begin
		Insert into dbo.Arkat 
		(
		Id,OrganizataId,NrArkes,HostName,FreskimIPlote,FLinkCode,PGMCode,NumriArkesGK,VerzioniIArkes,DataERegjistrimit,ShtypjaAutomatikeZRaport
		,KohaEShtypjesSeZRaportit,LejoKerkiminmeEmer,AplikocmiminMeShumiceKurarrihetPaketimi,LejoStokunNegative,LejoZbritjenNeArke
		,LejoNDerrimineCmimit,ShtypKopjenEKuponitFiskal,KerkoPassWordPerAplikiminEZbritjes
		,LejoRabatPerTeGjitheArtikujt,LejoZbritjeNeTotalVler ,RegjimiPunesOffline, IntervaliImportimitSekonda,IntervaliDergimitSekonda,TouchScreen,HapPortinNjeHere) 
		Values 
		( 
		 @Id,@OrganizataId,@NrArkes,@HostName,@FreskimIPlote,@FLinkCode,@PGMCode,@NumriArkesGK,@VerzioniIArkes,@DataERegjistrimit
		,@ShtypjaAutomatikeZRaport,@KohaEShtypjesSeZRaportit,@LejoKerkiminmeEmer,@AplikocmiminMeShumiceKurarrihetPaketimi,@LejoStokunNegative
		,@LejoZbritjenNeArke,@LejoNDerrimineCmimit,@ShtypKopjenEKuponitFiskal,@KerkoPassWordPerAplikiminEZbritjes
		,@LejoRabatPerTeGjitheArtikujt,@LejoZbritjeNeTotalVler,@RegjimiPunesOffline, @IntervaliImportimitSekonda,@IntervaliDergimitSekonda,@TouchScreen,@HapPortinNjeHere) 
End
		Select @Id as Id  
END 
Go -- 
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitVerifikoFaturenLokale_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitVerifikoFaturenLokale_Sp
			  End
Go -- 
CREATE PROCEDURE TOSHIBA.DaljaMallitVerifikoFaturenLokale_Sp
(
@OrganizataId int,
@IdLokal varchar(50)
)
AS
begin
Declare @Egziston bit=0
if exists 
	(
		SELECT IdLokal FROM dbo.DaljaMallit WHERE @IdLokal is not null and IdLokal =@IdLokal AND OrganizataId = @OrganizataId and IdLokal is not null and IdLokal <> ''
	) 

Begin
	set @Egziston=1
End
	select @Egziston
end
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitVerifikoVlerat_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitVerifikoVlerat_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_DaljaMallitVerifikoVlerat_sp
(
@DaljaMallitId BIGINT
)

AS BEGIN



DECLARE @VleraFatures DECIMAL(18,2), @VleraPageses DECIMAL(18,2)
SET @VleraFatures = (select CONVERT(decimal(18,2), SUM(DD.Sasia * ((DD.QmimiShitjes * (1 - DD.Rabati / 100))* (1 - DD.EkstraRabati / 100)))) 
from DaljaMallitDetale DD 
where DD.DaljaMallitID = @DaljaMallitId)


SET @VleraPageses = (SELECT CONVERT(decimal(18,2), SUM(EP.Vlera)) from EkzekutimiPageses EP
where EP.DaljaMallitID = @DaljaMallitId)

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_ArtikujtMeLirimKontrollo_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_ArtikujtMeLirimKontrollo_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_ArtikujtMeLirimKontrollo_Sp
    (
  @ArtikulliId INT  ,
  @OrganizataId INT ,
  @Sasia DECIMAL(18, 3)  
    )
AS
    BEGIN 
    SELECT  Id ,
    ArtikulliId ,
    Sasia ,
    Zbritja ,
    DataERegjistrimit ,
    RegjistruarNga
    FROM    dbo.ArtikujtMeLirim
    WHERE   Id IN (
    SELECT  MAX(Id) Id
    FROM    ( SELECT    A.Id ,
        A.ArtikulliId ,
        A.Sasia ,
        A.Sasia - @Sasia Dif ,
        A.Zbritja ,
        A.DataERegjistrimit ,
        A.RegjistruarNga ,
        AO.OrganizataId ,
        AO.Statusi
      FROM  dbo.ArtikujtMeLirim A
        LEFT OUTER JOIN dbo.ArtikujtMeLirimOrganizatat AO ON A.Id = AO.ArtikujtMeLirimId
      WHERE ArtikulliId = @ArtikulliId
        AND ( AO.Statusi IS NULL
          OR AO.Statusi = 1
        )
        AND ( AO.OrganizataId IS NULL
          OR AO.OrganizataId = @OrganizataId
        )
        ) GG
    WHERE   GG.Dif <= 0
    GROUP BY GG.ArtikulliId )  
    END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.FreskoKonfigurimetInsert_Sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.FreskoKonfigurimetInsert_Sp
			  End
Go --
 CREATE PROCEDURE TOSHIBA.FreskoKonfigurimetInsert_Sp
 AS
    BEGIN
        INSERT  INTO dbo.Konfigurimet
                ( Id ,
                  Pershkrimi ,
                  Statusi ,
                  Tipi ,
                  Vlera ,
                  DataERegjistrimit ,
                  Mxh_ObjektetAplikacionId
                )
                SELECT  A.Id ,
                        A.Pershkrimi ,
                        A.Statusi ,
                        A.Tipi ,
                        A.Vlera ,
                        A.DataERegjistrimit ,
                        A.Mxh_ObjektetAplikacionId
                FROM    ( ( SELECT  c.Id ,
                                    c.Pershkrimi ,
                                    c.Statusi ,
                                    c.Tipi ,
                                    c.Vlera ,
                                    c.DataERegjistrimit ,
                                    c.Mxh_ObjektetAplikacionId
                            FROM    ( VALUES --//////////////////////////////////////////////////////////////////////////////////////////////////
                                    ( 110, 'Lejo nderrimin e qmimit dhe zbritjen ne POS!', 0, 'POS', NULL, '2016-12-07 15:49:02.177', NULL),
                                    ( 111, 'Lejo nderrimin e qmimit dhe zbritjen ne POS!(Te vlera nese eshte 1 ateher kerkon fjalekalim)', 0, 'POS', '1', '2016-12-07 15:49:02.177', NULL),
                                    ( 114, 'Kerkimi i Artikullit me Emer!', 0, 'POS', NULL, '2016-12-07 15:49:02.177', NULL),
                                    ( 130, 'Apliko Çmimin me shumice ne arke nese sasia e kalon ose eshte e barabart me paketimin!', 0, 'POS', NULL, '2016-12-07 15:49:02.177', NULL),
                                    ( 144, 'Shteku i printimit te kuponave fiskal!', 1, 'POS', 'C:\Temp\', '2017-01-12 13:26:32.450', NULL),
                                    ( 158, 'Apliko Hapjen dhe mbylljen e nderrimeve ne POS', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 159, 'Dergo ne printer fiskal gjithmon artikull te ri', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 161, 'Rishtypja e Kuponave Fiskal ne POS me F9', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 162, 'Fshirja e Artikujve ne POS me fjalekalim', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 164, 'Regjimi Lokal Offline', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 165, 'Mos Zgjedhja Automatike e Menyres se Pageses Cash ne POS', 0, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 175, 'Paraqit Stokun ne POS', 1, 'POS', NULL, '2017-01-12 13:26:32.450', NULL),
                                    ( 193, 'POS Zbritja per Baumarket', 0, 'POS', NULL, '2017-04-30 20:54:19.413', NULL),
                                    ( 198, 'Ne POS lejohet ndryshimi sasise me tastier', 0, 'POS', NULL, '2017-10-20 15:27:45.573', NULL),
                                    ( 203, 'Barkodat e peshores- merr sasin me cop', 0, 'POS', NULL, '2017-10-20 15:27:45.573', NULL),
                                    ( 263, 'Ndalo Kontimin e shitjes me pakice ne POS', 0, 'POS', NULL, '2018-01-15 11:44:54.243', NULL),
                                    ( 264, 'Paraqit Shifren e Prodhuesit, Brendin dhe Numrin e katallogut ne POS', 0, 'POS', NULL, '2018-01-15 11:44:54.243', NULL),
                                    ( 265, 'Ne POS nuk punon regjimi Lokal', 0, 'POS', NULL, '2018-01-15 11:44:54.243', NULL),
                                    ( 277, 'Importimi i shenimeve cdo sa sekonda shenoni velren !', 1, 'POS', '20', '2018-03-17 18:26:00.547', NULL),
                                    ( 278, 'Dergimi i shenimeve cdo sa sekonda shenoni velren !', 1, 'POS', '20', '2018-03-17 18:26:00.547', NULL),
									( 289, 'Lejo nderrimin e regjimit te punes nga operatori!',1,'POS','20', '2018-03-17 18:26:00.547', null),
									( 292, 'Data fatures ne sinkronizim mirret ashtu si e ka ruajtu offline!',0,'POS','20','2018-03-17 18:26:00.547', null),
									( 303, 'KubitPos shtyp raportin e fletedergeses ne rast te shtypjes me kartele!',0,'POS','20','2018-03-17 18:26:00.547', null),
									( 304, 'KubitPos me F12 mbyllë pagesën automatikisht duke mar mënyrën e pagesës Kesh!',0,'POS','20','2018-03-17 18:26:00.547', null),
									( 319, 'Kubit Pompa Nese nuk vjen RFID nga microcontroller shfaqe dritaren qe te caktohet RFID',0,'POS','20','2018-03-17 18:26:00.547', null)
) c ( Id, Pershkrimi, Statusi, Tipi, Vlera, DataERegjistrimit, Mxh_ObjektetAplikacionId )
                            ) ) A
                        LEFT OUTER JOIN dbo.Konfigurimet B ON A.Id = B.Id
                WHERE   B.Id IS NULL 
    END
Go --

 IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.SubjektetSelect_sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.SubjektetSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.SubjektetSelect_sp
( 
	@Id int  =NULL,
	@Shifra varchar(50)  =NULL,
	@Pershkrimi varchar(60) =NULL,
	@NrFiskal varchar(20) = NULL
)
 
as begin 

Select  Id ,
        Shifra ,
        Pershkrimi ,
        NRB ,
        NrTVSH ,
        NumriFiskal ,
        Email ,
        Telefoni ,
        Fax ,
        Teleporosia ,
        Adresa ,
        VendiId ,
        NumriPostar ,
        Shteti ,
        Komuna ,
        PersoniKontaktues ,
        LlojiISubjektitID ,
        Koment ,
        DataERegjistrimit ,
        RegjistruarNga ,
        PranimiFaturaveEmail ,
        FinancatEmail ,
        Statusi ,
        MenaxhuesiSubjektitId ,
        KategoriaId ,
        NrLicenses ,
        K16 ,
        K17 ,
        K18 ,
        K19 ,
        K20 ,
        MenyraEPagesesId,
		Vendi,
		LlojiISubjektit,
		DataESkadences,
		AfatiPageses,
		AfatiPagesesShitje,
		MenaxheriPershrimi,
		MenaxheriEmail,
		MenaxheriSubjektitTel,
		SubjektiEshteKompani,
		MenyraEPageses,
		PershkrimiShkurter,
		Shkurtesa,
		KushtetEDergeses,
		ReferencaEKontrates,
	    LimitiSasi,
	    LimitiVlere,
		VleraEShpenzuar,
		SaldoFikse,
		LimitiObligimit
	  from dbo.Subjektet S with(nolock) 
	  where (@Id is null or S.Id=@Id)
	  and (@Shifra is null or S.Shifra=@Shifra)
      and (@Pershkrimi is null or S.Pershkrimi=@Pershkrimi)
	  and (@NrFiskal is null or S.NumriFiskal=@NrFiskal)
end
Go -- 

--IF EXISTS (SELECT * FROM sys.objects 
--			  WHERE object_id = OBJECT_ID(N'TOSHIBA.QarkullimiArtikujvePerNderriminEFunditSelect_sp')) 
--			  begin
--			  Drop  Procedure  TOSHIBA.QarkullimiArtikujvePerNderriminEFunditSelect_sp
--			  End
--Go --
--CREATE PROCEDURE TOSHIBA.QarkullimiArtikujvePerNderriminEFunditSelect_sp
--(
--@OrganizataId INT = NULL 
--)
--AS BEGIN

--				SELECT CASE WHEN GROUPING(A.Pershkrimi)=1 THEN 'Total : ' ELSE A.Pershkrimi END Artikulli , cast( SUM(B.Sasia ) AS FLOAT) Sasia 
--				FROM dbo.PMPTransaksionet B INNER JOIN dbo.Artikujt A ON B.ArtikulliId = A.Id 
--				WHERE BarazimiId is null AND (@OrganizataId is null or B.OrganizataId =@OrganizataId)
--				GROUP BY A.Pershkrimi
--				WITH ROLLUP  
--END
--Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.StoqetSelect_sp')) 
			  begin
			  Drop  Procedure  TOSHIBA.StoqetSelect_sp
			  End
Go --
Create Procedure TOSHIBA.StoqetSelect_sp 
					(
									@OrganizataId INT
									) 
									as begin 
									SELECT 
ROW_NUMBER() OVER (ORDER BY  GrupiMallit , Artikulli ) Nr,
GrupiMallit,
ArtikulliId,
CASE WHEN GROUPING(GrupiMallit)=1 and GROUPING(ArtikulliId)=1  THEN 'Total : ' ELSE Artikulli END Artikulli,
SUM(Stoku) Stoku,
MAX(QmimiIShitjes) QmimiIShitjes,
SUM(VleraShitjes) VleraShitjes 
FROM 
(
									SELECT   A.Grupi GrupiMallit
									,A.Id ArtikulliId
									,A.Pershkrimi Artikulli
									,(CAST(C.Stoku as Float)) Stoku
									,CAST(C.QmimiIShitjes AS FLOAT) QmimiIShitjes
									,CAST(C.Stoku*ISNULL(C.QmimiIShitjes ,0.00) AS FLOAT) VleraShitjes
									FROM dbo.Artikujt A  
									INNER JOIN dbo.Cmimorja C on A.Id=C.ArtikulliId and C.OrganizataId=@OrganizataId 
									WHERE C.Stoku<>0
									union all 
									SELECT distinct 
									 A.Grupi GrupiMallit
									,A.Id ArtikulliId
									, A.Pershkrimi Artikulli
									,(CAST(C.Stoku as Float)) Stoku
									,CAST(C.QmimiIShitjes AS FLOAT) QmimiIShitjes
									,CAST(C.Stoku*ISNULL(C.QmimiIShitjes ,0.00) AS FLOAT) VleraShitjes
									FROM dbo.Artikujt A  
									INNER JOIN dbo.Cmimorja C on A.Id=C.ArtikulliId and C.OrganizataId=@OrganizataId
) G
GROUP BY GROUPING SETS (( GrupiMallit,ArtikulliId,Artikulli),())
 
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitSelectReportEX_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitSelectReportEX_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitSelectReportEX_Sp
@DaljaMallitId BIGINT
AS
BEGIN  
SELECT 
S.Pershkrimi PershkrimiIBleresit,
S.Adresa BAdresa,
S.Shteti BShteti,
S.NRB BNRB,
S.NumriFiskal BNumriFiskal,
S.Email BEmail,
S.MenaxheriPershrimi MenaxheriSubjektit,
S.MenaxheriEmail MenaxheriSubjektitEmail,
S.MenaxheriSubjektitTel MenaxheriSubjektitTel,
M.Pershkrimi OPershkrimi ,
D.NumriFaturesManual NumriFaturesManual,
D.OrganizataId,
D.DokumentiId,
D.NrFatures,
D.DataERegjistrimit DataFatures,
D.DataERegjistrimit Data,
M.Pershkrimi RegjistruarNgaEmri
FROM dbo.DaljaMallit D
INNER JOIN dbo.Subjektet S ON D.SubjektiId=S.Id 
INNER JOIN dbo.Mxh_Filialet M ON D.OrganizataId = M.Id 
INNER JOIN dbo.Mxh_Operatoret O ON D.RegjistruarNga = O.Id 
WHERE D.Id=@DaljaMallitId
END 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitSelectReportDetaleEX_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitSelectReportDetaleEX_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitSelectReportDetaleEX_Sp
 @DaljaMallitId BIGINT
AS
BEGIN 

SELECT  A.Shifra ,
        A.Pershkrimi Emertimi ,
        NJ.Njesia ,
        DD.Sasia ,
        DD.QmimiShitjes ,
        DD.Sasia * ( DD.QmimiShitjes / ( 1.0 + DD.Tvsh / 100.0 ) ) VleraPaTVSH ,
        DD.Tvsh ,
        DD.Sasia * ( DD.QmimiShitjes - ( DD.QmimiShitjes / ( 1.0 + DD.Tvsh
                                                             / 100.0 ) ) ) VleraTVSH ,
        DD.Sasia * CONVERT(DECIMAL(18, 3), DD.QmimiShitjes) VleraMeTvhs
FROM    dbo.DaljaMallitDetale DD
        INNER JOIN dbo.Artikujt A ON DD.ArtikulliId = A.Id
        INNER JOIN dbo.Njesit NJ ON A.NjesiaID = NJ.Id 
WHERE DD.DaljaMallitID = @DaljaMallitId 

END 
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KonfigurimetGetId_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.KonfigurimetGetId_Sp
			  End
Go --

Create Procedure [TOSHIBA].[KonfigurimetGetId_Sp]
(  
@Id int ,  
@OrganizataId int=null  
  
  )   
as begin   
  
 if (select isnull(count(KonfigurimiId),0)  from KonfigurimetOrganizatat Where KonfigurimiId=@Id)=0  
 begin  
  Select Statusi    
  From Konfigurimet K   
  where  (Id=@Id)   
 end  
 else  
 begin  
  Select isnull(Statusi ,0)  Statusi  
  From Konfigurimet K left outer join KonfigurimetOrganizatat KO on K.Id=KO.KonfigurimiId   
  where  (Id=@Id)   
  and (KO.OrganizataId = @OrganizataId or @OrganizataId is null )  
 end  
 end
 Go --

 IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleSelectPOS_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleSelectPOS_sp
			  End
Go --

CREATE PROCEDURE [TOSHIBA].[DaljaMallitDetaleSelectPOS_sp]
 (
@Id int  =null,
@DaljaMallitID bigint  =null,
@ArtikulliId int  =null,
@NjesiaID tinyint  =null,
@Sasia decimal (18,3) =null,
@BazaFurnizuese decimal (18,5) =null,
@Kostoja decimal (18,5) =null,
@KostoMesatare decimal (18,2) =null,
@Tvsh decimal (18,2) =null,
@QmimiShitjes decimal (18,4) =null,
@Rabati decimal (18,2) =null,
@EkstraRabati decimal (18,2) =null,
@Stoku decimal (18,3) =null,
@HyrjaDetaleId int=NULL,
@ArtikujtEProdhuarId INT =NULL,
@DaljaMallitDetaleKthimiId Int = null 
 
		) 
as begin 

Declare @ShfaqetNumriSerikneVendTeBarkodit bit =0
set @ShfaqetNumriSerikneVendTeBarkodit=IsNull((select statusi from Konfigurimet where id =136),0)

Select Nr Nr,
		DD.Id,
		A.Shifra,
		Case when @ShfaqetNumriSerikneVendTeBarkodit=1 then A.NumriSerik else (Select top 1 Barkodi From Barkodat Where ArtikulliId =dd.ArtikulliId) End Barkodi,
		A.PershkrimiTiketa as Emertimi,
		A.PershkrimiFiskal as PershkrimiFiskal,
		DaljaMallitID,
		case when hd.Paketimi IS NULL then 1 when hd.Paketimi<1 THEN 1 ELSE hd.Paketimi end Paketimi,
		DD.ArtikulliId,
		DD.NjesiaID,
		NJ.Njesia as Njesia,
		NR,
		(DD.Sasia/case when hd.Paketimi IS NULL then 1 when hd.Paketimi<1 THEN 1 ELSE hd.Paketimi end) SasiaPaketave,
		DD.Sasia,
		DD.QmimiRregullt,
		DD.QmimiShumice,
		DD.QmimiShitjes,
		DD.Rabati,
		DD.EkstraRabati,
	    DD.Tvsh,
		((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00)) QmimiFinal,
		DD.Sasia*(((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00))/(1+DD.Tvsh/ 100.00)) VleraPaTvsh,
		T.Kategoria TvshKategoria,
		Convert(decimal(18,2),DD.Sasia*(((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00))-(((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00))/(1+DD.Tvsh/ 100.00)))) VleraTvsh,
		DD.Sasia*((DD.QmimiShitjes *(1-DD.Rabati / 100.00))*(1-DD.EkstraRabati/ 100.00)) VleraMeTvsh
		,CONVERT(bit,0) KaLirim
		,DD.ArtikujtEProdhuarId
		,DD.HyrjaDetaleId
		,cast(0 as bit) NdryshuarNgaShfrytezuesi
		,cast('' as varchar(50)) IdUnikeProdhimHyrje
		,IsNull(HD.DataESkadences,PDH.DataESkadences) DataESkadences
		,IsNull(HD.NumriSerik,PDH.NumriSerik) NumriSerik
		,0.00 AS SasiaMaksimale
		,DaljaMallitDetaleKthimiId
From DaljaMallitDetale DD INNER JOIN dbo.Artikujt A ON DD.ArtikulliId =A.Id INNER JOIN dbo.Njesit NJ ON A.NjesiaID =NJ.Id
INNER JOIN dbo.Tatimet T ON A.TatimetID =T.Id 
left outer join HyrjaMallitDetale HD on DD.HyrjaDetaleId =HD.Id
left outer join ProdhimiArikujtEProdhuar PDH on DD.ArtikujtEProdhuarId = PDH.Id

where  (DD.Id=@Id or @Id is null ) 
and  (DaljaMallitID=@DaljaMallitID or @DaljaMallitID is null ) 
and  (DD.ArtikulliId=@ArtikulliId or @ArtikulliId is null ) 
and  (DD.NjesiaID=@NjesiaID or @NjesiaID is null ) 
and  (DD.Sasia=@Sasia or @Sasia is null ) 
and  (DD.BazaFurnizuese=@BazaFurnizuese or @BazaFurnizuese is null ) 
and  (Kostoja=@Kostoja or @Kostoja is null ) 
and  (DD.KostoMesatare=@KostoMesatare or @KostoMesatare is null ) 
and  (DD.Tvsh=@Tvsh or @Tvsh is null ) 
and  (DD.QmimiShitjes=@QmimiShitjes or @QmimiShitjes is null ) 
and  (DD.Rabati=@Rabati or @Rabati is null ) 
and  (DD.EkstraRabati=@EkstraRabati or @EkstraRabati is null ) 
and  (DD.Stoku=@Stoku or @Stoku is null ) 
and  (dd.HyrjaDetaleId=@HyrjaDetaleId or @HyrjaDetaleId is null ) 
and  (DD.ArtikujtEProdhuarId=@ArtikujtEProdhuarId or @ArtikujtEProdhuarId is null ) 
ORDER BY DD.NR 
end
Go --

 IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitUpdate_sp
			  End
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KontrollimiIPeriudhes_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.KontrollimiIPeriudhes_Sp
			  End
Go --

CREATE PROCEDURE [TOSHIBA].[KontrollimiIPeriudhes_Sp]
@Data date ,
@Financiare bit = null,
@Mxh_ObjektiAplikacionId INT = NULL,
@OperatoriId INT = NULL
AS
Begin
Declare @Com varchar(200)='Për datën '+Convert(varchar(10),@Data,103)+' nuk lejohet regjistrimi i dokumenteve!'

	if(@Financiare is null or @Financiare=0)
		Begin
			If not exists (select 1 from FinKontrolliPeriodes where PrejDates <= @Data and DeriMeDaten >=@Data and StatusiMbyllur = 0)
				Begin
						RAISERROR (
						@Com,
						11,
						1,
						'Periudha!' )
				RETURN
				End
			End
		else
			Begin
				If not exists (select 1 from FinKontrolliPeriodes where PrejDates <= @Data and DeriMeDaten >=@Data and PjesaFinanciareMbyllur = 0)
					Begin
							RAISERROR (
							@Com,
							11,
							1,
							'Periudha!' )
					RETURN
					End
			End 
End
Go --

CREATE Procedure [TOSHIBA].[DaljaMallitUpdate_sp]  
(
	@DaljaMallitID bigint ,
	@Data date,
	@SubjektiId int ,
	@Koment varchar(1000)=null,
	@AfatiPageses INT = NULL ,
	@TavolinaId int=null,
	@NrDuditX3 varchar(50)=null,
	@DataFatures date = null ,
	@RaportDoganor bit = null ,
	@ValutaId int = null ,
	@Kursi decimal(18,8) = null,
	@FaturaKomulativeId BIGINT = NULL,
	@TrackingId INT = NULL,
	@NumriFaturesManual varchar(50) =null,
	@Validuar BIT = NULL
	--@StornuarNga INT = NULL
)
 
as begin 

if (@Data is null )
	Begin 
	set @Data=GETDATE()
	end

	execute TOSHIBA.KontrollimiIPeriudhes_Sp @Data

if exists(select statusi from Konfigurimet where id=206 and Statusi=1)
begin
	if @Data<convert(date,getdate())
	begin
	
		RAISERROR ('Nuk lejohet ndryshimi i faturave në periudhen paraprake!', -- Message text.
           11, -- Severity,
           1, -- State,
           N'Nuk lejohet ndryshimi i faturave në periudhen paraprake!')
		End
	
end

Update DaljaMallit 
set 
 Data=@Data
,SubjektiId=@SubjektiId
,Koment=@Koment
,AfatiPageses=ISNULL(@AfatiPageses,0)
,TavolinaId=@TavolinaId,
NrDuditX3=@NrDuditX3,
DataFatures=@DataFatures,
RaportDoganor=ISNULL(@RaportDoganor,0),
ValutaId=@ValutaId,
Kursi=ISNULL(@Kursi,1),
FaturaKomulativeId = @FaturaKomulativeId,
TrackingId = @TrackingId,
NumriFaturesManual=@NumriFaturesManual,
Validuar = isnull(@Validuar,0)
--StornuarNga=@StornuarNga
Where id=@DaljaMallitID

END
Go --

 IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleUpdate_sp
			  End
Go --

Create Procedure [TOSHIBA].[DaljaMallitDetaleUpdate_sp] 
(
@Id int ,
@Sasia decimal (18,3),
@QmimiShitjes decimal (18,8),
@Rabati decimal (18,2),
@EkstraRabati decimal (18,2),
@DaljaMallitDetaleKthimiId Int = null 	,
@QmimiFinal DECIMAL(18, 2) =Null	
) 
as begin 

declare @DaljaMallitID bigint
Select  @DaljaMallitID=DaljaMallitID from DaljaMallitDetale where id=@Id

if exists (select 1 from DaljaMallit WITH(NOLOCK) where Id = @DaljaMallitID and Validuar =1) 
begin
raiserror('Nuk mund ta ndryshoni dokumentin e validuar!',
11,
1,
'Vrejtje'
)
return
End


---------------------





-------------Merr Qmimi Aktual
Declare @QmimiRregullt decimal(18,4),@QmimiShumice decimal(18,4),@OrganizataId int ,@ArtikulliId Int
Select @ArtikulliId =ArtikulliId , @OrganizataId=OrganizataId From DaljaMallit D with(NoLock) inner join DaljaMallitDetale DD With(nolock) on D.Id=DD.DaljaMallitID where DD.Id= @Id
select @QmimiRregullt=QmimiIShitjes,@QmimiShumice=QmimiShumice From Cmimorja where ArtikulliId = @ArtikulliId and OrganizataId =@OrganizataId 
------------------------------

-----Pastaj vendoset sasia e re!!!
DECLARE @Stoqet TABLE  (ArtikulliId INT NOT NULL ,OrganizataId INT NOT NULL ,StokuVjeter DECIMAL(18,3) NOT NULL,StokuIRi DECIMAL(18,3) NOT NULL )
DELETE @Stoqet 
declare @ms varchar(50)
select @ArtikulliId=ArtikulliId from DaljaMallitDetale where id=@id
set @ms='Nuk lejohet rabati per artikullin ' + convert(varchar(50),(SELECT ShifraProdhuesit from Artikujt where Id = @ArtikulliId)) + '!'

if exists(Select ArtikulliId from ArtikujtPaZbritje Where ArtikulliId=@ArtikulliId)
begin
	if (IsNull(@Rabati,0.00)>0)
	begin
	raiserror(@ms,
	11,
	1,
	'Vrejtje'
	)
	return
	END
end
-----Pastaj vendoset sasia e re!!!
UPDATE  A
SET     A.Sasia = CONVERT(DECIMAL(18, 3), A.Sasia / ( B.Sasia / @Sasia )),
		A.QmimiIShitjes = C.QmimiIShitjes 
Output Deleted.ArtikulliPerberesId,@OrganizataId,Deleted.Sasia,Inserted.Sasia 
into @Stoqet
FROM    dbo.DaljaMallitArtikujtEPerber A
        INNER JOIN dbo.DaljaMallitDetale B ON A.DaljaMallitId = B.DaljaMallitID
		Inner join dbo.Cmimorja C on A.ArtikulliPerberesId = C.ArtikulliId
        AND A.ArtikulliPerberId = B.ArtikulliId
		Where B.Id=@Id and C.OrganizataId =@OrganizataId 

UPDATE DD SET 
 Sasia=@Sasia,
 QmimiShitjes=@QmimiShitjes,
 Rabati=@Rabati,
 EkstraRabati=isnull(@EkstraRabati,0),
 QmimiRregullt=@QmimiRregullt,
 QmimiShumice=ISNULL(@QmimiShumice,0.00),
 DaljaMallitDetaleKthimiId=@DaljaMallitDetaleKthimiId
output DELETED.ArtikulliId,D.OrganizataId,Deleted.Sasia,Inserted.Sasia 
into @Stoqet
FROM dbo.DaljaMallit D INNER JOIN dbo.DaljaMallitDetale DD ON DD.DaljaMallitID = D.Id 
WHERE DD.ID=@ID 


UPDATE C SET C.Stoku =C.Stoku+(StokuIRi-StokuVjeter) 
FROM @Stoqet A INNER JOIN dbo.Cmimorja C ON C.ArtikulliId = A.ArtikulliId AND C.OrganizataId = A.OrganizataId 
WHERE StokuVjeter-StokuIRi<>0 and StokuVjeter-StokuIRi<>C.Stoku 

End
Go --

 IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleDelete_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleDelete_sp
			  End
Go --

CREATE Procedure [TOSHIBA].[DaljaMallitDetaleDelete_sp] 
					(
									@Id int ) 
									as begin 

declare @DaljaMallitID bigint,@Validuar bit,@OrganizataId int 

select @DaljaMallitID=A.Id,@OrganizataId = OrganizataId ,@Validuar = Validuar   from DaljaMallit A WITH(NOLOCK)  inner join DaljaMallitDetale B WITH(NOLOCK)  
on A.Id=B.DaljamallitId where B.Id = @DaljaMallitID 

if (@Validuar =1) 
begin
raiserror('Nuk mund ta ndryshoni dokumentin e validuar!',
11,
1,
'Vrejtje'
)
return
End

Delete from DaljaMallitArtikujtEPerber where DaljaMallitDetaleId=@Id
Delete from DaljaMallitDetale   		Where  (Id=@Id) 


DECLARE @Stoqet TABLE  (Id INT NOT NULL ,ArtikulliId INT NOT NULL,Sasia DECIMAL(18,3) NOT NULL)
DELETE @Stoqet 

DELETE A
OUTPUT Deleted.Id,DELETED.ArtikulliPerberesId,Deleted.Sasia  INTO @Stoqet 
FROM dbo.DaljaMallitArtikujtEPerber A Inner join dbo.DaljaMallitDetale DD on A.DaljaMallitDetaleId =DD.Id 
WHERE DD.Id =@Id
UPDATE C SET C.Stoku = C.Stoku - [@Stoqet].Sasia FROM @Stoqet inner JOIN dbo.Cmimorja C ON C.ArtikulliId = [@Stoqet].ArtikulliId AND C.OrganizataId =@OrganizataId 

DELETE @Stoqet 

DELETE A
OUTPUT Deleted.Id,DELETED.ArtikulliId,Deleted.Sasia  INTO @Stoqet FROM dbo.DaljaMallitDetale A WHERE Id =@Id
UPDATE C SET C.Stoku = C.Stoku - [@Stoqet].Sasia FROM @Stoqet inner JOIN dbo.Cmimorja C ON C.ArtikulliId = [@Stoqet].ArtikulliId AND C.OrganizataId =@OrganizataId 



end
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KlasifikatoretUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.KlasifikatoretUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'KlasifikatoretType') 
			  begin
			  Drop  type  TOSHIBA.KlasifikatoretType
			  End 
Go --
CREATE TYPE [TOSHIBA].[KlasifikatoretType] AS TABLE
(
    [Id]               INT          NOT NULL,
    [Pershkrimi]       VARCHAR (50) NOT NULL,
    [PershkrimiKlient] VARCHAR (50) NOT NULL,
    [Tipi]             VARCHAR (50) NOT NULL,
    [Statusi]          BIT          NULL,
    [Obligativ]        BIT          NOT NULL
)
Go --
CREATE PROCEDURE [TOSHIBA].[KlasifikatoretUpdateInsert_sp]
(  
 @KlasifikatoretType TOSHIBA.KlasifikatoretType readonly   
)  
   
as begin 

 Update A set 
        A.Pershkrimi       = B.Pershkrimi   
	   ,A.PershkrimiKlient = B.PershkrimiKlient
	   ,A.Tipi             = B.Tipi
	   ,A.Statusi          = B.Statusi
	   ,A.Obligativ        = B.Obligativ
From dbo.Klasifikatoret A inner join @KlasifikatoretType B on A.id = B.id  

insert into dbo.Klasifikatoret  
      (Id  
      ,Pershkrimi  
      ,PershkrimiKlient  
      ,Tipi  
	  ,Statusi
	  ,Obligativ
      )  
SELECT Id  
      ,Pershkrimi  
      ,PershkrimiKlient  
      ,Tipi  
	  ,Statusi  
	  ,Obligativ  
  FROM @KlasifikatoretType WHERE Id NOT IN (SELECT Id FROM dbo.Klasifikatoret)  
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.RaportetFajllatInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.RaportetFajllatInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'RaportetFajllatType') 
			  begin
			  Drop  type  TOSHIBA.RaportetFajllatType
			  End 
Go --
CREATE TYPE [TOSHIBA].[RaportetFajllatType] AS TABLE
(
[Id] [int] NOT NULL,
[Raporti] [varbinary] (max) NOT NULL,
[Freskuar] [bit] NULL DEFAULT ((0)),
[Pershkrimi] [varchar] (200)  NOT NULL,
[EmriRaportit] [varchar] (200)  NULL,
[EmriPrinterit] [varchar] (200)  NULL,
[NumriKopjeve] [int] NULL
)
Go --
CREATE PROCEDURE TOSHIBA.RaportetFajllatInsert_sp
(  
	@RaportetFajllatTable TOSHIBA.RaportetFajllatType readonly
)  
   
as begin 

DELETE FROM dbo.RaportetFajllat

insert into dbo.RaportetFajllat
(
    Id,
    Raporti,
    Freskuar,
    Pershkrimi,
    EmriRaportit,
	EmriPrinterit,
	NumriKopjeve
) 
SELECT Id,
    Raporti,
    Freskuar,
    Pershkrimi,
    EmriRaportit,
	EmriPrinterit,
	NumriKopjeve FROM @RaportetFajllatTable A
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.RaportetFajllatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.RaportetFajllatSelect_sp
			  End
Go --

CREATE PROCEDURE [TOSHIBA].[RaportetFajllatSelect_sp] 
( 
	@Id INT = NULL,
	@Pershkrimi varchar(200) = null,
	@EmriRaportit VARCHAR(200) = NULL
) 
AS BEGIN 
SELECT A.Id ,
	   A.Raporti , 
	   A.EmriRaportit,
	   A.Pershkrimi,
	   A.Freskuar ,
A.EmriPrinterit,
A.NumriKopjeve
FROM dbo.RaportetFajllat A 
where (A.Pershkrimi = @Pershkrimi or @Pershkrimi IS NULL)
AND (A.EmriRaportit = @EmriRaportit OR @EmriRaportit IS NULL)
AND (A.Id = @Id OR @Id IS NULL)
  
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GetNumrinEDokumentit') ) 
			  begin
			  Drop  Procedure  TOSHIBA.GetNumrinEDokumentit
			  End
Go --
CREATE PROCEDURE TOSHIBA.GetNumrinEDokumentit 
( 
@OrganizataId int ,
@LlojiDokumentitId int  
) 
AS BEGIN 

select ISNULL((
							SELECT Max(NumriFatures) NumriFatures
							FROM 
							(
							SELECT max(NrFatures) NumriFatures
							FROM dbo.DaljaMallit with(nolock) WHERE Viti=YEAR(getdate()) 
							and  OrganizataId=@OrganizataId and DokumentiId =@LlojiDokumentitId
							) B
),0)+1
as Numri
END
Go --

IF EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_CmimorjaLokalePerSinkronizimSelect_sp') )
begin
Drop  Procedure TOSHIBA.POS_CmimorjaLokalePerSinkronizimSelect_sp
End
Go --
CREATE PROCEDURE TOSHIBA.POS_CmimorjaLokalePerSinkronizimSelect_sp
(
@OrganizataId int
)
AS
BEGIN

SELECT 
    C.OrganizataId,
	A.Shifra,
	C.QmimiIShitjes 
FROM  dbo.Artikujt A
INNER JOIN dbo.Cmimorja C ON C.ArtikulliId = A.Id
WHERE C.OrganizataId=@OrganizataId
END
Go --
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'TOSHIBA.ShitjetEPaBarazuaraSelect_sp') )
    BEGIN
        DROP  PROCEDURE TOSHIBA.ShitjetEPaBarazuaraSelect_sp
    END
Go --
Create PROCEDURE TOSHIBA.ShitjetEPaBarazuaraSelect_sp
    (
      @OrganizataId INT ,
      @SasiVler CHAR(4)
    )
AS
    BEGIN 
        IF ( @SasiVler = 'Vler' )
            BEGIN
			--///////////////////////////////////////////////
Select * into #G from 
					(									SELECT    T.OperatoriId ,
																	MP.Shkurtesa MP ,
																	CONVERT(DECIMAL(18, 2), SUM(T.Sasia * T.CmimiShitjes)) Vlera
														  FROM      dbo.PMPTransaksionet T
																	LEFT OUTER JOIN dbo.MenyratEPageses MP ON T.MenyraPagesesId = MP.Id
														  WHERE     T.BarazimiId IS NULL  
														  GROUP BY  T.OperatoriId ,
																	MP.Shkurtesa
										 UNION ALL 
															SELECT  T.OperatoriId ,
																	'PMP' MP ,
																	CONVERT(DECIMAL(18, 2), SUM(T.ZbritjaNeArkVlere)) Vlera
															FROM    dbo.PMPTransaksionet T
															WHERE   T.BarazimiId IS NULL AND T.ZbritjaNeArkVlere > 0
															GROUP BY T.OperatoriId 
										 UNION ALL  
															SELECT  T.OperatoriId ,
																	'Total' MP ,
																	CONVERT(DECIMAL(18, 2), SUM(T.Sasia *T.CmimiCmimore)) Vlera
															FROM    dbo.PMPTransaksionet T
															WHERE   T.BarazimiId IS NULL
															GROUP BY T.OperatoriId 
										 UNION ALL
															SELECT    T.OperatoriId ,
																	'ZB' MP ,
																	CONVERT(DECIMAL(18, 2), SUM(T.Sasia
																				  * (T.CmimiCmimore-T.CmimiShitjes))) Vlera
														  FROM      dbo.PMPTransaksionet T 
														  WHERE     T.BarazimiId IS NULL  
														  AND abs(T.CmimiCmimore-T.CmimiShitjes)<>0
														  GROUP BY  T.OperatoriId 
														) G
					--/////////////////////////////////////////////
					SELECT CASE WHEN GROUPING(V.Operatori)=1 THEN '		Total:' ELSE V.Operatori END Operatori,
									   Sum(V.Qarkullimi		) Qarkullimi		,
									   Sum(V.ZB		) ZB		, 
									   Sum(V.PMP			) PMP			,
									   Sum(V.NLB			) NLB			,
									   Sum(V.[TEB Starcard] ) [TEB S] ,
									   Sum(V.PP				) PP				,
									   Sum(V.PCB			) PCB			,
									   Sum(V.TEB			) TEB			,
									   Sum(V.BKT			) BKT			,
									   Sum(V.BEK			) BEK			,
									   Sum(V.RBK			) RBK			,
									   Sum(V.XH				) XH				,
									   Sum(V.Qarkullimi-V.ZB-V.PMP-V.NLB-V.[TEB Starcard]-V.PP-V.PCB-V.TEB-V.BKT-V.BEK-V.RBK-V.XH	) G				 
									   FROM 
								(
									SELECT   O.Emri +' '+ O.Mbiemri Operatori,
											ISNULL([Total], 0.00) Qarkullimi ,
											ISNULL(ZB, 0.00) ZB ,
											ISNULL(NLB, 0.00) NLB ,
											ISNULL([TEB Starcard], 0.00) [TEB Starcard] ,
											ISNULL(PMP, 0.00) PMP ,
											ISNULL(PP, 0.00) PP ,
											ISNULL(PCB, 0.00) PCB ,
											ISNULL(TEB, 0.00) TEB ,
											ISNULL(BKT, 0.00) BKT ,
											ISNULL(BEK, 0.00) BEK ,
											ISNULL(RBK, 0.00) RBK ,
											ISNULL(XH, 0.00) XH ,
											ISNULL(G, 0.00) G
									FROM    ( SELECT    *
											  FROM      (
															SELECT A.OperatoriId ,
																   A.MP ,
																   A.Vlera FROM #G A  
														) A PIVOT
							( SUM(Vlera) FOR MP IN ( [Total],[ZB],[PP], [PCB], [TEB], [BKT], [BEK], [RBK], [XH],
													 [G], [NLB], [TEB Starcard], [PMP] ) ) B
											) I
											INNER JOIN dbo.Mxh_Operatoret O ON I.OperatoriId = O.Id
								) V
								GROUP BY V.Operatori
								WITH ROLLUP
			END
        ELSE
            BEGIN
                SELECT CASE WHEN GROUPING(V.Operatori)=1 THEN '		Total:' ELSE V.Operatori END Operatori,
                   Sum(V.Qarkullimi		) Qarkullimi		,
                   Sum(V.NLB			) NLB			,
                   Sum(V.[TEB Starcard] ) [TEB S] ,
                   Sum(V.PMP			) PMP			,
                   Sum(V.PP				) PP				,
                   Sum(V.PCB			) PCB			,
                   Sum(V.TEB			) TEB			,
                   Sum(V.BKT			) BKT			,
                   Sum(V.BEK			) BEK			,
                   Sum(V.RBK			) RBK			,
                   Sum(V.XH				) XH				,
                   Sum(V.G				) G				 
				   FROM 
			(
                SELECT   O.Emri +' '+ O.Mbiemri Operatori,
                        ISNULL([PP], 0.00) + ISNULL([PCB], 0.00)
                        + ISNULL([TEB], 0.00) + ISNULL([BKT], 0.00)
                        + ISNULL([BEK], 0.00) + ISNULL([RBK], 0.00)
                        + ISNULL([XH], 0.00) + ISNULL([G], 0.00)
                        + ISNULL([NLB], 0.00) + ISNULL([TEB Starcard], 0.00)
                        + ISNULL([PMP], 0.00) Qarkullimi ,
                        ISNULL(NLB, 0.00) NLB ,
                        ISNULL([TEB Starcard], 0.00) [TEB Starcard] ,
                        ISNULL(PMP, 0.00) PMP ,
                        ISNULL(PP, 0.00) PP ,
                        ISNULL(PCB, 0.00) PCB ,
                        ISNULL(TEB, 0.00) TEB ,
                        ISNULL(BKT, 0.00) BKT ,
                        ISNULL(BEK, 0.00) BEK ,
                        ISNULL(RBK, 0.00) RBK ,
                        ISNULL(XH, 0.00) XH ,
                        ISNULL(G, 0.00) G
                FROM    ( SELECT    *
                          FROM      ( SELECT    T.OperatoriId ,
                                                MP.Shkurtesa MP ,
                                                CONVERT(DECIMAL(18, 2), SUM(T.Sasia)) Sasia
                                      FROM      dbo.PMPTransaksionet T
                                                LEFT OUTER JOIN dbo.MenyratEPageses MP ON T.MenyraPagesesId = MP.Id
                                      WHERE     T.BarazimiId IS NULL AND T.OrganizataId =@OrganizataId
                                      GROUP BY  T.OperatoriId ,
                                                MP.Shkurtesa
                                    ) A PIVOT
		( SUM(Sasia) FOR MP IN ( [PP], [PCB], [TEB], [BKT], [BEK], [RBK], [XH],
                                 [G], [NLB], [TEB Starcard], [PMP] ) ) B
                        ) I
                        INNER JOIN dbo.Mxh_Operatoret O ON I.OperatoriId = O.Id
            ) V
			GROUP BY V.Operatori
			WITH ROLLUP
            END
    END 
Go --
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'TOSHIBA.ShitjetEPaBarazuaraSipasArtikullitSelect_sp') )
    BEGIN
        DROP  PROCEDURE TOSHIBA.ShitjetEPaBarazuaraSipasArtikullitSelect_sp
    END
Go --
CREATE PROCEDURE TOSHIBA.ShitjetEPaBarazuaraSipasArtikullitSelect_sp
    (
      @OrganizataId INT ,
      @SasiVler CHAR(4)
    )
AS
    BEGIN 
        IF ( @SasiVler = 'Vler' )
            BEGIN
			SELECT CASE WHEN GROUPING(V.Artikulli)=1 THEN '		Total:' ELSE V.Artikulli END Artikulli,
                   Sum(V.Qarkullimi		) Qarkullimi		,
                   Sum(V.NLB			) NLB			,
                   Sum(V.[TEB Starcard] ) [TEB S] ,
                   Sum(V.PMP			) PMP			,
                   Sum(V.PP				) PP				,
                   Sum(V.PCB			) PCB			,
                   Sum(V.TEB			) TEB			,
                   Sum(V.BKT			) BKT			,
                   Sum(V.BEK			) BEK			,
                   Sum(V.RBK			) RBK			,
                   Sum(V.XH				) XH				,
                   Sum(V.G				) G				 
				   FROM 
			(
                SELECT   O.Pershkrimi Artikulli,
                        ISNULL([PP], 0.00) + ISNULL([PCB], 0.00)
                        + ISNULL([TEB], 0.00) + ISNULL([BKT], 0.00)
                        + ISNULL([BEK], 0.00) + ISNULL([RBK], 0.00)
                        + ISNULL([XH], 0.00) + ISNULL([G], 0.00)
                        + ISNULL([NLB], 0.00) + ISNULL([TEB Starcard], 0.00)
                        + ISNULL([PMP], 0.00) Qarkullimi ,
                        ISNULL(NLB, 0.00) NLB ,
                        ISNULL([TEB Starcard], 0.00) [TEB Starcard] ,
                        ISNULL(PMP, 0.00) PMP ,
                        ISNULL(PP, 0.00) PP ,
                        ISNULL(PCB, 0.00) PCB ,
                        ISNULL(TEB, 0.00) TEB ,
                        ISNULL(BKT, 0.00) BKT ,
                        ISNULL(BEK, 0.00) BEK ,
                        ISNULL(RBK, 0.00) RBK ,
                        ISNULL(XH, 0.00) XH ,
                        ISNULL(G, 0.00) G
                FROM    ( SELECT    *
                          FROM      ( SELECT    T.ArtikulliId ,
                                                MP.Shkurtesa MP ,
                                                CONVERT(DECIMAL(18, 2), SUM(T.Sasia
                                                              * CASE
                                                              WHEN T.NrDecimaleveQmimi = 1
                                                              THEN CONVERT(DECIMAL(18), T.FuelPrice)
                                                              / 10.00
                                                              WHEN T.NrDecimaleveQmimi = 2
                                                              THEN CONVERT(DECIMAL(18), T.FuelPrice)
                                                              / 100.00
                                                              WHEN T.NrDecimaleveQmimi = 3
                                                              THEN CONVERT(DECIMAL(18), T.FuelPrice)
                                                              / 1000.00
															  WHEN T.NrDecimaleveQmimi = 4
                                                              THEN CONVERT(DECIMAL(18), T.FuelPrice)
                                                              / 10000.00
                                                              END)) Vlera
                                      FROM      dbo.PMPTransaksionet T
                                                LEFT OUTER JOIN dbo.MenyratEPageses MP ON T.MenyraPagesesId = MP.Id
                                      WHERE     T.BarazimiId IS NULL AND T.OrganizataId =@OrganizataId
                                      GROUP BY  T.ArtikulliId ,
                                                MP.Shkurtesa
									 UNION ALL 
									 SELECT    T.ArtikulliId ,
                                                MP.Shkurtesa MP ,
                                                CONVERT(DECIMAL(18, 2), SUM(T.ZbritjaNeArkVlere)) Sasia
                                      FROM      dbo.PMPTransaksionet T
                                                ,dbo.MenyratEPageses MP 
                                      WHERE     MP.Id=25 AND T.ZbritjaNeArkVlere>0 AND T.BarazimiId IS NULL  AND T.OrganizataId =@OrganizataId
                                      GROUP BY  T.ArtikulliId ,
                                                MP.Shkurtesa
                                    ) A PIVOT
		( SUM(Vlera) FOR MP IN ( [PP], [PCB], [TEB], [BKT], [BEK], [RBK], [XH],
                                 [G], [NLB], [TEB Starcard], [PMP] ) ) B
                        ) I
                        INNER JOIN dbo.Artikujt O ON I.ArtikulliId = O.Id
            ) V
			GROUP BY V.Artikulli
			WITH ROLLUP
			END
        ELSE
            BEGIN
                SELECT CASE WHEN GROUPING(V.Artikulli)=1 THEN '		Total:' ELSE V.Artikulli END Artikulli,
                   Sum(V.Qarkullimi		) Qarkullimi		,
                   Sum(V.NLB			) NLB			,
                   Sum(V.[TEB Starcard] ) [TEB S] ,
                   Sum(V.PMP			) PMP			,
                   Sum(V.PP				) PP				,
                   Sum(V.PCB			) PCB			,
                   Sum(V.TEB			) TEB			,
                   Sum(V.BKT			) BKT			,
                   Sum(V.BEK			) BEK			,
                   Sum(V.RBK			) RBK			,
                   Sum(V.XH				) XH				,
                   Sum(V.G				) G				 
				   FROM 
			(
                SELECT   O.Pershkrimi Artikulli,
                        ISNULL([PP], 0.00) + ISNULL([PCB], 0.00)
                        + ISNULL([TEB], 0.00) + ISNULL([BKT], 0.00)
                        + ISNULL([BEK], 0.00) + ISNULL([RBK], 0.00)
                        + ISNULL([XH], 0.00) + ISNULL([G], 0.00)
                        + ISNULL([NLB], 0.00) + ISNULL([TEB Starcard], 0.00)
                        + ISNULL([PMP], 0.00) Qarkullimi ,
                        ISNULL(NLB, 0.00) NLB ,
                        ISNULL([TEB Starcard], 0.00) [TEB Starcard] ,
                        ISNULL(PMP, 0.00) PMP ,
                        ISNULL(PP, 0.00) PP ,
                        ISNULL(PCB, 0.00) PCB ,
                        ISNULL(TEB, 0.00) TEB ,
                        ISNULL(BKT, 0.00) BKT ,
                        ISNULL(BEK, 0.00) BEK ,
                        ISNULL(RBK, 0.00) RBK ,
                        ISNULL(XH, 0.00) XH ,
                        ISNULL(G, 0.00) G
                FROM    ( SELECT    *
                          FROM      ( SELECT    T.ArtikulliId ,
                                                MP.Shkurtesa MP ,
                                                CONVERT(DECIMAL(18, 2), SUM(T.Sasia)) Sasia
                                      FROM      dbo.PMPTransaksionet T
                                                LEFT OUTER JOIN dbo.MenyratEPageses MP ON T.MenyraPagesesId = MP.Id
                                      WHERE     T.BarazimiId IS NULL AND T.OrganizataId =@OrganizataId
                                      GROUP BY  T.ArtikulliId ,
                                                MP.Shkurtesa
                                    ) A PIVOT
		( SUM(Sasia) FOR MP IN ( [PP], [PCB], [TEB], [BKT], [BEK], [RBK], [XH],
                                 [G], [NLB], [TEB Starcard], [PMP] ) ) B
                        ) I
                        INNER JOIN dbo.Artikujt O ON I.ArtikulliId = O.Id
            ) V
			GROUP BY V.Artikulli
			WITH ROLLUP
            END
    END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjejeArtikulliId') ) 
			  begin
			  Drop  function TOSHIBA.GjejeArtikulliId
			  End
Go --
CREATE FUNCTION [TOSHIBA].[GjejeArtikulliId]
(
	-- Add the parameters for the function here
@ShifraOseBarkodi varchar(20)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar int
	Declare @DyNumratEParaTeBarkodit int,@Pershorja int,@Plu int,@Pesha decimal(18,3),@Sasia decimal(18,3),@Tipi varchar(20)

if (@ShifraOseBarkodi is not null)
	-- Add the T-SQL statements to compute the return value here
	
	if (LEN(@ShifraOseBarkodi)=13 and ISNUMERIC(@ShifraOseBarkodi) = 1)
			begin
			   select @DyNumratEParaTeBarkodit=Id,@Tipi=Tipi from StandardetEBarkodave  with(nolock) where Id=CONVERT(int,(LEFT(@ShifraOseBarkodi,2)))
			   if (@DyNumratEParaTeBarkodit is not null)
			   begin
					   if (@Tipi='FIX')
						   begin
						   Select @ResultVar=dbo.GjejeArtikulliId(LEFT(@ShifraOseBarkodi,7)),@Pesha=convert(Decimal(18,3),SUBSTRING(@ShifraOseBarkodi,8,5))
						   set @Sasia=@Pesha/ 100.000
						   end
					   else
						   begin
									Select @ResultVar=ArtikulliId from dbo.PLUPeshoret  with(nolock) where PLU=SUBSTRING(@ShifraOseBarkodi,3,5)
									
									if (@ResultVar is not null)
									begin											 
										 Select @Pershorja=SUBSTRING(@ShifraOseBarkodi,3,2),@Plu=SUBSTRING(@ShifraOseBarkodi,5,3),@Pesha=convert(Decimal(18,3),SUBSTRING(@ShifraOseBarkodi,8,5))
										 set @Sasia=@Pesha/ 100.000
									end
									--else
									--begin
									--	 Select @ResultVar=dbo.GjejeArtikulliId(LEFT(@ShifraOseBarkodi,7)),@Pesha=convert(Decimal(18,3),SUBSTRING(@ShifraOseBarkodi,8,5))
									--	 set @Sasia=@Pesha/ 100.000
									--end
									
						   End
					   end
			   else
					   Begin
			   							Select top 1 @ResultVar=Id from 
								(
								Select Id,Convert(varchar(13),Shifra) Barkodi from dbo.Artikujt  with(nolock)
								where Shifra =@ShifraOseBarkodi
								Union 
								Select ArtikulliId Id,Barkodi from Barkodat
								where Barkodi =@ShifraOseBarkodi
								--UNION 														
								--select Id,Convert(varchar(13),@ShifraOseBarkodi) Barkodi from dbo.Artikujt  with(nolock) 
								--Where id=(case when substring(@ShifraOseBarkodi,1,6)='200000' then substring(@ShifraOseBarkodi,7,6) else -1 end)
								UNION 
								Select Id,Convert(varchar(50),ShifraProdhuesit) Barkodi from dbo.Artikujt  with(nolock)
								where Shifra =@ShifraOseBarkodi
							
								--Select Id,BarkodiQeNukEgziston from 
								--(
								--SELECT Id,dbo.GjeneroBarkodin(Artikujt.Id) BarkodiQeNukEgziston FROM dbo.Artikujt 
								--) BA
								--where BarkodiQeNukEgziston not in (select Barkodi from Barkodat)
								) Art 
								where Art.Barkodi=@ShifraOseBarkodi

					   end
			end
		Else
			Begin
						Select 	@ResultVar=Id from 
					(
					Select Id,Convert(varchar(20),Shifra) Barkodi from dbo.Artikujt  with(nolock)
					Union All
					Select Id,Convert(varchar(20),ShifraProdhuesit) Barkodi from dbo.Artikujt  with(nolock) where ShifraProdhuesit is not null  and ShifraProdhuesit<>''
					Union All
					Select ArtikulliId Id,Barkodi from Barkodat  with(nolock)
					--UNION ALL 
     --               select Id,Convert(varchar(13),Shifra) Barkodi from dbo.Artikujt Where id=(case when substring(@ShifraOseBarkodi,1,6)='200000' then substring(@ShifraOseBarkodi,7,6) else -1 end)
					) Art 
					where Art.Barkodi=@ShifraOseBarkodi
			end
			
		if 	@ResultVar =-1
		begin
		set @ResultVar=isnull((select artikulliid from Barkodat where Barkodi=@ShifraOseBarkodi),-1)
		end

	-- Return the result of the function
	RETURN Isnull(@ResultVar,-1)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjeneroDokumentiId') ) 
			  begin
			  Drop  function TOSHIBA.GjeneroDokumentiId
			  End
Go --
CREATE FUNCTION [TOSHIBA].[GjeneroDokumentiId]
(
	@OrganizataId int,
	@DokumentiId int,
	@Viti int,
	@NrDokumentit int
)
RETURNS BigInt
AS
BEGIN

declare @OrganizataString varchar(10),@DokumentiIdString varchar(10),@VitiString varchar(10),@NrDokumentitString varchar(10)
set @OrganizataString=Convert(Varchar(10),(SELECT REPLICATE('0',5-LEN(RTRIM(@OrganizataId))) + RTRIM(@OrganizataId)))
set @DokumentiIdString=Convert(Varchar(10),(SELECT REPLICATE('0',3-LEN(RTRIM(@DokumentiId))) + RTRIM(@DokumentiId)))
set @VitiString=Convert(Varchar(10),substring(convert(varchar(10),@Viti),3,2))
set @NrDokumentitString=Convert(Varchar(10),(SELECT REPLICATE('0',7-LEN(RTRIM(@NrDokumentit))) + RTRIM(@NrDokumentit)))

Declare @Id bigint
set @Id=Convert(BigInt,@VitiString+@OrganizataString+@DokumentiIdString+@NrDokumentitString)
Return (@Id)
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_GjenerimiKuponitFiskal_Sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POS_GjenerimiKuponitFiskal_Sp
			  End
Go --
Create PROCEDURE [TOSHIBA].[POS_GjenerimiKuponitFiskal_Sp] -- @DaljaMallitId=18000040200000002
(
	 @DaljaMallitId BIGINT=null
)
WITH recompile
AS
Begin

declare @GjenerimiKuponit varchar(40) = null,@ShifraArtikullitGjithmonEre bit,@Nrfatures varchar(50),@PikeTefituara int,@KomentiPerPike varchar(50)

select @GjenerimiKuponit = Vlera
from dbo.Konfigurimet  with(nolock)
	where id = 154 



--SELECT @KomentiPerPike='Q,1,______,_,__;2;Kupona:' + convert(varchar(50),convert(int,isnull(sum(dd.Sasia),0))) ,@PikeTefituara=isnull(sum(dd.Sasia),0) 
--from DaljaMallit d inner join DaljaMallitDetale dd on d.id=dd.DaljaMallitID
--inner join [dbo].[ArtikujtNeLojeShperblyese] SH on dd.ArtikulliId=sh.ArtikulliID
--where d.id=@DaljaMallitId

DECLARE @Komanda AS VARCHAR(max)=''
 
if(@GjenerimiKuponit = 'Gekos' or @GjenerimiKuponit is null)
begin 


			-------------KuponiFiskal------------------------------
			Select @Nrfatures=D.NrFatures,@Komanda='Q,1,______,_,__;1;' + 
			CASE WHEN D.DokumentiId = 20 THEN   
			' Arka:'+CONVERT(VARCHAR(10),D.NumriArkes)+' Nr: ' 
			                                 +Case when CHARINDEX('SQLEXPRESS',@@SERVERNAME) > 0 then 
											        isnull(CONVERT(VARCHAR(50),D.[NumriFaturesManual]),'') 
											  when CHARINDEX('KUBITPOS',@@SERVERNAME) > 0 then 
											        isnull(CONVERT(VARCHAR(50),D.[NumriFaturesManual]),'') 
											  else CONVERT(VARCHAR(50),D.[NumriFaturesManual]) End
											  +CHAR(13)+CHAR(10)
		 
			+ ISNULL(o.Emri,'') + ' ' +  ISNULL(substring(o.Mbiemri,1,1),0) + '' +CHAR(13)+CHAR(10)
			else 
			'Nr. Fatures: ' +Case when CHARINDEX('SQLEXPRESS',@@SERVERNAME) > 0 then 
											        isnull(CONVERT(VARCHAR(50),D.[NumriFaturesManual]),'') 
											  else CONVERT(VARCHAR(50),D.[NumriFaturesManual]) End
											  +CHAR(13)+CHAR(10)
			+'Q,1,______,_,__;2;'+CHAR(13)+CHAR(10) end 

			FROM dbo.DaljaMallit D with(nolock) INNER JOIN dbo.EkzekutimiPageses E with(nolock) ON E.DaljaMallitID = D.Id 
			LEFT OUTER JOIN dbo.Mxh_Operatoret o ON d.RegjistruarNga=o.Id
			WHERE D.Id=@DaljaMallitId


			if (Select id from Konfigurimet with(nolock) where id=159 and Statusi=1) is not null
			begin
				set @ShifraArtikullitGjithmonEre=1
			end
			else
			begin
				set @ShifraArtikullitGjithmonEre=0
			end

			select @Komanda+=kom from (
			SELECT 'S,1,______,_,__;'+REPLACE(PershkrimiFiskal,';','')+';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),CmimiShitjesDyDec))+';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,3),Sasia))+';1;1;'+Kategoria+';0;'+ (case when @ShifraArtikullitGjithmonEre=1 then @Nrfatures + convert(varchar(50),Shifra) + convert(varchar(50),abs(isnull(nr,0)))  else Shifra end) + ';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),Rabati+ZbritjeMePerqindje))+';'
			+ Case when CONVERT(DECIMAL(18,2),0)=0 then '' else CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),0))+';' End + CHAR(13)+CHAR(10) kom
			FROM 
			(
				   SELECT G.Id ,
				   G.PershkrimiFiskal ,
				   G.Sasia ,
				   G.Kategoria ,
				   G.Shifra ,
				   nr,
				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then 0 else Rabati end Rabati,
				   CmimiShitjesDyDec,
				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then G.Sasia*(CmimiShitjesDyDec-CmimiShitjesshumeDec) +RabatiVlere else 0 end ZbritjeMeVler,
				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then convert(decimal(18,2),((G.Sasia*(CmimiShitjesDyDec-CmimiShitjesshumeDec) +RabatiVlere)/(G.Sasia*CmimiShitjesDyDec)*100)) else 0 end ZbritjeMePerqindje
					FROM 
					(
							SELECT 
							DD.Id
							,A.PershkrimiFiskal
							,QmimiShitjes
							,DD.Sasia 
							,dd.NR
							,(SELECT TOP 1 KATEGORIA FROM TATIMET WHERE VLERA=DD.TVSH) Kategoria 
							,A.Shifra 
							,CASE WHEN round(DD.QmimiShitjes, 2)-DD.QmimiShitjes<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END CmimiShitjesDyDec
							,DD.QmimiShitjes CmimiShitjesshumeDec
							,CONVERT(DECIMAL(18,2) ,convert(decimal(18,6),dd.QmimiShitjes-(dd.QmimiShitjes*(1-DD.Rabati/100.00)*(1-dd.EkstraRabati/100.00)))/dd.QmimiShitjes*100.00) Rabati
							--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
							,DD.Sasia *(((DD.QmimiShitjes*(DD.Rabati/ 100.00))+(DD.QmimiShitjes*(DD.EkstraRabati/100.00)))) RabatiVlere
							FROM dbo.DaljaMallitDetale DD with(nolock) 
							INNER JOIN dbo.Artikujt A with(nolock) ON A.Id = DD.ArtikulliId 
							INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
							WHERE DD.DaljaMallitID = @DaljaMallitId  and Sasia>0
							union all
							SELECT 
							DD.Id
							,A.PershkrimiFiskal
							,QmimiShitjes
							,DD.Sasia 
							,dd.NR
							,(SELECT TOP 1 KATEGORIA FROM TATIMET WHERE VLERA=DD.TVSH) Kategoria 
							,A.Shifra 
							,CASE WHEN round(DD.QmimiShitjes, 2)-DD.QmimiShitjes<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END CmimiShitjesDyDec
							,DD.QmimiShitjes CmimiShitjesshumeDec
							,CONVERT(DECIMAL(18,2) ,convert(decimal(18,6),dd.QmimiShitjes-(dd.QmimiShitjes*(1-DD.Rabati/100)*(1-dd.EkstraRabati/100)))/dd.QmimiShitjes*100) Rabati
							--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
							,DD.Sasia *(((DD.QmimiShitjes*(DD.Rabati/ 100.00))+(DD.QmimiShitjes*(DD.EkstraRabati/100.00)))) RabatiVlere
							FROM dbo.DaljaMallitDetale DD with(nolock) 
							INNER JOIN dbo.Artikujt A with(nolock) ON A.Id = DD.ArtikulliId 
							INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
							WHERE DD.DaljaMallitID = @DaljaMallitId  and Sasia<0
							
					) G 

			) YHY 

			) OP


			IF((SELECT COUNT(iD) FROM dbo.EkzekutimiPageses with(nolock) WHERE DaljaMallitID =@DaljaMallitId )>1)
			Begin
			Select  @Komanda +='T,1,______,_,__;'+Ep.Kesh+';'+case when Ep.Kesh ='0' then '' else CONVERT(VARCHAR(10),Ep.Vlera) end+CHAR(13)+CHAR(10)
			FROM 
					(SELECT Sum(Vlera) Vlera , Kesh,NumriPagesave
					FROM (
							SELECT  SUM(E.Paguar) Vlera ,
									CASE WHEN E.MenyraEPagesesId = 22 THEN '0'
										 ELSE '3'
									END Kesh
									,COUNT(E.MenyraEPagesesId) NumriPagesave
							FROM    dbo.EkzekutimiPageses E with(nolock)
							WHERE   E.DaljaMallitID = @DaljaMallitId
							GROUP BY E.MenyraEPagesesId
					) T 
					GROUP BY Kesh,NumriPagesave) Ep 
					ORDER BY Kesh desc
			END
			ELSE
			Begin
			Select  @Komanda +='T,1,______,_,__;'+Ep.Kesh+';'
			FROM 
					(SELECT Sum(Vlera) Vlera , Kesh,NumriPagesave
					FROM (
							SELECT  SUM(E.Paguar) Vlera ,
									CASE WHEN E.MenyraEPagesesId = 22 THEN '0'
										 ELSE '3'
									END Kesh
									,COUNT(E.MenyraEPagesesId) NumriPagesave
							FROM    dbo.EkzekutimiPageses E  with(nolock)
							WHERE   E.DaljaMallitID = @DaljaMallitId
							GROUP BY E.MenyraEPagesesId
					) T 
					GROUP BY Kesh,NumriPagesave) Ep 
			End

			Select @Komanda=REPLACE(@Komanda,'ë','e')
			Select @Komanda=REPLACE(@Komanda,'Ë','e')
			Select @Komanda=REPLACE(@Komanda,'ç','c')
			Select @Komanda=REPLACE(@Komanda,'Ç','c')
			Select @Komanda COLLATE SQL_Latin1_General_CP1_CI_AS
			RETURN;
			END

			END   


--Shqiperi 
if(@GjenerimiKuponit = 'Shqiperi')
begin
---------------KuponiFiskal------------------------------
		Select @Komanda='CLEAR'+CHAR(13)+CHAR(10)
		+'CHIAVE REG'+CHAR(13)+CHAR(10)


		SELECT @Komanda+='VEND REP=' + Kategoria + ',qty=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,3),ABS(Sasia))) + ',PREZZO=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),CmimiShitjesDyDec)) + ',DES=''' + REPLACE(REPLACE(REPLACE(PershkrimiFiskal,';',''),',',''),'''','') + '''' + case when sasia<0 then ',Storno' else '' end + CHAR(13)+CHAR(10)
		FROM 
		(
			   SELECT G.Id ,
			   G.PershkrimiFiskal ,
			   G.Sasia ,
			   G.Kategoria ,
			   G.Shifra ,
			   CmimiShitjesDyDec
		FROM 
		(
				SELECT 
				DD.Id
				,A.PershkrimiFiskal
				,convert(decimal(18,2),QmimiShitjes-(QmimiShitjes*Rabati/ 100.00)) QmimiShitjes
				,DD.Sasia 
				,T.Kategoria 
				,A.Shifra 
				,convert(decimal(18,2),DD.QmimiShitjes*(1-Rabati/100.00)*(1-EkstraRabati/100.00)) CmimiShitjesDyDec
				,convert(decimal(18,2),DD.QmimiShitjes*(1-Rabati/100.00)*(1-EkstraRabati/100.00)) CmimiShitjesshumeDec
				,CONVERT(DECIMAL(18,2) ,DD.Rabati) Rabati
				--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
				FROM dbo.DaljaMallitDetale DD  with(nolock)
				INNER JOIN dbo.Artikujt A  with(nolock) ON A.Id = DD.ArtikulliId  
				INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
				WHERE DD.DaljaMallitID = @DaljaMallitId 
		) G

		) YHY
		Order by Sasia desc,Id ASC

		select @Komanda +='Allega On'+ CHAR(13)+CHAR(10)

		Select  @Komanda +='CHIU TEND='+ MenyraPageses +CHAR(13)+CHAR(10)
		FROM (
						
						SELECT  CASE WHEN MAX(E.MenyraEPagesesId) = 22 THEN '1' ELSE '5' END MenyraPageses
						FROM    dbo.EkzekutimiPageses E  with(nolock)
						WHERE   E.DaljaMallitID = @DaljaMallitId
						GROUP BY E.DaljaMallitID
						
			) o


		Select @Komanda+='ALLEG Riga=''' + 'Arka:'+CONVERT(VARCHAR(10),D.NumriArkes)+', Nr. Paragonit: '+CONVERT(VARCHAR(10),D.NrFatures)+ '''' +CHAR(13)+CHAR(10)
					   +'ALLEG Riga=''' + 'Ju Faleminderit!'''
		FROM dbo.DaljaMallit D with(nolock) INNER JOIN dbo.EkzekutimiPageses E with(nolock) ON E.DaljaMallitID = D.Id 
		WHERE D.Id=@DaljaMallitId 

		select @Komanda+='' + CHAR(13)+CHAR(10)
		select @Komanda+='ALLEGA FINE'

		Select @Komanda=REPLACE(@Komanda,'ë','e')
		Select @Komanda=REPLACE(@Komanda,'Ë','e')
		Select @Komanda=REPLACE(@Komanda,'ç','c')
		Select @Komanda=REPLACE(@Komanda,'Ç','c')
		Select @Komanda COLLATE SQL_Latin1_General_CP1_CI_AS

		

		RETURN;


 end
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitIdServerUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitIdServerUpdate_sp
			  End
Go --

Create PROCEDURE [TOSHIBA].POS_DaljaMallitIdServerUpdate_sp
(
	@DaljaMallitIdServer bigint,
	@DaljaMallitIdLokal bigint
)
AS
begin
	update DaljaMallit
	set ServerId=@DaljaMallitIdServer
	where Id=@DaljaMallitIdLokal
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitKontrolloVleratSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitKontrolloVleratSelect_sp
			  End
Go --
Create PROCEDURE [TOSHIBA].POS_DaljaMallitKontrolloVleratSelect_sp
(
	@DaljaMallitId bigint
)
AS
begin
	
	select 'DD' Tab,0 Vlera from DaljaMallitDetale DD where DaljaMallitID=@DaljaMallitId
	union all 
	select 'EK' Tab,0 Vlera from EkzekutimiPageses DD where DaljaMallitID=@DaljaMallitId

end
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPSubjektetGrupetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.CCPSubjektetGrupetUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPSubjektetGrupetTP') 
			  begin
			  Drop  type  TOSHIBA.CCPSubjektetGrupetTP
			  End 
Go --
CREATE TYPE [TOSHIBA].[CCPSubjektetGrupetTP] AS TABLE (
[Id] int not null,
[CCPGrupiId] [int] NOT NULL,
[CCPSubjektiId] [int] NOT NULL,
[LimitiSasi] [decimal] (18, 2) NOT NULL,
[LimitiVlere] [decimal] (18, 2) NOT NULL,
[VleraEShpenzuar] [decimal] (18,2) not null
)
Go --
CREATE Procedure TOSHIBA.CCPSubjektetGrupetUpdateInsert_sp  
(
	@CCPSubjektetGrupetTP TOSHIBA.CCPSubjektetGrupetTP readonly	
)
 
as begin
 
Delete CCPSubjektetGrupet

INSERT INTO dbo.CCPSubjektetGrupet
        ( Id,
		  CCPGrupiId ,
          CCPSubjektiId ,
          LimitiSasi ,
          LimitiVlere,
		  VleraEShpenzuar
        )
select	  Id,
		  CCPGrupiId ,
          CCPSubjektiId ,
          LimitiSasi ,
          LimitiVlere,
		  VleraEShpenzuar
from @CCPSubjektetGrupetTP

end
Go --

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPSubjektetGrupetSelect_sp') ) 
begin
Drop  Procedure  TOSHIBA.CCPSubjektetGrupetSelect_sp
End
Go --
create procedure TOSHIBA.CCPSubjektetGrupetSelect_sp
(
@Id int
)
as begin
select Id,CCPGrupiId,CCPSubjektiId,LimitiSasi,LimitiVlere,VleraEShpenzuar from dbo.CCPSubjektetGrupet where Id=@Id
end
Go --
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TOSHIBA.ADuhetSinkronizimPerAlfa') ) 
begin
Drop  Procedure  TOSHIBA.ADuhetSinkronizimPerAlfa
End
Go --
Create Procedure TOSHIBA.ADuhetSinkronizimPerAlfa
as
select Serveri,User,Pass,DB From KAlfa
Go --
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TOSHIBA.PregaditNumratEDokumenteve_Sp') ) 
begin
Drop  Procedure  TOSHIBA.PregaditNumratEDokumenteve_Sp
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitRreshtatEPareInsert_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitRreshtatEPareInsert_Sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'DaljetTypePerDBTeRE') 
			  begin
			  Drop  type  TOSHIBA.DaljetTypePerDBTeRE
			  End 
Go --
CREATE Type TOSHIBA.DaljetTypePerDBTeRE AS TABLE 
(
	[Id] [bigint] NOT NULL,
	[OrganizataId] [int] NOT NULL,
	[Viti] [int] NOT NULL,
	[Data] [date] NOT NULL,
	[NrFatures] [int] NOT NULL,
	[DokumentiId] [int] NOT NULL,
	[RegjistruarNga] [int] NOT NULL,
	[NumriArkes] [int] NOT NULL,
	[DataERegjistrimit] [datetime] NOT NULL,
	[SubjektiId] [int] NULL,
	[ShitjeEPerjashtuar] [bit] NOT NULL,
	[Koment] [varchar](500) NULL,
	[Xhirollogari] [int] NOT NULL,
	[Sinkronizuar] [bit] NOT NULL,
	[NeTransfer] [bit] NOT NULL,
	[DepartamentiId] [int] NOT NULL,
	[Validuar] [bit] NULL,
	[AfatiPageses] [int] NOT NULL,
	[DaljaMallitKorrektuarId] [bigint] NULL,
	[TavolinaId] [int] NULL,
	[NrDuditX3] [nvarchar](50) NULL,
	[DataFatures] [date] NULL,
	[RaportDoganor] [bit] NOT NULL,
	[Kursi] [decimal](18, 8) NOT NULL,
	[ValutaId] [int] NULL,
	[KuponiFiskalShtypur] [bit] NOT NULL,
	[K6] [int] NULL,
	[K7] [int] NULL,
	[K8] [int] NULL,
	[K9] [int] NULL,
	[K10] [int] NULL,
	[DataValidimit] [date] NULL,
	[DaljaMallitImportuarId] [varchar](50) NULL,
	[IdLokal] [varchar](50) NULL,
	[FaturaKomulativeId] [bigint] NULL,
	[TrackingId] [int] NULL,
	[NumriFaturesManual] [varchar](50) NULL,
	[ZbritjeNgaOperatori] [int] NULL,
	[RFID] [varchar](30) NULL,
	[RFIDCCP] [varchar](30) NULL,
	[BarazimiId] [bigint] NULL,
	[ServerId] [bigint] NOT NULL,
	[ReferencaID] [int] NULL
)
Go --
Create Procedure TOSHIBA.DaljaMallitRreshtatEPareInsert_Sp
(
@Tabela TOSHIBA.DaljetTypePerDBTeRE ReadOnly
)
as 
Begin
Insert into  DaljaMallit
(
Id ,
          OrganizataId ,
          Viti ,
          Data ,
          NrFatures ,
          DokumentiId ,
          RegjistruarNga ,
          NumriArkes ,
          DataERegjistrimit ,
          SubjektiId ,
          ShitjeEPerjashtuar ,
          Koment ,
          Xhirollogari ,
          Sinkronizuar ,
          NeTransfer ,
          DepartamentiId ,
          Validuar ,
          AfatiPageses ,
          DaljaMallitKorrektuarId ,
          TavolinaId ,
          NrDuditX3 ,
          DataFatures ,
          RaportDoganor ,
          Kursi ,
          ValutaId ,
          KuponiFiskalShtypur ,
          K6 ,
          K7 ,
          K8 ,
          K9 ,
          K10 ,
          DataValidimit ,
          DaljaMallitImportuarId ,
          IdLokal ,
          FaturaKomulativeId ,
          TrackingId ,
          NumriFaturesManual ,
          ZbritjeNgaOperatori ,
          RFID ,
          RFIDCCP ,
          BarazimiId ,
          ServerId ,
          ReferencaID
)
select 
Id ,
          OrganizataId ,
          Viti ,
          Data ,
          NrFatures ,
          DokumentiId ,
          RegjistruarNga ,
          NumriArkes ,
          DataERegjistrimit ,
          SubjektiId ,
          ShitjeEPerjashtuar ,
          Koment ,
          Xhirollogari ,
          1 Sinkronizuar ,
          NeTransfer ,
          DepartamentiId ,
          Validuar ,
          AfatiPageses ,
          DaljaMallitKorrektuarId ,
          TavolinaId ,
          NrDuditX3 ,
          DataFatures ,
          RaportDoganor ,
          Kursi ,
          ValutaId ,
          1 KuponiFiskalShtypur,
          K6 ,
          K7 ,
          K8 ,
          K9 ,
          K10 ,
          DataValidimit ,
          DaljaMallitImportuarId ,
          IdLokal ,
          FaturaKomulativeId ,
          TrackingId ,
          NumriFaturesManual ,
          ZbritjeNgaOperatori ,
          RFID ,
          RFIDCCP ,
          BarazimiId ,
          ServerId ,
          ReferencaID
From @Tabela
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitKaShenimeSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitKaShenimeSelect_sp
			  End
Go --
Create PROCEDURE [TOSHIBA].[DaljaMallitKaShenimeSelect_sp] 
as
Select top 1 1 KaTeDhena from DaljaMallit 
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.QarkullimiSipasOperatoritDetale_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.QarkullimiSipasOperatoritDetale_sp
			  End
Go --

CREATE Procedure [TOSHIBA].[QarkullimiSipasOperatoritDetale_sp] 
(@OrganizataId int=null
,@DataPrej date=null
,@DataDeri date=null
,@ShifraOperatorit int=null
)
as
begin

set @DataPrej=dateadd(day,-3,getdate())

SELECT   dbo.DaljaMallit.Id, dbo.Mxh_Operatoret.Id Shifra, dbo.Mxh_Operatoret.Emri + ' ' + Mxh_Operatoret.Mbiemri Pershkrimi, 
           dbo.DaljaMallit.NumriArkes, dbo.DaljaMallit.NrFatures, dbo.EkzekutimiPageses.Vlera, 
		   dbo.EkzekutimiPageses.Paguar,CONVERT(VARCHAR(8),DaljaMallit.DataERegjistrimit,108)   Ora
FROM         dbo.EkzekutimiPageses with(nolock) INNER JOIN
                      dbo.DaljaMallit with(nolock) ON dbo.EkzekutimiPageses.DaljaMallitID = dbo.DaljaMallit.Id INNER JOIN
                      dbo.MenyratEPageses with(nolock) ON dbo.EkzekutimiPageses.MenyraEPagesesId = dbo.MenyratEPageses.Id INNER JOIN
                      dbo.Mxh_Operatoret with(nolock) ON dbo.DaljaMallit.RegjistruarNga = dbo.Mxh_Operatoret.Id
Where   (dbo.DaljaMallit.OrganizataId=@OrganizataId or @OrganizataId is null)
and (dbo.DaljaMallit.Data>=@DataPrej or @DataPrej is null)
and (dbo.DaljaMallit.Data<=@DataDeri or @DataDeri is null)
and (DaljaMallit.RegjistruarNga=@ShifraOperatorit or @ShifraOperatorit is null) 
end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljeInternePersonatUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljeInternePersonatUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'DaljeInternePersonatType') 
			  begin
			  Drop  type  TOSHIBA.DaljeInternePersonatType
			  End 
Go --

CREATE TYPE [TOSHIBA].[DaljeInternePersonatType] AS TABLE
(
    [Id]           INT         NULL,
    [Emri]         VARCHAR(30) NULL,
    [Mbiemri]      VARCHAR(30) NULL,
    [OrganizataId] INT         NULL
)
Go --

CREATE PROCEDURE [TOSHIBA].[DaljeInternePersonatUpdateInsert_sp]
(  
 @DaljeInternePersonatType TOSHIBA.DaljeInternePersonatType readonly   
)  
as begin 

 Update A set 
        A.Emri    = B.Emri   
	   ,A.Mbiemri = B.Mbiemri
From dbo.DaljeInternePersonat A inner join @DaljeInternePersonatType B on A.id = B.id  

insert into dbo.DaljeInternePersonat  
      (Id  
      ,Emri  
      ,Mbiemri  
      ,OrganizataId  
      )  
SELECT Id  
      ,Emri  
      ,Mbiemri  
      ,OrganizataId   
  FROM @DaljeInternePersonatType WHERE Id NOT IN (SELECT Id FROM dbo.DaljeInternePersonat)  
END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_BleresatFizikSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_BleresatFizikSelect_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[POS_BleresatFizikSelect_sp]
as
begin 

SELECT S.[Id]
	  ,convert(varchar(50), S.Shifra) Shifra
      ,S.[Pershkrimi]
      ,S.[NumriFiskal]
      ,S.[Email]
      ,S.[Telefoni]
      ,S.[Adresa]
      ,S.[LlojiISubjektitID]
	  ,SL.Pershkrimi LlojiISubjektit
      ,S.[Koment]
      ,S.[DataERegjistrimit]
      ,S.[RegjistruarNga]
      ,S.Statusi 
  FROM dbo.Subjektet S 
  inner join SubjektiLloji SL on S.LlojiISubjektitID=SL.Id
  and S.Statusi=1
  end
  Go --

  IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_QarkullimiSipasOperatoritDetale_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_QarkullimiSipasOperatoritDetale_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[POS_QarkullimiSipasOperatoritDetale_sp]
(@OrganizataId int=null
,@DataPrej date=null
,@DataDeri date=null
,@ShifraOperatorit int=null
)
as
begin

set @DataPrej=dateadd(day,-3,getdate())

SELECT   dbo.DaljaMallit.Id,Subjektet.Pershkrimi Subjekti, dbo.Mxh_Operatoret.Emri + ' ' + Mxh_Operatoret.Mbiemri Operatori, 
           dbo.DaljaMallit.NumriArkes, dbo.DaljaMallit.NrFatures, dbo.EkzekutimiPageses.Vlera, 
		   dbo.EkzekutimiPageses.Paguar,CONVERT(VARCHAR(8),DaljaMallit.DataERegjistrimit,108)   Ora
FROM         dbo.EkzekutimiPageses with(nolock) INNER JOIN
                      dbo.DaljaMallit with(nolock) ON dbo.EkzekutimiPageses.DaljaMallitID = dbo.DaljaMallit.Id INNER JOIN
                      dbo.MenyratEPageses with(nolock) ON dbo.EkzekutimiPageses.MenyraEPagesesId = dbo.MenyratEPageses.Id INNER JOIN
                      dbo.Mxh_Operatoret with(nolock) ON dbo.DaljaMallit.RegjistruarNga = dbo.Mxh_Operatoret.Id
					  left outer join Subjektet with(nolock) on DaljaMallit.SubjektiId=Subjektet.Id
Where   (dbo.DaljaMallit.OrganizataId=@OrganizataId or @OrganizataId is null)
and (dbo.DaljaMallit.Data>=@DataPrej or @DataPrej is null)
and (dbo.DaljaMallit.Data<=@DataDeri or @DataDeri is null)
and (DaljaMallit.RegjistruarNga=@ShifraOperatorit or @ShifraOperatorit is null)

ORDER BY DaljaMallit.DataERegjistrimit desc
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_SubjektiBankatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_SubjektiBankatSelect_sp
			  END
Go --
CREATE PROCEDURE [TOSHIBA].[POS_SubjektiBankatSelect_sp]
(
@DeriMeOren datetime=null ,
@SubjektiId int =null ,
@OrganizataId int = null
		) 
as begin 
Select A.Id
,A.SubjektiId
,A.BankaId
,A.NrLlogaris 
,Case when F.Id is not null then 'NJ' else 'SU' end FilialSubjekt
From SubjektiBankat A			with(nolock)
inner join Subjektet S			with(nolock) on A.SubjektiId = S.Id
left outer join Mxh_Filialet F	with(nolock) on S.Id=F.Id
where (A.Id in (
Select Id
From SubjektiBankat				with(nolock)
 
))
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPKartelatArtikujtSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.CCPKartelatArtikujtSelect_Sp
			  END
Go --
CREATE PROCEDURE TOSHIBA.CCPKartelatArtikujtSelect_Sp
(
@Id INT =NULL ,
@CCPKompaniaId INT =NULL,
@Statusi BIT=null
)
AS
BEGIN
SELECT A.Id ,
       A.CCPKompaniaId ,
       A.ArtikulliId ,
	   AR.Pershkrimi,
       A.Statusi 
FROM dbo.CCPKartelatArtikujt A with(nolock) left outer join Artikujt AR with(nolock) on A.ArtikulliId=AR.Id
WHERE 
	(@Id IS NULL OR A.Id=@Id)
AND (@CCPKompaniaId IS NULL OR A.CCPKompaniaId=@CCPKompaniaId)
AND (@Statusi IS NULL OR A.Statusi=@Statusi)
END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljeInternePersonatSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljeInternePersonatSelect_Sp
			  END
Go --
CREATE PROCEDURE TOSHIBA.DaljeInternePersonatSelect_Sp
(
@Id INT =NULL ,
@OrganizataId INT =NULL 
)
AS
BEGIN
SELECT A.Id ,
       A.Emri ,
       A.Mbiemri ,
       A.OrganizataId 
FROM dbo.DaljeInternePersonat A with(nolock)
WHERE 
	(@Id IS NULL OR A.Id=@Id)
AND (@OrganizataId IS NULL OR A.OrganizataId=@OrganizataId) 
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleRaport_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleRaport_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitDetaleRaport_Sp
(
  @DaljaId BIGINT = NULL 	
)
AS 
    BEGIN 
 SELECT			D.Id DaljaId,
                D.OrganizataId ,
                 substring(convert(varchar(50),D.Viti),3,2) Viti,
                D.DataERegjistrimit Data , 
                IsNull(convert(varchar(50),D.NumriFaturesManual),convert(varchar(50), D.NrFatures)) NrFatures ,
				D.IdLokal NumriFaturesManual,
                D.DokumentiId , 
                Ll.Pershkrimi PershkrimiDokumentit ,
                D.RegjistruarNga ,
                D.NumriArkes ,
                D.DataERegjistrimit ,
                D.SubjektiId ,
                B.Shifra AS ShifraBleresit ,
                CASE WHEN D.Personi IS NOT NULL or LEN(D.Personi)>0 THEN D.Personi ELSE B.Pershkrimi END AS PershkrimiIBleresit,
                CASE WHEN D.Adresa IS NOT NULL or LEN(D.Adresa)>0 THEN d.Adresa ELSE B.Adresa END AS BAdresa,
				B.Email as BEmail,
				B.Vendi BVendi,
				B.Shteti as BShteti,
				B.KushtetEDergeses BKushtetEDergeses,
				B.ReferencaEKontrates BReferencaEKontrates ,
				B.NrLicenses NrLicensesBleresit, 
				B.NRB AS BNRB ,
                B.NrTVSH AS BNrTVSH ,
                CASE WHEN D.NumriPersonal IS NOT NULL or LEN(D.NumriPersonal)>0 THEN D.NumriPersonal ELSE b.NumriFiskal END AS BNumriFiskal,
                CASE WHEN D.NrTel IS NOT NULL or LEN(D.NrTel)>0 THEN D.NrTel ELSE B.Telefoni END AS BTelefoni, 
				isnull(D.AfatiPageses,1) AfatiPageses,
                D.Koment,
                Ll.Tipi ,
                A.Shifra Shifra ,
				DD.Barkodi,
                A.Pershkrimi PershkrimiFiskal ,
				A.Pershkrimi Emertimi,
				'' Specifikacioni,
                DD.DaljaMallitID ,
                DD.ArtikulliId ,
                DD.NjesiaID ,
                NJ.Pershkrimi Njesia ,
                DD.NR ,
                DD.Sasia ,
                DD.Tvsh ,
				DD.QmimiShitjes/(1+DD.Tvsh/ 100.00) QmimiPaTVSH ,
				 DD.Sasia *(DD.QmimiShitjes/(1+DD.Tvsh/ 100.00)) VleraPaTVSHPaRabat ,
                DD.QmimiShitjes , 
                DD.Rabati ,
				DD.EkstraRabati,
                 DD.QmimiShitjes  QmimiFinal ,
                DD.Sasia * ((( DD.QmimiShitjes * ( 1- DD.Rabati/ 100.00 ) )*( 1- DD.EkstraRabati / 100.00 ))/( 1 + DD.Tvsh/ 100.00 )) AS VleraPaTvsh ,
                DD.Sasia * ( CONVERT(DECIMAL(18, 6), (( DD.QmimiShitjes*(1-DD.Rabati / 100.00 ) )*(1-DD.EkstraRabati/ 100.00)) - ((( DD.QmimiShitjes*(1-DD.Rabati / 100.00 ) )*(1-DD.EkstraRabati/ 100.00)) / ( 1 + DD.Tvsh / 100.00 )))) AS VleraTvsh ,
                DD.Sasia * ( (DD.QmimiShitjes * (1-DD.Rabati/ 100.00 ))*(1-DD.EkstraRabati/ 100.00) ) AS VleraMeTvsh ,
                (DD.Sasia*((DD.QmimiShitjes/(1+DD.Tvsh/ 100.00))*DD.Rabati/ 100.00)) RabatiVlere,
				( DD.Sasia *( ( (DD.QmimiShitjes/(1+DD.Tvsh/ 100.00)) *(1- DD.Rabati / 100.00) ) )*(DD.EkstraRabati/ 100.00)) EkstraRabatiVlere,
                CONVERT(BIT, 0) AS KaLirim ,
                O.Shifra AS OShifra ,
                O.Pershkrimi OPershkrimi,
                O.NRB ONRB,
                O.NrTVSH ONrTVSH,
                O.NumriFiskal ONumriFiskal,
                O.Email OEmail,
                O.Telefoni OTelefoni,
                O.Fax OFax,
                O.Adresa OAdresa,			
				O.NrLicenses ONrLicenses 
			    ,MO.Emri +' ' + MO.Mbiemri as RegjistruarNgaEmri
				,MO.Email RegjistruarNgaEmail
				,MO.Tel RegjistruarNgaTel
				,O.NumriFiskal + ' ' + O.NRB + ' ' + O.NrTVSH NIT
				,A.ShifraProdhuesit 
				,'' NumriKatallogut
				,'' KomentiArtikullit 
				,null MenaxheriSubjektit
				,null MenaxheriSubjektitEmail
				,null MenaxheriSubjektitTel 
				,(SELECT TOP 1 MenyraEPagesesId FROM dbo.EkzekutimiPageses WHERE DaljaMallitID =D.Id) MenyraPagesesId,
				D.NumriATK,
			    VA.Pershkrimi Valuta
				FROM  dbo.DaljaMallit D   with(nolock)
				Inner join dbo.DaljaMallitDetale DD with(nolock) on D.Id=DD.DaljaMallitID
				Inner join dbo.LlojetEDokumenteve AS Ll with(nolock) ON D.DokumentiId = Ll.Id
				Inner join Artikujt A with(nolock) on DD.ArtikulliId = A.Id
				inner join Njesit NJ with(nolock) on A.NjesiaID =NJ.Id
                INNER JOIN  dbo.Subjektet AS O with(nolock) ON D.OrganizataId = O.Id
                LEFT OUTER JOIN dbo.Subjektet AS B with(nolock) ON D.SubjektiId = B.Id 
				left outer join Mxh_Operatoret MO with(nolock) on d.RegjistruarNga=mo.Id  
				left outer join dbo.Valutat VA  with(nolock)on D.ValutaId=VA.Id 
    WHERE   ( D.Id = @DaljaId
      OR @DaljaId IS NULL
    ) 
    END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.SubjektiBankatUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.SubjektiBankatUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'SubjektiBankatType') 
			  begin
			  Drop  type  TOSHIBA.SubjektiBankatType
			  End 
Go --
CREATE TYPE TOSHIBA.SubjektiBankatType as Table (
	Id					INT             NOT NULL,
	SubjektiId          INT             NOT NULL,
    BankaId             INT             NOT NULL,
    NrLlogaris          VARCHAR (50)    NOT NULL 
	)
Go --
CREATE PROCEDURE TOSHIBA.SubjektiBankatUpdateInsert_sp
(
@SubjektiBankatType TOSHIBA.SubjektiBankatType readonly
)
AS
BEGIN

INSERT INTO dbo.SubjektiBankat
        ( Id ,
          SubjektiId ,
          BankaId ,
          NrLlogaris  
        )
SELECT 
		  I.Id ,
          I.SubjektiId ,
          I.BankaId ,
          I.NrLlogaris 
FROM @SubjektiBankatType I LEFT OUTER JOIN dbo.SubjektiBankat S ON I.Id=S.Id
WHERE S.Id IS NULL 

UPDATE  S SET S.NrLlogaris = I.NrLlogaris 
FROM @SubjektiBankatType I INNER JOIN dbo.SubjektiBankat S ON I.Id=S.Id
WHERE I.NrLlogaris<>S.NrLlogaris

End
Go -- 
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.BankatUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.BankatUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'BankatType') 
			  begin
			  Drop  type  TOSHIBA.BankatType
			  End 
Go --
CREATE TYPE TOSHIBA.BankatType as Table (
	[Id] [int] NOT NULL,
	[EmriIBankes] [varchar] (100) NOT NULL,
	[Shkurtesa] [varchar] (20) NOT NULL,
	[SwiftCode] [varchar] (20) NULL, 
	[IBAN] [varchar] (100) NULL,
	[Renditja] [int] NULL
	)
Go --
CREATE PROCEDURE TOSHIBA.BankatUpdateInsert_sp
(
@BankatType TOSHIBA.BankatType readonly
)
AS
BEGIN

UPDATE B SET  
             B.EmriIBankes=A.EmriIBankes ,
             B.Shkurtesa=A.Shkurtesa ,
             B.SwiftCode=A.SwiftCode ,
             B.IBAN=A.IBAN ,
             B.Renditja=A.Renditja
FROM dbo.Bankat B INNER JOIN @BankatType A ON B.Id=A.Id

INSERT INTO dbo.Bankat
        ( Id ,
          EmriIBankes ,
          Shkurtesa ,
          SwiftCode , 
          IBAN ,
          Renditja  
        )
SELECT	  A.Id ,
          A.EmriIBankes ,
          A.Shkurtesa ,
          A.SwiftCode , 
          A.IBAN ,
          A.Renditja  
FROM @BankatType A LEFT OUTER JOIN dbo.Bankat B ON A.Id=B.Id
WHERE B.Id IS NULL 

End
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ShitjetPer10DiteSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.ShitjetPer10DiteSelect_sp
			  END
Go --
CREATE PROCEDURE TOSHIBA.ShitjetPer10DiteSelect_sp
(
@VetemMarketi bit=null
)
AS
SELECT 
D.Id,
'Market' LlojiIShitjes ,
D.OrganizataId ,
D.Viti ,
Convert(date,D.Data) Data,
D.DokumentiId LlojiDokumentitId,
LD.Pershkrimi LlojiDokumentit,
D.NumriArkes,
D.NrFatures,
D.SubjektiId,
D.RFID,
D.RFIDCCP,
S.Pershkrimi PerskrimiSubjektit,
O.Emri +' '+O.Mbiemri Operatori,
CONVERT(DECIMAL(18,2),(SELECT sum(W.Sasia * ((W.QmimiShitjes * (1-W.Rabati/100.00))*(1-W.EkstraRabati/100.00))) FROM dbo.DaljaMallitDetale W WHERE W.DaljaMallitID = D.Id)) Vlera
FROM dbo.DaljaMallit D
LEFT OUTER JOIN dbo.Subjektet S ON D.SubjektiId = S.Id 
LEFT OUTER JOIN dbo.Mxh_Operatoret O ON D.RegjistruarNga = O.Id
LEFT OUTER JOIN dbo.LlojetEDokumenteve LD ON D.DokumentiId = LD.Id 
WHERE (@VetemMarketi is null or @VetemMarketi=1) and D.Data >=DATEADD(DAY,-10,GETDATE())
UNION ALL 
SELECT 
D.Id,
'Derivate' LlojiIShitjes ,
D.OrganizataId ,
YEAR(D.DataERegjistrimit) Viti ,
Convert(date,D.DataERegjistrimit) Data, 
D.LlojiDokumentitId LlojiDokumentitId,
LD.Pershkrimi LlojiDokumentit,
1 NumriArkes,
D.NrDokumentit NrFatures,
D.SubjektiId,
D.RFIDCardNumber RFID,
D.RFIDCardNumberCCP RFIDCCP,
S.Pershkrimi PerskrimiSubjektit,
O.Emri +' '+O.Mbiemri Operatori,
CONVERT(DECIMAL(18,2),(D.Sasia*D.CmimiShitjes)) Vlera
FROM dbo.PMPTransaksionet D
LEFT OUTER JOIN dbo.Subjektet S ON D.SubjektiId = S.Id 
LEFT OUTER JOIN dbo.Mxh_Operatoret O ON D.OperatoriId = O.Id
LEFT OUTER JOIN dbo.LlojetEDokumenteve LD ON D.LlojiDokumentitId = LD.Id 
WHERE (@VetemMarketi is null or @VetemMarketi=0) and D.DataERegjistrimit >=DATEADD(DAY,-10,GETDATE())

Order by Data Desc

Go --  

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ArtikujtFotoUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.ArtikujtFotoUpdateInsert_sp
			  END

Go -- 

IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ArtikujtFotoTYPE') 
			  begin
			  Drop  type  TOSHIBA.ArtikujtFotoTYPE
			  End 

Go --

CREATE TYPE [TOSHIBA].[ArtikujtFotoTYPE] AS TABLE
(
	[Id]          INT           NOT NULL,
    [Foto]        IMAGE         NOT NULL,
    [ArtikulliId] INT           NOT NULL
)

Go --

CREATE PROCEDURE TOSHIBA.ArtikujtFotoUpdateInsert_sp
(  
 @ArtikujtFotoTYPE TOSHIBA.ArtikujtFotoTYPE readonly   
)  
as begin 

 Update A set 
        A.Foto    = B.Foto   
From dbo.ArtikujtFoto A inner join @ArtikujtFotoTYPE B on A.id = B.id  

INSERT INTO dbo.ArtikujtFoto
        ( Id,
          Foto,
          ArtikulliId
        )
SELECT 
          Id,
          Foto,
          ArtikulliId
FROM @ArtikujtFotoTYPE WHERE Id NOT IN (SELECT Id FROM dbo.ArtikujtFoto WITH (NOLOCK))
 
END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleAktivitetetSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleAktivitetetSelect_sp
			  END

Go --

CREATE PROCEDURE TOSHIBA.DaljaMallitDetaleAktivitetetSelect_sp
(
	@Id int=null,
	@DaljaMallitDetaleId int=null,
	@AktivitetiId bigint=null,
	@Zbritja decimal(23,8)=null,
	@DaljaMallit bigint=null
) 
as begin 
Select A.Id,A.DaljaMallitDetaleId,A.AktivitetiId,A.Zbritja,A.DaljaMallitId 
From dbo.DaljaMallitDetaleAktivitetet as A
where  (@Id is null or A.Id=@Id) 
and  (@DaljaMallitDetaleId is null or A.DaljaMallitDetaleId=@DaljaMallitDetaleId) 
and  (@AktivitetiId is null or A.AktivitetiId=@AktivitetiId) 
and  (@Zbritja is null or A.Zbritja=@Zbritja) 
and  (@DaljaMallit is null or A.DaljaMallitId=@DaljaMallit)
 
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetFilterLlojetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetFilterLlojetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetFilterLlojetType') 
			  begin
			  Drop  type  TOSHIBA.POSAktivitetetFilterLlojetType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetFilterLlojetType] AS TABLE
(
    [Id]           INT           NOT NULL,
	[Pershkrimi]   VARCHAR(50)   NOT NULL,
	[QueryDinamik] VARCHAR(8000) NOT NULL,
	[EmriFushesPerFilter] VARCHAR(50) NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POSAktivitetetFilterLlojetUpdateInsert_sp
(  
 @POSAktivitetetFilterLlojetType TOSHIBA.POSAktivitetetFilterLlojetType readonly   
)  
   
as begin   

 Update A set 
        A.Pershkrimi          = B.Pershkrimi   
	   ,A.QueryDinamik        = B.QueryDinamik
	   ,A.EmriFushesPerFilter = B.EmriFushesPerFilter
From dbo.POSAktivitetetFilterLlojet A inner join @POSAktivitetetFilterLlojetType B on A.id = B.id  

insert into dbo.POSAktivitetetFilterLlojet  
      (Id  
      ,Pershkrimi  
      ,QueryDinamik  
      ,EmriFushesPerFilter  
      )  
SELECT Id  
      ,Pershkrimi  
      ,QueryDinamik  
      ,EmriFushesPerFilter  
  FROM @POSAktivitetetFilterLlojetType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetFilterLlojet)  
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetLlojetEZbritjesUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetLlojetEZbritjesUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetLlojetEZbritjesType') 
			  begin
			  Drop  type  TOSHIBA.POSAktivitetetLlojetEZbritjesType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetLlojetEZbritjesType] AS TABLE
(
    [Id]           INT           NOT NULL,
	[Pershkrimi]   VARCHAR(50)   NOT NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POSAktivitetetLlojetEZbritjesUpdateInsert_sp
(  
 @POSAktivitetetLlojetEZbritjesType TOSHIBA.POSAktivitetetLlojetEZbritjesType readonly   
)  
   
as begin   

 Update A set 
        A.Pershkrimi = B.Pershkrimi   
From dbo.POSAktivitetetLlojetEZbritjes A inner join @POSAktivitetetLlojetEZbritjesType B on A.id = B.id  

insert into dbo.POSAktivitetetLlojetEZbritjes  
      (Id  
      ,Pershkrimi  
      )  
SELECT Id  
      ,Pershkrimi  
  FROM @POSAktivitetetLlojetEZbritjesType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetLlojetEZbritjes)  
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSAktivitetetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetPerSinkronizimType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSAktivitetetPerSinkronizimType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetPerSinkronizimType] AS TABLE
(
    [Id]				   BIGINT        NOT NULL,
	[PrejDates]			   DATE          NOT NULL,
	[DeriMeDaten]		   DATE          NOT NULL,
	[PrejOres]			   DATETIME      NULL,
	[DeriMeOren]		   DATETIME      NULL,
	[Pershkrimi]		   VARCHAR(50)   NULL,
	[Statusi]			   BIT           NOT NULL,
	[OrganizataId]		   INT           NOT NULL,
	[LlojiIDokumentitId]   INT           NOT NULL,
	[NrDokumentit]		   INT           NOT NULL,
	[Viti]				   INT           NOT NULL,
	[Data]				   DATE          NOT NULL,
	[PerfshinKatallogun]   BIT           NOT NULL,
	[Dhe]				   BIT           NOT NULL,
	[AplikoZbritjenNeGrup] BIT           NOT NULL,
	[Mbishkruaj]           BIT           NOT NULL,
	[LlojiAktivitetitId]   INT           NULL,
	[AplikoNeRabat]        BIT DEFAULT 0 NOT NULL,
	[AplikoNeEkstraRabat]  BIT DEFAULT 0 NOT NULL   
)
Go --

CREATE PROCEDURE TOSHIBA.POSAktivitetetUpdateInsert_sp
(  
 @POSAktivitetetPerSinkronizimType TOSHIBA.POSAktivitetetPerSinkronizimType readonly   
)  
   
as BEGIN 

 Update A set 
        A.PrejDates				= B.PrejDates   
	   ,A.DeriMeDaten			= B.DeriMeDaten
	   ,A.PrejOres				= B.PrejOres
	   ,A.DeriMeOren			= B.DeriMeOren
	   ,A.Pershkrimi			= B.Pershkrimi
	   ,A.Statusi				= B.Statusi
	   ,A.Data					= B.Data
	   ,A.PerfshinKatallogun	= B.PerfshinKatallogun
	   ,A.Dhe					= B.Dhe
	   ,A.AplikoZbritjenNeGrup	= B.AplikoZbritjenNeGrup
	   ,A.Mbishkruaj			= B.Mbishkruaj
	   ,A.LlojiAktivitetitId    = B.LlojiAktivitetitId
	   ,A.AplikoNeRabat         = B.AplikoNeRabat
	   ,A.AplikoNeEkstraRabat   = B.AplikoNeEkstraRabat
From dbo.POSAktivitetet A inner join @POSAktivitetetPerSinkronizimType B on A.id = B.id  

insert into dbo.POSAktivitetet  
      (Id  
      ,PrejDates  
      ,DeriMeDaten  
      ,PrejOres  
	  ,DeriMeOren
	  ,Pershkrimi
	  ,Statusi
	  ,OrganizataId
	  ,LlojiIDokumentitId
	  ,NrDokumentit
	  ,Viti
	  ,Data
	  ,PerfshinKatallogun
	  ,Dhe
	  ,AplikoZbritjenNeGrup
	  ,Mbishkruaj
	  ,LlojiAktivitetitId
	  ,AplikoNeRabat
	  ,AplikoNeEkstraRabat
      )  
SELECT Id  
      ,PrejDates  
      ,DeriMeDaten  
      ,PrejOres  
	  ,DeriMeOren  
	  ,Pershkrimi  
	  ,Statusi  
	  ,OrganizataId  
	  ,LlojiIDokumentitId  
	  ,NrDokumentit  
	  ,Viti  
	  ,Data  
	  ,PerfshinKatallogun  
	  ,Dhe
	  ,AplikoZbritjenNeGrup
	  ,Mbishkruaj
	  ,LlojiAktivitetitId
	  ,AplikoNeRabat
	  ,AplikoNeEkstraRabat
  FROM @POSAktivitetetPerSinkronizimType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetet)  

  delete from POSAktivitetetFilter where AktivitetiId not in (select Id from @POSAktivitetetPerSinkronizimType)
  delete from POSAktivitetetOrganizatat where AktivitetiId not in (select Id from @POSAktivitetetPerSinkronizimType)
  delete from POSAktivitetetZbritjaNeVlere where AktivitetiId not in (select Id from @POSAktivitetetPerSinkronizimType)
  DELETE FROM POSAktivitetetFilterCCPGrupet where AktivitetiId not in (select Id from @POSAktivitetetPerSinkronizimType)
  delete from POSAktivitetet where Id not in (select Id from @POSAktivitetetPerSinkronizimType)
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetOrganizatatUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSAktivitetetOrganizatatUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetOrganizatatType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSAktivitetetOrganizatatType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetOrganizatatType] AS TABLE
(
    [Id]           INT    NOT NULL,
	[AktivitetiId] BIGINT NOT NULL,
	[OrganizataId] INT    NOT NULL,
	[Statusi]      BIT    NOT NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POSAktivitetetOrganizatatUpdateInsert_sp
(  
 @POSAktivitetetOrganizatatType TOSHIBA.POSAktivitetetOrganizatatType readonly   
)  
   
as BEGIN   

 Update A set 
        A.Statusi = B.Statusi   
From dbo.POSAktivitetetOrganizatat A inner join @POSAktivitetetOrganizatatType B on A.id = B.id  

insert into dbo.POSAktivitetetOrganizatat  
      (Id  
      ,AktivitetiId  
      ,OrganizataId  
      ,Statusi
      )  
SELECT Id  
      ,AktivitetiId  
      ,OrganizataId  
      ,Statusi
FROM @POSAktivitetetOrganizatatType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetOrganizatat)
  
DELETE FROM dbo.POSAktivitetetOrganizatat WHERE Id NOT IN(SELECT Id FROM @POSAktivitetetOrganizatatType)  

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetZbritjaNeVlereUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetZbritjaNeVlereUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetZbritjaNeVlereType') 
			  begin
			  Drop  type  TOSHIBA.POSAktivitetetZbritjaNeVlereType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetZbritjaNeVlereType] AS TABLE
(
    [Id]               INT           NOT NULL,
	[AktivitetiId]     BIGINT        NOT NULL,
	[LlojiIZbrijtesId] INT           NOT NULL,
	[Vlera]            DECIMAL(18,2) NOT NULL,
	[Zbritja]          DECIMAL(18,2) NOT NULL,
	[Komenti]          VARCHAR(200)  NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POSAktivitetetZbritjaNeVlereUpdateInsert_sp
(  
 @POSAktivitetetZbritjaNeVlereType TOSHIBA.POSAktivitetetZbritjaNeVlereType readonly   
)  
   
as begin   

 Update A set 
        A.LlojiIZbrijtesId = B.LlojiIZbrijtesId   
	   ,A.Vlera            = B.Vlera
	   ,A.Zbritja          = B.Zbritja
	   ,A.Komenti          = B.Komenti
From dbo.POSAktivitetetZbritjaNeVlere A inner join @POSAktivitetetZbritjaNeVlereType B on A.id = B.id  

insert into dbo.POSAktivitetetZbritjaNeVlere  
      (Id  
      ,AktivitetiId  
      ,LlojiIZbrijtesId  
      ,Vlera  
	  ,Zbritja
	  ,Komenti
      )  
SELECT Id  
      ,AktivitetiId  
      ,LlojiIZbrijtesId  
      ,Vlera  
	  ,Zbritja  
	  ,Komenti  
FROM @POSAktivitetetZbritjaNeVlereType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetZbritjaNeVlere)

DELETE FROM dbo.POSAktivitetetZbritjaNeVlere WHERE Id NOT IN(SELECT Id FROM @POSAktivitetetZbritjaNeVlereType)  

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetFilterUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSAktivitetetFilterUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetFilterType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSAktivitetetFilterType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetFilterType] AS TABLE
(
    [Id]                     INT           NOT NULL,
	[AktivitetiId]           BIGINT        NOT NULL,
	[FilteriId]              INT           NOT NULL,
	[Shifra]                 VARCHAR(50)   NOT NULL,
	[EmriFushesPerFilter]    VARCHAR(50)   NULL, 
	[AplikoZbritjen]         BIT           NOT NULL,
	[Pershkrimi]	         VARCHAR(50)   NULL,
	[LlojiZbritjesId]        INT           NULL,
	[Zbritja]                DECIMAL(18,2) NULL,
	[Vlera]                  DECIMAL(18,2) NULL,
	[Gratis]                 BIT           NOT NULL,
	[Perjashto]              BIT           NOT NULL,
	[Cmimi]                  DECIMAL(18,2) NULL
)
Go --

CREATE PROCEDURE TOSHIBA.POSAktivitetetFilterUpdateInsert_sp
(  
 @POSAktivitetetFilterType TOSHIBA.POSAktivitetetFilterType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.FilteriId              = B.FilteriId   
	   ,A.Shifra                 = B.Shifra
	   ,A.EmriFushesPerFilter    = B.EmriFushesPerFilter
	   ,A.AplikoZbritjen         = B.AplikoZbritjen
	   ,A.Pershkrimi             = B.Pershkrimi
	   ,A.LlojiZbritjesId        = B.LlojiZbritjesId
	   ,A.Zbritja                = B.Zbritja
	   ,A.Vlera                  = B.Vlera
	   ,A.Gratis                 = B.Gratis
	   ,A.Perjashto              = B.Perjashto
	   ,A.Cmimi					 = B.Cmimi
FROM dbo.POSAktivitetetFilter A INNER JOIN @POSAktivitetetFilterType B ON A.id = B.id  

INSERT INTO dbo.POSAktivitetetFilter  
      (Id  
      ,AktivitetiId  
      ,FilteriId  
      ,Shifra  
	  ,EmriFushesPerFilter
	  ,AplikoZbritjen
	  ,Pershkrimi
	  ,LlojiZbritjesId
	  ,Zbritja
	  ,Vlera
	  ,Gratis
	  ,Perjashto
	  ,Cmimi
      )  
SELECT Id  
      ,AktivitetiId  
      ,FilteriId  
      ,Shifra   
	  ,EmriFushesPerFilter 
	  ,AplikoZbritjen  
	  ,Pershkrimi
	  ,LlojiZbritjesId
	  ,Zbritja
	  ,Vlera
	  ,Gratis
	  ,Perjashto
	  ,Cmimi
FROM @POSAktivitetetFilterType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetFilter) 
  
DELETE FROM POSAktivitetetFilter WHERE Id NOT IN (SELECT Id FROM @POSAktivitetetFilterType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetLlojetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetLlojetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetLlojetType') 
			  begin
			  Drop  type  TOSHIBA.POSAktivitetetLlojetType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetLlojetType] AS TABLE
(
	[Id] INT NOT NULL,
	[Pershkrimi] VARCHAR(50) NOT NULL,
	[Kodi] VARCHAR(50) NULL
)
Go --
CREATE PROCEDURE TOSHIBA.POSAktivitetetLlojetUpdateInsert_sp
(  
 @AktivitetetLlojetType TOSHIBA.POSAktivitetetLlojetType readonly   
)  
   
as begin 

 Update A set 
        A.Pershkrimi = B.Pershkrimi   
	   ,A.Kodi       = B.Kodi
From dbo.POSAktivitetetLlojet A inner join @AktivitetetLlojetType B on A.id = B.id  

SET IDENTITY_INSERT POSAktivitetetLlojet ON

insert into dbo.POSAktivitetetLlojet  
      (Id  
      ,Pershkrimi  
      ,Kodi  
      )  
SELECT Id  
      ,Pershkrimi  
      ,Kodi   
  FROM @AktivitetetLlojetType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetLlojet)  

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleAktivitetetInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitDetaleAktivitetetInsert_sp
			  END

Go --

CREATE Procedure TOSHIBA.DaljaMallitDetaleAktivitetetInsert_sp  
(
	@DaljaMallitDetaleId int,
	@AktivitetiId bigint,
	@Zbritja decimal(23,8),
	@DaljaMallitId bigint=null
)
 
as begin 

Insert into dbo.DaljaMallitDetaleAktivitetet 
(
	DaljaMallitDetaleId,AktivitetiId,Zbritja,DaljaMallitId 
) 
Values 
( 
	@DaljaMallitDetaleId,@AktivitetiId,@Zbritja,@DaljaMallitId 
) 

Select scope_identity() as Id 

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetSelect_sp
			  END

Go --

Create Procedure TOSHIBA.POSAktivitetetSelect_sp
(
	@LlojiIDokumentitId int,
	@OrganizataId int
) 
as 
begin

	Select
		A.Id,
		A.PrejDates,
		A.DeriMeDaten,
		A.PrejOres,
		A.DeriMeOren,
		A.Pershkrimi,
		A.Statusi,
		A.OrganizataId,
		A.LlojiIDokumentitId,
		A.LlojiIDokumentitId ShifraDokumentit,
		LD.Pershkrimi PershkrimiDokumentit,
		A.NrDokumentit,
		A.Viti,
		A.Data,
		A.PerfshinKatallogun,
		case when A.Dhe = 1 then 1 else 0 End Dhe,
		case when A.Dhe <> 1 then 1 else 0 End Ose,
		A.AplikoZbritjenNeGrup,
		A.Mbishkruaj,
		A.LlojiAktivitetitId,
		LL.Pershkrimi LlojiAktivitetit,
		A.Validuar,
		Case when ISNULL(A.Validuar,0) = 1 then 0 else 1 End [Enabled],
		ISNULL(A.GratisFix,0) as GratisFix,
		A.AplikoNeRabat,
		A.AplikoNeEkstraRabat
	From dbo.POSAktivitetet A
		inner join dbo.LlojetEDokumenteve LD on A.LlojiIDokumentitId = LD.Id
		left outer join dbo.POSAktivitetetLlojet LL on A.LlojiAktivitetitId = LL.Id
	where   
		    (A.Statusi=1)
		AND (A.PrejDates <= CAST(GETDATE() as date))
		AND (A.DeriMeDaten >= CAST(GETDATE() as date))
		AND (A.LlojiIDokumentitId=@LlojiIDokumentitId)
		AND (A.Id IN (select AktivitetiId from POSAktivitetetOrganizatat where OrganizataId=@OrganizataId and Statusi=1))
	order by a.Mbishkruaj,A.PrejDates

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetFilterSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetFilterSelect_sp
			  END

Go --

Create Procedure TOSHIBA.POSAktivitetetFilterSelect_sp 
(
	@LlojiDokumentitId int,
	@OrganizataId int
) 
AS BEGIN
 
	SELECT 
		F.Id,
		F.AktivitetiId,
		F.FilteriId,
		L.Pershkrimi FilteriPershkrimi,
		F.Shifra,
		F.Pershkrimi,
		F.AplikoZbritjen,
		F.EmriFushesPerFilter,
		F.LlojiZbritjesId,
		Z.Pershkrimi LlojiZbritjes,
		F.Vlera,
		F.Zbritja,
		F.Gratis,
		F.Perjashto,
		F.Cmimi
	FROM dbo.POSAktivitetetFilter F 
		INNER JOIN POSAktivitetetFilterLlojet L ON F.FilteriId=L.Id
		INNER JOIN POSAktivitetet AK ON  F.AktivitetiId=AK.Id
		LEFT OUTER JOIN POSAktivitetetLlojetEZbritjes Z ON F.LlojiZbritjesId=Z.Id
	WHERE  
			(AK.Statusi=1)
		AND (AK.PrejDates <= CAST(GETDATE() as date))
		AND (AK.DeriMeDaten >= CAST(GETDATE() as date))
		AND (AK.LlojiIDokumentitId=@LlojiDokumentitId)
		AND (AK.Id IN (select AktivitetiId from POSAktivitetetOrganizatat where OrganizataId=@OrganizataId and Statusi=1))

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetZbritjaNeVlereSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetZbritjaNeVlereSelect_sp
			  END

Go --

CREATE PROCEDURE [TOSHIBA].[POSAktivitetetZbritjaNeVlereSelect_sp]
(
	@LlojiDokumentitId int,
	@OrganizataId int
) 
as begin
 
	Select 
		A.Id,
		A.AktivitetiId,
		A.LlojiIZbrijtesId,
		L.Pershkrimi,
		A.Vlera,
		A.Zbritja,
		A.Komenti 
	From dbo.POSAktivitetetZbritjaNeVlere A
		inner join POSAktivitetetLlojetEZbritjes as L on A.LlojiIZbrijtesId=L.Id
		inner join POSAktivitetet AK on A.AktivitetiId=AK.Id
	where 
		    (AK.Statusi=1)
		AND (AK.PrejDates <= CAST(GETDATE() as date))
		AND (AK.DeriMeDaten >= CAST(GETDATE() as date))
		AND (AK.LlojiIDokumentitId=@LlojiDokumentitId)
		AND (AK.Id IN (select AktivitetiId from POSAktivitetetOrganizatat where OrganizataId=@OrganizataId and Statusi=1))

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetPlotesoDetalet_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetPlotesoDetalet_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetType') 
			  begin
			  Drop  type  TOSHIBA.POSAktivitetetType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetType] AS TABLE
(
	[DaljaMallitDetaleId] INT             NULL,
    [ArtikulliId]         INT             NULL,
	[Artikulli]           VARCHAR(200)    NULL,
    [Sasia]               DECIMAL (18, 2) NULL,
    [QmimiShitjes]        DECIMAL (18, 2) NULL,
	[Rabati]              DECIMAL (18, 2) NULL,
	[Komenti]             VARCHAR (200)   NULL,
	[EkstraRabati]        DECIMAL (18, 2) NULL
)
Go --
CREATE PROCEDURE [TOSHIBA].[POSAktivitetetPlotesoDetalet_sp]
(  
   @POSAktivitetet [TOSHIBA].[POSAktivitetetType] READONLY  ,
   @OrganizataId int
)  
as  
begin  
  
		select 
			dd.DaljaMallitDetaleId,
			convert(varchar(50),dd.ArtikulliId) ArtikulliId,
			dd.Sasia,
			dd.QmimiShitjes,
			convert(decimal(18,2),(Sasia*QmimiShitjes*(1-dd.rabati/100.00)*(1-ISNULL(dd.EkstraRabati,0.00)/100.00))) Vlera,
			convert(varchar(50),isnull(a.BrendId,-1)) BrendiId,
			cast(0.00 as decimal(18,2)) Rabati,
			cast(0.00 as decimal(18,2)) EkstraRabati,
			dd.Komenti ,
			convert(bit,0) as AplikoZbritjen,
			case when c.StatusiQmimitId=13 then 1 else 0 end PerfshinKatallogun,
			isnull(substring(G6.shifra,1,12),'-')  G6,
			isnull(substring(G6.shifra,1,10),'-')  G5,
			isnull(substring(G6.shifra,1,8),'-')  G4,
			isnull(substring(G6.shifra,1,6),'-')  G3,
			isnull(substring(G6.shifra,1,4),'-')  G2,
			isnull(substring(G6.shifra,1,2),'-')  G1,

			isnull(substring(G6.shifra,1,6),'-')  N3,
			isnull(substring(G6.shifra,1,4),'-')  N2,
			isnull(substring(G6.shifra,1,2),'-')  N1,
			ISNULL(AD.NrDokumentit,-1) NrDokumentit,
			CAST(0.00 AS DECIMAL(18,2)) Cmimi,
			cast(1 as int) Artikujt
		from @POSAktivitetet dd  
		inner join Artikujt a with(nolock) on dd.ArtikulliId=a.Id  
		inner join Cmimorja c with(nolock) on a.id=c.ArtikulliId and c.OrganizataId=@OrganizataId
		inner join GrupetEMallrave G6 with(nolock) ON A.GrupiMallitID=G6.Id
		left outer join Brendet b with(nolock) on a.BrendId=b.Id
		left outer join   
			(	SELECT A.NrDokumentit, AD.ArtikulliId 
				FROM AktivitetetDokument A  with(nolock)  
					INNER join AktivitetetDokumentDetale AD  with(nolock) on A.Id=AD.AktivitetetDokumentId
					inner join (select max(id) ID,ArtikulliId from AktivitetetDokumentDetale group by ArtikulliId) f on ad.id=f.ID
			) AD on a.Id=AD.ArtikulliId

		order by dd.QmimiShitjes
 end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ArtikujtPerAmbalazhSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.ArtikujtPerAmbalazhSelect_sp
			  END
Go --
Create Procedure [TOSHIBA].[ArtikujtPerAmbalazhSelect_sp]
(    
@OrganizataId int  
  )   
as begin   
  
select A.Id  
,A.Shifra  
,A.Pershkrimi  
,A.ShifraProdhuesit  
,A.PershkrimiTiketa   
,A.NjesiaID  
,NJ.Pershkrimi Njesia   
,A.BrendId  
,BR.Pershkrimi Brendi  
,1 Sasia  
,B.Tvsh TVSH  
,B.QmimiIShitjes  
,B.QmimiIShitjes QmimiFinal  
,Case when B.QmimiShumice is null then B.QmimiIShitjes  
  When  B.QmimiShumice  <=0   then B.QmimiIShitjes  
  when  B.QmimiShumice  > 0   then B.QmimiShumice   
  else B.QmimiShumice end QmimiShumice  
,IsNull((Select ZbritjaPerqindje from POS_PlaniZbritjeve  with(nolock) where Convert(Date,GETDATE()) Between DataPrej and DataDeri ),0.00) Rabati  
,0.00 EkstraRabati  
,0.00 Vlera  
,B.Stoku  
,IsNull(A.Paketimi ,0) Paketimi  
,cast(isnull((Select top 1 Case when ArtikulliId Is null then 0  
     when ArtikulliId IS not null then 1   
     else 0 end from dbo.ArtikujtMeLirim  with(nolock) where ArtikulliId=A.Id and B.OrganizataId =@OrganizataId),0) as Bit) KaLirim  
,cast(isnull((select top 1 1 from ArtikujtPaZbritje where ArtikulliId = A.Id),0) as Bit) ArtikullPaZbritje  
,B.StatusiQmimitId 
,A.GrupiMallitID
,F.Foto
,A.LlojiIArtikullitId
From Artikujt A  with(nolock)  
INNER JOIN dbo.Cmimorja B  with(nolock) ON A.Id=B.ArtikulliId And B.OrganizataId=@OrganizataId  
INNER JOIN dbo.LlojetEArtikullit L  with(nolock) ON A.LlojiIArtikullitId =L.Id   
INNER JOIN Njesit NJ  WITH(NOLOCK) ON A.NjesiaID = NJ.Id  
LEFT OUTER JOIN Brendet BR WITH(NOLOCK) ON A.BrendId = BR.Id 
inner join GrupetEMallrave G on A.GrupiMallitID = G.Id
LEFT OUTER JOIN ArtikujtFoto F ON A.Id=F.ArtikulliId
Where    
    --(B.QmimiIShitjes >0)  and
 (A.LlojiIArtikullitId=20) 
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_OrganizataSelectBankat_sp') ) 
			  BEGIN
			  DROP  PROCEDURE TOSHIBA.POS_OrganizataSelectBankat_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[POS_OrganizataSelectBankat_sp]
(@NjesiaID INT =NULL,
@Mxh_ObjektiAplikacion INT = NULL)

AS 
BEGIN 
	DECLARE @paraqitja INT

	SELECT B.EmriIBankes,ISNULL(b.SwiftCode,'') SwiftCode,ISNULL(b.IBAN,'') IBAN,sb.NrLlogaris,Renditja
	FROM dbo.SubjektiBankat sb  WITH(NOLOCK)
	INNER JOIN dbo.Bankat b WITH(NOLOCK) ON b.Id = sb.BankaId
	WHERE sb.SubjektiId = @NjesiaID
	ORDER BY Renditja ASC
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSZbritjaMeKuponSelect_sp') ) 
			  BEGIN
			  DROP  PROCEDURE TOSHIBA.POSZbritjaMeKuponSelect_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[POSZbritjaMeKuponSelect_sp]
(
@Id int  =null,
@POSKuponatPerZbritjeId int  =null,
@Vlera decimal (18,2) =null,
@DaljaMallitId bigint  =null
) 
as begin
Select A.Id,A.POSKuponatPerZbritjeId,A.Vlera,A.DaljaMallitId,a.KodiKuponit
FROM dbo.POSZbritjaMeKupon as A
inner join POSKuponatPerZbritje K on K.Id = A.POSKuponatPerZbritjeId
where  (A.Id=@Id or @Id is null ) 
and  (A.POSKuponatPerZbritjeId=@POSKuponatPerZbritjeId or @POSKuponatPerZbritjeId is null ) 
and  (A.Vlera=@Vlera or @Vlera is null ) 
and  (A.DaljaMallitId=@DaljaMallitId or @DaljaMallitId is null ) 
 
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSZbritjaMeKuponInsert_sp') ) 
			  BEGIN
			  DROP  PROCEDURE TOSHIBA.POSZbritjaMeKuponInsert_sp
			  END
Go --

CREATE Procedure TOSHIBA.POSZbritjaMeKuponInsert_sp  
(
	@POSKuponatPerZbritjeId int ,
	@Vlera decimal (18,2),
	@DaljaMallitId bigint ,
	@KodiKuponit varchar(50)
									
)
 
as begin 
Insert into dbo.POSZbritjaMeKupon 
(
POSKuponatPerZbritjeId,Vlera,DaljaMallitId ,KodiKuponit) 
Values 
( 
@POSKuponatPerZbritjeId,@Vlera,@DaljaMallitId ,@KodiKuponit) 
Select scope_identity() as Id 
end 
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitDetaleAktivitetetDelete_sp') ) 
			  BEGIN
			  DROP  PROCEDURE TOSHIBA.DaljaMallitDetaleAktivitetetDelete_sp
			  END
Go --

CREATE Procedure TOSHIBA.DaljaMallitDetaleAktivitetetDelete_sp  
(
	@DaljaMallitId bigint
)
 
AS BEGIN 
	DELETE FROM DaljaMallitDetaleAktivitetet
	WHERE DaljaMallitId=@DaljaMallitId
end 
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KonfigurimetGetVleren_Sp') ) 
			  BEGIN
			  DROP  PROCEDURE TOSHIBA.KonfigurimetGetVleren_Sp
			  END
Go --

CREATE PROCEDURE TOSHIBA.KonfigurimetGetVleren_Sp
(
@Id int,
@OrganizataId int=null
) 
as begin 
Select isnull(Vlera ,2 ) Vlera
From Konfigurimet K with(nolock) left outer join KonfigurimetOrganizatat KO with(nolock) on K.Id=KO.KonfigurimiId 
where  (Id=@Id) 
and (KO.OrganizataId = @OrganizataId or @OrganizataId is null )
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.Mxh_OperatoretGrupetEMallraveUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.Mxh_OperatoretGrupetEMallraveUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'Mxh_OperatoretGrupetEMallraveType') 
			  BEGIN
			  Drop  type  TOSHIBA.Mxh_OperatoretGrupetEMallraveType
			  End 
Go --
CREATE TYPE [TOSHIBA].[Mxh_OperatoretGrupetEMallraveType] AS TABLE
(
	[Id]            INT    NOT NULL,
    [OperatoriId]   INT    NOT NULL,
    [GrupiMallitId] BIGINT NOT NULL
)
Go --
CREATE PROCEDURE TOSHIBA.Mxh_OperatoretGrupetEMallraveUpdateInsert_sp
(  
 @Mxh_OperatoretGrupetEMallraveType TOSHIBA.Mxh_OperatoretGrupetEMallraveType READONLY   
)  
   
AS BEGIN

UPDATE  A SET 
        A.OperatoriId = B.OperatoriId   
	   ,A.GrupiMallitId    = B.GrupiMallitId
From dbo.Mxh_OperatoretGrupetEMallrave A inner join @Mxh_OperatoretGrupetEMallraveType B on A.id = B.id

INSERT INTO dbo.Mxh_OperatoretGrupetEMallrave  
      (Id  
      ,OperatoriId  
      ,GrupiMallitId  
      )  
SELECT Id  
      ,OperatoriId  
      ,GrupiMallitId
  FROM @Mxh_OperatoretGrupetEMallraveType WHERE Id NOT IN (SELECT Id FROM dbo.Mxh_OperatoretGrupetEMallrave)
  
  DELETE FROM Mxh_OperatoretGrupetEMallrave WHERE Id NOT IN (SELECT Id FROM @Mxh_OperatoretGrupetEMallraveType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPGrupetUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.CCPGrupetUpdateInsert_sp
			  END
Go --
 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPGrupetType') 
			  BEGIN
			  Drop  type  TOSHIBA.CCPGrupetType
			  End 
Go --

CREATE TYPE [TOSHIBA].[CCPGrupetType] AS TABLE
(
    [Id]         INT           NOT NULL,
	[Pershkrimi] varchar(200)  NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.CCPGrupetUpdateInsert_sp
(  
 @CCPGrupetType TOSHIBA.CCPGrupetType READONLY   
)  
AS BEGIN

 UPDATE A SET 
        A.Pershkrimi=B.Pershkrimi

From dbo.CCPGrupet A inner join @CCPGrupetType B on A.id = B.id    

INSERT INTO dbo.CCPGrupet  
      (Id  
      ,Pershkrimi
      )  
SELECT Id  
      ,Pershkrimi
  FROM @CCPGrupetType WHERE Id NOT IN (SELECT Id FROM dbo.CCPGrupet) 
  
  DELETE FROM CCPGrupet WHERE id NOT IN (SELECT id FROM @CCPGrupetType)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetFilterCCPGrupetUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSAktivitetetFilterCCPGrupetUpdateInsert_sp
			  END
Go --
 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetFilterCCPGrupetType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSAktivitetetFilterCCPGrupetType
			  End 
Go --

CREATE TYPE [TOSHIBA].[POSAktivitetetFilterCCPGrupetType] AS TABLE
(
    [Id]           INT         NOT NULL,
	[AktivitetiId] BIGINT      NOT NULL,
	[CCPGrupiId]   INT         NOT NULL,
	[KodiKarteles] VARCHAR(80) NULL
)
Go --

CREATE PROCEDURE TOSHIBA.POSAktivitetetFilterCCPGrupetUpdateInsert_sp
(  
 @POSAktivitetetFilterCCPGrupetType TOSHIBA.POSAktivitetetFilterCCPGrupetType READONLY   
)  
AS BEGIN

 UPDATE A SET 
        A.CCPGrupiId=B.CCPGrupiId,
		A.KodiKarteles=B.KodiKarteles

From dbo.POSAktivitetetFilterCCPGrupet A inner join @POSAktivitetetFilterCCPGrupetType B on A.Id=B.Id

INSERT INTO dbo.POSAktivitetetFilterCCPGrupet  
      (Id  
      ,AktivitetiId  
      ,CCPGrupiId
	  ,KodiKarteles
      )  
SELECT Id  
      ,AktivitetiId  
      ,CCPGrupiId
	  ,KodiKarteles
  FROM @POSAktivitetetFilterCCPGrupetType WHERE Id NOT IN (SELECT Id FROM dbo.POSAktivitetetFilterCCPGrupet) 
  
  DELETE FROM POSAktivitetetFilterCCPGrupet WHERE id NOT IN (SELECT id FROM @POSAktivitetetFilterCCPGrupetType)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetFilterCCPGrupetSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetFilterCCPGrupetSelect_sp
			  END

Go --

CREATE PROCEDURE TOSHIBA.POSAktivitetetFilterCCPGrupetSelect_sp
(
    @LlojiDokumentitId int,
	@OrganizataId int
) 
as BEGIN

	Select 
		A.Id,
		A.AktivitetiId,
		A.CCPGrupiId,
		C.Pershkrimi,
		A.KodiKarteles
	From dbo.POSAktivitetetFilterCCPGrupet A
		INNER JOIN CCPGrupet C on A.CCPGrupiId=C.Id
		inner join POSAktivitetet P on A.AktivitetiId=P.Id
	where
			(P.Statusi=1)
		AND (P.PrejDates <= CAST(GETDATE() as date))
		AND (P.DeriMeDaten >= CAST(GETDATE() as date))
		AND (P.LlojiIDokumentitId=@LlojiDokumentitId)
		AND (P.Id IN (select AktivitetiId from POSAktivitetetOrganizatat where OrganizataId=@OrganizataId and Statusi=1))
 
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.RebuildIndexesLokalisht_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.RebuildIndexesLokalisht_Sp
			  END

Go --

CREATE PROCEDURE TOSHIBA.RebuildIndexesLokalisht_Sp
AS
BEGIN
			IF NOT EXISTS(select 1 from sys.tables where Name = 'RebuildIndexes') 
			BEGIN
			CREATE TABLE [dbo].[RebuildIndexes]
			(
				[Data] DATE NOT NULL PRIMARY KEY
			)
			end
			DECLARE @DataEFunditERebuild DATE=ISNULL((SELECT TOP 1 Data FROM [dbo].[RebuildIndexes]),DATEADD(DAY,-1,CONVERT(DATE,GETDATE())))
			IF(@DataEFunditERebuild<>CONVERT(DATE,GETDATE()))
			Begin
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
																DELETE [dbo].[RebuildIndexes]

					INSERT INTO [dbo].[RebuildIndexes] (Data)
					SELECT CONVERT(DATE,GETDATE())
			END

End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.LeximiSondaveInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.LeximiSondaveInsert_sp
			  END

Go --

CREATE PROCEDURE TOSHIBA.LeximiSondaveInsert_sp
(
	@TankNumber int,
	@FuelLevel int,
	@FuelVolume bigint,
	@WaterLevel int,
	@WaterVolume bigint,
	@Temperature int,
	@Volume int
)
AS
BEGIN
				INSERT INTO LeximiSondave
				(
				TankNumber,
				FuelLevel ,
				FuelVolume ,
				WaterLevel,
				WaterVolume,
				Temperature ,
				Volume
				) 
				VALUES
				(
				@TankNumber,
				@FuelLevel ,
				@FuelVolume ,
				@WaterLevel,
				@WaterVolume,
				@Temperature ,
				@Volume
				)		

End
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ParapagimetUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.ParapagimetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ParapagimetType') 
			  begin
			  Drop  type  TOSHIBA.ParapagimetType
			  End 
Go --
CREATE Type TOSHIBA.ParapagimetType AS TABLE 
(
	ParapagimiId BIGINT  NOT NULL PRIMARY KEY,
	Vlera DECIMAL(18,2) not null,
	SubjektiId int not null
)
Go --
CREATE PROCEDURE TOSHIBA.ParapagimetUpdateInsert_sp
(
@ParapagimetType TOSHIBA.ParapagimetType READONLY 
)
AS
BEGIN
Update A 
set 
A.Vlera=S.Vlera
From dbo.Parapagimet A inner join @ParapagimetType S on A.ParapagimiId=S.ParapagimiId

INSERT INTO dbo.Parapagimet 
    ( ParapagimiId,Vlera, SubjektiId
    )
SELECT 
	ParapagimiId,Vlera, SubjektiId 
 
FROM @ParapagimetType WHERE ParapagimiId NOT IN (SELECT ParapagimiId FROM dbo.Parapagimet)

delete from Parapagimet where ParapagimiId not in (SELECT ParapagimiId FROM @ParapagimetType)
End

Go --
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TOSHIBA.ParapagimetSelect_sp') ) 
BEGIN
DROP  PROCEDURE TOSHIBA.ParapagimetSelect_sp
END
Go --
CREATE PROCEDURE TOSHIBA.ParapagimetSelect_sp
(
	@SubjektiId int
)
AS
BEGIN

select ParapagimiId, SubjektiId, Vlera from Parapagimet where SubjektiId = @SubjektiId

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.ArtikujtPerberesOrganizatatDeleteInsert_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.ArtikujtPerberesOrganizatatDeleteInsert_sp
	END
Go --
 
IF EXISTS (SELECT * FROM sys.types 
	WHERE name = 'ArtikujtPerberesOrganizatatType') 
	BEGIN
		Drop  type  TOSHIBA.ArtikujtPerberesOrganizatatType
	End 
Go --

CREATE TYPE [TOSHIBA].[ArtikujtPerberesOrganizatatType] AS TABLE
(
	[OrganizataId] INT NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.ArtikujtPerberesOrganizatatDeleteInsert_sp
(  
	@ArtikujtPerberesOrganizatatType TOSHIBA.ArtikujtPerberesOrganizatatType READONLY   
)  
AS BEGIN

delete from ArtikujtPerberesOrganizatat   

INSERT INTO dbo.ArtikujtPerberesOrganizatat  
(
	OrganizataId
)  
SELECT
	OrganizataId  
FROM @ArtikujtPerberesOrganizatatType 

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.AktivitetetDokumentUpdateInsert_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.AktivitetetDokumentUpdateInsert_sp
	END
Go --
 
IF EXISTS (SELECT * FROM sys.types 
	WHERE name = 'AktivitetetDokumentType') 
	BEGIN
		Drop  type  TOSHIBA.AktivitetetDokumentType
	End 
Go --

CREATE TYPE [TOSHIBA].[AktivitetetDokumentType] AS TABLE
(
    Id BIGINT NOT NULL,
	Pershkrimi VARCHAR(100) NOT NULL,
	OrganizataId INT NOT NULL,
	NrDokumentit INT NOT NULL,
	Viti INT NOT NULL,
	Data DATE NOT NULL,
	DataRegjistrimit DATETIME NOT NULL,
	RegjistruarNga INT NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.AktivitetetDokumentUpdateInsert_sp
(  
    @AktivitetetDokumentType TOSHIBA.AktivitetetDokumentType readonly   
) 
AS BEGIN 

    UPDATE 
        A set 		   
        A.Pershkrimi=B.Pershkrimi,
        A.Data=B.Data,
        A.DataRegjistrimit=B.DataRegjistrimit,
        A.RegjistruarNga=B.RegjistruarNga
    FROM dbo.AktivitetetDokument A 
        INNER join @AktivitetetDokumentType B on A.id=B.id  

    INSERT INTO dbo.AktivitetetDokument(
        Id,
	    Pershkrimi,
	    OrganizataId,
	    NrDokumentit,
        Viti,
        Data,
        DataRegjistrimit,
        RegjistruarNga
    )
    SELECT 
        Id,  
        Pershkrimi,  
        OrganizataId,  
        NrDokumentit,
        Viti,
        Data,
        DataRegjistrimit,
        RegjistruarNga
    FROM @AktivitetetDokumentType WHERE Id NOT IN(SELECT Id FROM dbo.AktivitetetDokument)  

	DELETE FROM dbo.AktivitetetDokumentDetale WHERE AktivitetetDokumentId NOT IN (SELECT Id FROM @AktivitetetDokumentType)
    DELETE FROM dbo.AktivitetetDokument WHERE Id NOT IN(SELECT Id FROM @AktivitetetDokumentType)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.AktivitetetDokumentDetaleUpdateInsert_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.AktivitetetDokumentDetaleUpdateInsert_sp
	END
Go --
 
IF EXISTS (SELECT * FROM sys.types 
	WHERE name = 'AktivitetetDokumentDetaleType') 
	BEGIN
		Drop  type  TOSHIBA.AktivitetetDokumentDetaleType
	End 
Go --

CREATE TYPE [TOSHIBA].[AktivitetetDokumentDetaleType] AS TABLE
(
    Id INT NOT NULL,
	AktivitetetDokumentId BIGINT NOT NULL,
	ArtikulliId INT NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.AktivitetetDokumentDetaleUpdateInsert_sp
(  
    @AktivitetetDokumentDetaleType TOSHIBA.AktivitetetDokumentDetaleType readonly   
) 
AS BEGIN   

    INSERT INTO dbo.AktivitetetDokumentDetale(
        Id,
	    AktivitetetDokumentId,
	    ArtikulliId
    )
    SELECT 
        Id,  
        AktivitetetDokumentId,  
        ArtikulliId
    FROM @AktivitetetDokumentDetaleType WHERE Id NOT IN(SELECT Id FROM dbo.AktivitetetDokumentDetale)  

    DELETE FROM AktivitetetDokumentDetale WHERE Id NOT IN(SELECT Id FROM @AktivitetetDokumentDetaleType)

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_KuponiFiskalSelect_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.POS_KuponiFiskalSelect_sp
	END
Go --
CREATE PROCEDURE [TOSHIBA].[POS_KuponiFiskalSelect_sp]
    @DaljaMallitId BIGINT
AS
BEGIN
SELECT
    G.Pershkrimi,
    G.PershkrimiFiskal,
    G.Sasia,
    G.KategoriaTvsh,
    G.Shifra,
    G.RegjistruarNga,
    G.OrganizataId,
    --case when (CmimiShitjesDyDec-CmimiShitjesShumeDec)<>0 then 0 else Rabati end Rabati,
    --case when (CmimiShitjesDyDec-CmimiShitjesShumeDec)<>0 then 0 ELSE G.RabatiVlere END RabatiVlere,
    CmimiShitjesDyDec,
    --case when (CmimiShitjesDyDec-CmimiShitjesShumeDec)<>0 then G.Sasia * (CmimiShitjesDyDec - CmimiShitjesShumeDec) + RabatiVlere else 0 end ZbritjeVlere,
    case when (CmimiShitjesDyDec-CmimiShitjesShumeDec)<>0 then convert(decimal(18,2),
                                                                       ((G.Sasia * (CmimiShitjesDyDec - CmimiShitjesShumeDec) + RabatiVlere) / (G.Sasia * CmimiShitjesDyDec) * 100)) ELSE Rabati end ZbritjeMePerqindje,
    NrFatures,
    Tvsh,
    DataERegjistrimit,
    DokumentiId,
    NumriArkes,
    Operatori,
    NumriFaturesManual
FROM
    (
        SELECT
            REPLACE(REPLACE(A.Pershkrimi,Char(13),''), '''','') Pershkrimi
             ,A.PershkrimiFiskal
             ,DD.QmimiShitjes
             ,SUM(DD.Sasia) as Sasia
             ,T.Kategoria KategoriaTvsh
             ,A.Shifra
             ,D.RegjistruarNga
             ,D.OrganizataId

             ,
            CASE WHEN sum(DD.Sasia)<0 AND (DD.Rabati>0 OR DD.EkstraRabati>0) THEN CONVERT(DECIMAL(18,2),DD.QmimiShitjes*(1-rabati/100.00)*(1-EkstraRabati/100.00)) else
                CASE WHEN (ROUND(DD.QmimiShitjes, 2) - DD.QmimiShitjes)<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END end CmimiShitjesDyDec

             ,CASE WHEN sum(DD.Sasia)<0 AND (DD.Rabati>0 OR DD.EkstraRabati>0) THEN CONVERT(DECIMAL(18,2),DD.QmimiShitjes*(1-rabati/100.00)*(1-EkstraRabati/100.00)) else DD.QmimiShitjes end CmimiShitjesShumeDec
             ,case when sum(DD.Sasia)<0 then 0 else CONVERT(DECIMAL(18,2), CONVERT(decimal(18,6),dd.QmimiShitjes - (dd.QmimiShitjes*(1-DD.Rabati/100.00) * (1 - dd.EkstraRabati / 100.00))) / dd.QmimiShitjes * 100.00) end Rabati
             ,case when sum(DD.Sasia)<0 then 0 else SUM(DD.Sasia * (((DD.QmimiShitjes * (DD.Rabati / 100.00)) + (DD.QmimiShitjes * (DD.EkstraRabati / 100.00))))) end RabatiVlere
             ,D.NrFatures NrFatures
             ,DD.Tvsh Tvsh
             ,MAX(d.DataERegjistrimit) as DataERegjistrimit
             ,D.DokumentiId DokumentiId
             ,D.NumriArkes
             ,O.Emri +  ' ' + O.Mbiemri Operatori
             ,D.NumriFaturesManual
        FROM dbo.DaljaMallitDetale DD
                 INNER JOIN dbo.DaljaMallit D ON D.Id = DD.DaljaMallitID
                 INNER JOIN dbo.Artikujt A ON A.Id = DD.ArtikulliId
                 INNER JOIN dbo.Tatimet T ON A.TatimetID =T.Id
                 INNER JOIN dbo.Mxh_Operatoret AS O ON D.RegjistruarNga = O.Id
        WHERE DD.DaljaMallitID = @DaljaMallitId
        Group by A.Shifra, A.Pershkrimi, A.PershkrimiFiskal,DD.QmimiShitjes, T.Kategoria, D.RegjistruarNga, D.OrganizataId
               ,DD.Rabati, DD.EkstraRabati, d.NrFatures, dd.Tvsh, D.DataERegjistrimit, d.DokumentiId, d.NumriArkes, D.NumriFaturesManual
               ,o.Emri, o.Mbiemri
        --union all
        --SELECT 
        --A.Pershkrimi
        --,DD.QmimiShitjes
        --,DD.Sasia 
        --,T.Kategoria KategoriaTvsh
        --,A.Shifra 
        --,D.RegjistruarNga
        --,D.OrganizataId
        --,CASE WHEN round(DD.QmimiShitjes, 2) - DD.QmimiShitjes<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END CmimiShitjesDyDec
        --,DD.QmimiShitjes CmimiShitjesShumeDec
        --,CONVERT(DECIMAL(18,2), CONVERT(decimal(18,6),dd.QmimiShitjes - (dd.QmimiShitjes * (1 - DD.Rabati / 100) * (1 - dd.EkstraRabati / 100))) / dd.QmimiShitjes * 100) Rabati
        --,DD.Sasia * (((DD.QmimiShitjes * (DD.Rabati / 100.00)) + (DD.QmimiShitjes * (DD.EkstraRabati/100.00)))) RabatiVlere
        --,D.NrFatures NrFatures
        --FROM dbo.DaljaMallitDetale DD 
        --INNER JOIN dbo.DaljaMallit D ON D.Id = DD.DaljaMallitID
        --INNER JOIN dbo.Artikujt A ON A.Id = DD.ArtikulliId 
        --INNER JOIN dbo.Tatimet T ON A.TatimetID = T.Id
        --WHERE DD.DaljaMallitID = @DaljaMallitId and DD.Sasia < 0

    ) G where Rabati<99.99 ORDER BY G.Sasia desc
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_EkzekutimiPagesesSelect_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.POS_EkzekutimiPagesesSelect_sp
	END
Go --
Create Procedure [TOSHIBA].[POS_EkzekutimiPagesesSelect_sp] 
(
@Id int  =null,
@MenyraEPagesesId int  =null,
@DaljaMallitID bigint  =null,
@Vlera decimal (18,2) =null,
@Paguar decimal (18,2) =null,
@ShifraOperatorit int =null,
@RreshtiIIdPerSHfletim varchar(2000) = NULL	
		) 
as begin 
Select E.Id,
MenyraEPagesesId, 
m.Pershkrimi MenyraEPageses,
DaljaMallitID,
Vlera, 
Paguar ,
E.ShifraOperatorit,
E.DhenjeKesh,
E.ValutaId,
V.Pershkrimi Valuta,
V.Kursi,
CAST((E.Vlera/E.Kursi) AS DECIMAL(18,2)) VleraValutore,
E.KodiVoucherit,
E.LlojiVoucherit 
From EkzekutimiPageses E WITH(NOLOCK)
INNER JOIN dbo.MenyratEPageses m ON e.MenyraEPagesesId=m.Id
LEFT OUTER JOIN dbo.Valutat V ON E.ValutaId=V.Id
where  (E.Id=@Id or @Id is null ) 
and  (MenyraEPagesesId=@MenyraEPagesesId or @MenyraEPagesesId is null ) 
and  (DaljaMallitID=@DaljaMallitID or @DaljaMallitID is null ) 
and  (Vlera=@Vlera or @Vlera is null ) 
and  (Paguar=@Paguar or @Paguar is null ) 
and  (E.ShifraOperatorit=@ShifraOperatorit or @ShifraOperatorit is null ) 
And ((E.DaljaMallitID in (select Convert(bigint,Id) from dbo.KonvertoStringNeTabel(@RreshtiIIdPerSHfletim,','))) or @RreshtiIIdPerSHfletim is null )
ORDER BY CASE MenyraEPagesesId WHEN 22 THEN 0 ELSE 1 END, MenyraEPagesesId

end
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitKuponiFiskalShtypurUpdate_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.DaljaMallitKuponiFiskalShtypurUpdate_sp
	END
Go --

CREATE PROCEDURE [TOSHIBA].[DaljaMallitKuponiFiskalShtypurUpdate_sp]
(
	@DaljaMallitId bigint,
	@KuponiFiskalShtypur bit=null
)
AS

update DaljaMallit
Set KuponiFiskalShtypur = isnull(@KuponiFiskalShtypur,1)
where Id = @DaljaMallitId

Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.ValutatUpdateInsert_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.ValutatUpdateInsert_sp
	END
Go --

IF EXISTS (SELECT * FROM sys.types 
	WHERE name = 'ValutatType') 
	BEGIN
		DROP  TYPE  TOSHIBA.ValutatType
	END 
Go --

CREATE TYPE [TOSHIBA].[ValutatType] AS TABLE
(
    [Id] INT NOT NULL,
	[Shifra] VARCHAR(5) NOT NULL,
	[Pershkrimi] VARCHAR(50) NOT NULL,
	[Kursi] DECIMAL(18,10) NULL,
	[ValutaAktive] BIT NOT NULL
)
Go --

CREATE PROCEDURE [TOSHIBA].[ValutatUpdateInsert_sp]
(  
    @ValutatType TOSHIBA.ValutatType readonly   
)   
as begin 

 Update A set 		   
        A.Shifra=B.Shifra,
        A.Pershkrimi=B.Pershkrimi,
        A.Kursi=B.Kursi,
        A.ValutaAktive=B.ValutaAktive
From dbo.Valutat A inner join @ValutatType B on A.id=B.id  

insert into dbo.Valutat  
      (Id,
	   Shifra,
	   Pershkrimi,
	   Kursi,
       ValutaAktive
      )  
SELECT Id  
      ,Shifra  
      ,Pershkrimi  
      ,Kursi
      ,ValutaAktive
FROM @ValutatType WHERE Id NOT IN(SELECT Id FROM dbo.Valutat)  

delete from Valutat where Id not in(select Id from @ValutatType)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.ValutatSelect_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.ValutatSelect_sp
	END
Go --

CREATE PROCEDURE [TOSHIBA].[ValutatSelect_sp]
(
	@Id INT=NULL,
	@ValutaAktive BIT=NULL
)
AS
BEGIN
DECLARE @ValutaPunuese VARCHAR(50)
SELECT TOP 1 @ValutaPunuese=Shifra FROM dbo.Valutat WHERE ValutaAktive=1

	SELECT
		Id,
		Shifra,
		Pershkrimi,
		Kursi,
		ValutaAktive,
		'1'+Shifra+' = ' + CONVERT(VARCHAR(150),CONVERT(DECIMAL(18,2),1/Kursi))+' '+ISNULL(@ValutaPunuese,'') Kembimi
	FROM
		dbo.Valutat
	WHERE 
			(@Id IS NULL OR Id=@Id)
		AND (@ValutaAktive IS NULL OR ValutaAktive=@ValutaAktive)
END

Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.POSKuponatPerZbritjeKodetEKuponaveUpdateInsert_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.POSKuponatPerZbritjeKodetEKuponaveUpdateInsert_sp
	END
Go --

IF EXISTS (SELECT * FROM sys.types 
	WHERE name = 'POSKuponatPerZbritjeKodetEKuponaveType') 
	BEGIN
		DROP  TYPE  TOSHIBA.POSKuponatPerZbritjeKodetEKuponaveType
	END 
Go --

CREATE TYPE [TOSHIBA].[POSKuponatPerZbritjeKodetEKuponaveType] AS TABLE
(
    [Id] INT NOT NULL,
	[KuponatPerZbritjeId] INT NOT NULL,
	[KodiKuponit] VARCHAR(25) NOT NULL,
	[Aktivizuar] bit default 0 not null,
	[Aplikuar] bit default 0 not null
)
Go --

CREATE PROCEDURE [TOSHIBA].[POSKuponatPerZbritjeKodetEKuponaveUpdateInsert_sp]
(  
    @POSKuponatPerZbritjeKodetEKuponaveType TOSHIBA.POSKuponatPerZbritjeKodetEKuponaveType readonly   
)   
as begin 

 Update A set 		   
        A.Aktivizuar=B.Aktivizuar,
        A.Aplikuar=B.Aplikuar
From dbo.POSKuponatPerZbritjeKodetEKuponave A inner join @POSKuponatPerZbritjeKodetEKuponaveType B on A.id=B.id  

insert into dbo.POSKuponatPerZbritjeKodetEKuponave  
      (Id,
	   KuponatPerZbritjeId,
	   KodiKuponit,
	   Aktivizuar,
       Aplikuar
      )  
SELECT Id  
      ,KuponatPerZbritjeId  
      ,KodiKuponit  
      ,Aktivizuar
      ,Aplikuar
FROM @POSKuponatPerZbritjeKodetEKuponaveType WHERE Id NOT IN(SELECT Id FROM dbo.POSKuponatPerZbritjeKodetEKuponave)  

delete from POSKuponatPerZbritjeKodetEKuponave where Id not in(select Id from @POSKuponatPerZbritjeKodetEKuponaveType)

END
Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.KuponatSelect_sp')) 
	BEGIN
		DROP  PROCEDURE  TOSHIBA.KuponatSelect_sp
	END
Go --

CREATE PROCEDURE [TOSHIBA].[KuponatSelect_sp]
(
	@DaljaMallitId bigint
)
AS
BEGIN

select 
	Id,
	KuponatPerZbritjeId,
	KodiKuponit,
	Aktivizuar,
	Aplikuar,
	DaljaMallitIdGjeneruar,
	DaljaMallitIdAplikuar
from  
	POSKuponatPerZbritjeKodetEKuponave
where
	(DaljaMallitIdGjeneruar=@DaljaMallitId OR DaljaMallitIdAplikuar=@DaljaMallitId)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetKuponatUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSAktivitetetKuponatUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSAktivitetetKuponatType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSAktivitetetKuponatType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSAktivitetetKuponatType] AS TABLE
(
    [Id]                  INT           NOT NULL,
	[AktivitetiId]        BIGINT        NOT NULL,
	[KuponatPerZbritjeId] INT           NOT NULL,
	[Sasia]               decimal(18,2) NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.POSAktivitetetKuponatUpdateInsert_sp
(  
	@POSAktivitetetKuponatType TOSHIBA.POSAktivitetetKuponatType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.Sasia=B.Sasia
FROM dbo.POSAktivitetetKuponat A INNER JOIN @POSAktivitetetKuponatType B ON A.Id=B.Id  

INSERT INTO dbo.POSAktivitetetKuponat  
      (Id  
      ,AktivitetiId  
      ,KuponatPerZbritjeId  
      ,Sasia
      )  
SELECT Id  
      ,AktivitetiId  
      ,KuponatPerZbritjeId  
      ,Sasia
FROM @POSAktivitetetKuponatType WHERE Id NOT IN(SELECT Id FROM dbo.POSAktivitetetKuponat) 
  
DELETE FROM POSAktivitetetKuponat WHERE Id NOT IN(SELECT Id FROM @POSAktivitetetKuponatType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSAktivitetetKuponatSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POSAktivitetetKuponatSelect_sp
			  END

Go --

Create Procedure TOSHIBA.POSAktivitetetKuponatSelect_sp 
(
	@LlojiDokumentitId int,
	@OrganizataId int
) 
AS BEGIN
 
	SELECT 
		A.Id,
		A.AktivitetiId,
		A.KuponatPerZbritjeId,
		A.Sasia,
        K.PrejDates,
        K.DeriMeDaten
	FROM dbo.POSAktivitetetKuponat A
		INNER JOIN POSAktivitetet AK ON A.AktivitetiId=AK.Id
        INNER JOIN dbo.POSKuponatPerZbritje K ON A.KuponatPerZbritjeId=K.Id
	WHERE  
			(AK.Statusi=1)
		AND (AK.PrejDates <= CAST(GETDATE() as date))
		AND (AK.DeriMeDaten >= CAST(GETDATE() as date))
		AND (AK.LlojiIDokumentitId=@LlojiDokumentitId)
		AND (AK.Id IN (select AktivitetiId from POSAktivitetetOrganizatat where OrganizataId=@OrganizataId and Statusi=1))

end

Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.KuponatInsert_sp')) 
	BEGIN
		DROP  PROCEDURE  TOSHIBA.KuponatInsert_sp
	END
Go --

CREATE PROCEDURE [TOSHIBA].[KuponatInsert_sp]
(
	@KuponatPerZbritjeId int,
	@KodiKuponit VARCHAR(18),
	@DaljaMallitIdGjeneruar BIGINT=NULL
)
AS
BEGIN

DECLARE @Id int 
set @Id =isnull((select max(ID) from dbo.POSKuponatPerZbritjeKodetEKuponave),0)+1

Insert into dbo.POSKuponatPerZbritjeKodetEKuponave 
(
	Id,
	KuponatPerZbritjeId,
	KodiKuponit,
	Aktivizuar,
	Aplikuar,
	DaljaMallitIdGjeneruar
) 
Values 
( 
	@Id,
	@KuponatPerZbritjeId,
	@KodiKuponit,
	1,
	0,
	@DaljaMallitIdGjeneruar
)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPAlokimiBonuseveUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.CCPAlokimiBonuseveUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPAlokimiBonuseveType') 
			  BEGIN
			  Drop  type  TOSHIBA.CCPAlokimiBonuseveType
			  End 
Go --
CREATE TYPE [TOSHIBA].[CCPAlokimiBonuseveType] AS TABLE
(
	[Id]                INT             NOT NULL,
    [CCPKompaniaId]     INT             NOT NULL,
    [VleraBonusit]      DECIMAL (18,10) NOT NULL,
    [DaljaMallitId]     BIGINT          NULL,
    [DataERegjistrimit] DATETIME        NOT NULL,
    [Koment]            VARCHAR(600)    NULL,
    [DataBonusit]       DATE            NULL,
    [RegjistruarNga]    INT             NULL
)
Go --

CREATE PROCEDURE TOSHIBA.CCPAlokimiBonuseveUpdateInsert_sp
(  
	@CCPAlokimiBonuseveType TOSHIBA.CCPAlokimiBonuseveType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.VleraBonusit=B.VleraBonusit,
		A.DataERegjistrimit=B.DataERegjistrimit,
		A.Koment=B.Koment,
		A.DataBonusit=B.DataBonusit,
		A.RegjistruarNga=B.RegjistruarNga
FROM dbo.CCPAlokimiBonuseve A INNER JOIN @CCPAlokimiBonuseveType B ON A.Id=B.Id  

INSERT INTO dbo.CCPAlokimiBonuseve  
      (Id  
      ,CCPKompaniaId
	  ,VleraBonusit
      ,DaljaMallitId  
	  ,DataERegjistrimit
	  ,Koment
	  ,DataBonusit
	  ,RegjistruarNga
      )  
SELECT Id  
      ,CCPKompaniaId
	  ,VleraBonusit
      ,DaljaMallitId  
	  ,DataERegjistrimit
	  ,Koment
	  ,DataBonusit
	  ,RegjistruarNga
FROM @CCPAlokimiBonuseveType WHERE Id NOT IN(SELECT Id FROM dbo.CCPAlokimiBonuseve) 
  
DELETE FROM CCPAlokimiBonuseve WHERE Id NOT IN(SELECT Id FROM @CCPAlokimiBonuseveType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPShperblimetArtikujtUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.CCPShperblimetArtikujtUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPShperblimetArtikujtType') 
			  BEGIN
			  Drop  type  TOSHIBA.CCPShperblimetArtikujtType
			  End 
Go --
CREATE TYPE [TOSHIBA].[CCPShperblimetArtikujtType] AS TABLE
(
	[Id]          INT           NOT NULL,
    [ArtikulliId] INT           NOT NULL,
    [NrPikeve]    DECIMAL(18,2) NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.CCPShperblimetArtikujtUpdateInsert_sp
(  
	@CCPShperblimetArtikujtType TOSHIBA.CCPShperblimetArtikujtType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.NrPikeve=B.NrPikeve
FROM dbo.CCPShperblimetArtikujt A INNER JOIN @CCPShperblimetArtikujtType B ON A.Id=B.Id  

INSERT INTO dbo.CCPShperblimetArtikujt  
      (Id  
      ,ArtikulliId  
      ,NrPikeve  
      )  
SELECT Id  
      ,ArtikulliId  
      ,NrPikeve
FROM @CCPShperblimetArtikujtType WHERE Id NOT IN(SELECT Id FROM dbo.CCPShperblimetArtikujt) 
  
DELETE FROM CCPShperblimetArtikujt WHERE Id NOT IN(SELECT Id FROM @CCPShperblimetArtikujtType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPShperblimetArtikujtSelect_sp') ) 
			  BEGIN
			  DROP  PROCEDURE  TOSHIBA.CCPShperblimetArtikujtSelect_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[CCPShperblimetArtikujtSelect_sp]
as begin 
	SELECT 
		C.Id,
		C.ArtikulliId Shifra,
		A.Pershkrimi,
		C.NrPikeve 
	FROM dbo.CCPShperblimetArtikujt C INNER JOIN dbo.Artikujt A ON C.ArtikulliId=A.Id
	where a.statusi=1
END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPAlokimiBonuseveSelect_sp') ) 
			  BEGIN
			  DROP  PROCEDURE  TOSHIBA.CCPAlokimiBonuseveSelect_sp
			  END
Go --

CREATE PROCEDURE [TOSHIBA].[CCPAlokimiBonuseveSelect_sp]
(
	@CCPKompaniaId int=null
) 
as begin 
Select 
	Id,
	CCPKompaniaId,
	VleraBonusit,
	DaljaMallitId,
	DataERegjistrimit,
	Koment,
	DataBonusit,
	RegjistruarNga
From dbo.CCPAlokimiBonuseve 
WHERE
	(CCPKompaniaId=@CCPKompaniaId) 
 
end

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPFaturatUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.CCPFaturatUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPFaturatType') 
			  BEGIN
			  Drop  type  TOSHIBA.CCPFaturatType
			  End 
Go --
CREATE TYPE [TOSHIBA].[CCPFaturatType] AS TABLE
(
	[Id]               INT            NOT NULL,
    [CCPKompaniaId]    INT            NOT NULL,
    [DaljaMallitId]    BIGINT         NULL,
    [CCPBonusiId]      INT            NULL,
    [DataRegjistrimit] DATETIME       NOT NULL,
    [RegjistruarNga]   INT            NOT NULL,
    [Shtypur]          BIT            NULL,
    [VleraPikeve]      DECIMAL (18,2) NULL
)
Go --

CREATE PROCEDURE TOSHIBA.CCPFaturatUpdateInsert_sp
(  
	@CCPFaturatType TOSHIBA.CCPFaturatType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.DataRegjistrimit=B.DataRegjistrimit,
		A.RegjistruarNga=B.RegjistruarNga,
		A.Shtypur=B.Shtypur,
		A.VleraPikeve=B.VleraPikeve
FROM dbo.CCPFaturat A INNER JOIN @CCPFaturatType B ON A.Id=B.Id  

SET IDENTITY_INSERT dbo.CCPFaturat ON

INSERT INTO dbo.CCPFaturat  
      (Id  
      ,CCPKompaniaId  
      ,DaljaMallitId
	  ,CCPBonusiId
	  ,DataRegjistrimit
	  ,RegjistruarNga
	  ,Shtypur
	  ,VleraPikeve  
      )  
SELECT Id  
      ,CCPKompaniaId  
      ,DaljaMallitId
	  ,CCPBonusiId
	  ,DataRegjistrimit
	  ,RegjistruarNga
	  ,Shtypur
	  ,VleraPikeve

FROM @CCPFaturatType WHERE Id NOT IN(SELECT Id FROM dbo.CCPFaturat)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_VleraEPikeveKuponiFiskal_sp')) 
	BEGIN
		Drop  Procedure  TOSHIBA.POS_VleraEPikeveKuponiFiskal_sp
	END
Go --

CREATE PROCEDURE TOSHIBA.[POS_VleraEPikeveKuponiFiskal_sp]
	@DaljaMallitId BIGINT
AS
BEGIN
DECLARE @KompaniaId INT = NULL
SELECT @KompaniaId = CCPKompaniaId FROM dbo.CCPFaturat WITH(NOLOCK) WHERE DaljaMallitId = @DaljaMallitId
	SELECT 
		ISNULL(SUM(VleraPikeve),0) TotaliPikeveTeFatures,
		ISNULL((SELECT SUM(VleraBonusit) FROM dbo.CCPAlokimiBonuseve WITH(NOLOCK) WHERE CCPKompaniaId = @KompaniaId),0) TotaliIBonusit
FROM dbo.CCPFaturat WITH(NOLOCK) WHERE DaljaMallitId = @DaljaMallitId
END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPArtikujtEPerjashtuarUpdateInsert_sp') ) 
			  BEGIN
			  DROP  PROCEDURE  TOSHIBA.CCPArtikujtEPerjashtuarUpdateInsert_sp
			  END
Go --

IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'CCPArtikujtEPerjashtuarType') 
			  BEGIN
			  DROP  TYPE  TOSHIBA.CCPArtikujtEPerjashtuarType
			  END 
Go --

CREATE TYPE [TOSHIBA].[CCPArtikujtEPerjashtuarType] AS TABLE
(
	[Id]           INT  NOT NULL,
    [ArtikulliID]  INT  NOT NULL,
    [Prejdates]    DATE NULL,
    [DeriMeDaten]  DATE NULL,
	[Perkohesisht] bit  not null default(0)
)
Go --

CREATE PROCEDURE TOSHIBA.CCPArtikujtEPerjashtuarUpdateInsert_sp
(  
	@CCPArtikujtEPerjashtuarType TOSHIBA.CCPArtikujtEPerjashtuarType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.Prejdates=B.Prejdates,
		A.DeriMeDaten=B.DeriMeDaten,
		A.Perkohesisht=B.Perkohesisht
FROM dbo.CCPArtikujtEPerjashtuar A INNER JOIN @CCPArtikujtEPerjashtuarType B ON A.ArtikulliID=B.ArtikulliID

INSERT INTO dbo.CCPArtikujtEPerjashtuar  
      (Id  
      ,ArtikulliID  
      ,Prejdates
	  ,DeriMeDaten
	  ,Perkohesisht  
      )  
SELECT Id  
      ,ArtikulliID  
      ,Prejdates
	  ,DeriMeDaten
	  ,Perkohesisht

FROM @CCPArtikujtEPerjashtuarType WHERE ArtikulliID NOT IN(SELECT ArtikulliID FROM dbo.CCPArtikujtEPerjashtuar)

DELETE FROM dbo.CCPArtikujtEPerjashtuar WHERE ArtikulliID NOT IN(SELECT ArtikulliID FROM @CCPArtikujtEPerjashtuarType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.CCPGrupetSelect_sp') ) 
			  BEGIN
			  DROP  PROCEDURE  TOSHIBA.CCPGrupetSelect_sp
			  END
Go --

CREATE PROCEDURE TOSHIBA.CCPGrupetSelect_sp
(
	@Pershkrimi varchar(200)
)
AS
BEGIN
	
		SELECT
			Id CCPGrupiId,
			Pershkrimi,
			'' KodiKarteles
		FROM
			CCPGrupet
		WHERE
			(Pershkrimi=@Pershkrimi)
end
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POSKuponatPerZbritjeUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.POSKuponatPerZbritjeUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'POSKuponatPerZbritjeType') 
			  BEGIN
			  Drop  type  TOSHIBA.POSKuponatPerZbritjeType
			  End 
Go --
CREATE TYPE [TOSHIBA].[POSKuponatPerZbritjeType] AS TABLE
(
	[Id] INT NOT NULL,
    [Vlera] DECIMAL(18,2) NOT NULL,
    [Aplikuar] BIT NOT NULL,
    [DataERegjistrimit] DATETIME NULL,
    [RegjistruarNga] INT NOT NULL,
    [KuponatPerZbritjeLlojetEZbritjesId] INT NOT NULL,
    [Dhe] BIT NULL,
    [PrejDates] DATE NULL,
    [DeriMeDaten] DATE NULL,
	[VleraFatures] DECIMAL(18,2) NULL,
	[VLFMeEVogelApliko] BIT DEFAULT(1) NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.POSKuponatPerZbritjeUpdateInsert_sp
(  
	@POSKuponatPerZbritjeType TOSHIBA.POSKuponatPerZbritjeType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.Vlera=B.Vlera,
		A.Aplikuar=B.Aplikuar,
		A.DataERegjistrimit=B.DataERegjistrimit,
		A.RegjistruarNga=B.RegjistruarNga,
		A.KuponatPerZbritjeLlojetEZbritjesId=B.KuponatPerZbritjeLlojetEZbritjesId,
		A.Dhe=B.Dhe,
		A.PrejDates=B.PrejDates,
		A.DeriMeDaten=B.DeriMeDaten,
		A.VleraFatures=B.VleraFatures,
		A.VLFMeEVogelApliko=B.VLFMeEVogelApliko
FROM dbo.POSKuponatPerZbritje A INNER JOIN @POSKuponatPerZbritjeType B ON A.Id=B.Id  

INSERT INTO dbo.POSKuponatPerZbritje  
      (Id  
      ,Vlera  
      ,Aplikuar
	  ,DataERegjistrimit
	  ,RegjistruarNga
	  ,KuponatPerZbritjeLlojetEZbritjesId
	  ,Dhe
	  ,PrejDates
	  ,DeriMeDaten
	  ,VleraFatures
	  ,VLFMeEVogelApliko
      )  
SELECT Id  
      ,Vlera  
      ,Aplikuar
	  ,DataERegjistrimit
	  ,RegjistruarNga
	  ,KuponatPerZbritjeLlojetEZbritjesId
	  ,Dhe
	  ,PrejDates
	  ,DeriMeDaten
	  ,VleraFatures
	  ,VLFMeEVogelApliko
FROM @POSKuponatPerZbritjeType WHERE Id NOT IN(SELECT Id FROM dbo.POSKuponatPerZbritje)

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitVaucheratInsert_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitVaucheratInsert_Sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitVaucheratInsert_Sp
(
            @DaljaMallitID bigint = null
           ,@Vlera decimal(18,2) = 0
           ,@KodiVaucherit varchar(250)= null
           ,@Emri varchar(250)= null
           ,@Mbiemri varchar(250)= null
           ,@Lloji varchar(250)= null
		   ,@KuponatPerZbritjeLlojetEZbritjesId int =null
)
as
Begin

--if exists(select 1 from [dbo].[DaljaMallitVaucherat] where KodiVaucherit=@KodiVaucherit) 
--begin
--raiserror('Ky voucher është shpenzuar!',
--11,
--1,
--'Vrejtje'
--)
--return
--end

INSERT INTO [dbo].[DaljaMallitVaucherat]
           (
			[DaljaMallitID]
           ,[Vlera]
           ,[KodiVaucherit]
           ,[Emri]
           ,[Mbiemri]
           ,[Lloji]
		   ,[KuponatPerZbritjeLlojetEZbritjesId]
		   )
values
(
			@DaljaMallitID
           ,@Vlera
           ,@KodiVaucherit
           ,@Emri
           ,@Mbiemri
           ,@Lloji
		   ,@KuponatPerZbritjeLlojetEZbritjesId
)
End
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitVaucheratSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitVaucheratSelect_Sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[DaljaMallitVaucheratSelect_Sp]
(
@DaljaMallitId bigint=null
)
AS
BEGIN
	SELECT Id ,
           DaljaMallitID ,
           Vlera ,
           KodiVaucherit ,
           Emri ,
           Mbiemri ,
           Lloji ,
		   KuponatPerZbritjeLlojetEZbritjesId
FROM [dbo].[DaljaMallitVaucherat] D	
WHERE D.DaljaMallitID= @DaljaMallitId 
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.ArtikujtPaZbritjeUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.ArtikujtPaZbritjeUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'ArtikujtPaZbritjeType') 
			  BEGIN
			  Drop  type  TOSHIBA.ArtikujtPaZbritjeType
			  End 
Go --
CREATE TYPE [TOSHIBA].[ArtikujtPaZbritjeType] AS TABLE
(
	[Id] INT NOT NULL,
    [ArtikulliId] INT NOT NULL,
    [Statusi] BIT NOT NULL,
    [Tipi] VARCHAR(20) NOT NULL,
    [DataERegjistrimit] DATETIME NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.ArtikujtPaZbritjeUpdateInsert_sp
(  
	@ArtikujtPaZbritjeType TOSHIBA.ArtikujtPaZbritjeType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.DataERegjistrimit=B.DataERegjistrimit
FROM dbo.ArtikujtPaZbritje A INNER JOIN @ArtikujtPaZbritjeType B ON A.Id=B.Id

SET IDENTITY_INSERT ArtikujtPaZbritje ON

INSERT INTO dbo.ArtikujtPaZbritje  
      (Id  
      ,ArtikulliId  
      ,Statusi
	  ,Tipi
	  ,DataERegjistrimit 
      )  
SELECT Id  
      ,ArtikulliId  
      ,Statusi
	  ,Tipi
	  ,DataERegjistrimit
FROM @ArtikujtPaZbritjeType WHERE Id NOT IN(SELECT Id FROM dbo.ArtikujtPaZbritje)

delete from ArtikujtPaZbritje where Id NOT IN(select Id from @ArtikujtPaZbritjeType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitPerSinkronizimSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitPerSinkronizimSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.DaljaMallitPerSinkronizimSelect_sp 
(
@Sinkronizuar bit ,
@Validuar bit
)  
AS BEGIN 
SELECT 
Id, OrganizataId, Viti, Data, NrFatures, DokumentiId, RegjistruarNga
, NumriArkes, DataERegjistrimit, SubjektiId, ShitjeEPerjashtuar, Koment
, Xhirollogari, Sinkronizuar, NeTransfer, DepartamentiId, Validuar
, AfatiPageses, DaljaMallitKorrektuarId, TavolinaId, NrDuditX3, DataFatures
, RaportDoganor, Kursi, ValutaId, KuponiFiskalShtypur, K6, K7, K8, K9, K10
, DataValidimit, DaljaMallitImportuarId, IdLokal, FaturaKomulativeId, TrackingId
, NumriFaturesManual, ZbritjeNgaOperatori, RFID, RFIDCCP, BarazimiId, ServerId
, ReferencaID, KartelaId, MeVete, NumriATK, NumriArkesGK, Personi, Adresa
, NumriPersonal, NrTel
From DaljaMallit A  
where   
    ((@Sinkronizuar=0 AND ServerId IS Null ) or (@Sinkronizuar=1 AND ServerId IS NOT Null ))
and ( A.Validuar =@Validuar) 
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.DaljaMallitIdServerUpdate_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.DaljaMallitIdServerUpdate_sp
			  End
Go --

Create PROCEDURE TOSHIBA.DaljaMallitIdServerUpdate_sp
(
	@ServerId bigint,
	@Id bigint
)
AS
begin
	update DaljaMallit
	set ServerId=@ServerId
	where Id=@Id
end
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_TatimetSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_TatimetSelect_Sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[POS_TatimetSelect_Sp]
(
	@Id tinyint  =null,
	@Pershkrimi varchar (50) =null,
	@Vlera decimal (18,2) =null,
	@Statusi bit  =null,
	@Kategoria Varchar(2) =null
) 
as begin 
Select 
	Id,
	Pershkrimi,
	Vlera,
	Statusi,
	Kategoria
From Tatimet 
where  (Id=@Id or @Id is null ) 
and  (Pershkrimi=@Pershkrimi or @Pershkrimi is null ) 
and  (Vlera=@Vlera or @Vlera is null ) 
and  (Statusi=1) 
and  (Kategoria=@Kategoria or @Kategoria is null ) 
order by Vlera

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KuponatKubitSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.KuponatKubitSelect_sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[KuponatKubitSelect_sp]
	@DaljaMallitId BIGINT
AS
	SELECT K.Id, K.KuponatPerZbritjeId, K.KodiKuponit, K.Aktivizuar, 
		K.Aplikuar, K.DaljaMallitIdGjeneruar, K.DaljaMallitIdAplikuar, 
		KZ.PrejDates, KZ.DeriMeDaten, KZ.VleraFatures, KZ.Aplikuar
	FROM POSKuponatPerZbritjeKodetEKuponave K
	INNER JOIN POSKuponatPerZbritje KZ ON KZ.Id=K.KuponatPerZbritjeId
	WHERE K.DaljaMallitIdGjeneruar=@DaljaMallitId
		AND K.Aplikuar=0
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitPaPrintuaraSelect_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitPaPrintuaraSelect_sp
			  End
Go --
CREATE PROCEDURE TOSHIBA.POS_DaljaMallitPaPrintuaraSelect_sp 
AS
BEGIN
	SELECT D.Id,
		convert(bigint, D.NrFatures) NrFatures,
		D.DataERegjistrimit as Data,
		E.Vlera,
		E.Paguar,
		CONVERT(DECIMAL(18, 2), E.Kursi) Kursi
		From DaljaMallit D with(nolock)
			INNER JOIN EkzekutimiPageses E ON E.DaljaMallitID=D.Id
	WHERE D.KuponiFiskalShtypur = 0
		AND D.BarazimiId is NULL
END
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.FaturatNgaPOSSelect') ) 
			  begin
			  Drop  Procedure  TOSHIBA.FaturatNgaPOSSelect
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[FaturatNgaPOSSelect]
(
	@DataPrej DATE=NULL,
	@DataDeri DATE=NULL,
	@ArtikulliId INT=NULL,
	@OperatoriId INT=NULL,
	@TotalPrej DECIMAL(18,2)=NULL,
	@TotalDeri DECIMAL(18,2)=NULL,
	@NrArkes INT=NULL,
	@OrganizataId int ,
	@TePabarazuara BIT = null
)
AS
BEGIN

select top 100 * from 
(
	SELECT 
		D.Id,
		D.Data,
		D.DataERegjistrimit,
		D.NrFatures NrDokumentit,
		D.NumriArkes NrArkes,
		O.Emri + ' ' + O.Mbiemri Operatori,
		convert(decimal(18,2),SUM(DD.Sasia*((DD.QmimiShitjes*(1-Rabati/100))*(1-DD.EkstraRabati/100)))) Total,
		S.Id SubjektiId,
		S.Pershkrimi Subjekti,
		D.ServerId,
		D.DokumentiId LlojiDokumentitId
	FROM
		dbo.DaljaMallit D
		INNER JOIN dbo.DaljaMallitDetale DD ON D.Id=DD.DaljaMallitID
		INNER JOIN Mxh_Operatoret O ON D.RegjistruarNga=O.Id
		LEFT OUTER JOIN Subjektet S ON S.Id=D.SubjektiId
	WHERE 
		D.OrganizataId=@OrganizataId 
		AND (@TePabarazuara IS NULL OR (@TePabarazuara = 1 AND D.BarazimiId IS NULL ) OR (@TePabarazuara = 0 AND D.BarazimiId IS NOT NULL ))
		AND	(@DataPrej IS NULL OR D.Data>=@DataPrej)
		AND (@DataDeri IS NULL OR D.Data<=@DataDeri)
		AND (@ArtikulliId IS NULL OR DD.ArtikulliId=@ArtikulliId)
		AND (@OperatoriId IS NULL OR D.RegjistruarNga=@OperatoriId)
		AND (@NrArkes IS NULL OR D.NumriArkes=@NrArkes)
	GROUP BY
		D.Id,
		D.Data,
		D.DataERegjistrimit,
		D.NrFatures,
		D.NumriArkes,
		O.Emri,
		O.Mbiemri,
		S.Pershkrimi,
		D.ServerId,
		S.Id,
		D.DokumentiId

) G
where 
				    (@TotalPrej IS NULL OR Total>=@TotalPrej)
				AND (@TotalDeri IS NULL OR Total<=@TotalDeri)
order by DataERegjistrimit DESC
END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.LlojetEDokumenteveSelect_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.LlojetEDokumenteveSelect_Sp
			  End
Go --
Create PROCEDURE TOSHIBA.LlojetEDokumenteveSelect_Sp 
(
@Id INT =NULL,
@Shkurtesa VARCHAR(50)=NULL ,
@Pershkrimi VARCHAR(150)=NULL ,
@PrindiID INT =null
)
AS
BEGIN

SELECT Id,
       Pershkrimi,
       Tipi,
       PrindiID,
       Shkurtesa,
       Shenja,
       DokumentIJashtem,
       Tatimi,
       DataERegjistrimit,
       TrackingTipi,
       KaKurs 
FROM dbo.LlojetEDokumenteve D
WHERE 
(@Id IS NULL OR Id=@Id)
AND (@Shkurtesa IS NULL OR @Shkurtesa = D.Shkurtesa)
AND (@Pershkrimi IS NULL OR @Pershkrimi = D.Pershkrimi) 
AND (@PrindiID IS NULL OR @PrindiID = D.PrindiID) 
End
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KthimiMallitArsyetUpdateInsert_sp') ) 
			  BEGIN
			  Drop  Procedure  TOSHIBA.KthimiMallitArsyetUpdateInsert_sp
			  END
Go -- 
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'KthimiMallitArsyetType') 
			  BEGIN
			  Drop  type  TOSHIBA.KthimiMallitArsyetType
			  End 
Go --
CREATE TYPE [TOSHIBA].[KthimiMallitArsyetType] AS TABLE
(
	[Id] INT NOT NULL,
    [Pershkrimi] VARCHAR(500) NOT NULL
)
Go --

CREATE PROCEDURE TOSHIBA.KthimiMallitArsyetUpdateInsert_sp
(  
	@KthimiMallitArsyetType TOSHIBA.KthimiMallitArsyetType READONLY   
) 
AS BEGIN   

 UPDATE A SET 
        A.Pershkrimi=B.Pershkrimi
FROM dbo.KthimiMallitArsyet A INNER JOIN @KthimiMallitArsyetType B ON A.Id=B.Id AND A.Pershkrimi<>B.Pershkrimi

INSERT INTO dbo.KthimiMallitArsyet  
      (Id  
      ,Pershkrimi 
      )  
SELECT Id  
      ,Pershkrimi
FROM @KthimiMallitArsyetType WHERE Id NOT IN(SELECT Id FROM dbo.KthimiMallitArsyet)

delete from KthimiMallitArsyet where Id not in(select Id from @KthimiMallitArsyetType)

END

Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.KthimiMallitArsyetSelect_sp') ) 
			  BEGIN
			  DROP  PROCEDURE  TOSHIBA.KthimiMallitArsyetSelect_sp
			  END
Go --
CREATE PROCEDURE TOSHIBA.KthimiMallitArsyetSelect_sp
(
@Id int =NULL
)
AS
BEGIN 
SELECT 
A.Id, A.Pershkrimi 
FROM 
dbo.KthimiMallitArsyet A
WHERE
(@Id IS NULL OR A.Id=@Id)
END
Go --

IF EXISTS (SELECT * FROM sys.objects 

			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitKushteEBleresitUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitKushteEBleresitUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'DaljaMallitKushteEBleresitTP') 
			  begin
			  Drop  type  TOSHIBA.DaljaMallitKushteEBleresitTP
			  End 
Go --
CREATE TYPE TOSHIBA.DaljaMallitKushteEBleresitTP as Table (
    [Id]                    INT             NULL,
    [SubjektiId]            INT             NULL,
    [Rabati]                DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [EkstraRabati]          DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [LimitiObligimit]       DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [DataPrej]              DATE            NULL,
    [DataDeri]              DATE            NULL,
    [NrDokumentit]          INT             NULL,
    [DataRegjistrimit]      DATETIME        DEFAULT (getdate()) NOT NULL,
    [RegjistruarNga]        INT             NULL,
    [AfatiPageses]          INT             DEFAULT ((0)) NOT NULL,
    [KategoriaID]           INT             NULL,
    [Marzha]                DECIMAL (18, 6) DEFAULT ((0.00)) NULL,  
    [AplikoCmimetEShumices] BIT             DEFAULT ((0)) NOT NULL,
    [VleresimiSubjektitId]  INT             NULL,
    [Klasifikatori]         VARCHAR (50)    NULL,
    [KlasifikatoriDate]     DATETIME        NULL
	)
Go --

CREATE Procedure TOSHIBA.POS_DaljaMallitKushteEBleresitUpdateInsert_sp  
(
	@DaljaMallitKushteEBleresitTP TOSHIBA.DaljaMallitKushteEBleresitTP readonly	
)
as begin 
 Update A set									  
        A.[SubjektiId]			      =B.[SubjektiId]
       ,A.[Rabati]					  =B.[Rabati]
       ,A.[EkstraRabati]              =B.[EkstraRabati]
       ,A.[LimitiObligimit]		      =B.[LimitiObligimit]
       ,A.[DataPrej]		          =B.[DataPrej]
       ,A.[DataDeri]				  =B.[DataDeri]
       ,A.[NrDokumentit]			  =B.[NrDokumentit]
       ,A.[DataRegjistrimit]		  =B.[DataRegjistrimit]
       ,A.[RegjistruarNga]			  =B.[RegjistruarNga] 
	   ,A.[AfatiPageses]              =B.[AfatiPageses]
	   ,A.[KategoriaID]				  =B.[KategoriaID]
	   ,A.[Marzha]                    =B.[Marzha]
	   ,A.[AplikoCmimetEShumices]	  =B.[AplikoCmimetEShumices]
       ,A.[VleresimiSubjektitId]	  =B.[VleresimiSubjektitId]
       ,A.[Klasifikatori]             =B.[Klasifikatori]
       ,A.[KlasifikatoriDate]		  =B.[KlasifikatoriDate]
FROM dbo.DaljaMallitKushteEBleresit A INNER JOIN @DaljaMallitKushteEBleresitTP B ON A.id = B.id

INSERT INTO dbo.DaljaMallitKushteEBleresit
     ( 
	   Id,
	   SubjektiId,
       Rabati,
       EkstraRabati,
       LimitiObligimit,
       DataPrej,
       DataDeri,
       NrDokumentit,
       DataRegjistrimit,
       RegjistruarNga,
       AfatiPageses,
       KategoriaID,
       Marzha,    
       AplikoCmimetEShumices,
       VleresimiSubjektitId,
       Klasifikatori,
       KlasifikatoriDate 
	  )
SELECT  Id,
	   SubjektiId,
       Rabati,
       EkstraRabati,
       LimitiObligimit,
       DataPrej,
       DataDeri,
       NrDokumentit,
       DataRegjistrimit,
       RegjistruarNga,
       AfatiPageses,
       KategoriaID,
       Marzha,    
       AplikoCmimetEShumices,
       VleresimiSubjektitId,
       Klasifikatori,
       KlasifikatoriDate 
  FROM @DaljaMallitKushteEBleresitTP WHERE Id NOT IN (SELECT Id FROM dbo.DaljaMallitKushteEBleresit)
end
Go -- 

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_DaljaMallitKushteEBleresitArtikujtUpdateInsert_sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_DaljaMallitKushteEBleresitArtikujtUpdateInsert_sp
			  End
Go --
IF EXISTS (SELECT * FROM sys.types 
			  WHERE name = 'DaljaMallitKushteEBleresitArtikujtTP') 
			  begin
			  Drop  type  TOSHIBA.DaljaMallitKushteEBleresitArtikujtTP
			  End 
Go --
CREATE TYPE TOSHIBA.DaljaMallitKushteEBleresitArtikujtTP as Table (
    [Id]            INT             NULL,
    [SubjektiId]    INT             NULL,
    [ArtikulliId]   INT             NULL,
    [Rabati]        DECIMAL (18, 2) NULL,
    [EkstraRabati]  DECIMAL (18, 2) NULL,
    [Qmimi]         DECIMAL (18, 3) NULL,
    [Marzha]        DECIMAL (18, 6) DEFAULT ((0.00)) NULL,
    [ZbritjaNeQmim] DECIMAL (18, 2) DEFAULT ((0.00)) NULL,
    [InsertedDate]  DATETIME        DEFAULT (getdate()) NULL,
	[DataERegjistrimit]  DATETIME   DEFAULT (getdate()) NOT NULL,
	[Komanda] VARCHAR(20) NULL
	)
Go --

CREATE Procedure TOSHIBA.POS_DaljaMallitKushteEBleresitArtikujtUpdateInsert_sp  
(
	@DaljaMallitKushteEBleresitArtikujtTP TOSHIBA.DaljaMallitKushteEBleresitArtikujtTP readonly	
)
as begin 
 Update A set									  
        A.[SubjektiId]			      =B.[SubjektiId]
       ,A.[ArtikulliId]				  =B.[ArtikulliId]
       ,A.[Rabati]		              =B.[Rabati]
       ,A.[EkstraRabati]		      =B.[EkstraRabati]
       ,A.[Qmimi]				      =B.[Qmimi]
       ,A.[Marzha]			          =B.[Marzha]
       ,A.[ZbritjaNeQmim]		      =B.[ZbritjaNeQmim]
	   ,A.[InsertedDate]              =B.[InsertedDate]
	   ,A.[DataERegjistrimit]		  =B.[DataERegjistrimit]
FROM dbo.DaljaMallitKushteEBleresitArtikujt A INNER JOIN @DaljaMallitKushteEBleresitArtikujtTP B ON A.id = B.id
WHERE B.Komanda = 'InsertUpdate'

INSERT INTO dbo.DaljaMallitKushteEBleresitArtikujt
(
    Id,
    SubjektiId,
    ArtikulliId,
    Rabati,
    EkstraRabati,
    Qmimi,
    Marzha,
    ZbritjaNeQmim,
    InsertedDate,
    DataERegjistrimit
)
SELECT Id,
       SubjektiId,
       ArtikulliId,
       Rabati,
       EkstraRabati,
       Qmimi,
       Marzha,
       ZbritjaNeQmim,
       InsertedDate,
       DataERegjistrimit
FROM @DaljaMallitKushteEBleresitArtikujtTP
WHERE Id NOT IN
      (
          SELECT Id FROM dbo.DaljaMallitKushteEBleresitArtikujt
      ) AND Komanda = 'InsertUpdate'

Delete A From 
dbo.DaljaMallitKushteEBleresitArtikujt A left outer join @DaljaMallitKushteEBleresitArtikujtTP B on A.Id=B.Id
WHERE B.Komanda = 'Delete'

end
Go -- 
