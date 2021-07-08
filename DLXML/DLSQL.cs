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

        public void EditItem(Item item)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Order> GetOrders()
        {
            using (IDbConnection cnn = new SQLiteConnection(SQLTools.LoadConnection()))
            {

                return cnn.Query<Order>("Select * From Orders", new DynamicParameters());
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

        public void PayOrder(string orderID, bool paid)
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
