namespace DO
{
    public class Item
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
        /// is the item still active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// price of the item
        /// </summary>
        public double Price { get; set; }

    }
}
