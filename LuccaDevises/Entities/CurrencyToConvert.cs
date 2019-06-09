using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Entities
{
    /// <summary>
    /// Input data of currency to convert (from, amount and to)
    /// </summary>
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]    
    public class CurrencyToConvert : IEquatable<CurrencyToConvert>
    {
        [FieldTrim(TrimMode.Both)]
        public string InputCurrencyFrom { get; set; }

        [FieldTrim(TrimMode.Both)]
        public decimal InputAmountToExchage { get; set; }

        [FieldTrim(TrimMode.Both)]
        public string InputCurrencyTo { get; set; }

        /// <summary>
        /// Override Equals Method - Useful to compare to CurrencyToConvert objects
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CurrencyToConvert other)
        {
            return other != null &&
                   this.InputCurrencyFrom == other.InputCurrencyFrom &&
                   this.InputAmountToExchage == other.InputAmountToExchage &&
                   this.InputCurrencyTo == other.InputCurrencyTo;
        }

        /// <summary>
        /// Override Equals Method - Useful to compare to CurrencyToConvert objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            return Equals(obj as CurrencyToConvert);
        }

        /// <summary>
        /// Override GetHashCode Method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(InputCurrencyFrom, InputAmountToExchage, InputCurrencyTo);
        }
    }
}
