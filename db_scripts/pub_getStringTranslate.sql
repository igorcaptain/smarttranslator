CREATE PROCEDURE [Translate].[pub_getStringTranslate] 
    @PrimaryString VARCHAR(900)
AS

SET NOCOUNT ON

BEGIN TRY

	SELECT d.UkrainianString 
	FROM [Translate].[Dictionary] d WITH(NOLOCK)
	WHERE d.ItalianString = @PrimaryString

END TRY
BEGIN CATCH
    RETURN 1;
END CATCH