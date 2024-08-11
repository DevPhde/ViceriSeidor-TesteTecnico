using Moq;
using SuperHeroes.Application.Handlers.SuperHeroes;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class GetAllHeroesHandlerTests
{
    private readonly Mock<IHeroRepository> _mockHeroRepository;
    private readonly GetAllHeroesHandler _handler;

    public GetAllHeroesHandlerTests()
    {
        _mockHeroRepository = new Mock<IHeroRepository>();
        _handler = new GetAllHeroesHandler(_mockHeroRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfHeroes()
    {
        // Arrange
        var heroesList = new List<Heroi>
{
    new Heroi
    {
        Id = 1,
        Nome = "Hero 1",
        NomeHeroi = "Superhero 1",
        DataNascimento = new DateTime(1990, 1, 1),
        Altura = 1.80F,
        Peso = 80.0F,
        HeroisSuperpoderes = new List<HeroiSuperpoder>
        {
            new HeroiSuperpoder { SuperpoderId = 1, Superpoderes = new Superpoder { Id = 1, SuperpoderNome = "Super Strength" } }
        }
    },
    new Heroi
    {
        Id = 2,
        Nome = "Hero 2",
        NomeHeroi = "Superhero 2",
        DataNascimento = new DateTime(1985, 5, 15),
        Altura = 1.85F,
        Peso = 85.0F,
        HeroisSuperpoderes = new List<HeroiSuperpoder>
        {
            new HeroiSuperpoder { SuperpoderId = 2, Superpoderes = new Superpoder { Id = 2, SuperpoderNome = "Flight" } }
        }
    }
};


        _mockHeroRepository.Setup(repo => repo.GetAllHeroesAsync())
            .ReturnsAsync(heroesList);

        // Act
        var result = await _handler.Handle();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Hero 1", result[0].Nome);
        Assert.Equal("Hero 2", result[1].Nome);

        _mockHeroRepository.Verify(repo => repo.GetAllHeroesAsync(), Times.Once);
    }
}
