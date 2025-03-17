using Moq;
using NextHome.Application.UseCases.Properties;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;

namespace NextHome.Application.Tests.UseCases.Properties
{
    public class CreatePropertyTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnPropertyId_WhenPropertyIsCreated()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Property>>();
            var property = new Property
            {
                Id = 1,
                Bathrooms = 1,
                Category = Domain.Enums.ListingCategory.ForRent,
                AddressId = 1,
                Address = new PropertyAddress
                {
                    Id = 1,
                    City = "Test",
                    MinutesToStation = 1,
                    NearestStation = "Test",
                    PostalCode = "Test",
                    Prefecture = "Test",
                    Street = "Test"
                },
                Price = 250000
            };

            // repository mock
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            var useCase = new CreatePropertyUseCase(mockRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(property);

            // Assert
            Assert.Equal(1, result); 
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()), Times.Once); // Verifica se o método foi chamado uma vez
        }
    }
}
