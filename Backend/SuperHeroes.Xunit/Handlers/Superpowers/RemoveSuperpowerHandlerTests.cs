using Moq;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.Superpowers;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;
using Xunit;

public class RemoveSuperpowerHandlerTests
{
    private readonly RemoveSuperpowerHandler _handler;
    private readonly Mock<ISuperpowerRepository> _mockSuperpowerRepository;

    public RemoveSuperpowerHandlerTests()
    {
        _mockSuperpowerRepository = new Mock<ISuperpowerRepository>();
        _handler = new RemoveSuperpowerHandler(_mockSuperpowerRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenSuperpowerExists_RemovesSuperpower()
    {
        // Arrange
        var superpowerId = 1;
        var superpower = new Superpoder("Super Strength", "Incredible strength");

        _mockSuperpowerRepository
            .Setup(repo => repo.GetSuperpowerByIdAsync(superpowerId))
            .ReturnsAsync(superpower);

        _mockSuperpowerRepository
            .Setup(repo => repo.RemoveSuperpowerAsync(superpower))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(superpowerId);

        // Assert
        _mockSuperpowerRepository.Verify(repo => repo.RemoveSuperpowerAsync(superpower), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenSuperpowerDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var superpowerId = 1;

        _mockSuperpowerRepository
            .Setup(repo => repo.GetSuperpowerByIdAsync(superpowerId))
            .ReturnsAsync((Superpoder)null); // Superpoder não encontrado

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(superpowerId));
    }
}
