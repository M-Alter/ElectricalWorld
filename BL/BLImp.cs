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
                CustomerID = dl.GetNewCustomerID().ToString(@"00000"),
                Name = cust.Name,
                Company = cust.Company,
                Address = cust.Address,
                PostCode = cust.PostCode,
                City = cust.City,
                Email = cust.Email,
                Phone = cust.Phone,
                Mobile = cust.Mobile
            });
        }

        public void AddItem(Item item, double costPrice)
        {
            //foreach (var doItem in dl.GetItems())
            //{
            //    if (doItem.ItemID == item.ItemID)
            //    {
            //        dl.EditStock(item.ItemID, item.Quantity - dl.GetStockItem(doItem.ItemID));
            //        return;
            //    }
            //}
            string itemID = dl.GetNewItemID().ToString(@"00000");
            dl.AddItem(new DO.Item
            {
                ItemID = itemID,
                Brand = item.Brand,
                Description = item.Description,
                ModelNumber = item.ModelNumber,
                Price = item.Price,
                IsActive = true

            });
            dl.AddStockItem(new DO.StockItem
            {
                ItemID = itemID,
                Quantity = item.Quantity,
                Date = DateTime.Now,
                Price = costPrice
            });
            if (item.Image != null)
                dl.AddImage(item.ItemID, item.Image);
        }

        public string AddOrder(Order order)
        {
            string orderID = dl.GetNewOrderID().ToString(@"000000");
            dl.AddOrder(new DO.Order
            {
                OrderID = orderID,
                CustomerID = order.Customer.CustomerID,
                OrderTime = DateTime.Now,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid
            });
            foreach (var item in order.Items)
            {
                dl.AddOrderItem(new DO.OrderItem
                {
                    OrderID = orderID,
                    ItemID = item.ItemID,
                    Price = item.Price,
                    Profit = item.Price - dl.GetCostPrice(item.ItemID)
                });
                dl.SubtractStock(item.ItemID, -1);
            }
            return orderID;
        }

        public void EditCustomer(Customer cust)
        {
            dl.EditCustomer(new DO.Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Company = cust.Company,
                Address = cust.Address,
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
                       Address = cust.Address,
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
                                    select (new Category { CategoryID = cat, CategoryName = dl.GetCategories().Where(ct => ct.CategoryID == cat).Select(ct => ct.CategoryName).FirstOrDefault() }),
                       Image = dl.GetItemImages().Where(im => im.ItemID == item.ItemID).Select(im => im.Image).FirstOrDefault()
                   }
                   where filter(temp)
                   select temp;
        }

        public IEnumerable<Order> GetOrders(Predicate<Order> filter)
        {
            return from order in dl.GetOrders()
                   let temp = new Order
                   {
                       OrderID = order.OrderID,
                       Customer = GetCutomers(id => id.CustomerID == order.CustomerID).FirstOrDefault(),
                       OrderTime = order.OrderTime,
                       TotalPrice = order.TotalPrice,
                       Paid = order.Paid,
                       Profit = dl.GetOrderItems().Where(it => it.OrderID == order.OrderID).Select(it => it.Profit).Sum(),
                       Items = from item in dl.GetOrderItems()
                               where item.OrderID == order.OrderID
                               select GetItems(it => it.ItemID == item.ItemID).FirstOrDefault()
                   }
                   where filter(temp)
                   orderby temp.OrderID
                   select (temp);
        }

        public void PayOrder(string orderID, bool paid)
        {
            dl.PayOrder(orderID, paid);
        }

        public void RemoveItem(string itemID)
        {
            dl.RemoveItem(itemID);
        }
    }
}
