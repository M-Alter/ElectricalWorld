using DO;
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

        double GetPaidAmount(string orderID);
        
        /// <summary>
        /// get a single customer
        /// </summary>
        /// <param name="customerID">id of the customer</param>
        /// <returns>the customer with id <b>customerID</b></returns>
        Customer GetSingleCutomer(string customerID);

        /// <summary>
        /// update details of the customer
        /// </summary>
        /// <param name="cust">update the customer to cust according to the cust CustomerID</param>
        void EditCustomer(Customer cust);

        /// <summary>
        /// add a new item
        /// </summary>
        /// <param name="item">item to add</param>
        /// <returns>the new ItemID</returns>
        int AddItem(Item item);

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
        /// <returns>the new OrderID</returns>
        int AddOrder(Order order);

        /// <summary>
        /// get all the orders
        /// </summary>
        /// <returns>a collection of all the orders</returns>
        IEnumerable<Order> GetOrders(string where = "");

        /// <summary>
        /// mark if an order is paid
        /// </summary>
        /// <param name="amount">amount paid</param>
        void PayOrder(string orderID, double amount);

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
