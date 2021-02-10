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
        void AddItemCategory(int itemID, int categoryID);


        /// <summary>
        /// remove a category from an item
        /// </summary>
        /// <param name="itemID">itemID</param>
        /// <param name="categoryID">categoryID</param>
        void RemoveItemCategory(int itemID, int categoryID);


        /// <summary>
        /// get the category IDs of an item
        /// </summary>
        /// <param name="itemID">item </param>
        /// <returns>a collection of the category IDs</returns>
        IEnumerable<int> GetItemCategories(int itemID);



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
        void AddOrderItem(int orderID, int itemID, double price);

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
    }
}
