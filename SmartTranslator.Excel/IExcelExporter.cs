using System.Collections.Generic;

namespace SmartTranslator.Excel
{
    public interface IExcelExporter<T>
    {
        void ExportData(IList<T> data, string filePath, string valueFieldName, string cellFieldName);
    }
}
