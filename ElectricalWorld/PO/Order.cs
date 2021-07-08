using System;
using System.Collections.Generic;

namespace PO
{
    public class Order
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// customer of the order
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// time of the order
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// total price of the order
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// is the order paid
        /// </summary>
        public bool Paid { get; set; }

        /// <summary>
        /// items of the order
        /// </summary>
        public IEnumerable<InvoiceItem> Items { get; set; }

        /// <summary>
        /// profit earned on this order
        /// </summary>
        public double Profit { get; set; }

    }
}
