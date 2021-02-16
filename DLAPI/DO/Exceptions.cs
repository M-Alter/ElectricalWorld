using System;

namespace DO
{
    public class BadCustomerException : Exception
    {
        Exception exception;
        string customerID;
        public BadCustomerException(string customerID, string message, Exception e) : base(message, e)
        {
            this.customerID = customerID;
            this.exception = e;
        }

        public override string ToString()
        {
            return "CustomerID " + customerID + base.ToString();
        }
    }
    public class BadOrderException : Exception
    {
        Exception exception;
        string orderID;
        public BadOrderException(string orderID, string message, Exception e) : base(message, e)
        {
            this.orderID = orderID;
            this.exception = e;
        }

        public override string ToString()
        {
            return "OrderID " + orderID + base.ToString();
        }
    }

    public class BadItemException : Exception
    {
        Exception exception;
        string itemID;
        public BadItemException(string itemID, string message, Exception e) : base(message, e)
        {
            this.itemID = itemID;
            this.exception = e;
        }

        public override string ToString()
        {
            return "ItemID " + itemID + base.ToString();
        }
    }

    public class SaveFileException : Exception
    {
        Exception exception;
        string fileName;
        public SaveFileException(string fileName, string message, Exception e) : base(message, e)
        {
            this.fileName = fileName;
            this.exception = e;
        }

        public override string ToString()
        {
            return "File " + fileName + base.ToString();
        }
    }


    public class BadImageException : Exception
    {
        Exception exception;
        string fileName;
        public BadImageException(string fileName, string message, Exception e) : base(message, e)
        {
            this.fileName = fileName;
            this.exception = e;
        }

        public override string ToString()
        {
            return "Image " + fileName + base.ToString();
        }
    }

}
