

using APISirene.Domain.Models;

namespace APISirene.Domain.Interfaces.InterfaceRepository
{
    public interface IInformationsRepository
    {
        Task<IEnumerable<Informations>> GetAllAsync();

        Task<Informations> GetByIdAsync(string id);

        Task<Informations> AddAsync(Informations informations);

        Task<bool> UpdateAsync(Informations informations);

        Task<bool> DeleteAsync(string id);
    }
}
