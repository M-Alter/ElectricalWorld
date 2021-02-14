﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using DO;

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
            catch (Exception)
            {
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
            return false;
        }


        internal static Customer CreateCustomerInstance(XElement cust)
        {
            return new Customer
            {
                CustomerID = cust.Element("CustomerID").Value,
                Name = cust.Element("FirstName").Value,
                Address = cust.Element("Address").Value,
                PostCode = cust.Element("PostCode").Value,
                Email = cust.Element("Email").Value,
                Phone = cust.Element("Phone").Value,
                Mobile = cust.Element("Mobile").Value
            };
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


        internal static Order CreateOrderInstance(XElement order)
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


        internal static OrderItem CreateOrderItemInstance(XElement orderItem)
        {
            return new OrderItem
            {
                ItemID = orderItem.Element("ItemID").Value,
                OrderID = orderItem.Element("OrderID").Value,
                Price = double.Parse(orderItem.Element("Price").Value)
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





    }
}
