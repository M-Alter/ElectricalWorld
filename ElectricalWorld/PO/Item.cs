﻿using System.Collections.Generic;

namespace PO
{
    public class Item : InvoiceItem
    {
        /// <summary>
        /// private ID of the item <br>for internal use</br>
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// brand of the item
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// model number of the item
        /// </summary>
        public string ModelNumber { get; set; }

        /// <summary>
        /// description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// image of the item
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// category of the item
        /// </summary>
        public IEnumerable<Category> Categories { get; set; }

        /// <summary>
        /// price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// quantity of the item in stock
        /// </summary>
        public int Quantity { get; set; }


        /// <summary>
        /// cost price of the item
        /// </summary>
        public double CostPrice { get; set; }
    }
}
