using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SmartTranslator.Excel
{
    public class ExcelExporter<T> : IExcelExporter<T>
    {
        public void ExportData(IList<T> data, string filePath, string valueFieldName, string cellFieldName)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);

            foreach(var item in data)
            {
                Type modelType = typeof(T);
                PropertyInfo valueProperty = modelType.GetProperty(valueFieldName);
                PropertyInfo cellProperty = modelType.GetProperty(cellFieldName);

                worksheet.Cell(cellProperty.GetValue(item).ToString()).Value = valueProperty.GetValue(item)?.ToString();
            }

            workbook.SaveAs("[tanslated]_" + filePath);
        }
    }
}
