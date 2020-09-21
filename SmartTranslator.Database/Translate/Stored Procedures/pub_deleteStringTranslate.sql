CREATE PROCEDURE [Translate].[pub_deleteStringTranslate]
	 @StringID [int]
AS

SET NOCOUNT ON

BEGIN TRY

	DECLARE @Now DATETIME = GETDATE();
	
	DELETE dict
	FROM [Translate].[Dictionary] dict
	WHERE dict.StringID = @StringID

END TRY
BEGIN CATCH
	RETURN 1;
END CATCH