using System;
using System.IO;
using System.Text;
using LuccaDevises.Module;

namespace LuccaDevises
{
    class Program
    {
        static void Main(string[] args)
        {
            var CurrencyExchange = new CurrencyExchange();
            CurrencyExchange.ConvertCurrencyFromExternalFile();
        }
    }
}
