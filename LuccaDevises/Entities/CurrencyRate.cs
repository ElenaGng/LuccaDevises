using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Entities
{
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]
    public class CurrencyRate
    {
        [FieldTrim(TrimMode.Both)]
        public string CurrencyFrom { get; set; }

        [FieldTrim(TrimMode.Both)]
        public string CurrencyTo { get; set; }

        [FieldTrim(TrimMode.Both)]
        public decimal Rate { get; set; }
    }
}
