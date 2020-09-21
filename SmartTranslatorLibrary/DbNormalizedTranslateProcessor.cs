using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SmartTranslator.Data;
using SmartTranslator.DictionaryRepository;

namespace SmartTranslator.Processor
{
    public class DbNormalizedTranslateProcessor : ITranslateProcessor
    {
        IDictionaryRepository _repository;

        public DbNormalizedTranslateProcessor()
        {
            _repository = new DbDictionaryRepository();
        }

        public void TranslateProcess(IList<TranslateString> data)
        {
            string translatedStringWithToken, translatedString;
            MatchCollection replaceTokens;

            for (int i = 0; i < data.Count; i++)
            {
                translatedStringWithToken = _repository.GetTranslateString(data[i].NormalizedString);
                translatedString = translatedStringWithToken;

                replaceTokens = Regex.Matches(data[i].ReplaceToken, @"#\d+(\.|,)?\d*#");
                for (int j = 0; j < replaceTokens.Count; j++)
                {
                    translatedString = translatedString?.Replace($"#{j}#", replaceTokens[j]?.ToString()?.Replace("#", ""));
                }

                data[i].TranslatedString = translatedString;
                data[i].TranslateStringState = String.IsNullOrEmpty(translatedString) ? TranslateStringState.TranslateFailed : TranslateStringState.TranslatedNormalized;
            }
        }
    }
}
