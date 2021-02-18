using DLAPI;
using DLXML;
using DO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


namespace DL
{
    class DLXML : IDL
    {
        #region Singleton
        private static readonly DLXML instance = new DLXML();
        static DLXML() { }
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use

        #endregion

        #region Files
        string imagesFolderPath = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + @"\ElectricalWorld\images\";
        string categoriesFilePath = @"categories.xml";
        string itemCategoriesFilePath = @"itemCategories.xml";
        string customersFilePath = @"customers.xml";
        string ordersFilePath = @"orders.xml";
        string imagesFilePath = @"images.xml";
        string itemsFilePath = @"items.xml";
        string orderItemsFilePath = @"orderItems.xml";
        string stockItemsFilePath = @"stockItems.xml";
        string idNumberGenertatorFile = @"IdFile.xml";




        #endregion



        //======================================================================================================
        //need to add the category number
        //======================================================================================================
        public void AddCategory(string category)
        {
            XElement rootElem = XMLTools.LoadFile(categoriesFilePath);

            XElement categoryElem = new XElement("Category",
                new XElement("CategoryID", GetNewCategoryID()),
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
                new XElement("Name", cust.Name),
                new XElement("Company", cust.Company),
                new XElement("Address", cust.Address),
                new XElement("PostCode", cust.PostCode),
                new XElement("City", cust.City),
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
        public void AddImage(string itemID, string image)
        {
            try
            {
                if (!Directory.Exists(imagesFolderPath)) Directory.CreateDirectory(imagesFolderPath);

                //var file = File.Create(imagesFolderPath + itemID + @".bmp");
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(image);
                bitmap.EndInit();
                Bitmap bitmap1 = new Bitmap(image);
                bitmap1.Save(imagesFolderPath + itemID + @".png", ImageFormat.Png);

                XElement rootElem = XMLTools.LoadFile(imagesFilePath);

                rootElem.Add(new XElement("Image",
                    new XElement("ItemID", itemID),
                    new XElement("Image", imagesFolderPath + itemID + @".png")
                    )
                    );

                XMLTools.SaveFile(rootElem, imagesFilePath);
            }
            catch (Exception e)
            {
                throw new BadImageException(itemID, " Error while trying to add image ", e);
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
                new XElement("Price", item.Price)
                )
                );

            XMLTools.SaveFile(rootElem, itemsFilePath);
        }

        public void AddItemCategory(string itemID, string categoryID)
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

        public void AddOrderItem(OrderItem item)
        {
            XElement rootElem = XMLTools.LoadFile(orderItemsFilePath);

            rootElem.Add(new XElement("OrderItem",
                new XElement("OrderID", item.OrderID),
                new XElement("ItemID", item.ItemID),
                new XElement("Price", item.Price),
                new XElement("Profit", item.Profit)
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
                new XElement("Name", cust.Name),
                new XElement("Company", cust.Company),
                new XElement("Address", cust.Address),
                new XElement("PostCode", cust.PostCode),
                new XElement("City", cust.City),
                new XElement("Phone", cust.Phone),
                new XElement("Mobile", cust.Mobile),
                new XElement("Email", cust.Email)
                );

            rootElem.Add(customerElem);

            XMLTools.SaveFile(rootElem, customersFilePath);
        }
        //========================================================================

        public void EditImage(string itemID, string image)
        {
            try
            {
                if (!Directory.Exists(imagesFolderPath)) Directory.CreateDirectory(imagesFolderPath);

                XElement rootElem = XMLTools.LoadFile(imagesFilePath);

                XElement imageElem = (from im in rootElem.Elements()
                                      let cur = XMLTools.CreateImageInstance(im)
                                      where cur.ItemID == itemID
                                      select im).FirstOrDefault();

                File.Copy(image, imagesFolderPath + itemID, true);
            }
            catch (Exception e)
            {
                throw new BadImageException(itemID, " Error while trying to edit image ", e);
            }

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
                new XElement("Price", item.Price)
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

        //==============================================================
        //create an exception
        //==============================================================
        public IEnumerable<string> GetItemCategories(string itemID)
        {
            XElement rootElem = XMLTools.LoadFile(itemCategoriesFilePath);

            return from cat in rootElem.Elements()
                   where cat.Element("ItemID").Value == itemID
                   select cat.Element("CategoryID").Value;
        }
        //===================================================================
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
                   where temp.IsActive == true
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

        public void RemoveImage(string itemID)
        {
            try
            {
                if (!Directory.Exists(imagesFolderPath)) Directory.CreateDirectory(imagesFolderPath);

                XElement rootElem = XMLTools.LoadFile(imagesFilePath);

                XElement imageElem = (from im in rootElem.Elements()
                                      let cur = XMLTools.CreateImageInstance(im)
                                      where cur.ItemID == itemID
                                      select im).FirstOrDefault();

                imageElem.Remove();

                XMLTools.SaveFile(rootElem, imagesFilePath);

                File.Delete(imagesFolderPath + itemID);
            }
            catch (Exception e)
            {
                throw new BadImageException(itemID, " Error while trying to remove image ", e);
            }
        }

        public void RemoveItem(string itemID)
        {
            XElement rootElem = XMLTools.LoadFile(itemsFilePath);

            XElement itemElem = (from it in rootElem.Elements()
                                 let temp = XMLTools.CreateItemInstance(it)
                                 where temp.ItemID == itemID
                                 select it).FirstOrDefault();
            if (itemElem != null)
            {
                itemElem.Element("IsActive").SetValue(false);
            }

            XMLTools.SaveFile(rootElem, itemsFilePath);
        }

        public void RemoveItemCategory(string itemID, string categoryID)
        {
            XElement rootElem = XMLTools.LoadFile(itemCategoriesFilePath);

            XElement itemCatElem = (from cat in rootElem.Elements()
                                    where cat.Element("ItemID").Value == itemID && cat.Element("CategoryID").Value == categoryID
                                    select cat).FirstOrDefault();

            itemCatElem.Remove();

            XMLTools.SaveFile(rootElem, itemCategoriesFilePath);
        }

        public int GetNewCustomerID()
        {
            XElement rootElem = XMLTools.LoadFile(idNumberGenertatorFile);

            if (rootElem.Element("Customer") == null)
                rootElem.Add(new XElement("Customer", 100));

            int result = int.Parse(rootElem.Element("Customer").Value);
            rootElem.Element("Customer").SetValue(result + 1);

            XMLTools.SaveFile(rootElem, idNumberGenertatorFile);

            return result;
        }

        public int GetNewCategoryID()
        {
            XElement rootElem = XMLTools.LoadFile(idNumberGenertatorFile);

            if (rootElem.Element("Category") == null)
                rootElem.Add(new XElement("Category", 100));

            int result = int.Parse(rootElem.Element("Category").Value);
            rootElem.Element("Category").SetValue(result + 1);

            XMLTools.SaveFile(rootElem, idNumberGenertatorFile);

            return result;
        }

        public int GetNewItemID()
        {
            XElement rootElem = XMLTools.LoadFile(idNumberGenertatorFile);

            if (rootElem.Element("Item") == null)
                rootElem.Add(new XElement("Item", 100));

            int result = int.Parse(rootElem.Element("Item").Value);
            rootElem.Element("Item").SetValue(result + 1);

            XMLTools.SaveFile(rootElem, idNumberGenertatorFile);

            return result;
        }

        public int GetNewOrderID()
        {
            XElement rootElem = XMLTools.LoadFile(idNumberGenertatorFile);

            if (rootElem.Element("Order") == null)
                rootElem.Add(new XElement("Order", 100));

            int result = int.Parse(rootElem.Element("Order").Value);
            rootElem.Element("Order").SetValue(result + 1);

            XMLTools.SaveFile(rootElem, idNumberGenertatorFile);

            return result;
        }

        public void AddStockItem(StockItem item)
        {
            XElement rootElem = XMLTools.LoadFile(stockItemsFilePath);

            rootElem.Add(new XElement("StckItem",
                new XElement("ItemID", item.ItemID),
                new XElement("Quantity", item.Quantity),
                new XElement("Date", item.Date),
                new XElement("Price", item.Price)
                )
                );

            XMLTools.SaveFile(rootElem, stockItemsFilePath);

        }

        public void SubtractStock(string itemID, int qnt)
        {
            XElement rootElem = XMLTools.LoadFile(stockItemsFilePath);

            var stockItemElem = (from item in rootElem.Elements()
                                 let stockItem = XMLTools.CreateStockItemInstance(item)
                                 where stockItem.ItemID == itemID
                                 where stockItem.Quantity > 0
                                 orderby stockItem.Date
                                 select item).FirstOrDefault();

            stockItemElem.Element("Quantity").SetValue(int.Parse(stockItemElem.Element("Quantity").Value) + qnt);

            if (stockItemElem.Element("Quantity").Value == "0")
            {
                stockItemElem.Remove();
            }

            XMLTools.SaveFile(rootElem, stockItemsFilePath);
        }

        public int GetStockItem(string itemID)
        {
            XElement rootElem = XMLTools.LoadFile(stockItemsFilePath);

            return (from item in rootElem.Elements()
                    let stockItem = XMLTools.CreateStockItemInstance(item)
                    where stockItem.ItemID == itemID
                    orderby stockItem.Date
                    select stockItem.Quantity).DefaultIfEmpty(0).Sum();
        }

        public void PayOrder(string orderID, bool paid)
        {
            XElement rootElem = XMLTools.LoadFile(ordersFilePath);

            var orderElem = (from ord in rootElem.Elements()
                             let order = XMLTools.CreateOrderInstance(ord)
                             where order.OrderID == orderID
                             select ord).FirstOrDefault();

            orderElem.SetElementValue("Paid", paid);

            XMLTools.SaveFile(rootElem, ordersFilePath);
        }

        public double GetCostPrice(string itemID)
        {
            XElement rootElem = XMLTools.LoadFile(stockItemsFilePath);

            var stockItemElem = (from item in rootElem.Elements()
                                 let stockItem = XMLTools.CreateStockItemInstance(item)
                                 where stockItem.ItemID == itemID
                                 where stockItem.Quantity > 0
                                 orderby stockItem.Date
                                 select stockItem).FirstOrDefault();

            return stockItemElem.Price;
            
        }
    }
}
