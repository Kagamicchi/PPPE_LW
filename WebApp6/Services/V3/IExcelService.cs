using ClosedXML.Excel;

namespace WebApp6.Services.V3
{
    public interface IExcelService
    {
        Task<XLWorkbook> Get();
    }
}
