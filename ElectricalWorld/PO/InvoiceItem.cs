namespace PO
{
    public interface InvoiceItem
    {
        /// <summary>
        /// price of the item
        /// </summary>
        double Price { get; set; }

        /// <summary>
        /// brand of the item
        /// </summary>
        string Brand { get; set; }
    }
}
