using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using SmartTranslator.Data;
using SmartTranslator.Excel;
using SmartTranslator.Processor;
using SmartTranslator.Utils.Dictionary;
using SmartTranslator.DictionaryRepository;
using SmartTranslator.Utils.DataNormalizer;

namespace SmartTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            IExcelImporter<TranslateString> excelImporter = new ExcelImporter<TranslateString>();
            string fileName = @"123.xlsx";
            var data = excelImporter.GetData(fileName, "LoadedString", "CellCode");

            //PrintData(data);

            IDataNormalizer normalizer = new DataNormalizer();

            //repository

            IDictionaryRepository repository = new DbDictionaryRepository();

            //IDictionaryProcessor dictionaryProcessor = new DictionaryProcessor();
            //dictionaryProcessor.UpdateOriginalNormalizedAndTranslatedNormalizedFields();
            
            normalizer.DataNormalize(ref data);

            ITranslateProcessor processor = new DbNormalizedTranslateProcessor();
            processor.TranslateProcess(data);

            stopwatch.Stop();

            Console.ReadLine();

            Console.WriteLine("Saving...");
            IExcelExporter<TranslateString> excelExporter = new ExcelExporter<TranslateString>();
            excelExporter.ExportData(data, fileName, "TranslatedString", "CellCode");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
