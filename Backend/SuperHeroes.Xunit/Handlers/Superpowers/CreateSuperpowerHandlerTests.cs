using FluentAssertions;
using Moq;
using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.Superpowers;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace SuperHeroes.Tests.Handlers
{
    public class CreateSuperpowerHandlerTests
    {
        private readonly Mock<ISuperpowerRepository> _mockSuperpowerRepository;
        private readonly CreateSuperpowerHandler _handler;

        public CreateSuperpowerHandlerTests()
        {
            _mockSuperpowerRepository = new Mock<ISuperpowerRepository>();
            _handler = new CreateSuperpowerHandler(_mockSuperpowerRepository.Object);
        }

        [Fact]
        public async Task Handle_WhenSuperpowerAlreadyExists_ShouldThrowConflictException()
        {
            // Arrange
            var existingSuperpower = new Superpoder("Super Strength", "Incredible strength");
            _mockSuperpowerRepository
                .Setup(repo => repo.GetSuperpowerByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(existingSuperpower);

            var superpowerDto = new SuperpowerDTO { SuperpoderNome = "Super Strength", Descricao = "Incredible strength" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(superpowerDto));
            Assert.Equal("Superpoder já cadastrado com esse nome.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenSuperpowerDoesNotExist_ShouldCreateAndReturnSuperpowerResponse()
        {
            // Arrange
            _mockSuperpowerRepository
                .Setup(repo => repo.GetSuperpowerByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Superpoder)null);

            _mockSuperpowerRepository
                .Setup(repo => repo.AddSuperpowerAsync(It.IsAny<Superpoder>()))
                .Callback<Superpoder>(sp => sp.Id = 1);

            var superpowerDto = new SuperpowerDTO { SuperpoderNome = "Super Strength", Descricao = "Incredible strength" };

            // Act
            var result = await _handler.Handle(superpowerDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.SuperpoderNome.Should().Be("Super Strength");
            result.Descricao.Should().Be("Incredible strength");
        }
    }
}
