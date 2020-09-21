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

            PrintData(data, TranslateStringState.TranslatedNormalized, stopwatch.ElapsedMilliseconds / 1000.0);

            Console.ReadLine();

            
            
            Console.WriteLine("Saving...");
            IExcelExporter<TranslateString> excelExporter = new ExcelExporter<TranslateString>();
            excelExporter.ExportData(data, fileName, "TranslatedString", "CellCode");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void PrintData(IList<TranslateString> data, TranslateStringState? state, double deltaTime)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("PRINTING DATA");
            int countOfTranslated = 0;
            foreach (var item in data)
            {
                if (state != null && item.TranslateStringState != state)
                    continue;
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"{item.CellCode} - {item.LoadedString}; \nnormalized: {item.NormalizedString}; \nreplaceToken: {item.ReplaceToken}; \ntranslate: {item.TranslatedString}");
                Console.WriteLine("---------------------------------------");
                countOfTranslated++;
            }
            Console.WriteLine($"Result: {countOfTranslated} translated string(s) for {deltaTime} sec...");
            Console.WriteLine("END OF PRINTING");
            Console.WriteLine();
        }
    }
}
