using Moq;
using SuperHeroes.Application.Handlers.Superpowers;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class GetAllSuperpowersHandlerTests
{
    private readonly GetAllSuperpowersHandler _handler;
    private readonly Mock<ISuperpowerRepository> _mockSuperpowerRepository;

    public GetAllSuperpowersHandlerTests()
    {
        _mockSuperpowerRepository = new Mock<ISuperpowerRepository>();
        _handler = new GetAllSuperpowersHandler(_mockSuperpowerRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenSuperpowersExist_ReturnsListOfSuperpowerResponse()
    {
        // Arrange
        var superpowers = new List<Superpoder>
        {
            new Superpoder("Super Strength", "Incredible strength"),
            new Superpoder("Flight", "Ability to fly")
        };

        _mockSuperpowerRepository
            .Setup(repo => repo.GetAllSuperpowersAsync())
            .ReturnsAsync(superpowers);

        // Act
        var result = await _handler.Handle();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SuperpowerResponse>>(result);
        Assert.Equal(superpowers.Count, result.Count);
        Assert.Equal("Super Strength", result[0].SuperpoderNome);
        Assert.Equal("Incredible strength", result[0].Descricao);
        Assert.Equal("Flight", result[1].SuperpoderNome);
        Assert.Equal("Ability to fly", result[1].Descricao);
    }

    [Fact]
    public async Task Handle_WhenNoSuperpowersExist_ReturnsEmptyList()
    {
        // Arrange
        _mockSuperpowerRepository
            .Setup(repo => repo.GetAllSuperpowersAsync())
            .ReturnsAsync(new List<Superpoder>());

        // Act
        var result = await _handler.Handle();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SuperpowerResponse>>(result);
        Assert.Empty(result);
    }
}
