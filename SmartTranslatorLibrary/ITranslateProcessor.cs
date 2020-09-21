using SmartTranslator.Data;
using System.Collections.Generic;

namespace SmartTranslator.Processor
{
    public interface ITranslateProcessor
    {
        void TranslateProcess(IList<TranslateString> data);
    }
}
