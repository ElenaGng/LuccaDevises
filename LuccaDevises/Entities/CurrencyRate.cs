using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Entities
{
    /// <summary>
    /// Currency information (from/to and rate)
    /// </summary>
    [DelimitedRecord(";")]
    [IgnoreEmptyLines]
    public class CurrencyRate : IEquatable<CurrencyRate>
    {
        [FieldTrim(TrimMode.Both)]
        public string CurrencyFrom { get; set; }

        [FieldTrim(TrimMode.Both)]
        public string CurrencyTo { get; set; }

        [FieldTrim(TrimMode.Both)]
        public decimal Rate { get; set; }

        /// <summary>
        /// Override Equals Method - Useful to compare to CurrencyRate objects
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CurrencyRate other)
        {
            return other != null &&
                   this.CurrencyFrom == other.CurrencyFrom &&
                   this.CurrencyTo == other.CurrencyTo &&
                   this.Rate == other.Rate;
        }

        /// <summary>
        /// Override Equals Method - Useful to compare to CurrencyRate objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            return Equals(obj as CurrencyRate);
        }

        /// <summary>
        /// Override GetHashCode Method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(CurrencyFrom, CurrencyTo, Rate);
        }
    }
}
