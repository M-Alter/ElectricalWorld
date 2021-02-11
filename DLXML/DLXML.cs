﻿using DLAPI;
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
                File.Copy(image, imagesFolderPath + itemID);
                XElement rootElem = XMLTools.LoadFile(imagesFilePath);

                rootElem.Add(new XElement("Image",
                    new XElement("ItemID", itemID),
                    new XElement("Image", imagesFolderPath + itemID)
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

        //========================================================================
        //need to make sure that temp is actually a reference
        //========================================================================
        public void EditCustomer(Customer cust)
        {
            XElement rootElem = XMLTools.LoadFile(customersFilePath);

            XElement customerElem = (from elem in rootElem.Elements("Customer")
                             let cur = XMLTools.CreateCustomerInstance(elem)
                             where cur.CustomerID == cust.CustomerID
                             select elem).FirstOrDefault();

            customerElem.Remove();

            customerElem = new XElement("Customer",
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
        //========================================================================

        public void EditImage(int itemID, string image)
        {
            //XElement rootElem = XMLTools.LoadFile(imagesFilePath);

            //XElement imageElem = (from im in rootElem.Elements()
            //                      let cur = XMLTools.CreateImageInstance(im)
            //                      where cur.ItemID == itemID
            //                      select im).FirstOrDefault();

            File.Copy(image, imagesFolderPath + itemID, true);

        }

        public void EditItem(Item item)
        {
            XElement rootElem = XMLTools.LoadFile(itemsFilePath);

            XElement itemElem = (from it in rootElem.Elements()
                                 let temp = XMLTools.CreateItemInstance(it)
                                 where temp.ItemID == item.ItemID
                                 select it).FirstOrDefault();

            itemElem.Remove();

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

        public IEnumerable<Category> GetCategories()
        {
            XElement rootElem = XMLTools.LoadFile(categoriesFilePath);

            return from cat in rootElem.Elements()
                   let temp = XMLTools.CreateCategoryInstance(cat)
                   select temp;
        }

        public IEnumerable<Customer> GetCutomers()
        {
            XElement rootElem = XMLTools.LoadFile(customersFilePath);

            return from cus in rootElem.Elements()
                   let temp = XMLTools.CreateCustomerInstance(cus)
                   select temp;
        }

        public IEnumerable<int> GetItemCategories(int itemID)
        {
            XElement rootElem = XMLTools.LoadFile(itemCategoriesFilePath);

            return from cat in rootElem.Elements()
                   where int.Parse(cat.Element("ItemID").Value) == itemID
                   select int.Parse(cat.Element("CategoryID").Value);
        }

        public IEnumerable<ItemImage> GetItemImages()
        {
            XElement rootElem = XMLTools.LoadFile(imagesFilePath);

            return from im in rootElem.Elements()
                   let temp = XMLTools.CreateImageInstance(im)
                   select temp;
        }

        public IEnumerable<Item> GetItems()
        {
            XElement rootElem = XMLTools.LoadFile(itemsFilePath);

            return from item in rootElem.Elements()
                   let temp = XMLTools.CreateItemInstance(item)
                   select temp;
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            XElement rootElem = XMLTools.LoadFile(orderItemsFilePath);

            return from item in rootElem.Elements()
                   let temp = XMLTools.CreateOrderItemInstance(item)
                   select temp;
        }

        public IEnumerable<Order> GetOrders()
        {
            XElement rootElem = XMLTools.LoadFile(ordersFilePath);

            return from order in rootElem.Elements()
                   let temp = XMLTools.CreateOrderInstance(order)
                   select temp;
        }

        public void RemoveImage(int itemID)
        {
            XElement rootElem = XMLTools.LoadFile(imagesFilePath);

            XElement imageElem = (from im in rootElem.Elements()
                                  let cur = XMLTools.CreateImageInstance(im)
                                  where cur.ItemID == itemID
                                  select im).FirstOrDefault();

            imageElem.Remove();

            XMLTools.SaveFile(rootElem, imagesFilePath);

            File.Delete(imagesFolderPath + itemID);

        }

        public void RemoveItem(Item item)
        {
            XElement rootElem = XMLTools.LoadFile(itemsFilePath);

            XElement itemElem = (from it in rootElem.Elements()
                                 let temp = XMLTools.CreateItemInstance(it)
                                 where temp.ItemID == item.ItemID
                                 select it).FirstOrDefault();
            if (itemElem != null)
            {
                itemElem.Element("IsActive").SetValue(false);
            }

            XMLTools.SaveFile(rootElem, itemsFilePath);
        }

        public void RemoveItemCategory(int itemID, int categoryID)
        {
            XElement rootElem = XMLTools.LoadFile(itemCategoriesFilePath);

            XElement itemCatElem = (from cat in rootElem.Elements()
                                    where int.Parse(cat.Element("ItemID").Value) == itemID && int.Parse(cat.Element("CategoryID").Value)  == categoryID
                                    select cat).FirstOrDefault();

            itemCatElem.Remove();

            XMLTools.SaveFile(rootElem, itemCategoriesFilePath);
        }
        #endregion


    }
}
