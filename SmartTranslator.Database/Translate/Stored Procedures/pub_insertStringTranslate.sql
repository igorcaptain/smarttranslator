
CREATE PROCEDURE [Translate].[pub_insertStringTranslate]
	@VendorCode [varchar](100) = NULL,
	@OriginalString [varchar](900),
	@OriginalNormalizedString [varchar](900) = NULL,
	@TranslatedString [varchar](900) = NULL,
	@TranslatedNormalizedString [varchar](900) = NULL,
	@DimensionSuffix [varchar](100) = NULL
AS

SET NOCOUNT ON

BEGIN TRY

	DECLARE @Now DATETIME = GETDATE();
	
	INSERT INTO [Translate].[Dictionary] 
	(
		 [VendorCode]
		,[OriginalString]
		,[OriginalNormalizedString]
		,[TranslatedString]
		,[TranslatedNormalizedString]
		,[DimensionSuffix]
		,[CreatedDate]
	)
	VALUES 
	(
		 @VendorCode
		,@OriginalString
		,@OriginalNormalizedString
		,@TranslatedString
		,@TranslatedNormalizedString
		,@DimensionSuffix
		,@Now
	);

END TRY
BEGIN CATCH
	RETURN 1;
END CATCH
