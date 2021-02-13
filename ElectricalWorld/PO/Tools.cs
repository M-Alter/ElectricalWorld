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
                Image = item.Image,
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
                Image = item.Image,
                Price = item.Price,
                Quantity = item.Quantity,
                Categories = from cat in item.Categories
                             select new BO.Category { CategoryID = cat.CategoryID, CategoryName = cat.CategoryName}
            };
        }


        public static Customer POCustomer(BO.Customer cust)
        {
            return new Customer
            {
                CustomerID = cust.CustomerID,
                Name = cust.Name,
                Address = cust.Address,
                PostCode = cust.PostCode,
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
                Address = cust.Address,
                PostCode = cust.PostCode,
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
                Customer = POCustomer(order.Customer),
                OrderTime = order.OrderTime,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid,
                Items = from item in order.Items
                        select POItem(item)
            };
        }


        public static BO.Order BOOrder(Order order)
        {
            return new BO.Order
            {
                OrderID = order.OrderID,
                Customer = BOCustomer(order.Customer),
                OrderTime = order.OrderTime,
                TotalPrice = order.TotalPrice,
                Paid = order.Paid,
                Items = from item in order.Items
                        select BOItem(item)
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