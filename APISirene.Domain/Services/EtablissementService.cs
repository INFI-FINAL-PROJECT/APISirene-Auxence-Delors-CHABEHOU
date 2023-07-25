

using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;

namespace APISirene.Domain.Services
{
    public class EtablissementService : IEtablissementService
    {
        private readonly IEtablissementRepository _etablissementRepository;
        private readonly HttpClient _httpClient;

        public EtablissementService(IEtablissementRepository etablissementRepository, HttpClient httpClient)
        {
            _etablissementRepository = etablissementRepository;
            _httpClient = httpClient;
        }

        #region Récupère les Etablissements depuis l'API Sirene
        public async Task<IEnumerable<Etablissement>> GetEtablissementsFromApi(string codeNaf, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "799acb30-0560-3826-a941-375cf6d0bd83");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = $"https://api.insee.fr/entreprises/sirene/V3/siret?q=activitePrincipaleUniteLegale:{codeNaf} AND dateCreationEtablissement:[{dateDebut:yyyy-MM-dd} TO {dateFin:yyyy-MM-dd}]&nombre=1000";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<SireneApiResponse>(content);

                    return apiResponse.etablissements;
                }
                else
                {
                    
                    throw new Exception("Error while fetching establishments from the Sirene API.");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error while fetching establishments from the Sirene API.", ex);
            }
        }
        #endregion


        #region Sauvegarde les Etablissements dans la base de données
        public async Task SaveEtablissementsToDatabase(string codeNaf, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                var etablissements = await GetEtablissementsFromApi(codeNaf, dateDebut, dateFin);

                foreach (var etablissement in etablissements)
                {
                    await _etablissementRepository.AddAsync(etablissement);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while saving establishments to the database: {ex.Message}");
            }
        }
        #endregion


        #region Exporte les Etablissements au format Excel
        public async Task<byte[]> ExportEtablissementsToExcel(ExcelPackage package)
        {
            var etablissements = await GetAllEtablissementAsync();

            var worksheet = package.Workbook.Worksheets.Add("Etablissements");

            // Add headers
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Score";
            worksheet.Cells[1, 3].Value = "Siren";

            // Add data
            var row = 2;
            foreach (var etablissement in etablissements)
            {
                worksheet.Cells[row, 1].Value = etablissement.Id;
                worksheet.Cells[row, 2].Value = etablissement.Score;
                worksheet.Cells[row, 3].Value = etablissement.Siren;

                row++;
            }

            return package.GetAsByteArray();
        }
        #endregion

        #region Récupère tous les Etablissements de manière asynchrone
        public async Task<IEnumerable<Etablissement>> GetAllEtablissementAsync()
        {
            return await _etablissementRepository.GetAllAsync();
        }
        #endregion

        #region Récupère un Etablissement par ID de manière asynchrone
        public async Task<Etablissement> GetEtablissementByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return await _etablissementRepository.GetByIdAsync(id);
        }
        #endregion

        #region Crée un nouvel Etablissement de manière asynchrone
        public async Task<Etablissement> CreateEtablissementAsync(Etablissement etablissement)
        {
            if (etablissement == null)
            {
                throw new ArgumentNullException(nameof(etablissement));
            }

            return await _etablissementRepository.AddAsync(etablissement);
        }
        #endregion

        #region Met à jour un Etablissement de manière asynchrone
        public async Task<bool> UpdateEtablissementAsync(string id, Etablissement etablissement)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (etablissement == null)
            {
                throw new ArgumentNullException(nameof(etablissement));
            }

            var existingEtablissement = await _etablissementRepository.GetByIdAsync(id);

            if (existingEtablissement == null)
            {
                return false;
            }

            existingEtablissement.Score = etablissement.Score;

            return await _etablissementRepository.UpdateAsync(existingEtablissement);
        }
        #endregion

        #region Supprime un Etablissement de manière asynchrone
        public async Task<bool> DeleteEtablissementAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _etablissementRepository.DeleteAsync(id);
        }
        #endregion
    }
}
