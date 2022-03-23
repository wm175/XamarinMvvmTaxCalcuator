using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;

namespace TaxCalculatorMobile.Services
{
    public class TaxService : ITaxService
    {
        private ITaxCalculator _calculator;

        public TaxService(ITaxCalculator calc)
        {
            this._calculator = calc;
        }

        public decimal CalculateTaxForOrder(Order order)
        {
            return _calculator.CalculateTaxForOrder(order);
        }

        public decimal GetTaxRateForLocation(Location location)
        {
            return _calculator.GetTaxRateForLocation(location);
        }
    }
}
