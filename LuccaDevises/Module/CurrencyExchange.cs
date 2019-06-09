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

        /// <summary>
        /// Read an external file and convert currency
        /// </summary>
        /// <param name="FullFilePath"></param>
        public decimal ConvertCurrencyFromExternalFile (string FullFilePath)
        {
            var TargetCurrencyToConvert = new CurrencyToConvert();
            var CurrencyRates = new List<CurrencyRate>();

            //Read Te file and get all the rows from file
            var rows = fileManager.ReadExternalCurrencyExchageFile(FullFilePath);

            //Mapping of the rows to TargetCurrencyToConvert and CurrencyRates information
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

            //Calculate the exchange rate
            var result = ExchageCalculator(TargetCurrencyToConvert, CurrencyRates);

            //Show the result in the console and return its value.
            Console.WriteLine(result);

            return result;
        }

        /// <summary>
        /// Calculate Currency exchange From/To and amount by using a list of currency rates
        /// </summary>
        /// <param name="currencyToConvert"></param>
        /// <param name="currencyRates"></param>
        /// <returns></returns>
        private decimal ExchageCalculator(CurrencyToConvert currencyToConvert, List<CurrencyRate> currencyRates)
        {
            //The intermediate result from evaluating all currencies one to one, starting from the InputCurrencyFrom
            var currenciesUsed = new Dictionary<string, decimal>(StringComparer.CurrentCultureIgnoreCase);
            currenciesUsed.Add(currencyToConvert.InputCurrencyFrom, currencyToConvert.InputAmountToExchage);

            try
            {
                //Create a list of all the currencies from the currencyRates (information)
                var allCurrencies = GetAllCurrenciesInList(currencyRates);

                //Check if the list of currency rate (information) has any error
                CheckError(allCurrencies, currencyToConvert, currencyRates);

                //Loop finish when the InputCurrencyTo is in the list of currenciesUsed - It means i already have the result from evaluation
                while (currenciesUsed.Any() && !currenciesUsed.Any(m => m.Key == currencyToConvert.InputCurrencyTo) && allCurrencies.Any())
                {
                    //CurrencyInUse is the currency in evaluation
                    var CurrencyInUse = currenciesUsed.First();
                    allCurrencies.Remove(CurrencyInUse.Key);

                    //Next currencies to be evaluated linked to CurrencyInUse.
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
                            currenciesUsed.Add(NextCurrency.CurrencyTo, Math.Round(NextCurrency.Rate * CurrencyInUse.Value, 4));
                        }
                    }

                    //Remove the currency already evaluated
                    currenciesUsed.Remove(CurrencyInUse.Key);
                }

                if (!currenciesUsed.Any())
                {
                    throw new Exception("Error : List of currency rates incomplete - No match was found");
                }

                return Math.Round(currenciesUsed.FirstOrDefault(m => m.Key == currencyToConvert.InputCurrencyTo).Value, 0);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<string> GetAllCurrenciesInList(List<CurrencyRate> currencyRates)
        {
            var result = new List<string>();

            foreach (var item in currencyRates)
            {
                if (!result.Contains(item.CurrencyFrom))
                {
                    result.Add(item.CurrencyFrom);
                }

                if (!result.Contains(item.CurrencyTo))
                {
                    result.Add(item.CurrencyTo);
                }
            }

            return result;
        }

        private void CheckError(List<string> allCurrencies, CurrencyToConvert currencyToConvert, List<CurrencyRate> currencyRates)
        {
            if (!allCurrencies.Contains(currencyToConvert.InputCurrencyFrom))
                throw new Exception($"Error : Currency rate list doesn't contain the Input Currency : <{currencyToConvert.InputCurrencyFrom}>");          

            if (!allCurrencies.Contains(currencyToConvert.InputCurrencyTo))
                throw new Exception($"Error : Currency rate list doesn't contain the Input Currency : <{currencyToConvert.InputCurrencyTo}>");

            if (currencyRates.Any(m => m.Rate < 0) || currencyToConvert.InputAmountToExchage < 0)
                throw new Exception("Error : Currency rate cannot be negative !");
        }
    }
}