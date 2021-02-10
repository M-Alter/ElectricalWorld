using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Order
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// private ID of the Customer <br>for internal use</br>
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// time of the order
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// total price of the order
        /// </summary>
        public double TotalPrice { get; set; }

    }
}
