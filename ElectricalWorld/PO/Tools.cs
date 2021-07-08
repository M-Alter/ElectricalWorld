using System.Linq;


namespace PO
{
    public class Tools
    {
        public static Item POItem(BO.Item item)
        {
            return new Item
            {
                ItemID = item.ItemID,
                Brand = item.Brand,
                ModelNumber = item.ModelNumber,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                Categories = from cat in item.Categories
                             select POCategory(cat)
            };
        }


        public static BO.Item BOItem(Item item)
        {
            return new BO.Item
            {
                ItemID = item.ItemID,
                Brand = item.Brand,
                ModelNumber = item.ModelNumber,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                Categories = from cat in item.Categories
                             select new BO.Category { CategoryID = cat.CategoryID, CategoryName = cat.CategoryName }
            };
        }


        public static Customer POCustomer(BO.Customer cust)
        {
            return new Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Company = cust.Company,
                Address = cust.Address,
                PostCode = cust.PostCode,
                City = cust.City,
                Phone = cust.Phone,
                Mobile = cust.Mobile,
                Email = cust.Email,
                OrderIDs = cust.OrderIDs
            };
        }


        public static BO.Customer BOCustomer(Customer cust)
        {
            return new BO.Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Company = cust.Company,
                Address = cust.Address,
                PostCode = cust.PostCode,
                City = cust.City,
                Phone = cust.Phone,
                Mobile = cust.Mobile,
                Email = cust.Email,
                OrderIDs = cust.OrderIDs
            };
        }

        public static Order POOrder(BO.Order order)
        {
            return new Order
            {
                OrderID = order.OrderID,
                //CustomerID = POCustomer(order.CustomerID),
                OrderTime = order.OrderTime,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid,
                Profit = order.Profit,
                Items = from item in order.Items
                        select POItem(item)
            };
        }


        public static BO.Order BOOrder(Order order)
        {
            return new BO.Order
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderTime = order.OrderTime,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid,
                Profit = order.Profit,
                Items = from item in order.Items
                        where item is Item
                        select BOItem(item as Item)
            };
        }


        public static Category POCategory(BO.Category cat)
        {
            return new Category
            {
                CategoryID = cat.CategoryID,
                CategoryName = cat.CategoryName
            };
        }

    }

}