
CREATE PROCEDURE [Translate].[pub_getAllStringTranslates]
AS

SET NOCOUNT ON

BEGIN TRY

	DECLARE @Now DATETIME = GETDATE();
	
	SELECT 
		 dict.StringID
		,dict.VendorCode
		,dict.OriginalString
		,dict.OriginalNormalizedString
		,dict.TranslatedString
		,dict.TranslatedNormalizedString
		,dict.DimensionSuffix
		,dict.CreatedDate
		,dict.ModifiedDate
	FROM [Translate].[Dictionary] dict

END TRY
BEGIN CATCH
	RETURN 1;
END CATCH
