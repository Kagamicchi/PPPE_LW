using WebApp6.Models;
using WebApp6.Models.Dto.History;

namespace WebApp6.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryModel ToModel(this CategoryRequest request)
        {
            return new CategoryModel
            {
                CategoryName = request.CategoryName
            };
        }
    }
}
