using Moq;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.Heroes;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class RemoveHeroHandlerTests
{
    private readonly Mock<IHeroSuperpowerRepository> _mockHeroSuperpowerRepository;
    private readonly Mock<IHeroRepository> _mockHeroRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly RemoveHeroHandler _handler;

    public RemoveHeroHandlerTests()
    {
        _mockHeroSuperpowerRepository = new Mock<IHeroSuperpowerRepository>();
        _mockHeroRepository = new Mock<IHeroRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new RemoveHeroHandler(
            _mockHeroSuperpowerRepository.Object,
            _mockUnitOfWork.Object,
            _mockHeroRepository.Object
        );
    }

    [Fact]
    public async Task Handle_HeroExists_ShouldRemoveHeroAndCommitTransaction()
    {
        // Arrange
        var heroId = 1;
        var existingHero = new Heroi
        {
            Id = heroId,
            Nome = "Hero 1",
            NomeHeroi = "Superhero 1",
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

        var heroSuperpowers = new List<HeroiSuperpoder>
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
    };

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync(existingHero);

        _mockHeroSuperpowerRepository.Setup(repo => repo.GetHeroSuperpowersByHeroId(heroId))
            .ReturnsAsync(heroSuperpowers);

        // Act
        await _handler.Handle(heroId);

        // Assert
        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(heroId), Times.Once);
        _mockHeroRepository.Verify(repo => repo.RemoveHeroWithoutSaveChanges(existingHero), Times.Once);
        _mockHeroSuperpowerRepository.Verify(repo => repo.RemoveHeroSuperpowersByIdsWithoutSaveChanges(heroSuperpowers), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_HeroNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var heroId = 1;

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync((Heroi)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(heroId));

        _mockHeroRepository.Verify(repo => repo.GetHeroByIdAsync(heroId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
        _mockUnitOfWork.Verify(uow => uow.Rollback(), Times.Once);
    }

    [Fact]
    public async Task Handle_ExceptionOccurs_ShouldRollbackTransaction()
    {
        // Arrange
        var heroId = 1;
        var existingHero = new Heroi
        {
            Id = heroId,
            Nome = "Hero 1",
            NomeHeroi = "Superhero 1",
            HeroisSuperpoderes = new List<HeroiSuperpoder>()
        };

        _mockHeroRepository.Setup(repo => repo.GetHeroByIdAsync(heroId))
            .ReturnsAsync(existingHero);

        _mockHeroSuperpowerRepository.Setup(repo => repo.GetHeroSuperpowersByHeroId(heroId))
            .ThrowsAsync(new Exception("Test exception"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(heroId));

        _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Rollback(), Times.Once);
    }
}
