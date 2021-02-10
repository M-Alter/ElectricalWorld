﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class StockItem
    {
        /// <summary>
        /// private ID of the item <br>for internal use</br>
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// quantity of the item in stock
        /// </summary>
        public int Quantity { get; set; }
    }
}
