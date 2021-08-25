 if not exists(
select 1 from sys.schemas where name ='TOSHIBA'
)
Begin 
 exec ('CREATE SCHEMA [TOSHIBA] AUTHORIZATION [dbo]')
End
Go --

IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_GjenerimiKomandaveFiskale_Sp') ) 
			  begin
			  Drop  Procedure  TOSHIBA.POS_GjenerimiKomandaveFiskale_Sp
			  End
Go --
			  CREATE PROCEDURE [TOSHIBA].[POS_GjenerimiKomandaveFiskale_Sp] --'Operatori',10038
@Kerkesa VARCHAR(100),
@OperatoriId int = null
AS
DECLARE @Komanda AS VARCHAR(MAX)=''

-------------HapSirtarin------------------------------
IF(@Kerkesa='HapSirtarin')
BEGIN 
SET @Komanda='V,1,______,_,__j15'
SELECT @Komanda
RETURN
END

-------------Operatori------------------------------
IF(@Kerkesa='Operatori')
BEGIN 
select @Komanda='V,1,______,_,__f1,'+LEFT(CONVERT(VARCHAR(10),Id),4)+','+ Emri +'  ' + Mbiemri From dbo.Mxh_Operatoret  with(nolock) where Id=@OperatoriId+''
SELECT @Komanda
RETURN
END


-------------ShtypKopje------------------------------
IF(@Kerkesa='ShtypKopje')
BEGIN 
select @Komanda='V,1,______,_,__m'
SELECT @Komanda
RETURN
END
--Go --
--IF EXISTS (SELECT * FROM sys.objects 
--			  WHERE object_id = OBJECT_ID(N'POS.POS_GjenerimiKuponitFiskal_Sp') ) 
--			  begin
--			  Drop  Procedure  POS.POS_GjenerimiKuponitFiskal_Sp
--			  End
--Go --
--CREATE PROCEDURE POS.[POS_GjenerimiKuponitFiskal_Sp] -- @DaljaMallitId=18000040200000002
--(
--	 @DaljaMallitId BIGINT=null
--)
--WITH recompile
--AS
--Begin

--declare @GjenerimiKuponit varchar(40) = null,@ShifraArtikullitGjithmonEre bit,@Nrfatures varchar(50),@PikeTefituara int,@KomentiPerPike varchar(50)

--select @GjenerimiKuponit = Vlera
--from dbo.Konfigurimet  with(nolock)
--	where id = 154 



--SELECT @KomentiPerPike='Q,1,______,_,__;2;Pike:' + convert(varchar(50),convert(int,isnull(sum(dd.Sasia),0))) ,@PikeTefituara=isnull(sum(dd.Sasia),0) 
--from DaljaMallit d inner join DaljaMallitDetale dd on d.id=dd.DaljaMallitID
--where dd.ArtikulliId in (182615,
--182616,
--187268) and d.id=@DaljaMallitId

--DECLARE @Komanda AS VARCHAR(max)=''
 
--if(@GjenerimiKuponit = 'Gekos' or @GjenerimiKuponit is null)
--begin 


--			-------------KuponiFiskal------------------------------
--			Select @Nrfatures=D.NrFatures,@Komanda='Q,1,______,_,__;1;' + 
--			CASE WHEN D.DokumentiId = 20 THEN   
--			' Arka:'+CONVERT(VARCHAR(10),D.NumriArkes)+' Nr: ' 
--			                                 +Case when CHARINDEX('SQLEXPRESS',@@SERVERNAME) > 0 then 
--											        isnull(CONVERT(VARCHAR(50),D.IdLokal),'') 
--											  when CHARINDEX('KUBITPOS',@@SERVERNAME) > 0 then 
--											        isnull(CONVERT(VARCHAR(50),D.IdLokal),'') 
--											  else CONVERT(VARCHAR(50),D.NrFatures) End
--											  +CHAR(13)+CHAR(10)
		 
--			+ case when @PikeTefituara>0 then convert(varchar(50),isnull(@KomentiPerPike,'')) else 'Q,1,______,_,__;2;Reklamimi 24 h!' end +CHAR(13)+CHAR(10)
--			else 
--			'Nr. Fatures: ' +CONVERT(VARCHAR(50),D.DokumentiId) + ' - ' 
--			                                 +Case when CHARINDEX('SQLEXPRESS',@@SERVERNAME) > 0 then 
--											        isnull(CONVERT(VARCHAR(50),D.IdLokal),'') 
--											  else CONVERT(VARCHAR(50),D.NrFatures) End
--											  +CHAR(13)+CHAR(10)
--			+'Q,1,______,_,__;2;'+CHAR(13)+CHAR(10) end 

--			FROM dbo.DaljaMallit D with(nolock) INNER JOIN dbo.EkzekutimiPageses E with(nolock) ON E.DaljaMallitID = D.Id 
--			WHERE D.Id=@DaljaMallitId


--			if (Select id from Konfigurimet with(nolock) where id=159 and Statusi=1) is not null
--			begin
--				set @ShifraArtikullitGjithmonEre=1
--			end
--			else
--			begin
--				set @ShifraArtikullitGjithmonEre=0
--			end

--			select @Komanda+=kom from (
--			SELECT 'S,1,______,_,__;'+REPLACE(PershkrimiFiskal,';','')+';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),CmimiShitjesDyDec))+';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,3),Sasia))+';1;1;'+Kategoria+';0;'+ (case when @ShifraArtikullitGjithmonEre=1 then @Nrfatures + convert(varchar(50),Shifra) + convert(varchar(50),abs(isnull(nr,0)))  else Shifra end) + ';'+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),Rabati+ZbritjeMePerqindje))+';'
--			+ Case when CONVERT(DECIMAL(18,2),0)=0 then '' else CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),0))+';' End + CHAR(13)+CHAR(10) kom
--			FROM 
--			(
--				   SELECT G.Id ,
--				   G.PershkrimiFiskal ,
--				   G.Sasia ,
--				   G.Kategoria ,
--				   G.Shifra ,
--				   nr,
--				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then 0 else Rabati end Rabati,
--				   CmimiShitjesDyDec,
--				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then G.Sasia*(CmimiShitjesDyDec-CmimiShitjesshumeDec) +RabatiVlere else 0 end ZbritjeMeVler,
--				   case when (CmimiShitjesDyDec-CmimiShitjesshumeDec)<>0 then convert(decimal(18,2),((G.Sasia*(CmimiShitjesDyDec-CmimiShitjesshumeDec) +RabatiVlere)/(G.Sasia*CmimiShitjesDyDec)*100)) else 0 end ZbritjeMePerqindje
--					FROM 
--					(
--							SELECT 
--							DD.Id
--							,A.PershkrimiFiskal
--							,QmimiShitjes
--							,DD.Sasia 
--							,dd.NR
--							,T.Kategoria 
--							,A.Shifra 
--							,CASE WHEN round(DD.QmimiShitjes, 2)-DD.QmimiShitjes<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END CmimiShitjesDyDec
--							,DD.QmimiShitjes CmimiShitjesshumeDec
--							,CONVERT(DECIMAL(18,2) ,convert(decimal(18,6),dd.QmimiShitjes-(dd.QmimiShitjes*(1-DD.Rabati/100.00)*(1-dd.EkstraRabati/100.00)))/dd.QmimiShitjes*100.00) Rabati
--							--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
--							,DD.Sasia *(((DD.QmimiShitjes*(DD.Rabati/ 100.00))+(DD.QmimiShitjes*(DD.EkstraRabati/100.00)))) RabatiVlere
--							FROM dbo.DaljaMallitDetale DD with(nolock) 
--							INNER JOIN dbo.Artikujt A with(nolock) ON A.Id = DD.ArtikulliId 
--							INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
--							WHERE DD.DaljaMallitID = @DaljaMallitId  and Sasia>0 and dd.QmimiShitjes<>0 and a.[LlojiIArtikullitId]<>20
--							union all
--							SELECT 
--							DD.Id
--							,A.PershkrimiFiskal
--							,QmimiShitjes
--							,DD.Sasia 
--							,dd.NR
--							,T.Kategoria 
--							,A.Shifra 
--							,CASE WHEN round(DD.QmimiShitjes, 2)-DD.QmimiShitjes<>0 THEN  round(DD.QmimiShitjes+0.01, 2, 1) ELSE DD.QmimiShitjes END CmimiShitjesDyDec
--							,DD.QmimiShitjes CmimiShitjesshumeDec
--							,CONVERT(DECIMAL(18,2) ,convert(decimal(18,6),dd.QmimiShitjes-(dd.QmimiShitjes*(1-DD.Rabati/100)*(1-dd.EkstraRabati/100)))/dd.QmimiShitjes*100) Rabati
--							--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
--							,DD.Sasia *(((DD.QmimiShitjes*(DD.Rabati/ 100.00))+(DD.QmimiShitjes*(DD.EkstraRabati/100.00)))) RabatiVlere
--							FROM dbo.DaljaMallitDetale DD with(nolock) 
--							INNER JOIN dbo.Artikujt A with(nolock) ON A.Id = DD.ArtikulliId 
--							INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
--							WHERE DD.DaljaMallitID = @DaljaMallitId  and Sasia<0 and dd.QmimiShitjes<>0 and a.[LlojiIArtikullitId]<>20
							
--					) G 

--			) YHY 

--			) OP


--			IF((SELECT COUNT(iD) FROM dbo.EkzekutimiPageses with(nolock) WHERE DaljaMallitID =@DaljaMallitId )>1)
--			Begin
--			Select  @Komanda +='T,1,______,_,__;'+Ep.Kesh+';'+case when Ep.Kesh ='0' then '' else CONVERT(VARCHAR(10),Ep.Vlera) end+CHAR(13)+CHAR(10)
--			FROM 
--					(SELECT Sum(Vlera) Vlera , Kesh,NumriPagesave
--					FROM (
--							SELECT  SUM(E.Paguar) Vlera ,
--									CASE WHEN E.MenyraEPagesesId = 22 THEN '0'
--										 ELSE '3'
--									END Kesh
--									,COUNT(E.MenyraEPagesesId) NumriPagesave
--							FROM    dbo.EkzekutimiPageses E with(nolock)
--							WHERE   E.DaljaMallitID = @DaljaMallitId
--							GROUP BY E.MenyraEPagesesId
--					) T 
--					GROUP BY Kesh,NumriPagesave) Ep 
--					ORDER BY Kesh desc
--			END
--			ELSE
--			Begin
--			Select  @Komanda +='T,1,______,_,__;'+Ep.Kesh+';'
--			FROM 
--					(SELECT Sum(Vlera) Vlera , Kesh,NumriPagesave
--					FROM (
--							SELECT  SUM(E.Paguar) Vlera ,
--									CASE WHEN E.MenyraEPagesesId = 22 THEN '0'
--										 ELSE '3'
--									END Kesh
--									,COUNT(E.MenyraEPagesesId) NumriPagesave
--							FROM    dbo.EkzekutimiPageses E  with(nolock)
--							WHERE   E.DaljaMallitID = @DaljaMallitId
--							GROUP BY E.MenyraEPagesesId
--					) T 
--					GROUP BY Kesh,NumriPagesave) Ep 
--			End

--			Select @Komanda=REPLACE(@Komanda,'ë','e')
--			Select @Komanda=REPLACE(@Komanda,'Ë','e')
--			Select @Komanda=REPLACE(@Komanda,'ç','c')
--			Select @Komanda=REPLACE(@Komanda,'Ç','c')
--			--Select @Komanda COLLATE SQL_Latin1_General_CP1_CI_AS
--			RETURN;
--			END

--			END   


----Shqiperi 
--if(@GjenerimiKuponit = 'Shqiperi')
--begin
-----------------KuponiFiskal------------------------------
--		Select @Komanda='CLEAR'+CHAR(13)+CHAR(10)
--		+'CHIAVE REG'+CHAR(13)+CHAR(10)


--		SELECT @Komanda+='VEND REP=' + Kategoria + ',qty=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,3),ABS(Sasia))) + ',PREZZO=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),CmimiShitjesDyDec)) + ',DES=''' + REPLACE(REPLACE(REPLACE(PershkrimiFiskal,';',''),',',''),'''','') + '''' + case when sasia<0 then ',Storno' else '' end + CHAR(13)+CHAR(10)
--		FROM 
--		(
--			   SELECT G.Id ,
--			   G.PershkrimiFiskal ,
--			   G.Sasia ,
--			   G.Kategoria ,
--			   G.Shifra ,
--			   CmimiShitjesDyDec
--		FROM 
--		(
--				SELECT 
--				DD.Id
--				,A.PershkrimiFiskal
--				,convert(decimal(18,2),QmimiShitjes-(QmimiShitjes*Rabati/ 100.00)) QmimiShitjes
--				,DD.Sasia 
--				,T.Kategoria 
--				,A.Shifra 
--				,convert(decimal(18,2),DD.QmimiShitjes*(1-Rabati/100.00)*(1-EkstraRabati/100.00)) CmimiShitjesDyDec
--				,convert(decimal(18,2),DD.QmimiShitjes*(1-Rabati/100.00)*(1-EkstraRabati/100.00)) CmimiShitjesshumeDec
--				,CONVERT(DECIMAL(18,2) ,DD.Rabati) Rabati
--				--,CONVERT(DECIMAL(18,2) ,DD.EkstraRabati) EkstraRabati
--				FROM dbo.DaljaMallitDetale DD  with(nolock)
--				INNER JOIN dbo.Artikujt A  with(nolock) ON A.Id = DD.ArtikulliId  
--				INNER JOIN dbo.Tatimet T with(nolock) ON A.TatimetID =T.Id
--				WHERE DD.DaljaMallitID = @DaljaMallitId 
--		) G

--		) YHY
--		Order by Sasia desc,Id ASC

--		select @Komanda +='Allega On'+ CHAR(13)+CHAR(10)

--		Select  @Komanda +='CHIU TEND='+ MenyraPageses +CHAR(13)+CHAR(10)
--		FROM (
						
--						SELECT  CASE WHEN MAX(E.MenyraEPagesesId) = 22 THEN '1' ELSE '5' END MenyraPageses
--						FROM    dbo.EkzekutimiPageses E  with(nolock)
--						WHERE   E.DaljaMallitID = @DaljaMallitId
--						GROUP BY E.DaljaMallitID
						
--			) o


--		Select @Komanda+='ALLEG Riga=''' + 'Arka:'+CONVERT(VARCHAR(10),D.NumriArkes)+', Nr. Paragonit: '+CONVERT(VARCHAR(10),D.NrFatures)+ '''' +CHAR(13)+CHAR(10)
--					   +'ALLEG Riga=''' + 'Ju Faleminderit!'''
--		FROM dbo.DaljaMallit D with(nolock) INNER JOIN dbo.EkzekutimiPageses E with(nolock) ON E.DaljaMallitID = D.Id 
--		WHERE D.Id=@DaljaMallitId 

--		select @Komanda+='' + CHAR(13)+CHAR(10)
--		select @Komanda+='ALLEGA FINE'

--		Select @Komanda=REPLACE(@Komanda,'ë','e')
--		Select @Komanda=REPLACE(@Komanda,'Ë','e')
--		Select @Komanda=REPLACE(@Komanda,'ç','c')
--		Select @Komanda=REPLACE(@Komanda,'Ç','c')
--		--Select @Komanda COLLATE SQL_Latin1_General_CP1_CI_AS

		

--		RETURN;


-- end

Go --
IF EXISTS (SELECT * FROM sys.objects 
			  WHERE object_id = OBJECT_ID(N'TOSHIBA.POS_GjenerimiKuponitFiskalStorno_Sp') ) 
			  begin
			  Drop  Procedure TOSHIBA.POS_GjenerimiKuponitFiskalStorno_Sp
			  End
Go --
CREATE PROCEDURE [TOSHIBA].[POS_GjenerimiKuponitFiskalStorno_Sp]
(
	 @DaljaMallitId BIGINT=null
)
WITH recompile
AS
Begin

declare @GjenerimiKuponit varchar(40) = null

select @GjenerimiKuponit = Vlera
from dbo.Konfigurimet where id = 154 

DECLARE @Komanda AS VARCHAR(max)=''
 
--if(@GjenerimiKuponit = 'Gekos' or @GjenerimiKuponit is null)
--begin 
----Kosova
--End
--Shqiperi 
if(@GjenerimiKuponit = 'Shqiperi')
begin
---------------KuponiFiskal------------------------------
		Select @Komanda='CLEAR'+CHAR(13)+CHAR(10)
		+'CHIAVE REG'+CHAR(13)+CHAR(10)


		SELECT @Komanda+='VEND REP=' + Kategoria + ',qty=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,3),Sasia)) + ',PREZZO=' + CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),CmimiShitjesDyDec)) + ',DES=''' + REPLACE(REPLACE(REPLACE(PershkrimiFiskal,';',''),',',''),'''','') + '''' + CHAR(13)+CHAR(10)
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
				,convert(decimal(18,2),DD.QmimiShitjes-(QmimiShitjes*Rabati/ 100.00)) CmimiShitjesDyDec
				,convert(decimal(18,2),DD.QmimiShitjes-(QmimiShitjes*Rabati/ 100.00)) CmimiShitjesshumeDec
				,CONVERT(DECIMAL(18,2) ,DD.Rabati) Rabati
				FROM dbo.DaljaMallitDetale DD INNER JOIN dbo.Artikujt A ON A.Id = DD.ArtikulliId INNER JOIN dbo.Tatimet T ON A.TatimetID =T.Id
				WHERE DD.DaljaMallitID = @DaljaMallitId 
		) G

		) YHY
		Order by Sasia desc,Id ASC

		Select @Komanda+='INP TERM=TSTVOID' +CHAR(13)+CHAR(10)
		select @Komanda+='INP TERM=TSCONF'

		Select @Komanda=REPLACE(@Komanda,'ë','e')
		Select @Komanda=REPLACE(@Komanda,'Ë','e')
		Select @Komanda=REPLACE(@Komanda,'ç','c')
		Select @Komanda=REPLACE(@Komanda,'Ç','c')
		Select @Komanda COLLATE SQL_Latin1_General_CP1_CI_AS

 end		

		RETURN;


 end
Go --