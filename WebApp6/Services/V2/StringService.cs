using WebApp6.Models.Dto;

namespace WebApp6.Services.V2
{
    public class StringService : IStringService
    {
        public async Task<BaseResponse<string>> GetSomeText()
        {
            try
            {
                return new BaseResponse<string>
                {
                    Message = "Success",
                    Success = true,
                    StatusCode = 200,
                    ValueCount = 1,
                    Values = new List<string> { await Task.FromResult("Lorem Ipsum is simply dummy text of the printing and typesetting industry.") }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
