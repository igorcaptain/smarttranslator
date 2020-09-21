

CREATE PROCEDURE [Translate].[pub_getStringTranslate] 
    @PrimaryString VARCHAR(900)
AS

SET NOCOUNT ON

BEGIN TRY

	SELECT d.TranslatedNormalizedString 
	FROM [Translate].[Dictionary] d WITH(NOLOCK)
	WHERE d.[OriginalNormalizedString] = @PrimaryString
    
END TRY
BEGIN CATCH
    RETURN 1;
END CATCH
