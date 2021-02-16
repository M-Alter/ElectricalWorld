namespace PO
{
    public class Payment : InvoiceItem
    {
        /// <summary>
        /// price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// brand of the item
        /// </summary>
        public string Brand { get; set; }
    }
}
