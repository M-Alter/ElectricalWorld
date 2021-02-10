using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLXML;
using System.IO;

namespace DL
{
    class DLXML: IDL
    {
        #region Singleton
        private static readonly DLXML instance = new DLXML();
        static DLXML() { }
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use

        #region Files
        string imagesFolderPath = @"images/";
        string categoriesFilePath = @"categories.xml";
        string itemCategoriesFilePath = @"itemCategories.xml";
        string customersFilePath = @"customers.xml";
        string ordersFilePath = @"orders.xml";
        string imagesFilePath = @"images.xml";
        string itemsFilePath = @"items.xml";
        string orderItemsFilePath = @"OrderItems.xml";




        #endregion



        //======================================================================================================
        //need to add the category number
        //======================================================================================================
        public void AddCategory(string category)
        {
            XElement rootElem = XMLTools.LoadFile(categoriesFilePath);

            XElement categoryElem = new XElement("Category",
                new XElement("CategoryID", 0),
                new XElement("CategoryName", category)
                );
            rootElem.Add(categoryElem);

            XMLTools.SaveFile(rootElem, categoriesFilePath);
        }
        //=======================================================================================================

        public void AddCustomer(Customer cust)
        {
            XElement rootElem = XMLTools.LoadFile(customersFilePath);

            XElement customerElem = new XElement("Customer",
                new XElement("CustomerID", cust.CustomerID),
                new XElement("FirstName", cust.FirstName),
                new XElement("LastName", cust.LastName),
                new XElement("HouseNumber", cust.HouseNumber),
                new XElement("Address", cust.Address),
                new XElement("PostCode", cust.PostCode),
                new XElement("Phone", cust.Phone),
                new XElement("Mobile", cust.Mobile),
                new XElement("Email", cust.Email)
                );

            rootElem.Add(customerElem);

            XMLTools.SaveFile(rootElem, customersFilePath);
        }


        //=========================================================================================
        //add catch
        //=========================================================================================
        public void AddImage(int itemID, string image)
        {
           try
            {
                File.Copy(image, imagesFolderPath + image);
                XElement rootElem = XMLTools.LoadFile(imagesFilePath);

                rootElem.Add(new XElement("Image",
                    new XElement("ItemID", itemID),
                    new XElement("Image", imagesFolderPath + image)
                    )
                    );

                XMLTools.SaveFile(rootElem, imagesFilePath);
            }
            catch
            {

            }
        }
        //=========================================================================================

        public void AddItem(Item item)
        {
            XElement rootElem = XMLTools.LoadFile(itemsFilePath);

            rootElem.Add(new XElement("Item",
                new XElement("ItemID", item.ItemID),
                new XElement("Brand", item.Brand),
                new XElement("ModelNumber", item.ModelNumber),
                new XElement("Description", item.Description),
                new XElement("IsActive", item.IsActive),
                new XElement("Price", item.Price),
                new XElement("Quantity", item.Quantity)
                )
                );

            XMLTools.SaveFile(rootElem, itemsFilePath);
        }

        public void AddItemCategory(int itemID, int categoryID)
        {
            XElement rootElem = XMLTools.LoadFile(itemCategoriesFilePath);

            rootElem.Add(new XElement("ItemCategory",
                new XElement("ItemID", itemID),
                new XElement("CategoryID", categoryID)
                )
                );

            XMLTools.SaveFile(rootElem, itemCategoriesFilePath);      
        }

        public void AddOrder(Order order)
        {
            XElement rootElem = XMLTools.LoadFile(ordersFilePath);

            rootElem.Add(new XElement("Order",
                new XElement("OrderID", order.OrderID),
                new XElement("CustomerID", order.CustomerID),
                new XElement("OrderTime", order.OrderTime),
                new XElement("TotalPrice", order.TotalPrice),
                new XElement("Paid", order.Paid)
                )
                );

            XMLTools.SaveFile(rootElem, ordersFilePath);
        }

        public void AddOrderItem(int orderID, int itemID, double price)
        {
            XElement rootElem = XMLTools.LoadFile(orderItemsFilePath);

            rootElem.Add(new XElement("OrderItem",
                new XElement("OrderID", orderID),
                new XElement("ItemID", itemID),
                new XElement("Price", price)
                )
                );

            XMLTools.SaveFile(rootElem, orderItemsFilePath);
        }

        public void EditCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public void EditImage(int itemID, string image)
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

        public IEnumerable<Customer> GetCutomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetItemCategories(int itemID)
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

        public IEnumerable<OrderItem> GetOrderItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void RemoveImage(int itemID, string image)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemCategory(int itemID, int categoryID)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
