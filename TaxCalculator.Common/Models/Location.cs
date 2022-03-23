using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Common.Models
{
    public class Location
    {
        /// <summary>
        /// Country of the location
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// State/Providence of the location
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Zipcode for the location
        /// </summary>
        public string Zipcode { get; set;}
        /// <summary>
        /// City of the location
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Street Address of the location
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Initializes a new instance of Location
        /// </summary>

        public Location()
        {
            this.Country = "";
            this.State = "";
            this.Zipcode = "";
            this.City = "";
            this.StreetAddress = "";
        }
    }
}
