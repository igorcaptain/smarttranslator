
CREATE PROCEDURE [Translate].[pub_updateStringTranslate]
	 @StringID [int]
	,@VendorCode [varchar](100) = NULL
	,@OriginalString [varchar](900)
	,@OriginalNormalizedString [varchar](900) = NULL
	,@TranslatedString [varchar](900) = NULL
	,@TranslatedNormalizedString [varchar](900) = NULL
	,@DimensionSuffix [varchar](100) = NULL
AS

SET NOCOUNT ON

BEGIN TRY

	DECLARE @Now DATETIME = GETDATE();
	
	UPDATE dict
	SET 
		 dict.[VendorCode] = @VendorCode
		,dict.[OriginalString] = @OriginalString
		,dict.[OriginalNormalizedString] = @OriginalNormalizedString
		,dict.[TranslatedString] = @TranslatedString
		,dict.[TranslatedNormalizedString] = @TranslatedNormalizedString
		,dict.[DimensionSuffix] = @DimensionSuffix
		,dict.[ModifiedDate] = @Now
	FROM [Translate].[Dictionary] dict
	WHERE dict.StringID = @StringID

END TRY
BEGIN CATCH
	RETURN 1;
END CATCH
