using BO;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();


        public void AddCustomer(Customer cust)
        {
            dl.AddCustomer(new DO.Customer
            {
                Name = cust.Name ?? "",
                Company = cust.Company ?? "",
                Address1 = cust.Address1 ?? "",
                Address2 = cust.Address2 ?? "",
                PostCode = cust.PostCode ?? "",
                City = cust.City ?? "",
                Email = cust.Email ?? "",
                Phone = cust.Phone ?? "",
                Mobile = cust.Mobile ?? ""
            });
        }

        public void AddItem(Item item, double costPrice)
        {
            int itemID = dl.AddItem(new DO.Item
            {
                Brand = item.Brand ?? "",
                Description = item.Description ?? "",
                ModelNumber = item.ModelNumber ?? "",
                Price = item.Price,
                IsActive = true

            });
            dl.AddStockItem(new DO.StockItem
            {
                ItemID = itemID.ToString(),
                Quantity = item.Quantity,
                Date = DateTime.Now,
                Price = costPrice
            });
        }

        public string AddOrder(Order order)
        {

            int orderID = dl.AddOrder(new DO.Order
            {
                CustomerID = order.Customer.CustomerID,
                OrderTime = DateTime.Now,
                TotalPrice = order.TotalPrice
            });
            foreach (var item in order.Items)
            {
                dl.AddOrderItem(new DO.OrderItem
                {
                    OrderID = orderID.ToString(),
                    ItemID = item.ItemID,
                    Price = item.Price,
                    Profit = item.Price - dl.GetCostPrice(item.ItemID)
                });
                dl.SubtractStock(item.ItemID, -1);
            }
            if (order.Paid > 0)
                dl.PayOrder(orderID.ToString(), order.Paid);
            return orderID.ToString(@"000000");

        }

        public void AddStock(Item item, double costPrice)
        {
            dl.AddStockItem(new DO.StockItem
            {
                Date = DateTime.Now,
                ItemID = item.ItemID,
                Quantity = item.Quantity,
                Price = costPrice
            });
        }

        public void EditCustomer(Customer cust)
        {
            dl.EditCustomer(new DO.Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Company = cust.Company,
                Address1 = cust.Address1,
                Address2 = cust.Address2,
                PostCode = cust.PostCode,
                City = cust.City,
                Email = cust.Email,
                Phone = cust.Phone,
                Mobile = cust.Mobile
            });
        }

        public void EditItem(Item item)
        {
            dl.EditItem(new DO.Item
            {
                ItemID = item.ItemID,
                Brand = item.Brand,
                Description = item.Description,
                ModelNumber = item.ModelNumber,
                Price = item.Price,
                IsActive = true
            });
            //dl.EditStock(item.ItemID, -dl.GetStockItem(item.ItemID) + item.Quantity);
        }

        public IEnumerable<Customer> GetCutomers(Predicate<Customer> filter)
        {
            return from cust in dl.GetCutomers()
                   let temp = new Customer
                   {
                       CustomerID = cust.CustomerID,
                       Name = cust.Name,
                       Company = cust.Company,
                       Address1 = cust.Address1,
                       Address2 = cust.Address2,
                       PostCode = cust.PostCode,
                       City = cust.City,
                       Email = cust.Email,
                       Phone = cust.Phone,
                       Mobile = cust.Mobile,
                       OrderIDs = dl.GetOrders().Where(order => order.CustomerID == cust.CustomerID).Select(order => order.OrderID)
                   }
                   where filter(temp)
                   orderby temp.Name
                   select temp;
        }

        public IEnumerable<Item> GetItems(Predicate<Item> filter)
        {
            return from item in dl.GetItems()
                   let temp = new Item
                   {
                       ItemID = item.ItemID,
                       Brand = item.Brand,
                       Description = item.Description,
                       ModelNumber = item.ModelNumber,
                       Price = item.Price,
                       Quantity = dl.GetStockItem(item.ItemID),
                       Categories = from cat in dl.GetItemCategories(item.ItemID)
                                    select (new Category { CategoryID = cat, CategoryName = dl.GetCategories().Where(ct => ct.CategoryID == cat).Select(ct => ct.CategoryName).FirstOrDefault() })
                   }
                   where filter(temp)
                   select temp;
        }

        public IEnumerable<Order> GetOrders(Predicate<Order> filter, string whereSql = "")
        {
            return from order in dl.GetOrders(whereSql)
                   let temp = new Order
                   {
                       OrderID = order.OrderID,
                       Customer = GetSingleCustomer(order.CustomerID),
                       OrderTime = order.OrderTime,
                       TotalPrice = order.TotalPrice,
                       Paid = dl.GetPaidAmount(order.OrderID),
                       Profit = dl.GetOrderItems().Where(it => it.OrderID == order.OrderID).Select(it => it.Profit).Sum(),
                       Items = from item in dl.GetOrderItems()
                               where item.OrderID == order.OrderID
                               select GetItems(it => it.ItemID == item.ItemID).FirstOrDefault()
                   }
                   where filter(temp)
                   orderby temp.OrderID
                   select (temp);
        }

        public Customer GetSingleCustomer(string customerID)
        {
            var cust = dl.GetSingleCutomer(customerID);
            return new Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Company = cust.Company,
                Address1 = cust.Address1,
                Address2 = cust.Address2,
                PostCode = cust.PostCode,
                City = cust.City,
                Email = cust.Email,
                Phone = cust.Phone,
                Mobile = cust.Mobile,
                OrderIDs = dl.GetOrders().Where(order => order.CustomerID == cust.CustomerID).Select(order => order.OrderID)
            };
        }

        public void PayOrder(string orderID, double amount)
        {
            dl.PayOrder(orderID, amount);
        }

        public void RemoveItem(string itemID)
        {
            dl.RemoveItem(itemID);
        }
    }
}
