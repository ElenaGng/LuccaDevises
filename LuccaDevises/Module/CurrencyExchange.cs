using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                else
                {
                    CurrencyRates.Add((CurrencyRate)item);
                }                    
            }

            var result = ExchageCalculator(TargetCurrencyToConvert, CurrencyRates);
        }

        /// <summary>
        /// Read an external file using MultiRecordEngine
        /// </summary>
        /// <param name="FullFilePath"></param>
        /// <returns>An single dimentional array with all the rows in the file</returns>
        private object[] ReadExternalFile(string FullFilePath)
        {          
            using (FileStream fs = new FileStream(FullFilePath, FileMode.Open, FileAccess.Read))
            {
                var engine = new MultiRecordEngine(typeof(CurrencyToConvert), typeof(CurrencyRate));

                engine.RecordSelector = new RecordTypeSelector(CustomSelector);

                return engine.ReadFile(FullFilePath);
            }            
        }

        /// <summary>
        /// Get the type of the row in the file
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="recordLine"></param>
        /// <returns></returns>
        private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            //Return null if the line is empty or is the second one -> I don't need it ;)
            if (recordLine.Length == 0 || engine.LineNumber == 2)
                return null;      

            if (Char.IsNumber(recordLine[5]))
                return typeof(CurrencyToConvert);

            else
                return typeof(CurrencyRate);
        }

        private decimal ExchageCalculator(CurrencyToConvert currencyToConvert, List<CurrencyRate> currencyRates)
        {
            var currenciesInUse = new Dictionary<string, decimal>();
            currenciesInUse.Add(currencyToConvert.InputCurrencyFrom, currencyToConvert.InputAmountToExchage);

            var allCurrencies = new List<string>();

            foreach (var item in currencyRates)
            {
                if (!allCurrencies.Contains(item.CurrencyFrom))
                {
                    allCurrencies.Add(item.CurrencyFrom);
                }

                if (!allCurrencies.Contains(item.CurrencyTo))
                {
                    allCurrencies.Add(item.CurrencyTo);
                }
            }

            while (!currenciesInUse.Any(m => m.Key == currencyToConvert.InputCurrencyTo) && allCurrencies.Any())
            {                
                var CurrencyInUse = currenciesInUse.First();
                allCurrencies.Remove(CurrencyInUse.Key);

                var NextCurrenciesInUse = currencyRates.Where(m => m.CurrencyFrom == CurrencyInUse.Key || m.CurrencyTo == CurrencyInUse.Key);

                foreach (var NextCurrency in NextCurrenciesInUse)
                {
                    if (NextCurrency.CurrencyTo == CurrencyInUse.Key)
                    {
                        NextCurrency.CurrencyTo = NextCurrency.CurrencyFrom;
                        NextCurrency.CurrencyFrom = CurrencyInUse.Key;                        
                        NextCurrency.Rate = Math.Round(1 / NextCurrency.Rate, 4);
                    }

                    if (allCurrencies.Contains(NextCurrency.CurrencyTo))
                    {
                        currenciesInUse.Add(NextCurrency.CurrencyTo, Math.Round(NextCurrency.Rate * CurrencyInUse.Value, 4));
                    }                    
                }

                currenciesInUse.Remove(CurrencyInUse.Key);
            }

            return currenciesInUse.FirstOrDefault(m => m.Key == currencyToConvert.InputCurrencyTo).Value;
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