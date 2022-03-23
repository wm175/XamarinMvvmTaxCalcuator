using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common.Models;

namespace TaxCalculatorMobile.Services
{
    public interface ITaxService
    {
        decimal CalculateTaxForOrder(Order order);
        decimal GetTaxRateForLocation(Location location);
    }
}
