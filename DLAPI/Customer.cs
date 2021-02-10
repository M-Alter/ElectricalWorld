using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Customer
    {
        /// <summary>
        /// private ID of the customer <br>for internal use</br>
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// lastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// house number of the customer
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// address of the customer
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// post code of the customer
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// phone number of the customer
        /// </summary>
        public long Phone { get; set; }

        /// <summary>
        /// mobile number of the customer
        /// </summary>
        public long Mobile { get; set; }

        /// <summary>
        /// email address of the customer
        /// </summary>
        public string email { get; set; }
    }
}
