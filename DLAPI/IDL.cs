﻿using DO;
using System.Collections.Generic;

namespace DLAPI
{
    public interface IDL
    {
        /// <summary>
        /// add a new customer
        /// </summary>
        /// <param name="cust"></param>
        void AddCustomer(Customer cust);

        /// <summary>
        /// get customers
        /// </summary>
        /// <returns>a collction of all the customers</returns>
        IEnumerable<Customer> GetCutomers();

        /// <summary>
        /// update details of the customer
        /// </summary>
        /// <param name="cust">update the customer to cust according to the cust CustomerID</param>
        void EditCustomer(Customer cust);

        /// <summary>
        /// add a new item
        /// </summary>
        /// <param name="item">item to add</param>
        void AddItem(Item item);

        /// <summary>
        /// edit details of an item
        /// </summary>
        /// <param name="item">item to update</param>
        void EditItem(Item item);

        /// <summary>
        /// get all the items 
        /// </summary>
        /// <returns>a collection of the items</returns>
        IEnumerable<Item> GetItems();

        /// <summary>
        /// marks an item as InActive
        /// </summary>
        /// <param name="item">item to mark</param>
        void RemoveItem(string itemID);

        /// <summary>
        /// add an image to an item
        /// </summary>
        /// <param name="itemID">of the item </param>
        /// <param name="image">string of the image</param>
        void AddImage(string itemID, string image);

        /// <summary>
        /// Edit an image to an item
        /// </summary>
        /// <param name="itemID">ID of the item </param>
        /// <param name="image">string of the image</param>
        void EditImage(string itemID, string image);

        /// <summary>
        /// Remove an image to an item
        /// </summary>
        /// <param name="itemID">ID of the item </param>
        void RemoveImage(string itemID);

        /// <summary>
        /// get item images 
        /// </summary>
        /// <returns>a collection of all the images</returns>
        IEnumerable<ItemImage> GetItemImages();

        /// <summary>
        /// add a new category
        /// </summary>
        /// <param name="category"></param>
        void AddCategory(string category);

        /// <summary>
        /// get all the categories
        /// </summary>
        /// <returns>a collecion of the categories</returns>
        IEnumerable<Category> GetCategories();


        /// <summary>
        /// add a category to an item
        /// </summary>
        /// <param name="itemID">itemID</param>
        /// <param name="categoryID">categoryID</param>
        void AddItemCategory(string itemID, string categoryID);


        /// <summary>
        /// remove a category from an item
        /// </summary>
        /// <param name="itemID">itemID</param>
        /// <param name="categoryID">categoryID</param>
        void RemoveItemCategory(string itemID, string categoryID);


        /// <summary>
        /// get the category IDs of an item
        /// </summary>
        /// <param name="itemID">item </param>
        /// <returns>a collection of the category IDs</returns>
        IEnumerable<string> GetItemCategories(string itemID);



        /// <summary>
        /// get all the order items
        /// </summary>
        /// <returns>a collection of all the orderItems</returns>
        IEnumerable<OrderItem> GetOrderItems();

        /// <summary>
        /// add an orderItem 
        /// </summary>
        /// <param name="orderID">ID of the order</param>
        /// <param name="itemID">Id of the Item</param>
        /// <param name="price">price of the item</param>
        void AddOrderItem(OrderItem item);

        /// <summary>
        /// add a new order 
        /// </summary>
        /// <param name="customerID">customer of the order</param>
        /// <param name="time">time of the order</param>
        /// <param name="price">price of the order</param>
        void AddOrder(Order order);

        /// <summary>
        /// get all the orders
        /// </summary>
        /// <returns>a collection of all the orders</returns>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// mark if an order is paid
        /// </summary>
        /// <param name="paid">truenif paid</param>
        void PayOrder(string orderID, bool paid);

        /// <summary>
        /// generates a new customerID
        /// </summary>
        /// <returns>a new CustomerID</returns>
        int GetNewCustomerID();

        /// <summary>
        /// generates a new CategoryID
        /// </summary>
        /// <returns></returns>
        int GetNewCategoryID();

        /// <summary>
        /// generates a new ItemID
        /// </summary>
        /// <returns></returns>
        int GetNewItemID();

        /// <summary>
        /// generates a new OrderID
        /// </summary>
        /// <returns></returns>
        int GetNewOrderID();

        /// <summary>
        /// add a new stock item insctance
        /// </summary>
        /// <param name="item">stockitem to add</param>
        void AddStockItem(StockItem item);

        /// <summary>
        /// edit the stock of an item
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="qnt">qnt of the item</param>
        void SubtractStock(string itemID, int qnt);

        /// <summary>
        /// get the stock of an item
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <returns> in the current qnt stock</returns>
        int GetStockItem(string itemID);

        /// <summary>
        /// get the cost of the item id
        /// </summary>
        /// <param name="itemID">id of the requeated item</param>
        /// <returns>the cost price of the item</returns>
        double GetCostPrice(string itemID);
    }
}
