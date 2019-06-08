using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LuccaDevises.Entities;

namespace LuccaDevises.Module
{
    public class CurrencyExchange : ICurrencyExchange
    {
        private IFileManager fileManager;

        public CurrencyExchange(IFileManager _fileManager)
        {
            fileManager = _fileManager;
        }

        public void ConvertCurrencyFromExternalFile (string FullFilePath)
        {
            var TargetCurrencyToConvert = new CurrencyToConvert();
            var CurrencyRates = new List<CurrencyRate>();

            var rows = fileManager.ReadExternalCurrencyExchageFile(FullFilePath);

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

       
        private decimal ExchageCalculator(CurrencyToConvert currencyToConvert, List<CurrencyRate> currencyRates)
        {
            var currenciesInUse = new Dictionary<string, decimal>(StringComparer.CurrentCultureIgnoreCase);
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

            return Math.Round(currenciesInUse.FirstOrDefault(m => m.Key == currencyToConvert.InputCurrencyTo).Value,0);
        }
    }
}