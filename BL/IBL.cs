using BO;
using System;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
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
        /// get a single customer by id
        /// </summary>
        /// <param name="customerID">customer id of the customer</param>
        /// <returns>the customer with <b>customerID</b></returns>
        Customer GetSingleCustomer(string customerID);


        /// <summary>
        /// update details of the customer
        /// </summary>
        /// <param name="cust">update the customer to cust according to the cust CustomerID</param>
        void EditCustomer(Customer cust);

        /// <summary>
        /// add a new item
        /// </summary>
        /// <param name="item">item to add</param>
        void AddItem(Item item, double costPrice);

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
        void RemoveItem(string itemID);

        /// <summary>
        /// add a new order 
        /// </summary>
        /// <param name="order">order to add</param>
        string AddOrder(Order order);

        /// <summary>
        /// get orders 
        /// </summary>
        /// <param name="filter">filter the orders</param>
        /// <returns>return a collection of all the orders that comply to the filter</returns>
        IEnumerable<Order> GetOrders(Predicate<Order> filter, string where = "");

        /// <summary>
        /// set order as paid
        /// </summary>
        /// <param name="orderID">id of the order</param>
        /// <param name="amount">amount paid</param>
        void PayOrder(string orderID, double amount);

        /// <summary>
        /// add stock to an existing item
        /// </summary>
        /// <param name="item"></param>
        void AddStock(Item item, double costPrice);

    }
}
