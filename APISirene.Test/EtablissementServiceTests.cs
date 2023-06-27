using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Models;
using APISirene.Domain.Services;
using Moq;

namespace APISirene.Test
{
        [TestClass]
        public class EtablissementServiceTests
        {
            private Mock<IEtablissementRepository> _etablissementRepositoryMock;
            private IEtablissementService _etablissementService;

            [TestInitialize]
            public void Initialize()
            {
                _etablissementRepositoryMock = new Mock<IEtablissementRepository>();
                _etablissementService = new EtablissementService(_etablissementRepositoryMock.Object);
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
                Assert.IsNotNull(etablissements);
                Assert.AreEqual(expectedEtablissements, etablissements);
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
                Assert.IsNotNull(etablissement);
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
                Assert.IsNotNull(createdEtablissement);
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
            public async Task UpdateEtablissementAsync_InvalidId_ThrowsException()
            {
                // Act & Assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _etablissementService.UpdateEtablissementAsync(null, new Etablissement()));
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _etablissementService.UpdateEtablissementAsync("", new Etablissement()));
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
            public async Task DeleteEtablissementAsync_InvalidId_ThrowsException()
            {
                // Act & Assert
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _etablissementService.DeleteEtablissementAsync(null));
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _etablissementService.DeleteEtablissementAsync(""));
            }
        }
    }