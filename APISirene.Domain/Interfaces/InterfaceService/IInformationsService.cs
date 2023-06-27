

using APISirene.Domain.Models;
using OfficeOpenXml;

namespace APISirene.Domain.Interfaces.InterfaceService
{
    public interface IInformationsService
    {
        Task<IEnumerable<Informations>> GetAllInformationsAsync();

        Task<Informations> GetInformationsByIdAsync(string id);

        Task<Informations> CreateInformationsAsync(Informations informations);

        Task<bool> UpdateInformationsAsync(string id, Informations informations);

        Task<bool> DeleteInformationsAsync(string id);

        Task<byte[]> ExportInformationsToExcel(ExcelPackage package);
    }
}
