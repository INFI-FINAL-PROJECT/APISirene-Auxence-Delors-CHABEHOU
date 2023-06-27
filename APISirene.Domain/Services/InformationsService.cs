using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;

namespace APISirene.Domain.Services
{
    public class InformationsService : IInformationsService
    {
        private readonly IInformationsRepository _informationsRepository;

        public InformationsService(IInformationsRepository informationsRepository)
        {
            _informationsRepository = informationsRepository;
        }

        #region Récupère les Informations depuis l'API Sirene
        public async Task<IEnumerable<Informations>> GetInformationsFromApi(string startDate, string endDate)
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

                    var informations = JsonConvert.DeserializeObject<IEnumerable<Informations>>(content);

                    return informations;
                }
                else
                {
                    // Gérer les erreurs de la requête à l'API Sirene
                    throw new Exception("Erreur lors de la récupération des établissements depuis l'API Sirene.");
                }
            }
        }
        #endregion

        #region Sauvegarde les Informations dans la base de données
        public async Task SaveInformationsToDatabase(string startDate, string endDate)
        {
            var informations = await GetInformationsFromApi(startDate, endDate);

            foreach (var information in informations)
            {
                await _informationsRepository.AddAsync(information);
            }
        }
        #endregion

        #region Exporte les Informations dans un fichier Excel
        public async Task<byte[]> ExportInformationsToExcel(ExcelPackage package)
        {
            var informations = await GetAllInformationsAsync();

            var worksheet = package.Workbook.Worksheets.Add("Informations");

            // Add headers
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "ServiceState";
            worksheet.Cells[1, 3].Value = "ServiceStates";

            // Add data
            var row = 2;
            foreach (var information in informations)
            {
                worksheet.Cells[row, 1].Value = information.Id;
                worksheet.Cells[row, 2].Value = information.ServiceState;
                worksheet.Cells[row, 3].Value = information.ServiceStates;

                row++;
            }

            return package.GetAsByteArray();
        }
        #endregion

        #region Récupère les Informations de manière asynchrone
        public async Task<IEnumerable<Informations>> GetAllInformationsAsync()
        {
            return await _informationsRepository.GetAllAsync();
        }
        #endregion

        #region Récupère les Informations par Id de manière asynchrone
        public async Task<Informations> GetInformationsByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return await _informationsRepository.GetByIdAsync(id);
        }
        #endregion

        #region Crée une Information de manière asynchrone
        public async Task<Informations> CreateInformationsAsync(Informations informations)
        {
            if (informations == null)
            {
                throw new ArgumentNullException(nameof(informations));
            }

            return await _informationsRepository.AddAsync(informations);
        }
        #endregion

        #region Met à jour une Information de manière asynchrone
        public async Task<bool> UpdateInformationsAsync(string id, Informations informations)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (informations == null)
            {
                throw new ArgumentNullException(nameof(informations));
            }

            var existingInformations = await _informationsRepository.GetByIdAsync(id);

            if (existingInformations == null)
            {
                return false;
            }

            existingInformations.ServiceState = informations.ServiceState;

            return await _informationsRepository.UpdateAsync(existingInformations);
        }
        #endregion

        #region Supprime une Information de manière asynchrone
        public async Task<bool> DeleteInformationsAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _informationsRepository.DeleteAsync(id);
        }
        #endregion

    }
}
