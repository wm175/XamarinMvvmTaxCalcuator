using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Common
{
    public interface ITaxCalculator
    {
        /// <summary>
        /// Returns the tax rate for a given location
        /// </summary>
        /// <param name="location">location to get tax rate for</param>
        /// <returns>The tax rate for the location or -1 if there was an error</returns>
        decimal GetTaxRateForLocation(Location location);

        /// <summary>
        /// Returns the taxable amount for a given order
        /// </summary>
        /// <param name="location">location to get tax rate for</param>
        /// <returns>The taxable amount for the order or -1 if there was an error</returns>
        decimal CalculateTaxForOrder(Order order);
    }
}
