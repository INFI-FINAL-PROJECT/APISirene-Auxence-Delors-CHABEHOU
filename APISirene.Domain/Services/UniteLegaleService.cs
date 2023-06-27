using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;

namespace APISirene.Domain.Services
{
    public class UniteLegaleService : IUniteLegaleService
    {
        private readonly IUniteLegaleRepository _uniteLegaleRepository;

        public UniteLegaleService(IUniteLegaleRepository uniteLegaleRepository)
        {
            _uniteLegaleRepository = uniteLegaleRepository;
        }

        #region Récupère les UniteLegale depuis l'API Sirene
        public async Task<IEnumerable<UniteLegale>> GetUniteLegaleFromApi(string startDate, string endDate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 799acb30-0560-3826-a941-375cf6d0bd83");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = $"https://api.insee.fr/entreprises/sirene/V3/siret?periode(etatAdministratifUniteLegale)=A&periode(dateDernierTraitementUniteLegale)=[{startDate}%20TO%20{endDate}]";

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var uniteLegale = JsonConvert.DeserializeObject<IEnumerable<UniteLegale>>(content);

                    return uniteLegale;
                }
                else
                {
                    // Gérer les erreurs de la requête à l'API Sirene
                    throw new Exception("Erreur lors de la récupération des établissements depuis l'API Sirene.");
                }
            }
        }
        #endregion

        #region Sauvegarde les UniteLegale dans la base de données
        public async Task SaveInformationsToDatabase(string startDate, string endDate)
        {
            var uniteLegale = await GetUniteLegaleFromApi(startDate, endDate);

            foreach (var information in uniteLegale)
            {
                await _uniteLegaleRepository.AddAsync(information);
            }
        }
        #endregion

        #region Exporte les UniteLegale dans un fichier Excel
        public async Task<byte[]> ExportUniteLegalesToExcel(ExcelPackage package)
        {
            var uniteLegale = await GetAllUniteLegaleAsync();

            var worksheet = package.Workbook.Worksheets.Add("UniteLegale");

            // Add headers
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Score";
            worksheet.Cells[1, 3].Value = "Siren";

            // Add data
            var row = 2;
            foreach (var uniteLegal in uniteLegale)
            {
                worksheet.Cells[row, 1].Value = uniteLegal.Id;

                row++;
            }

            return package.GetAsByteArray();
        }
        #endregion

        #region Récupère toutes les UniteLegale
        public async Task<IEnumerable<UniteLegale>> GetAllUniteLegaleAsync()
        {
            return await _uniteLegaleRepository.GetAllAsync();
        }
        #endregion

        #region Récupère une UniteLegale par son Id
        public async Task<UniteLegale> GetUniteLegaleByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return await _uniteLegaleRepository.GetByIdAsync(id);
        }
        #endregion

        #region Créer une UniteLegale
        public async Task<UniteLegale> CreateUniteLegaleAsync(UniteLegale uniteLegale)
        {
            if (uniteLegale == null)
            {
                throw new ArgumentNullException(nameof(uniteLegale));
            }

            return await _uniteLegaleRepository.AddAsync(uniteLegale);
        }
        #endregion

        #region Met à jour une UniteLegale
        public async Task<bool> UpdateUniteLegaleAsync(string id, UniteLegale uniteLegale)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (uniteLegale == null)
            {
                throw new ArgumentNullException(nameof(uniteLegale));
            }

            var existingUniteLegale = await _uniteLegaleRepository.GetByIdAsync(id);

            if (existingUniteLegale == null)
            {
                return false;
            }

            existingUniteLegale.Score = uniteLegale.Score;

            return await _uniteLegaleRepository.UpdateAsync(existingUniteLegale);
        }
        #endregion

        #region Supprime une UniteLegale
        public async Task<bool> DeleteUniteLegaleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return await _uniteLegaleRepository.DeleteAsync(id);
        }
        #endregion
    }
}
