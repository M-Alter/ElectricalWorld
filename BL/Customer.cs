﻿using System.Collections.Generic;

namespace BO
{
    public class Customer
    {
        /// <summary>
        /// private ID of the customer <br>for internal use</br>
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// address of the customer
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// post code of the customer
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// City of the customer
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// phone number of the customer
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// mobile number of the customer
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// email address of the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// order numbers of this customer
        /// </summary>
        public IEnumerable<string> OrderIDs { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
