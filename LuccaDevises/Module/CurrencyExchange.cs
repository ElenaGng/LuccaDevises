using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileHelpers;
using FileHelpers.Events;
using LuccaDevises.Entities;

namespace LuccaDevises.Module
{
    public class CurrencyExchange : ICurrencyExchange
    {
        public CurrencyToConvert TargetCurrencyToConvert { get; set; }
        public List<CurrencyRate> CurrencyRates { get; set; }

        public CurrencyExchange()
        {
            TargetCurrencyToConvert = new CurrencyToConvert();
            CurrencyRates = new List<CurrencyRate>();
        }

        public void ConvertCurrencyFromExternalFile ()
        {
            var FullFilePath = "C:\\Users\\Elena\\Desktop\\DeviseToConvert.csv";

            FileInfo finfo = new FileInfo(FullFilePath);

            if (!finfo.Exists || finfo.Length == 0)
            {
                Console.Write($"This file does not exist or is empty : {FullFilePath}");
            }

            var rows = ReadExternalFile(FullFilePath);

            foreach (var item in rows)
            {
                if (typeof(CurrencyToConvert) == item.GetType())
                {
                    TargetCurrencyToConvert = (CurrencyToConvert)item;
                }
                CurrencyRates.Add((CurrencyRate)item);
            }



        }

        private object[] ReadExternalFile(string FullFilePath)
        {          
            using (FileStream fs = new FileStream(FullFilePath, FileMode.Open, FileAccess.Read))
            {
                var engine = new MultiRecordEngine(typeof(CurrencyToConvert), typeof(CurrencyRate));

                engine.RecordSelector = new RecordTypeSelector(CustomSelector);

                return engine.ReadFile(FullFilePath);
            }            
        }

        private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0 || engine.LineNumber == 2)
                return null;      

            if (Char.IsNumber(recordLine[5]))
                return typeof(CurrencyToConvert);

            else
                return typeof(CurrencyRate);
        }
    }
}


//using (FileStream fs = File.OpenRead(path))
//{
//    byte[] b = new byte[1024];
//    UTF8Encoding temp = new UTF8Encoding(true);

//    while (fs.Read(b, 0, b.Length) > 0)
//    {
//        var a = temp.GetString(b);
//    }
//}