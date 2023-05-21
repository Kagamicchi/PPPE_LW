using WebApp6.Models;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.Manufacturer;

namespace WebApp6.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task<BaseResponse<ManufacturerModel>> Get(Guid id);
        Task<BaseResponse<ManufacturerModel>> GetAll();
        Task<BaseResponse<ManufacturerModel>> Post(ManufacturerRequest request);
        Task<BaseResponse<ManufacturerModel>> Put(Guid id, ManufacturerModel manufacturer);
        Task<BaseResponse<ManufacturerModel>> Delete(Guid id);
    }
}