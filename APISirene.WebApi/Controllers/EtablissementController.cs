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
                    var fileContents = await _etablissementService.ExportEtablissementsToExcel(package);
                    var fileName = "etablissements.xlsx";

                    return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while exporting the etablissements to Excel: {ex.Message}");
            }
        }

    }
}
