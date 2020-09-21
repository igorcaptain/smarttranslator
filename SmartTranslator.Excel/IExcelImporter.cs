using System.Collections.Generic;

namespace SmartTranslator.Excel
{
    public interface IExcelImporter<T>
    {
        IList<T> GetData(string filePath, string valueFieldName, string cellFieldName);
    }
}
