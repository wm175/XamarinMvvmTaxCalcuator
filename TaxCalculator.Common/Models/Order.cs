using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Common.Models
{
    public class Order
    {
        /// <summary>
        /// Total cost of the order before shipping
        /// </summary>
        public decimal OrderTotal { get; set; }
        /// <summary>
        /// How much it costs to ship the order
        /// </summary>
        public decimal ShippingCost { get; set; }
        /// <summary>
        /// Location where the order is originating
        /// </summary>
        public Location FromLocation { get; set; } 
        /// <summary>
        /// Location where the order is going
        /// </summary>
        public Location ToLocation { get; set; }

        /// <summary>
        /// Initializes a new instance of an Order
        /// </summary>
        public Order()
        {
            OrderTotal = 0;
            ShippingCost = 0;
            FromLocation = new Location();
            ToLocation = new Location();
        }
    }
}
