using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace APISirene.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementController : ControllerBase
    {
        private readonly IEtablissementService _etablissementService;

        public EtablissementController(IEtablissementService etablissementService)
        {
            _etablissementService = etablissementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEtablissements()
        {
            try
            {
                var etablissements = await _etablissementService.GetAllEtablissementAsync();
                return Ok(etablissements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the etablissements: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEtablissementById(string id)
        {
            try
            {
                var etablissement = await _etablissementService.GetEtablissementByIdAsync(id);
                if (etablissement == null)
                {
                    return NotFound();
                }
                return Ok(etablissement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the etablissement: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEtablissement(Etablissement etablissement)
        {
            try
            {
                var createdEtablissement = await _etablissementService.CreateEtablissementAsync(etablissement);
                return CreatedAtAction(nameof(GetEtablissementById), new { id = createdEtablissement.Id }, createdEtablissement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the etablissement: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEtablissement(string id, Etablissement etablissement)
        {
            try
            {
                var updated = await _etablissementService.UpdateEtablissementAsync(id, etablissement);
                if (updated)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the etablissement: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtablissement(string id)
        {
            try
            {
                var deleted = await _etablissementService.DeleteEtablissementAsync(id);
                if (deleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the etablissement: {ex.Message}");
            }
        }

        [HttpGet("export-to-excel")]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    // Récupérer les établissements à exporter depuis le service
                    var etablissements = await _etablissementService.GetAllEtablissementAsync();

                    // Vérifier s'il y a des établissements à exporter
                    if (etablissements != null && etablissements.Any())
                    {
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

                        // Save the Excel package to a byte array
                        var fileContents = package.GetAsByteArray();
                        var fileName = "etablissements.xlsx";

                        // Return the Excel file as a FileContentResult
                        return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                    else
                    {
                        // Handle the case when no data is available to export
                        return NotFound("No data available to export.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while exporting the etablissements to Excel: {ex.Message}");
            }
        }


    }
}
