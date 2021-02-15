using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BadCustomerExceptions: Exception
    {
        Exception exception;
        string customerID;
        public BadCustomerExceptions(string customerID, string message, Exception e):base(message, e)
        {
            this.customerID = customerID;
            this.exception = e;
        }

        public override string ToString()
        {
            return "CustomerID" + customerID + base.ToString();
        }


    }
}
