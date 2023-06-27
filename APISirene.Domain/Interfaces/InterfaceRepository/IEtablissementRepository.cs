using APISirene.Domain.Models;

namespace APISirene.Domain.Interfaces.InterfaceRepository
{
    public interface IEtablissementRepository
    {
        Task<IEnumerable<Etablissement>> GetAllAsync();

        Task<Etablissement> GetByIdAsync(string id);

        Task<Etablissement> AddAsync(Etablissement etablissement);

        Task<bool> UpdateAsync(Etablissement etablissement);

        Task<bool> DeleteAsync(string id);
    }
}
