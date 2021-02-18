using DO;
using System;
using System.IO;
using System.Xml.Linq;

namespace DLXML
{
    public class XMLTools
    {

        public static string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\xml\";

        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// loads the file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>the root element of the file</returns>
        public static XElement LoadFile(string path)
        {
            try
            {
                if (File.Exists(dir + path))
                {
                    return XElement.Load(dir + path);
                }
                else
                {
                    XElement rootElement = new XElement(path);
                    rootElement.Save(dir + path);
                    return rootElement;
                }
            }
            catch (FileLoadException e)
            {
                throw new FileLoadException(e.Message, e.FileName);
            }
            catch (Exception e)
            {
                throw new FileLoadException(e.Message, dir + path);
            }
        }

        /// <summary>
        /// saves the root element to the file
        /// </summary>
        /// <param name="rootElem"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool SaveFile(XElement rootElem, string path)
        {
            try
            {
                rootElem.Save(dir + path);
                return true;
            }
            catch (Exception e)
            {
                throw new SaveFileException(dir + path, "Error while loading file", e);
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }


        internal static Customer CreateCustomerInstance(XElement cust)
        {

            try
            {
                return new Customer
                {
                    CustomerID = cust.Element("CustomerID").Value,
                    Name = cust.Element("Name").Value,
                    Company = cust.Element("Company").Value,
                    Address = cust.Element("Address").Value,
                    PostCode = cust.Element("PostCode").Value,
                    City = cust.Element("City").Value,
                    Email = cust.Element("Email").Value,
                    Phone = cust.Element("Phone").Value,
                    Mobile = cust.Element("Mobile").Value
                };
            }
            catch (Exception e)
            {
                throw new BadCustomerException(cust.Element("CustomerID").Value != "" ? cust.Element("CustomerID").Value : "Unknown Customer ID", "failed to load the customer", e);
            }
        }


        internal static ItemImage CreateImageInstance(XElement image)
        {
            FileInfo fi = new FileInfo(image.Element("Image").Value);

            return new ItemImage
            {
                ItemID = image.Element("ItemID").Value,
                Image = fi.FullName
            };

        }


        internal static Item CreateItemInstance(XElement item)
        {
            try
            {
                return new Item
                {
                    ItemID = item.Element("ItemID").Value,
                    Brand = item.Element("Brand").Value,
                    ModelNumber = item.Element("ModelNumber").Value,
                    Description = item.Element("Description").Value,
                    Price = double.Parse(item.Element("Price").Value),
                    IsActive = bool.Parse(item.Element("IsActive").Value)
                };
            }
            catch (Exception e)
            {
                throw new BadItemException(item.Element("ItemID").Value != "" ? item.Element("ItemID").Value : "Unknown Item ID", "failed to load the item", e);
            }
        }


        internal static Order CreateOrderInstance(XElement order)
        {
            try
            {
                return new Order
                {
                    CustomerID = order.Element("CustomerID").Value,
                    OrderID = order.Element("OrderID").Value,
                    OrderTime = DateTime.Parse(order.Element("OrderTime").Value),
                    TotalPrice = double.Parse(order.Element("TotalPrice").Value),
                    Paid = bool.Parse(order.Element("Paid").Value)
                };
            }
            catch (Exception e)
            {
                throw new BadOrderException(order.Element("OrderID").Value != "" ? order.Element("OrderID").Value : "Unknown order ID", "failed to load the order", e);
            }
        }


        internal static OrderItem CreateOrderItemInstance(XElement orderItem)
        {
            return new OrderItem
            {
                ItemID = orderItem.Element("ItemID").Value,
                OrderID = orderItem.Element("OrderID").Value,
                Price = double.Parse(orderItem.Element("Price").Value),
                Profit = double.Parse(orderItem.Element("Profit").Value)
            };
        }


        internal static Category CreateCategoryInstance(XElement category)
        {
            return new Category
            {
                CategoryID = category.Element("CategoryID").Value,
                CategoryName = category.Element("CategoryName").Value
            };
        }


        internal static StockItem CreateStockItemInstance(XElement item)
        {
            return new StockItem
            {
               ItemID = item.Element("ItemID").Value,
               Quantity = int.Parse(item.Element("Quantity").Value),
               Price = double.Parse(item.Element("Price").Value),
               Date = DateTime.Parse(item.Element("Date").Value)
            };
        }



    }
}
