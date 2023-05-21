using WebApp6.Extensions;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.History;

namespace WebApp6.Services.HistoryService
{
    public class CategoryService: ICategoryService
    {
        private readonly List<CategoryModel> _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new List<CategoryModel>()
            {
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Vegetables"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Fruits"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Berries"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Meat/fish/eggs"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Cereals"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Tea"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Coffee"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Dairy"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Other drinks"
                },
                new CategoryModel()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "Pet food"
                }
            };
        }

        public async Task<BaseResponse<CategoryModel>> Get(Guid id)
        {
            try
            {
                var category = await Task.FromResult(_categoryRepository.Find(g => g.CategoryId == id));
                if (category != null)
                {
                    return new BaseResponse<CategoryModel>()
                    {
                        Success = true,
                        Values = new List<CategoryModel> { category },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<CategoryModel>
                {
                    Message = "Category not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<CategoryModel>> GetAll()
        {
            try
            {
                var categories = await Task.FromResult(_categoryRepository);
                return new BaseResponse<CategoryModel>()
                {
                    Success = true,
                    Values = categories,
                    ValueCount = categories.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<CategoryModel>> Post(CategoryRequest request)
        {
            try
            {
                var category = request.ToModel();
                var id = await Task.FromResult(category.CategoryId = Guid.NewGuid());
                _categoryRepository.Add(category);

                return new BaseResponse<CategoryModel>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<CategoryModel> { category },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<CategoryModel>> Put(Guid id, CategoryModel category)
        {
            try
            {
                if (category.CategoryId != id)
                {
                    return new BaseResponse<CategoryModel>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_categoryRepository.Find(x => x.CategoryId == id));
                if (current == null)
                {
                    return new BaseResponse<CategoryModel>()
                    {
                        Message = "Category not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    current.CategoryName = category.CategoryName;
                }

                return new BaseResponse<CategoryModel>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<CategoryModel> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<CategoryModel>> Delete(Guid id)
        {
            try
            {
                var current = _categoryRepository.Find(x => x.CategoryId == id);
                if (current == null)
                {
                    return new BaseResponse<CategoryModel>()
                    {
                        Message = "Category not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_categoryRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<CategoryModel>()
                    {
                        Success = true,
                        Message = "Category deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<CategoryModel>()
                {
                    Message = "Category was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
