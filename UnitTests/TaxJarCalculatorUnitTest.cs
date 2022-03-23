using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Net.Http;
using TaxCalculator.Calculators;
using TaxCalculator.Common.Models;

namespace TaxCalculator.UnitTests
{
    [TestClass]
    public class TaxJarCalculatorUnitTest
    {
        private const string BaseAddress = "https://api.taxjar.com/v2/";

        [TestMethod]
        public void Is_Get_Tax_Rate_Returning_Combined_Rate()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "rates*")
                    .Respond("application/json",
                    "{" +
                        "'rate': {" +
                            "'combined_rate': '0.0725'," +
                        "}" +
                    "}");
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.GetTaxRateForLocation(new Location());

            //Assert
            Assert.AreEqual(result, 0.0725m);
        }

        [TestMethod]
        public void Is_Calculate_Taxes_Returning_Amount_To_Collect()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "taxes*")
                    .Respond("application/json",
                    "{" +
                        "'tax': {" +
                            "'amount_to_collect': '1.234'," +
                        "}" +
                    "}");
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.CalculateTaxForOrder(new Order());

            //Assert
            Assert.AreEqual(result, 1.234m);
        }

        [TestMethod]
        public void Is_Calculate_Taxes_Negative_One_On_HttpRequestException()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "taxes*")
                    .Throw(new HttpRequestException());
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.CalculateTaxForOrder(new Order());

            //Assert
            Assert.AreEqual(result, -1m);
        }

        [TestMethod]
        public void Is_Get_Rate_Negative_One_On_HttpRequestException()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "taxes*")
                    .Throw(new HttpRequestException());
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.GetTaxRateForLocation(new Location());

            //Assert
            Assert.AreEqual(result, -1m);
        }

        [TestMethod]
        public void Is_Get_Rate_Negative_One_On_Unsucessful_Status_Code()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "taxes*")
                    .Respond(System.Net.HttpStatusCode.Unauthorized);
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.GetTaxRateForLocation(new Location());

            //Assert
            Assert.AreEqual(result, -1m);
        }

        [TestMethod]
        public void Is_Calculate_Tax_Negative_One_On_Unsucessful_Status_Code()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(BaseAddress + "taxes*")
                    .Respond(System.Net.HttpStatusCode.Unauthorized);
            var mockClient = mockHttp.ToHttpClient();
            TaxJarCalculator taxjar = new TaxJarCalculator(mockClient);

            //Act
            decimal result = taxjar.CalculateTaxForOrder(new Order());

            //Assert
            Assert.AreEqual(result, -1m);
        }
    }
}
