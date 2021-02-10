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
        /// get all the stock items that comply to the filter
        /// </summary>
        /// <param name="filter">filter to apply to the collection</param>
        /// <returns>a collection of all the item that comply to the filter</returns>
        IEnumerable<StockItem> GetStockItems(Predicate<StockItem> filter);

        /// <summary>
        /// update the quantity in the stock
        /// </summary>
        /// <param name="itemID">ID of the item to update</param>
        /// <param name="qnt">how much to add (or remove if negative)</param>
        void UpdateStock(int itemID, int qnt);

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
