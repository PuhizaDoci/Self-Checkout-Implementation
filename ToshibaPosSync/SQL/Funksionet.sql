if not exists(
select 1 from sys.schemas where name ='TOSHIBA'
)
Begin 
 exec ('CREATE SCHEMA [TOSHIBA] AUTHORIZATION [dbo]')
End
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_GjejeZbritjenNeArtikull') ) 
			  begin
			  Drop  Function  TOSHIBA.POS_GjejeZbritjenNeArtikull
			  End 
			  Go -- 
CREATE FUNCTION TOSHIBA.[POS_GjejeZbritjenNeArtikull]
(@ArtikulliId int,@Sasia Decimal(18,3))
RETURNS Decimal(18,2)
AS
BEGIN

	DECLARE @ResultVar Decimal(18,3)

Select top 1 @ResultVar=Zbritja from ArtikujtMeLirim with(nolock)
where ArtikulliId=@ArtikulliId and Sasia<=@Sasia order by Sasia desc
	RETURN Isnull(@ResultVar,0)
END 
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjejeArtikulliId') ) 
			  begin
			  Drop  Function  TOSHIBA.GjejeArtikulliId
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
						   Select @ResultVar=TOSHIBA.GjejeArtikulliId(LEFT(@ShifraOseBarkodi,7)),@Pesha=convert(Decimal(18,3),SUBSTRING(@ShifraOseBarkodi,8,5))
						   set @Sasia=@Pesha/ 100.000
						   end
					   else
						   begin
									Select @ResultVar=ArtikulliId from dbo.PLUPeshoret  with(nolock) where PLU=SUBSTRING(@ShifraOseBarkodi,5,3) and PeshoretId=(Select Id from dbo.Peshoret where Shifra=SUBSTRING(@ShifraOseBarkodi,3,2))
									if (@ResultVar is not null)
									begin											 
									Select @Pershorja=SUBSTRING(@ShifraOseBarkodi,3,2),@Plu=SUBSTRING(@ShifraOseBarkodi,5,3),@Pesha=convert(Decimal(18,3),SUBSTRING(@ShifraOseBarkodi,8,5))
									set @Sasia=@Pesha/ 100.000
									end
						   End
					   end
			   else
					   Begin
			   							Select @ResultVar=Id from 
								(
								Select Id,Convert(varchar(13),Shifra) Barkodi from dbo.Artikujt  with(nolock)
								Union All
								Select ArtikulliId Id,Barkodi from Barkodat
								UNION ALL 
														
								select Id,Convert(varchar(13),@ShifraOseBarkodi) Barkodi from dbo.Artikujt  with(nolock) Where id=(case when substring(@ShifraOseBarkodi,1,6)='200000' then substring(@ShifraOseBarkodi,7,6) else -1 end)
							
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
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.FinKonfigShitjaMallit') ) 
			  begin
			  Drop  Function  TOSHIBA.FinKonfigShitjaMallit
			  End 
			  Go --
			  CREATE FUNCTION [TOSHIBA].[FinKonfigShitjaMallit]
(
	@VleraKerkuar varchar(50),@DaljaMallitID BigInt=null,@MenyraPageses int=null
)
RETURNS decimal(18,2)
AS
BEGIN

Declare @Shuma decimal(18,2),@KMSH decimal(18,2),@VleraPaTVSH decimal(18,2),@TVSH decimal(18,2),@VleraMP decimal(18,2),@TeHyra decimal(18,2),@StokuDepoDoganore  decimal(18,2)

Select @KMSH=SUM(t1.KostoMesatare),
@VleraPaTVSH=SUM(t1.VleraPaTVSH),
@TVSH=(SUM(t1.TotalVlera)-SUM(t1.VleraPaTVSH))

From (
		Select round((DD.Sasia * DD.KostoMesatare),2) AS KostoMesatare,
		       round((DD.Sasia * ((DD.QmimiShitjes*(1-Rabati/ 100.00)*(1-EkstraRabati/ 100.00))/(1+TVSH/ 100.00))),2) AS VleraPaTVSH,
		       round((DD.Sasia * (DD.QmimiShitjes*(1-Rabati/ 100.00)*(1-EkstraRabati/ 100.00))),2) AS TotalVlera
		FROM [dbo].[DaljaMallit] D inner join DaljaMallitDetale DD on  D.id=DD.DaljaMallitID
		where  D.Id=@DaljaMallitID 
		
) t1


Select @StokuDepoDoganore=SUM(BazaDoganore)
From (
		Select round((DD.Sasia * HD.BazaFurnizuese),2) AS BazaFurnizuese,
		       round((DD.Sasia * Transporti),2) AS Transporti,
			   round((DD.Sasia *( hd.BazaFurnizuese+hd.Transporti+hd.Korrekcioni+hd.KorrekcioniTransport+hd.ShpenzimeTjera)),2) AS BazaDoganore
		FROM [dbo].[DaljaMallit] D inner join DaljaMallitDetale DD on  D.id=DD.DaljaMallitID
		inner join HyrjaMallitDetale HD on DD.HyrjaDetaleId=HD.id
		where  (D.Id=@DaljaMallitID ) and DD.HyrjaDetaleId is not null
		
) t1
			
Select @VleraMP=isnull(SUM(vlera),0)
from DaljaMallit D inner join EkzekutimiPageses E on D.Id=E.DaljaMallitID
where  (D.Id=@DaljaMallitID) and E.MenyraEPagesesId=@MenyraPageses 


Select @TeHyra=SUM(vlera)-@TVSH
from DaljaMallit D inner join EkzekutimiPageses E on D.Id=E.DaljaMallitID
where  (D.Id=@DaljaMallitID)


		if (@VleraKerkuar='TeHyra')
		Begin
				Set @Shuma=@TeHyra		
		End
		Else IF (@VleraKerkuar='TVSH')
		Begin
				Set @Shuma=@TVSH
		end
		Else IF (@VleraKerkuar='KMSH')
		Begin
				Set @Shuma=@KMSH
	    end
		Else IF (@VleraKerkuar='VleraMP')
		Begin
				Set @Shuma=@VleraMP
		End
		Else IF (@VleraKerkuar='StokuDepoDoganore')
		Begin
				Set @Shuma=@StokuDepoDoganore
		End
			
Return @Shuma
END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjeneroBarkodin') ) 
			  begin
			  Drop  Function  TOSHIBA.GjeneroBarkodin
			  End 
			  Go --
			  CREATE FUNCTION [TOSHIBA].[GjeneroBarkodin]
(
@ArtikulliId int
)
RETURNS varchar(20)
AS
BEGIN
	
	DECLARE @Result varchar(20),@Barkodi varchar(20)
	Select @Result = null
if (@ArtikulliId is not null)
	
	begin
	Select  @Barkodi='200000'+Convert(Varchar(10),Shifra)+[dbo].[Kontorollo_Digit_Ean]('200000'+Convert(Varchar(10),Shifra)) from Artikujt
		where Artikujt.Id=@ArtikulliId
		end
		
Declare @Egziston int
select @Egziston=ArtikulliId from Barkodat where Barkodi=@Barkodi
	if (@Egziston is not null)
	begin
	set @Result=null
	end
	else
	begin
	set @Result=@Barkodi
	end
	RETURN @Result

END
Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.Kontorollo_Digit_Ean') ) 
			  begin
			  Drop  Function  TOSHIBA.Kontorollo_Digit_Ean
			  End 
			  Go --
			  CREATE  FUNCTION [TOSHIBA].[Kontorollo_Digit_Ean] --@barcode=5998710967612
(    
    @barcode    varchar(20)
)
RETURNS CHAR(1)
AS
BEGIN
    DECLARE
        @chk_digit    int,
        @chk        int
 
    DECLARE    @num TABLE
    (
        num    int
    )
 
    IF    LEN(@barcode) NOT IN (7, 12)
    BEGIN
        RETURN     NULL
    END
 
    INSERT INTO @num 
    SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL  SELECT  5 UNION ALL SELECT  6 UNION ALL
    SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12
 
    SELECT    @chk_digit = SUM(CONVERT(int, SUBSTRING(@barcode, LEN(@barcode) - num + 1, 1)) * CASE WHEN num % 2 = 1 THEN 3 ELSE 1 END)
    FROM    @num
    WHERE    num    <= LEN(@barcode)
 
    SELECT    @chk_digit = (10 - (@chk_digit % 10)) % 10

     RETURN  CHAR(ASCII('0') + @chk_digit)
     
             
END
Go -- 
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjeneroBarkodinCCP_fc') ) 
			  begin
			  Drop  Function  TOSHIBA.GjeneroBarkodinCCP_fc
			  End 
			  Go --
			  
CREATE FUNCTION [TOSHIBA].[GjeneroBarkodinCCP_fc]
(
)
RETURNS varchar(15)
AS
BEGIN
	DECLARE @ResultVar varchar(50)
	
	Select @ResultVar=isnull(max(convert(decimal(18,0),substring(KodiKarteles,1,12)))+1,'400000003692') From dbo.CCPKompanite where len(KodiKarteles)=13
	
	Set @ResultVar=@ResultVar + dbo.Kontorollo_Digit_Ean(@ResultVar)

	RETURN @ResultVar

END

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.EmriTransaksionit') ) 
			  begin
			  Drop  Function  TOSHIBA.EmriTransaksionit
			  End 
			  Go --
			  CREATE FUNCTION [TOSHIBA].[EmriTransaksionit] ( )
RETURNS VARCHAR(20)

AS 
    BEGIN
   
        DECLARE @EmriTransaksionit NCHAR(60)='Jo i palikueshem'
        SELECT TOP 1
                @EmriTransaksionit = tas.name
        FROM    sys.dm_tran_active_transactions tas
                INNER JOIN sys.dm_tran_database_transactions tds ON ( tas.transaction_id = tds.transaction_id )
                INNER JOIN sys.dm_tran_session_transactions trans ON ( trans.transaction_id = tas.transaction_id )
        WHERE   trans.session_id = @@SPID 
        RETURN (@EmriTransaksionit)
    END
Go --


IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.GjeneroDokumentiId') ) 
			  begin
			  Drop  Function  TOSHIBA.GjeneroDokumentiId
			  End 
			  Go --
CREATE FUNCTION TOSHIBA.GjeneroDokumentiId
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

IF NOT EXISTS (
SELECT 1 FROM sys.objects WHERE name ='KonvertoStringNeTabel'
) 
BEGIN 
EXECUTE ('CREATE FUNCTION dbo.KonvertoStringNeTabel(@input AS Varchar(4000),@Delimiter CHAR(1) ) RETURNS
      @Result TABLE(Id BIGINT)
AS
BEGIN
      DECLARE @str VARCHAR(20)
      DECLARE @ind Int
      IF(@input is not null)
      BEGIN
            SET @ind = CharIndex(@Delimiter,@input)
            WHILE @ind > 0
            BEGIN
                  SET @str = SUBSTRING(@input,1,@ind-1)
                  SET @input = SUBSTRING(@input,@ind+1,LEN(@input)-@ind)
                  INSERT INTO @Result values (@str)
                  SET @ind = CharIndex(@Delimiter,@input)
            END
            SET @str = @input
            INSERT INTO @Result values (@str)
      END
	  
      RETURN
END'
)
End
Go --