using System;

namespace DO
{
    public class StockItem
    {
        /// <summary>
        /// private ID of the item <br>for internal use</br>
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// quantity of the item in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// price each item costed me
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// date the otem came in stock
        /// </summary>
        public DateTime Date { get; set; }
    }
}
