namespace StockManagement.Models
{
    /// <summary>
    /// Represents a product in the inventory.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current quantity in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }
    }
}
