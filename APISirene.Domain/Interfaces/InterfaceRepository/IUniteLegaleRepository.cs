
using APISirene.Domain.Models;

namespace APISirene.Domain.Interfaces.InterfaceRepository
{
    public interface IUniteLegaleRepository
    {
        Task<IEnumerable<UniteLegale>> GetAllAsync();

        Task<UniteLegale> GetByIdAsync(string id);

        Task<UniteLegale> AddAsync(UniteLegale uniteLegale);

        Task<bool> UpdateAsync(UniteLegale uniteLegale);

        Task<bool> DeleteAsync(string id);
    }
}
