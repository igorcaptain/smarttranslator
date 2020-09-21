CREATE FUNCTION [Translate].[fn_getStringTranslate] (@PrimaryString VARCHAR(900))
	RETURNS VARCHAR(900)
BEGIN
	DECLARE @TranslateString VARCHAR(900) = NULL;

	SELECT @TranslateString = d.UkrainianString 
	FROM [Translate].[Dictionary] d WITH(NOLOCK)
	WHERE d.ItalianString = @PrimaryString
		
	RETURN @TranslateString;
END