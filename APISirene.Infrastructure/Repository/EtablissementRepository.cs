

using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Models;
using MongoDB.Driver;

namespace APISirene.Infrastructure.Repository
{
    public class EtablissementRepository : IEtablissementRepository
    {
        private readonly IMongoCollection<Etablissement> _etablissement;

        public EtablissementRepository(IMongoDatabase database)
        {
            _etablissement = database.GetCollection<Etablissement>("Etablissement");
        }

        #region Récupère tous les Etablissement de manière asynchrone
        public async Task<IEnumerable<Etablissement>> GetAllAsync()
        {
            return await _etablissement.Find(_ => true).ToListAsync();
        }
        #endregion

        #region Récupère un Etablissement par ID de manière asynchrone
        public async Task<Etablissement> GetByIdAsync(string id)
        {
            // Récupère un role en fonction de son ID
            return await _etablissement.Find(etablissement => etablissement.Id == id).FirstOrDefaultAsync();
        }
        #endregion

        #region Ajoute un nouvel Etablissement de manière asynchrone
        public async Task<Etablissement> AddAsync(Etablissement etablissement)
        {
            // Ajoute un Etablissement à la base de données
            await _etablissement.InsertOneAsync(etablissement);
            // Renvoie l'Etablissement
            return etablissement;
        }
        #endregion

        #region Met à jour un Etablissement existant de manière asynchrone
        public async Task<bool> UpdateAsync(Etablissement etablissement)
        {
            // Remplace l'Etablissement existant par le nouveau role
            var updateResult = await _etablissement.ReplaceOneAsync(u => u.Id == etablissement.Id, etablissement);
            // Renvoie un booléen indiquant le succès ou l'échec de la mise à jour
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        #endregion

        #region Supprime un Etablissement par ID de manière asynchrone
        public async Task<bool> DeleteAsync(string id)
        {
            // Supprime un role en fonction de son ID
            var deleteResult = await _etablissement.DeleteOneAsync(etablissement => etablissement.Id == id);
            // Renvoie un booléen indiquant le succès ou l'échec de la suppression
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        #endregion
    }
}
