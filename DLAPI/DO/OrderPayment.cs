using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class OrderPayment
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// amount paid
        /// </summary>
        public double AmountPaid { get; set; }


    }
}
