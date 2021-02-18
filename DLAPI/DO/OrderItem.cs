namespace DO
{
    public class OrderItem
    {
        /// <summary>
        /// private ID of the Transaction <br>for internal use</br>
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// private ID of the item <br>for internal use</br>
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// profit of the sale of this item
        /// </summary>
        public double Profit { get; set; }
    }
}
