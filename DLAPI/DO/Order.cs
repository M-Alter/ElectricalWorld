using System;

namespace DO
{
    public class Order
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// private ID of the Customer <br>for internal use</br>
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

    }
}
