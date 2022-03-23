using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;

namespace TaxCalculator.Calculators
{
    public class TaxJarCalculator : IAnsyncTaxCalculator
    {
        protected const string BaseAddress = "https://api.taxjar.com/v2/";
        private const string Token = "YOUR API_KEY GOES HERE";
        private HttpClient _client;

        public TaxJarCalculator(HttpClient client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public decimal CalculateTaxForOrder(Order order)
        {
            return this.CalculateTaxForOrderAsync(order).GetAwaiter().GetResult();
        }


        /// <inheritdoc/>
        public decimal GetTaxRateForLocation(Location location)
        {
            return this.GetTaxRateForLocationAsync(location).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<decimal> GetTaxRateForLocationAsync(Location location)
        {
            decimal rate = 0;

            try
            {
                
                var uriBuilder = new UriBuilder(BaseAddress + "rates");
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                uriBuilder.Port = -1;
                query["zip"] = location.Zipcode;
                uriBuilder.Query = query.ToString();
                string url = uriBuilder.ToString();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = await _client.GetAsync(url).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                string resultString = await response.Content.ReadAsStringAsync();
                JObject resultObject = (JObject)JsonConvert.DeserializeObject(resultString);

                rate = (decimal)resultObject["rate"]["combined_rate"];
            }
            catch (HttpRequestException e)
            {
                rate = -1;
            }

            return rate;
        }

        /// <inheritdoc/>
        public async Task<decimal> CalculateTaxForOrderAsync(Order order)
        {
            decimal amount = 0;

            try
            {
                string url = BaseAddress + "taxes";

                var contentString = BuildContentStringForCalculateTax(order);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                string resultString = await response.Content.ReadAsStringAsync();
                JObject resultObject = (JObject)JsonConvert.DeserializeObject(resultString);
                amount = (decimal)resultObject["tax"]["amount_to_collect"];
            }
            catch (Exception e)
            {
                amount = -1;
            }

            return amount;
        }

        private string BuildContentStringForCalculateTax(Order order)
        {
            var content = new JObject();

            //from location
            content.Add("from_country", order.FromLocation.Country);
            content.Add("from_state", order.FromLocation.State);
            content.Add("from_city", order.FromLocation.City);
            content.Add("from_zip", order.FromLocation.Zipcode);

            //to location
            content.Add("to_country", order.FromLocation.Country);
            content.Add("to_state", order.FromLocation.State);
            content.Add("to_city", order.FromLocation.City);
            content.Add("to_zip", order.FromLocation.Zipcode);

            //costs
            content.Add("amount", order.OrderTotal);
            content.Add("shipping", order.ShippingCost);

            return content.ToString();
        }
    }
}
