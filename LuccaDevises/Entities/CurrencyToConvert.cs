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
        [FieldTrim(TrimMode.Both)]
        public string InputCurrencyFrom { get; set; }

        [FieldTrim(TrimMode.Both)]
        public decimal InputAmountToExchage { get; set; }

        [FieldTrim(TrimMode.Both)]
        public string InputCurrencyTo { get; set; }
    }
}
