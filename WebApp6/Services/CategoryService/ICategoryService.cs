using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.History;

namespace WebApp6.Services.HistoryService
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryModel>> Get(Guid id);
        Task<BaseResponse<CategoryModel>> GetAll();
        Task<BaseResponse<CategoryModel>> Post(CategoryRequest request);
        Task<BaseResponse<CategoryModel>> Put(Guid id, CategoryModel category);
        Task<BaseResponse<CategoryModel>> Delete(Guid id);
    }
}