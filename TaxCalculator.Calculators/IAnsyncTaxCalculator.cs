using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Calculators
{
    public interface IAnsyncTaxCalculator : ITaxCalculator
    {
        /// <summary>
        /// Asynchronous method that returns the tax rate for a given location
        /// </summary>
        /// <param name="location">location to get tax rate for</param>
        /// <returns>The tax rate for the location or -1 if there was an error</returns>
        Task<decimal> GetTaxRateForLocationAsync(Location location);

        /// <summary>
        /// Ansynchronous method that returns the taxable amount for a given order
        /// </summary>
        /// <param name="location">location to get tax rate for</param>
        /// <returns>The taxable amount for the order or -1 if there was an error</returns>
        Task<decimal> CalculateTaxForOrderAsync(Order order);
    }
}
