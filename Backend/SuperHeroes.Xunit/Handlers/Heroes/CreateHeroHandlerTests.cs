using Moq;
using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Handlers.SuperHeroes;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class CreateHeroHandlerTests
{
    private readonly CreateHeroHandler _handler;
    private readonly Mock<IHeroRepository> _mockHeroRepository;
    private readonly Mock<IHeroSuperpowerRepository> _mockHeroSuperpowerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public CreateHeroHandlerTests()
    {
        _mockHeroRepository = new Mock<IHeroRepository>();
        _mockHeroSuperpowerRepository = new Mock<IHeroSuperpowerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateHeroHandler(
            _mockHeroRepository.Object,
            _mockHeroSuperpowerRepository.Object,
            _mockUnitOfWork.Object
        );
    }

    [Fact]
    public async Task Handle_WhenHeroNameAlreadyExists_ThrowsConflictException()
    {
        // Arrange
        var existingHero = new Heroi("Existing Hero", "ExistingHero", new DateTime(1990, 1, 1), 1.80F, 80);
        _mockHeroRepository.Setup(repo => repo.GetHeroByHeroName("ExistingHero"))
            .ReturnsAsync(existingHero);

        var heroDto = new HeroDTO(
            id: 0,
            nome: "Existing Hero",
            nomeHeroi: "ExistingHero",
            dataNascimento: new DateTime(1990, 1, 1),
            altura: 1.80F,
            peso: 80.0F,
            superpowers: new List<SuperpowerDTO>()
        );

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(heroDto));
        _mockUnitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenHeroIsCreated_ReturnsHeroDtoWithId()
    {
        // Arrange
        var newHero = new Heroi("New Hero", "NewHero", new System.DateTime(1990, 1, 1), 1.80F, 80);
        _mockHeroRepository.Setup(repo => repo.GetHeroByHeroName("NewHero"))
            .ReturnsAsync((Heroi)null);
        _mockHeroRepository.Setup(repo => repo.AddHeroAsyncWithoutSaveChanges(It.IsAny<Heroi>()))
            .ReturnsAsync(newHero);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.Commit()).Returns(Task.CompletedTask);

        var heroDto = new HeroDTO(0, "New Hero", "NewHero", new System.DateTime(1990, 1, 1), 1.80F, 80, new List<SuperpowerDTO>());

        // Act
        var result = await _handler.Handle(heroDto);

        // Assert
        Assert.Equal(newHero.Id, result.Id);
        _mockHeroRepository.Verify(repo => repo.AddHeroAsyncWithoutSaveChanges(It.IsAny<Heroi>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Exactly(2));
        _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenExceptionOccurs_RollsBackTransaction()
    {
        // Arrange
        _mockHeroRepository.Setup(repo => repo.GetHeroByHeroName(It.IsAny<string>()))
            .ReturnsAsync((Heroi)null);
        _mockHeroRepository.Setup(repo => repo.AddHeroAsyncWithoutSaveChanges(It.IsAny<Heroi>()))
            .ThrowsAsync(new System.Exception());

        var heroDto = new HeroDTO(
            id: 0,
            nome: "New Hero",
            nomeHeroi: "NewHeroi",
            dataNascimento: DateTime.Now,
            altura: 1.80F,
            peso: 80.0F,
            superpowers: new List<SuperpowerDTO>()
        );

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _handler.Handle(heroDto));
        _mockUnitOfWork.Verify(u => u.Rollback(), Times.Once);
    }

}
