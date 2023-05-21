using WebApp6.Extensions;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Product;

namespace WebApp6.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly List<ProductModel> _productRepository;

        public ProductService()
        {
            _productRepository = new List<ProductModel>()
            {
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Tomato",
                    ProductPrice = 76.95,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Potato",
                    ProductPrice = 9.59,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Carrot",
                    ProductPrice = 56.95,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Onion",
                    ProductPrice = 59.95,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Apple",
                    ProductPrice = 39.95,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Banana",
                    ProductPrice = 62.85,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Kiwi",
                    ProductPrice = 76.90,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Orange",
                    ProductPrice = 49.85,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Strawberry",
                    ProductPrice = 170.10,
                    ProductDescription = "price per kilogram",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Canned food for adult cats with beef in Pouch sauce",
                    ProductPrice = 14.90,
                    ProductDescription = "price per pack",
                    ProductMass = "100 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Canned goods for adult dogs of small breeds with chicken and carrots in Pouch sauce",
                    ProductPrice = 14.70,
                    ProductDescription = "price per pack",
                    ProductMass = "100 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Coffee in beans",
                    ProductPrice = 319.50,
                    ProductDescription = "price per pack",
                    ProductMass = "1 kg"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Ground coffee",
                    ProductPrice = 104.60,
                    ProductDescription = "price per pack",
                    ProductMass = "230 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Floral Karkade",
                    ProductPrice = 37.60,
                    ProductDescription = "price per pack",
                    ProductMass = "80 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Ceylon black medium leaf tea with bergamot aroma",
                    ProductPrice = 61.90,
                    ProductDescription = "price per pack",
                    ProductMass = "100 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Chinese green leaf tea with the aroma of milk",
                    ProductPrice = 63.90,
                    ProductDescription = "price per pack",
                    ProductMass = "100 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Ultra-pasteurized milk 1%",
                    ProductPrice = 28.80,
                    ProductDescription = "price per pack",
                    ProductMass = "900 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Plant-milk mixture 72.5%",
                    ProductPrice = 71.60,
                    ProductDescription = "price per pack",
                    ProductMass = "400 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Sour cream 15%",
                    ProductPrice = 36.60,
                    ProductDescription = "price per pack",
                    ProductMass = "350 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Cottage cheese glazed with boiled condensed milk 23%",
                    ProductPrice = 10.40,
                    ProductDescription = "price for one",
                    ProductMass = "36 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Processed cheese 38%",
                    ProductPrice = 17.80,
                    ProductDescription = "price for one",
                    ProductMass = "70 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Chicken egg",
                    ProductPrice = 37.90,
                    ProductDescription = "price per ten",
                    ProductMass = "400-650 g"
                },
                new ProductModel()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "The thigh of broiler chickens is chilled",
                    ProductPrice = 110.99,
                    ProductDescription = "price per kilogram",
                    ProductMass = "700-900 g"
                }
            };
        }

        public async Task<BaseResponse<ProductModel>> Get(Guid id)
        {
            try
            {
                var product = await Task.FromResult(_productRepository.Find(g => g.ProductId == id));
                if (product != null)
                {
                    return new BaseResponse<ProductModel>()
                    {
                        Success = true,
                        Values = new List<ProductModel> { product },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<ProductModel>
                {
                    Message = "Product not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ProductModel>> GetAll()
        {
            try
            {
                var products = await Task.FromResult(_productRepository);
                return new BaseResponse<ProductModel>()
                {
                    Success = true,
                    Values = products,
                    ValueCount = products.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ProductModel>> Post(ProductRequest request)
        {
            try
            {
                var product = request.ToModel();
                var id = await Task.FromResult(() => {
                    product.ProductId = Guid.NewGuid();
                    _productRepository.Add(product);
                    return product.ProductId;
                });

                return new BaseResponse<ProductModel>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<ProductModel> { product },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ProductModel>> Put(Guid id, ProductModel product)
        {
            try
            {
                if (product.ProductId != id)
                {
                    return new BaseResponse<ProductModel>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_productRepository.Find(x => x.ProductId == id));
                if (current == null)
                {
                    return new BaseResponse<ProductModel>()
                    {
                        Message = "Product not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(product.ProductName))
                {
                    product.ProductName = product.ProductName;
                }
                if (product.ProductPrice >= 0)
                {
                    product.ProductPrice = product.ProductPrice;
                }
                if (!string.IsNullOrWhiteSpace(product.ProductDescription))
                {
                    product.ProductDescription = product.ProductDescription;
                }
                if (!string.IsNullOrWhiteSpace(product.ProductMass))
                {
                    product.ProductMass = product.ProductMass;
                }

                return new BaseResponse<ProductModel>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<ProductModel> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ProductModel>> Delete(Guid id)
        {
            try
            {
                var current = _productRepository.Find(x => x.ProductId == id);
                if (current == null)
                {
                    return new BaseResponse<ProductModel>()
                    {
                        Message = "Product not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_productRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<ProductModel>()
                    {
                        Success = true,
                        Message = "Product deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<ProductModel>()
                {
                    Message = "Product was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
