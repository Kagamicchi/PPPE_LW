using WebApp6.Models.Dto;

namespace WebApp6.Services.V1
{
    public interface INumberService
    {
        Task<BaseResponse<int>> GetRandomInteger();
    }
}
