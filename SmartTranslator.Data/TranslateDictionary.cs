using System;

namespace SmartTranslator.Data
{
    public class TranslateDictionary
    {
        public int StringID { get; set; }
        public string VendorCode { get; set; }
        public string OriginalString { get; set; }
        public string OriginalNormalizedString { get; set; }
        public string TranslatedString { get; set; }
        public string TranslatedNormalizedString { get; set; }
        public string DimensionSuffix { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
