using SmartTranslator.Data;
using SmartTranslator.DictionaryRepository;
using SmartTranslator.Utils.DataNormalizer;

namespace SmartTranslator.Utils.Dictionary
{
    public class DictionaryProcessor : IDictionaryProcessor
    {
        IDictionaryRepository _repository;
        IDataNormalizer _normalizer;

        public DictionaryProcessor()
        {
            _repository = new DbDictionaryRepository();
            _normalizer = new DataNormalizer.DataNormalizer();
        }

        public void UpdateOriginalNormalizedAndTranslatedNormalizedFields()
        {
            var translates = _repository.GetAllTranslates();
            TranslateDictionary translate;
            foreach (TranslateDictionary item in translates)
            {
                translate = item;
                translate.OriginalNormalizedString = _normalizer.GetNormalizedTranslateString(translate.OriginalString).NormalizedString;
                translate.TranslatedNormalizedString = _normalizer.GetNormalizedTranslateString(translate.TranslatedString).NormalizedString;
                _repository.UpdateTranslate(translate);
            }
        }
    }
}
