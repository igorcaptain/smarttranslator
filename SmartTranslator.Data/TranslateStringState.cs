namespace SmartTranslator.Data
{
    public enum TranslateStringState : int
    {
        Empty,
        Loaded,
        Normalized,
        Error,
        TranslatedVendorCode,
        TranslatedNormalized,
        DuplicateFull,
        DuplicateNormalized,
        TranslateFailed
    }
}
