IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WEB_PAGING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WEB_PAGING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_WEB_PAGING]
 @TABLENAMES VARCHAR(max),
 @COLUMNS VARCHAR(max),
 @ORDERCOLUMNNAME VARCHAR(1000),
 @WHERE VARCHAR(max),
 @PAGECURRENTINDEX INT,
 @PAGEEACHSIZE INT,
 @PAGECOUNT INT OUTPUT,
 @ITEMSCOUNT INT OUTPUT
AS
BEGIN
 DECLARE @sqlRecordCount NVARCHAR(max);
 DECLARE @recordCount INT;
 DECLARE @sqlSelect NVARCHAR(max);
 DECLARE @sqlSelectWhere NVARCHAR(2000);
 
 if (@WHERE <> '')
  set @sqlSelectWhere = ' where '+ @WHERE;
 else
  set @sqlSelectWhere = '';

 SET @sqlRecordCount=N'select @recordCount = count(*) from (SELECT ' + @COLUMNS + ' FROM ' + @TABLENAMES + @sqlSelectWhere + ') T';
 exec sp_executesql @sqlRecordCount,N'@recordCount int output',@recordCount output;
 
 set @ITEMSCOUNT = @recordCount;
 
 if(@recordCount % @PAGEEACHSIZE = 0)
  set @PAGECOUNT = @recordCount / @PAGEEACHSIZE;
 else
  set @PAGECOUNT = @recordCount / @PAGEEACHSIZE + 1;
  
 set @sqlSelect = 
  N'select * '
  + ' from ( '
  + ' select row_number() over (order by ' + @ORDERCOLUMNNAME 
  + ' ) as tempRowIndex,'
  + @COLUMNS
  + ' from ' 
  + @TABLENAMES 
  + @sqlSelectWhere
  + ') as tempTableName '
  + ' where tempRowIndex between ' 
  + str((@PAGECURRENTINDEX - 1) * @PAGEEACHSIZE + 1) + ' and '+ str(@PAGECURRENTINDEX * @PAGEEACHSIZE);
 
 exec (@sqlSelect);
END



GO
