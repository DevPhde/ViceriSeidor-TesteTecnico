using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using Xunit;

public class SuperpowerMapperTests
{
    [Fact]
    public void ToDto_ShouldMapSuperpowerRequestToSuperpowerDTO()
    {
        // Arrange
        var request = new SuperpowerRequest
        {
            Id = 1,
            SuperpoderNome = "Super Speed",
            Descricao = "Super fast movement"
        };

        // Act
        var dto = request.ToDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(request.Id, dto.Id);
        Assert.Equal(request.SuperpoderNome, dto.SuperpoderNome);
        Assert.Equal(request.Descricao, dto.Descricao);
    }

    [Fact]
    public void ToResponse_ShouldMapSuperpowerDTOToSuperpowerResponse()
    {
        // Arrange
        var dto = new SuperpowerDTO
        {
            Id = 1,
            SuperpoderNome = "Super Speed",
            Descricao = "Super fast movement"
        };

        // Act
        var response = dto.ToResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(dto.Id, response.Id);
        Assert.Equal(dto.SuperpoderNome, response.SuperpoderNome);
        Assert.Equal(dto.Descricao, response.Descricao);
    }

    [Fact]
    public void ToResponse_ShouldMapSuperpoderEntityToSuperpowerResponse()
    {
        // Arrange
        var entity = new Superpoder
        {
            Id = 1,
            SuperpoderNome = "Super Speed",
            Descricao = "Super fast movement"
        };

        // Act
        var response = entity.ToResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(entity.Id, response.Id);
        Assert.Equal(entity.SuperpoderNome, response.SuperpoderNome);
        Assert.Equal(entity.Descricao, response.Descricao);
    }
}
