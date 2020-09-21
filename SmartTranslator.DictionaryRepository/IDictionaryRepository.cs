using SmartTranslator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTranslator.DictionaryRepository
{
    public interface IDictionaryRepository : IDisposable
    {
        IEnumerable<TranslateDictionary> GetAllTranslates();
        string GetTranslateString(string itString);
        void InsertTranslate(TranslateDictionary translateDictionary);
        void DeleteTranslate(int idTranslate);
        void UpdateTranslate(TranslateDictionary translateDictionary);
    }
}
