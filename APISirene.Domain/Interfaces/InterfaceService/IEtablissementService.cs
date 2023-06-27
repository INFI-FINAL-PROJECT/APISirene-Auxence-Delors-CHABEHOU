using APISirene.Domain.Models;
using OfficeOpenXml;

namespace APISirene.Domain.Interfaces.InterfaceService
{
    public interface IEtablissementService
    {
        Task<IEnumerable<Etablissement>> GetAllEtablissementAsync();

        Task<Etablissement> GetEtablissementByIdAsync(string id);

        Task<Etablissement> CreateEtablissementAsync(Etablissement etablissement);

        Task<bool> UpdateEtablissementAsync(string id, Etablissement etablissement);

        Task<bool> DeleteEtablissementAsync(string id);

        Task<byte[]> ExportEtablissementsToExcel(ExcelPackage package);
    }
}
