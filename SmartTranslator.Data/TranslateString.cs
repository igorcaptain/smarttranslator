using System;

namespace SmartTranslator.Data
{
    public class TranslateString : ICloneable
    {
        //ID isn't needed
        public string CellCode { get; set; }
        public string VendorCode { get; set; } //maybe isn't needed
        public string LoadedString { get; set; }
        public string NormalizedString { get; set; }
        public string ReplaceToken { get; set; }
        public string TranslatedString { get; set; }
        public TranslateStringState TranslateStringState { get; set; } = 0;
        public int DuplicateReference { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
