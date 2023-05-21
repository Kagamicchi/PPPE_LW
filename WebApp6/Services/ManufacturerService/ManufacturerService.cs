using WebApp6.Extensions;
using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Manufacturer;

namespace WebApp6.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly List<ManufacturerModel> _manufacturerRepository;

        public ManufacturerService()
        {
            _manufacturerRepository = new List<ManufacturerModel>()
            {
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Fort"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Jacobs"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Nescafe"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "De Luxe Foods&Goods Selected"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Monomax"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Lovare"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Agrobusiness"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Agro-Ukraine"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "Own Line"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(), 
                    ManufacturerName = "A smart choice"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Merry Cow"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Slavyanochka"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Farm"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Our Freckle"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Whiskas"
                },
                new ManufacturerModel()
                {
                    ManufacturerId = Guid.NewGuid(),
                    ManufacturerName = "Club 4 paws"
                }
            };
        }

        public async Task<BaseResponse<ManufacturerModel>> Get(Guid id)
        {
            try
            {
                var manufacturer = await Task.FromResult(_manufacturerRepository.Find(g => g.ManufacturerId == id));
                if (manufacturer != null)
                {
                    return new BaseResponse<ManufacturerModel>()
                    {
                        Success = true,
                        Values = new List<ManufacturerModel> { manufacturer },
                        ValueCount = 1,
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new BaseResponse<ManufacturerModel>
                {
                    Message = "Manufacturer not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ManufacturerModel>> GetAll()
        {
            try
            {
                var manufacturers = await Task.FromResult(_manufacturerRepository);
                return new BaseResponse<ManufacturerModel>()
                {
                    Success = true,
                    Values = manufacturers,
                    ValueCount = manufacturers.Count,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ManufacturerModel>> Post(ManufacturerRequest request)
        {
            try
            {
                var manufacturer = request.ToModel();
                var id = await Task.FromResult(manufacturer.ManufacturerId = Guid.NewGuid());
                _manufacturerRepository.Add(manufacturer);

                return new BaseResponse<ManufacturerModel>()
                {
                    StatusCode = StatusCodes.Status201Created,
                    Success = true,
                    Values = new List<ManufacturerModel> { manufacturer },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ManufacturerModel>> Put(Guid id, ManufacturerModel manufacturer)
        {
            try
            {
                if (manufacturer.ManufacturerId != id)
                {
                    return new BaseResponse<ManufacturerModel>()
                    {
                        Message = "Id mismatch"
                    };
                }

                var current = await Task.FromResult(_manufacturerRepository.Find(x => x.ManufacturerId == id));
                if (current == null)
                {
                    return new BaseResponse<ManufacturerModel>()
                    {
                        Message = "Manufacturer not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (!string.IsNullOrWhiteSpace(manufacturer.ManufacturerName))
                {
                    current.ManufacturerName = manufacturer.ManufacturerName;
                }

                return new BaseResponse<ManufacturerModel>()
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Values = new List<ManufacturerModel> { current },
                    ValueCount = 1
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ManufacturerModel>> Delete(Guid id)
        {
            try
            {
                var current = _manufacturerRepository.Find(x => x.ManufacturerId == id);
                if (current == null)
                {
                    return new BaseResponse<ManufacturerModel>()
                    {
                        Message = "Manufacturer not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var isRemoved = await Task.FromResult(_manufacturerRepository.Remove(current));
                if (isRemoved)
                {
                    return new BaseResponse<ManufacturerModel>()
                    {
                        Success = true,
                        Message = "Manufacturer deleted",
                        StatusCode = StatusCodes.Status204NoContent
                    };
                }
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = "Manufacturer was not deleted",
                    StatusCode = StatusCodes.Status204NoContent
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManufacturerModel>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
