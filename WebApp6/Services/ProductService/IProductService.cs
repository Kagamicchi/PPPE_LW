using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Product;

namespace WebApp6.Services.ProductService
{
    public interface IProductService
    {
        Task<BaseResponse<ProductModel>> Get(Guid id);
        Task<BaseResponse<ProductModel>> GetAll();
        Task<BaseResponse<ProductModel>> Post(ProductRequest request);
        Task<BaseResponse<ProductModel>> Put(Guid id, ProductModel product);
        Task<BaseResponse<ProductModel>> Delete(Guid id);
    }
}
