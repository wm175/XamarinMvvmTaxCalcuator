using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;
using TaxCalculator.Calculators;
using TaxCalculatorMobile.Services;

namespace TaxCalculator.UnitTests
{
    [TestClass]
    public class TaxServiceUnitTest
    {

        [TestMethod]
        public void Is_TaxService_Rate_Return_SameAs_Calculator_Rate_Return()
        {
            //Arrange
            ITaxCalculator calc = new Test_Calculator_TenPercent();
            TaxService service = new TaxService(calc);
            Location loc = new Location();

            //Act
            var serviceRate = service.GetTaxRateForLocation(loc);
            var calcRate = calc.GetTaxRateForLocation(loc);
            //Assert
            Assert.AreEqual(serviceRate, calcRate);
        }

        [TestMethod]
        public void Is_TaxService_CalculateTax_Return_SameAs_Calculator_Rate_Return()
        {
            //Arrange
            ITaxCalculator calc = new Test_Calculator_TenPercent();
            TaxService service = new TaxService(calc);
            Order order = new Order()
            {
                OrderTotal = 10,
                ShippingCost = 2
            };

            //Act
            var serviceTax = service.CalculateTaxForOrder(order);
            var calcTax = calc.CalculateTaxForOrder(order);
            //Assert
            Assert.AreEqual(serviceTax, calcTax);
        }
    }
}
