using WebApp6.Models;
using WebApp6.Models.Dto.Manufacturer;

namespace WebApp6.Extensions
{
    public static class ManufacturerExtensions
    {
        public static ManufacturerModel ToModel(this ManufacturerRequest request)
        {
            return new ManufacturerModel
            {
                ManufacturerName = request.ManufacturerName
            };
        }
    }
}
