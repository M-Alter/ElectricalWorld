using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

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
        /// get customers that comply to the filter
        /// </summary>
        /// <param name="filter">filter of the collection</param>
        /// <returns>a collction of all the customers that comply to the filter</returns>
        IEnumerable<Customer> GetCutomers(Predicate<Customer> filter);

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
        /// get all the items that comply to the filter
        /// </summary>
        /// <param name="filter">filter the items</param>
        /// <returns>a collection of the items that comply to the filter</returns>
        IEnumerable<Item> GetItems(Predicate<Item> filter);

        /// <summary>
        /// marks an item as InActive
        /// </summary>
        /// <param name="item">item to mark</param>
        void RemoveItem(Item item);

        /// <summary>
        /// add an image to an item
        /// </summary>
        /// <param name="itemID">of the item </param>
        /// <param name="image">string of the image</param>
        void AddImage(int itemID, string image);

        /// <summary>
        /// Edit an image to an item
        /// </summary>
        /// <param name="itemID">ID of the item </param>
        /// <param name="image">string of the image</param>
        void EditImage(int itemID, string image);

        /// <summary>
        /// Remove an image to an item
        /// </summary>
        /// <param name="itemID">ID of the item </param>
        /// <param name="image">string of the image</param>
        void RemoveImage(int itemID, string image);

        /// <summary>
        /// get item images 
        /// </summary>
        /// <param name="filter">filter the images</param>
        /// <returns>a collection of all the images that comply to the filter</returns>
        IEnumerable<ItemImage> GetItemImages(Predicate<ItemImage> filter);

        /// <summary>
        /// add a new category
        /// </summary>
        /// <param name="category"></param>
        void AddCategory(string category);

        /// <summary>
        /// get all the categories
        /// </summary>
        /// <param name="filter">remove categories that dont comply to the filter</param>
        /// <returns>a collecion of the categories that comply to the filter</returns>
        IEnumerable<Category> GetCategories(Predicate<Category> filter);

        /// <summary>
        /// add a category to an item
        /// </summary>
        /// <param name="itemID">itemID</param>
        /// <param name="categoryID">categoryID</param>
        void AddItemCategory(int itemID, int categoryID);


        /// <summary>
        /// remove a category from an item
        /// </summary>
        /// <param name="itemID">itemID</param>
        /// <param name="categoryID">categoryID</param>
        void RemoveItemCategory(int itemID, int categoryID);



        /// <summary>
        /// get all the order items the comply to the filter
        /// </summary>
        /// <param name="filter">filter to apply to the collection</param>
        /// <returns>a collection of all the orderItems that comply to the filter</returns>
        IEnumerable<OrderItem> GetOrderItems(Predicate<OrderItem> filter);

        /// <summary>
        /// add an orderItem 
        /// </summary>
        /// <param name="orderID">ID of the order</param>
        /// <param name="itemID">Id of the Item</param>
        /// <param name="price">price of the item</param>
        void AddOrderItem(int orderID, int itemID, double price);

        /// <summary>
        /// add a new order 
        /// </summary>
        /// <param name="customerID">customer of the order</param>
        /// <param name="time">time of the order</param>
        /// <param name="price">price of the order</param>
        void AddOrder(int customerID, DateTime time, double price);

        /// <summary>
        /// get all the orders
        /// </summary>
        /// <param name="filter">filter the orders</param>
        /// <returns>a collection of all the orders that comply to the filter</returns>
        IEnumerable<Order> GetOrders(Predicate<Order> filter);
    }
}
