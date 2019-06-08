using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Entities
{
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]
    public class CurrencyToConvert
    {
        public string InputCurrencyFrom { get; set; }
        public decimal InputAmountToExchage { get; set; }
        public string InputCurrencyTo { get; set; }
    }
}
