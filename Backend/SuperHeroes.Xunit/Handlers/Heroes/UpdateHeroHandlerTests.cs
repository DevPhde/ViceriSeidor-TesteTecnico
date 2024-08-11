using Moq;
using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.Heroes;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class UpdateHeroHandlerTests
{
    private readonly Mock<IHeroSuperpowerRepository> _mockHeroSuperpowerRepository;
    private readonly Mock<IHeroRepository> _mockHeroRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdateHeroHandler _handler;

    public UpdateHeroHandlerTests()
    {
        _mockHeroSuperpowerRepository = new Mock<IHeroSuperpowerRepository>();
        _mockHeroRepository = new Mock<IHeroRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new UpdateHeroHandler(
            _mockHeroSuperpowerRepository.Object,
            _mockHeroRepository.Object,
            _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_HeroExists_ShouldUpdateAndReturnHero()
    {
        // Arrange
        var heroId = 1;
        var existingHero = new Heroi
        {
            Id = heroId,
            Nome = "Hero 1",
            NomeHeroi = "Superhero 1",
            DataNascimento = new DateTime(1990, 1, 1),
            Altura = 1.80f,
            Peso = 80.0f,
            HeroisSuperpoderes = new List<HeroiSuperpoder>
        {
            new HeroiSuperpoder(heroId, 1)
            {
                Superpoderes = new Superpoder
                {
                    Id = 1,
                    SuperpoderNome = "Super Speed",
                    Descricao = "Super fast movement"
                }
            },
            new HeroiSuperpoder(heroId, 2)
            {
                Superpoderes = new Superpoder
                {
                    Id = 2,
                    SuperpoderNome = "Flight",
                    Descricao = "Ability to fly"
                }
            }
        }
        };

        var heroDto = new HeroDTO(heroId, "Updated Hero", "Updated Superhero", new DateTime(1990, 1, 1), 1.85f, 85.0f, new List<SuperpowerDTO>()
    {
        new SuperpowerDTO(3, "New Superpower", "Descrição")
    });

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync(existingHero);

        _mockHeroRepository.Setup(repo => repo.GetHeroByHeroName("Updated Superhero"))
            .ReturnsAsync((Heroi)null);

        _mockHeroSuperpowerRepository.Setup(repo => repo.GetHeroSuperpowersByHeroId(heroId))
            .ReturnsAsync(new List<HeroiSuperpoder>(existingHero.HeroisSuperpoderes));

        // Act
        var result = await _handler.Handle(heroDto, heroId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(heroDto.Nome, result.Nome);
        Assert.Equal(heroDto.NomeHeroi, result.NomeHeroi);

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(heroId), Times.AtLeastOnce);
        _mockHeroSuperpowerRepository.Verify(repo => repo.GetHeroSuperpowersByHeroId(heroId), Times.Once);
        _mockHeroSuperpowerRepository.Verify(repo => repo.AddHeroSuperpowerAsyncWithoutSaveChanges(It.IsAny<List<HeroiSuperpoder>>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
    }



    [Fact]
    public async Task Handle_HeroNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var heroId = 1;
        var heroDto = new HeroDTO(heroId, "Updated Hero", "Updated Superhero", new DateTime(1990, 1, 1), 1.85f, 85.0f, new List<SuperpowerDTO>() { new SuperpowerDTO(3, "New Superpower", "Descrição") });

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync((Heroi)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(heroDto, heroId));

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(heroId), Times.Once);
    }



    [Fact]
    public async Task Handle_ConflictingHeroName_ShouldThrowConflictException()
    {
        // Arrange
        var heroId = 1;
        var existingHero = new Heroi { Id = heroId, NomeHeroi = "OriginalName", HeroisSuperpoderes = new List<HeroiSuperpoder>() };
        var conflictingHero = new Heroi { Id = 2, NomeHeroi = "Updated Superhero", HeroisSuperpoderes = new List<HeroiSuperpoder>() };

        var heroDto = new HeroDTO(heroId, "Updated Hero", "Updated Superhero", new DateTime(1990, 1, 1), 1.85f, 85.0f, new List<SuperpowerDTO>() { new SuperpowerDTO(3, "New Superpower", "Descrição") });

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync(existingHero);

        // Simule um herói existente com o mesmo nome de herói que está sendo atualizado
        _mockHeroRepository.Setup(repo => repo.GetHeroByHeroName("Updated Superhero"))
            .ReturnsAsync(conflictingHero);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(heroDto, heroId));

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(heroId), Times.Once);
        _mockHeroRepository.Verify(repo => repo.GetHeroByHeroName("Updated Superhero"), Times.Once);
    }


    [Fact]
    public async Task Handle_ExceptionOccurs_ShouldRollbackTransaction()
    {
        // Arrange
        var heroId = 1;
        var heroDto = new HeroDTO(heroId, "Updated Hero", "Updated Superhero", new DateTime(1990, 1, 1), 1.85f, 85.0f, new List<SuperpowerDTO>() { new SuperpowerDTO(3, "New Superpower", "Descrição") });

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
    .ThrowsAsync(new Exception("Test exception"));


        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(heroDto, heroId));

        _mockUnitOfWork.Verify(uow => uow.Rollback(), Times.Once);
    }
}
