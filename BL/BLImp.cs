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
                CustomerID = dl.GetNewCustomerID().ToString("00000"),
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Address = cust.Address,
                HouseNumber = cust.HouseNumber,
                PostCode = cust.PostCode,
                Email = cust.Email,
                Phone = cust.Phone,
                Mobile = cust.Mobile
            });
        }

        public void AddItem(Item item)
        {
            dl.AddItem(new DO.Item
            {
                ItemID = dl.GetNewItemID().ToString("00000"),
                Brand = item.Brand,
                Description = item.Description,
                ModelNumber = item.ModelNumber,
                Price = item.Price,
                Quantity = item.Quantity,
                IsActive = item.IsActive

            });
            if (item.Image != null)
                dl.AddImage(item.ItemID, item.Image);
        }

        public string AddOrder(Order order)
        {
            string OrderID = dl.GetNewOrderID().ToString("000000");
            dl.AddOrder(new DO.Order
            {
                OrderID = OrderID,
                CustomerID = order.Customer.CustomerID,
                OrderTime = DateTime.Now,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid
            });
            foreach (var item in order.Items)
            {
                dl.AddOrderItem(OrderID, item.ItemID, item.Price);
                dl.EditItem(new DO.Item
                {
                    ItemID = item.ItemID,
                    Brand = item.Brand,
                    Description = item.Description,
                    ModelNumber = item.ModelNumber,
                    Price = item.Price,
                    Quantity = item.Quantity - 1
                });
            }
            return OrderID;
        }

        public void EditCustomer(Customer cust)
        {
            dl.EditCustomer(new DO.Customer
            {
                CustomerID = cust.CustomerID,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Address = cust.Address,
                HouseNumber = cust.HouseNumber,
                PostCode = cust.PostCode,
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
                Quantity = item.Quantity
            });
        }

        public IEnumerable<Customer> GetCutomers(Predicate<Customer> filter)
        {
            return from cust in dl.GetCutomers()
                   let temp = new Customer
                   {
                       CustomerID = cust.CustomerID,
                       FirstName = cust.FirstName,
                       LastName = cust.LastName,
                       Address = cust.Address,
                       HouseNumber = cust.HouseNumber,
                       PostCode = cust.PostCode,
                       Email = cust.Email,
                       Phone = cust.Phone,
                       Mobile = cust.Mobile,
                       OrderIDs = dl.GetOrders().Where(order => order.CustomerID == cust.CustomerID).Select(order => order.OrderID)
                   }
                   where filter(temp)
                   orderby temp.FirstName
                   orderby temp.LastName
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
                       Quantity = item.Quantity,
                       Categories = from cat in dl.GetItemCategories(item.ItemID)
                                    select (new Category { CategoryID = cat, CategoryName = dl.GetCategories().Where(ct => ct.CategoryID == cat).Select(ct => ct.CategoryName).FirstOrDefault() }),
                       Image = dl.GetItemImages().Where(im => im.ItemID == item.ItemID).Select(im => im.Image).FirstOrDefault(),
                       IsActive = item.IsActive

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
                       Items = from item in dl.GetOrderItems()
                               where item.OrderID == order.OrderID
                               select GetItems(it => it.ItemID == item.ItemID).FirstOrDefault()
                   }
                   where filter(temp)
                   orderby temp.OrderID
                   select (temp);
        }

        public void RemoveItem(Item item)
        {
            dl.RemoveItem(new DO.Item
            {
                ItemID = item.ItemID,
                Brand = item.Brand,
                Description = item.Description,
                ModelNumber = item.ModelNumber,
                Price = item.Price,
                Quantity = item.Quantity
            });
        }
    }
}
