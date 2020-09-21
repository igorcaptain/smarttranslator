using SmartTranslator.Data;
using System.Collections.Generic;

namespace SmartTranslator.Utils.DataNormalizer
{
    public interface IDataNormalizer
    {
        void DataNormalize(ref IList<TranslateString> data);
        IList<TranslateString> GetNormalizedData(IList<TranslateString> data);
        TranslateString GetNormalizedTranslateString(TranslateString input);
        TranslateString GetNormalizedTranslateString(string input);
    }
}
