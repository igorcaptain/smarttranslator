using System;
using ClosedXML.Excel;
using System.Reflection;
using System.Collections.Generic;

namespace SmartTranslator.Excel
{
    public class ExcelImporter<T> : IExcelImporter<T>
    {
        public ExcelImporter()
        {

        }

        public IList<T> GetData(string filePath, string valueFieldName, string cellFieldName)
        {
            IList<T> result = new List<T>();

            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed();

            foreach (var row in rows)
            {
                var cells = row.Cells();

                foreach (var cell in cells)
                {
                    Type modelType = typeof(T);
                    T modelInstance = Activator.CreateInstance<T>();
                    PropertyInfo valueProperty = modelType.GetProperty(valueFieldName);
                    PropertyInfo cellProperty = modelType.GetProperty(cellFieldName);

                    valueProperty.SetValue(modelInstance, cell.Value);
                    cellProperty.SetValue(modelInstance, cell.Address.ToString());

                    result.Add(modelInstance);
                }
            }

            return result;
        }
    }
}
