using WebApp6.Models.Dto;

namespace WebApp6.Services.V2
{
    public interface IStringService
    {
        Task<BaseResponse<string>> GetSomeText();
    }
}
