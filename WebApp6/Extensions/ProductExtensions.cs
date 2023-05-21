using WebApp6.Models;
using WebApp6.Models.Dto.Product;

namespace WebApp6.Extensions
{
    public static class ProductExtensions
    {
        public static ProductModel ToModel(this ProductRequest request)
        {
            return new ProductModel
            {
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProductDescription = request.ProductDescription
            };
        }
    }
}
