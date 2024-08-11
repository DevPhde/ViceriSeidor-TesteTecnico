using Moq;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.SuperHeroes;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Xunit;

public class GetHeroByIdHandlerTests
{
    private readonly Mock<IHeroRepository> _mockHeroRepository;
    private readonly GetHeroByIdhandler _handler;

    public GetHeroByIdHandlerTests()
    {
        _mockHeroRepository = new Mock<IHeroRepository>();
        _handler = new GetHeroByIdhandler(_mockHeroRepository.Object);
    }

    [Fact]
    public async Task Handle_HeroExists_ShouldReturnHero()
    {
        // Arrange
        var hero = new Heroi
        {
            Id = 1,
            Nome = "Hero 1",
            NomeHeroi = "Superhero 1",
            DataNascimento = new DateTime(1980, 1, 1),
            Altura = 1.80F,
            Peso = 75.0F,
            HeroisSuperpoderes = new List<HeroiSuperpoder>
        {
            new HeroiSuperpoder { SuperpoderId = 1, Superpoderes = new Superpoder { Id = 1, SuperpoderNome = "Super Strength" } }
        }
        };

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(hero.Id))
            .ReturnsAsync(hero);

        // Act
        var result = await _handler.Handle(hero.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hero.Id, result.Id);
        Assert.Equal(hero.Nome, result.Nome);
        Assert.Equal(hero.NomeHeroi, result.NomeHeroi);

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(hero.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_HeroDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var nonExistentHeroId = 99;

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(nonExistentHeroId))
            .ReturnsAsync((Heroi)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(nonExistentHeroId));

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(nonExistentHeroId), Times.Once);
    }
}
