using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Calculators
{
    /// <summary>
    /// Implementation of ITaxCalculator inteded for testing
    /// </summary>
    public class Test_Calculator_TenPercent : ITaxCalculator
    {
        /// <summary>
        /// Test function meant to return a fixed value based on the order object
        /// </summary>
        /// <param name="order">Order to test with</param>
        /// <returns>Returns 10% of the sum of the given order's total and shipping cost</returns>
        public decimal CalculateTaxForOrder(Order order)
        {
            decimal cost = order.OrderTotal + order.ShippingCost;
            return cost * .1m;
        }
        /// <summary>
        /// Test function meant to return a fixed value no matter what
        /// </summary>
        /// <param name="location">Location object</param>
        /// <returns>0.1 no matter what</returns>
        public decimal GetTaxRateForLocation(Location location)
        {
            return .1m;
        }
    }
}
