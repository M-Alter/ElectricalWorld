using Dapper;
using DLAPI;
using DLSQL;
using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

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

        public int AddItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into Item (ItemID, Brand, ModelNumber, Description, IsActive, Price) values (@ItemID, @Brand, @ModelNumber, @Description, @IsActive, @Price) ", item);
                return cnn.ExecuteScalar<int>("select seq FROM SQLITE_SEQUENCE WHERE name = \"Item\"");
            }
        }

        public void AddItemCategory(string itemID, string categoryID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into ItemCategory (ItemID, CategoryID) values (@ItemID, @CategoryID)");
            }
        }

        public int AddOrder(Order order)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute("insert into Orders (CustomerID, OrderTime, TotalPrice) values (@CustomerID, @OrderTime, @TotalPrice)", order);
                return cnn.ExecuteScalar<int>("select seq FROM SQLITE_SEQUENCE WHERE name = \"Orders\"");
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
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute($"UPDATE Customer SET CustomerID = \"{cust.CustomerID}\", Name = \"{cust.Name}\", Company = \"{cust.Company}\", Address = \"{cust.Address}\", PostCode = \"{cust.PostCode}\", City = \"{cust.City}\", Phone = \"{cust.Phone}\", Mobile = \"{cust.Mobile}\", Email = \"{cust.Email}\" WHERE CustomerID = \"{cust.CustomerID}\"");
            }
        }

        public void EditItem(Item item)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute($"UPDATE Item SET ItemID = \"{item.ItemID}\", Brand = \"{item.Brand}\", ModelNumber = \"{item.ModelNumber}\", Description = \"{item.Description}\", IsActive = \"{(item.IsActive? 1:0)}\", Price = \"{item.Price}\" WHERE ItemID = \"{item.ItemID}\"");
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<Category>("Select * From Category", new DynamicParameters());
            }
        }

        public double GetCostPrice(string itemID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                var prices = cnn.Query<StockItem>($"Select * From StockItem WHERE ItemID ={itemID} and Quantity > 0 ", new DynamicParameters());
                return (from p in prices
                        orderby p.Date
                        select p.Price).FirstOrDefault();
            }
        }

        public IEnumerable<Customer> GetCutomers()
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<Customer>("Select * From Customer", new DynamicParameters());
            }
        }

        public IEnumerable<string> GetItemCategories(string itemID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<string>($"Select CategoryID From ItemCategories where ItemID = {itemID} ", new DynamicParameters());
            }
        }

        
        public IEnumerable<Item> GetItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<Item>("Select * From Item where IsActive = 1 order by Brand asc", new DynamicParameters());
            }
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<OrderItem>("Select * From OrderItem", new DynamicParameters());
            }
        }

        public IEnumerable<Order> GetOrders(string where = "")
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<Order>("Select * From Orders" + where, new DynamicParameters());
            }
        }

        public double GetPaidAmount(string orderID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.ExecuteScalar<double>($"Select sum(AmountPaid) From OrderPayment where OrderID = \"{orderID}\"", new DynamicParameters());
            }
        }

        public Customer GetSingleCutomer(string customerID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                return cnn.Query<Customer>($"Select * From Customer Where CustomerID = {customerID}", new DynamicParameters()).FirstOrDefault();
            }
        }

        public int GetStockItem(string itemID)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                var stocks = cnn.Query<StockItem>($"Select * FROM StockItem where ItemID = {itemID}", new DynamicParameters());
                return stocks.Sum(s => s.Quantity);
            }
        }

        public void PayOrder(string orderID, double amount)
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute($"INSERT INTO OrderPayment (OrderID, OrderTime, AmountPaid) values ({orderID},{DateTime.Now.Ticks},{amount})");
            }
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
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {
                cnn.Execute($"UPDATE StockItem set Quantity = (Quantity - 1) Where ItemId = \"{itemID}\" AND Date = (SELECT min(Date) FROM StockItem)");
                cnn.Execute($"DELETE FROM StockItem WHERE Quantity = 0");
            }
        }
    }
}
