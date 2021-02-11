using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// customer of the order
        /// </summary>
        public Customer Customer { get; set; }

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
        public IEnumerable<Item> Items { get; set; }

    }
}
