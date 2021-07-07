using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DLAPI;
using DLSQL;
using DO;

namespace DL
{
    class DLSQL : IDL
    {


        #region Singleton
        private static readonly DLSQL instance = new DLSQL();
        static DLSQL() { }
        DLSQL() { } // default => private
        public static DLSQL Instance { get => instance; }// The public Instance property to use

        #endregion


        public void AddCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(Customer cust)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into Customer (Name, Company, Address, PostCode, City, Phone, Mobile , Email) values (@Name, @Company, @Address, @PostCode, @City, @Phone, @Mobile , @Email)", cust);
            }
        }

        public void AddImage(string itemID, string image)
        {
            throw new NotImplementedException();
        }

        public void AddItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into Item (ItemID, Brand, ModelNumber, Description, IsActive, Price) values (@ItemID, @Brand, @ModelNumber, @Description, @IsActive, @Price) ", item);
            }
        }

        public void AddItemCategory(string itemID, string categoryID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into ItemCategory (ItemID, CategoryID) values (@ItemID, @CategoryID)");
            }
        }

        public void AddOrder(Order order)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into Order (OrderID, CustomerID, OrderTime, TotalPrice, Paid) values (@OrderID, @CustomerID, @OrderTime, @TotalPrice, @Paid)", order);
            }
        }

        public void AddOrderItem(OrderItem item)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into OrderItem (OrderID, ItemID, Price, Profit) values (@OrderID, @ItemID, @Price, @Profit)", item);
            }
        }

        public void AddStockItem(StockItem item)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into StockItem (ItemID, Quantity, Date, Price) values (@ItemID, @Quantity, @Date, @Price)", item);
            }
        }

        public void EditCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public void EditImage(string itemID, string image)
        {
            throw new NotImplementedException();
        }

        public void EditItem(Item item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public double GetCostPrice(string itemID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCutomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetItemCategories(string itemID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemImage> GetItemImages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public int GetNewCategoryID()
        {
            throw new NotImplementedException();
        }

        public int GetNewCustomerID()
        {
            throw new NotImplementedException();
        }

        public int GetNewItemID()
        {
            throw new NotImplementedException();
        }

        public int GetNewOrderID()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public int GetStockItem(string itemID)
        {
            throw new NotImplementedException();
        }

        public void PayOrder(string orderID, bool paid)
        {
            throw new NotImplementedException();
        }

        public void RemoveImage(string itemID)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string itemID)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemCategory(string itemID, string categoryID)
        {
            throw new NotImplementedException();
        }

        public void SubtractStock(string itemID, int qnt)
        {
            throw new NotImplementedException();
        }
    }
}
