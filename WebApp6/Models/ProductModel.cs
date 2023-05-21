namespace WebApp6.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double ProductPrice { get; set; } = 0;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductMass { get; set; } = string.Empty;
    }
}
