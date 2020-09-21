using SmartTranslator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartTranslator.Utils.DataNormalizer
{
    public class DataNormalizer : IDataNormalizer
    {
        private IList<TranslateString> CleanInvalid(IList<TranslateString> data)
        {
            return data.Where(d => !string.IsNullOrEmpty(d.LoadedString))
                //.Where(d => !double.TryParse(d.LoadedString, out _))
                .Where(d => !Regex.IsMatch(d.LoadedString, @"^(:?[\d,]+(\.|,))*\d+$"))
                .ToList();
        }

        //private void NormalizeStringWithReplaceToken(IList<TranslateString> data)
        //{
        //    string loadedString = "";
        //    string normalized = "";
        //    string replaceToken = "";

        //    for (int i = 0; i < data.Count(); i++)
        //    {
        //        loadedString = data[i].LoadedString;
        //        //    .Replace(" ", "_");
        //        //loadedString = "HELLO 40mm 30 28,4 15.5 20a30";
        //        replaceToken = "";
        //        normalized = loadedString;

        //        MatchCollection numbers = Regex.Matches(loadedString, @"\d+(\.|,)?\d*");

        //        int tokenIndex = 0;
        //        foreach (var match in numbers)
        //        {
        //            //loadedString = loadedString.Replace(match.ToString(), "#" + tokenIndex++ + "#");
        //            var regex = new Regex(Regex.Escape(match.ToString()));
        //            normalized = regex.Replace(normalized, "#" + tokenIndex++ + "#", 1);
        //            replaceToken += "#" + match.ToString() + "#";
        //        }

        //        data[i].NormalizedString = normalized;
        //        data[i].ReplaceToken = replaceToken;
        //        data[i].TranslateStringState = TranslateStringState.Normalized;
        //    }
        //}

        private void NormalizeStringWithReplaceToken(IList<TranslateString> data)
        {
            for (int i = 0; i < data.Count(); i++)
                data[i] = GetNormalizedTranslateString(data[i]);
        }

        public TranslateString GetNormalizedTranslateString(TranslateString input)
        {
            TranslateString result = input.Clone() as TranslateString;

            string loadedString = input.LoadedString;
            string replaceToken = "";
            string normalized = loadedString;

            MatchCollection numbers = Regex.Matches(loadedString, @"\d+(\.|,)?\d*");

            int tokenIndex = 0;
            foreach (var match in numbers)
            {
                var regex = new Regex(Regex.Escape(match.ToString()));
                normalized = regex.Replace(normalized, "#" + tokenIndex++ + "#", 1);
                replaceToken += "#" + match.ToString() + "#";
            }

            result.NormalizedString = normalized;
            result.ReplaceToken = replaceToken;
            result.TranslateStringState = TranslateStringState.Normalized;

            return result;
        }

        public TranslateString GetNormalizedTranslateString(string input)
        {
            string loadedString = input;
            string replaceToken = "";
            string normalized = loadedString;

            MatchCollection numbers = Regex.Matches(loadedString, @"\d+(\.|,)?\d*");

            int tokenIndex = 0;
            foreach (var match in numbers)
            {
                var regex = new Regex(Regex.Escape(match.ToString()));
                normalized = regex.Replace(normalized, "#" + tokenIndex++ + "#", 1);
                replaceToken += "#" + match.ToString() + "#";
            }

            return new TranslateString()
            {
                LoadedString = loadedString,
                NormalizedString = normalized,
                ReplaceToken = replaceToken,
                TranslateStringState = TranslateStringState.Normalized
            };
        }

        public void DataNormalize(ref IList<TranslateString> data)
        {
            data = CleanInvalid(data);
            NormalizeStringWithReplaceToken(data);
        }

        public IList<TranslateString> GetNormalizedData(IList<TranslateString> data)
        {
            throw new NotImplementedException();
        }
    }
}
