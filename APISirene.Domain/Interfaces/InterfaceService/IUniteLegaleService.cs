

using APISirene.Domain.Models;
using OfficeOpenXml;

namespace APISirene.Domain.Interfaces.InterfaceService
{
    public interface IUniteLegaleService
    {
        Task<IEnumerable<UniteLegale>> GetAllUniteLegaleAsync();

        Task<UniteLegale> GetUniteLegaleByIdAsync(string id);

        Task<UniteLegale> CreateUniteLegaleAsync(UniteLegale uniteLegale);

        Task<bool> UpdateUniteLegaleAsync(string id, UniteLegale uniteLegale);

        Task<bool> DeleteUniteLegaleAsync(string id);

        Task<byte[]> ExportUniteLegalesToExcel(ExcelPackage package);
    }
}
