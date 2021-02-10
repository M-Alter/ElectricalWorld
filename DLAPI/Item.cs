using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Item
    {
        /// <summary>
        /// private ID of the item <br>for internal use</br>
        /// </summary>
        public int ItemID { get; set; }

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
        public string Category1 { get; set; }

        /// <summary>
        /// secondary category of the item
        /// </summary>
        public string Category2 { get; set; }

        /// <summary>
        /// is the item still active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// price of the item
        /// </summary>
        public double Price { get; set; }

    }
}
