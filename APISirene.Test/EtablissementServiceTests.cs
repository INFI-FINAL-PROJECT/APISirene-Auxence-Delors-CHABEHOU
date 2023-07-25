using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using APISirene.Domain.Services;
using System.Net;

namespace APISirene.Test
{
   [TestClass]
    public class EtablissementServiceTests
    {
        private Mock<IEtablissementRepository> _etablissementRepositoryMock;
        private Mock<HttpClient> _httpClientMock;
        private IEtablissementService _etablissementService;

        [TestInitialize]
        public void Initialize()
        {
            _etablissementRepositoryMock = new Mock<IEtablissementRepository>();
            _httpClientMock = new Mock<HttpClient>();
            _etablissementService = new EtablissementService(_etablissementRepositoryMock.Object, _httpClientMock.Object);
        }

        [TestMethod]
        public async Task GetAllEtablissementAsync_ReturnsListOfEtablissements()
        {
            // Arrange
            var expectedEtablissements = new List<Etablissement>
            {
                new Etablissement { Id = "1", Score = 10, Siren = "123456789" },
                new Etablissement { Id = "2", Score = 20, Siren = "987654321" }
            };

            _etablissementRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedEtablissements);

            // Act
            var etablissements = await _etablissementService.GetAllEtablissementAsync();

            // Assert
            CollectionAssert.AreEqual(expectedEtablissements, new List<Etablissement>(etablissements));
        }

        [TestMethod]
        public async Task GetEtablissementByIdAsync_ValidId_ReturnsEtablissement()
        {
            // Arrange
            var expectedEtablissement = new Etablissement { Id = "1", Score = 10, Siren = "123456789" };

            _etablissementRepositoryMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(expectedEtablissement);

            // Act
            var etablissement = await _etablissementService.GetEtablissementByIdAsync("1");

            // Assert
            Assert.AreEqual(expectedEtablissement, etablissement);
        }

        [TestMethod]
        public async Task CreateEtablissementAsync_ValidEtablissement_ReturnsCreatedEtablissement()
        {
            // Arrange
            var etablissementToCreate = new Etablissement { Score = 10, Siren = "123456789" };
            var expectedCreatedEtablissement = new Etablissement { Id = "1", Score = 10, Siren = "123456789" };

            _etablissementRepositoryMock.Setup(r => r.AddAsync(etablissementToCreate)).ReturnsAsync(expectedCreatedEtablissement);

            // Act
            var createdEtablissement = await _etablissementService.CreateEtablissementAsync(etablissementToCreate);

            // Assert
            Assert.AreEqual(expectedCreatedEtablissement, createdEtablissement);
        }

        [TestMethod]
        public async Task UpdateEtablissementAsync_ValidIdAndEtablissement_ReturnsTrue()
        {
            // Arrange
            var existingEtablissement = new Etablissement { Id = "1", Score = 10, Siren = "123456789" };
            var updatedEtablissement = new Etablissement { Id = "1", Score = 20, Siren = "123456789" };

            _etablissementRepositoryMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(existingEtablissement);
            _etablissementRepositoryMock.Setup(r => r.UpdateAsync(existingEtablissement)).ReturnsAsync(true);

            // Act
            var isUpdated = await _etablissementService.UpdateEtablissementAsync("1", updatedEtablissement);

            // Assert
            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public async Task UpdateEtablissementAsync_NullEtablissement_ThrowsException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _etablissementService.UpdateEtablissementAsync("1", null));
        }

        [TestMethod]
        public async Task DeleteEtablissementAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            _etablissementRepositoryMock.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

            // Act
            var isDeleted = await _etablissementService.DeleteEtablissementAsync("1");

            // Assert
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public async Task GetEtablissementsFromApi_ReturnsListOfEtablissementsFromApi()
        {
            // Arrange
            var expectedEtablissements = new List<Etablissement>
            {
                new Etablissement { Id = "1", Score = 10, Siren = "123456789" },
                new Etablissement { Id = "2", Score = 20, Siren = "987654321" }
            };
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(expectedEtablissements);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content),
            };

            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<System.Net.Http.HttpCompletionOption>(), It.IsAny<CancellationToken>())).ReturnsAsync(responseMessage);

            // Act
            var etablissements = await _etablissementService.GetEtablissementsFromApi();

            // Assert
            CollectionAssert.AreEqual(expectedEtablissements, new List<Etablissement>(etablissements));
        }

        [TestMethod]
        public async Task SaveEtablissementsToDatabase_SuccessfullySavesEtablissements()
        {
            // Arrange
            var expectedEtablissements = new List<Etablissement>
            {
                new Etablissement { Id = "1", Score = 10, Siren = "123456789" },
                new Etablissement { Id = "2", Score = 20, Siren = "987654321" }
            };
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(expectedEtablissements))
                });

            _etablissementRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Etablissement>())).Returns(Task.FromResult(new Etablissement()));

            // Act
            await _etablissementService.SaveEtablissementsToDatabase();

            // Assert
            foreach (var etablissement in expectedEtablissements)
            {
                _etablissementRepositoryMock.Verify(r => r.AddAsync(etablissement), Times.Once);
            }
        }
    }
}
