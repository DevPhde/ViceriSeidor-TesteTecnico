using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

public class HeroMapperTests
{
    [Fact]
    public void ToDto_ShouldMapHeroRequestToHeroDTO()
    {
        // Arrange
        var heroRequest = new HeroRequest
        {
            Id = 1,
            Nome = "Hero Name",
            NomeHeroi = "Superhero Name",
            DataNascimento = new DateTime(1990, 1, 1),
            Altura = 1.85f,
            Peso = 85.0f,
            Superpoderes = new List<SuperpowerRequest>
            {
                new SuperpowerRequest { Id = 1, SuperpoderNome = "Super Speed", Descricao = "Super fast movement" },
                new SuperpowerRequest { Id = 2, SuperpoderNome = "Flight", Descricao = "Ability to fly" }
            }
        };

        // Act
        var heroDto = heroRequest.ToDto();

        // Assert
        Assert.NotNull(heroDto);
        Assert.Equal(heroRequest.Id, heroDto.Id);
        Assert.Equal(heroRequest.Nome, heroDto.Nome);
        Assert.Equal(heroRequest.NomeHeroi, heroDto.NomeHeroi);
        Assert.Equal(heroRequest.DataNascimento, heroDto.DataNascimento);
        Assert.Equal(heroRequest.Altura, heroDto.Altura);
        Assert.Equal(heroRequest.Peso, heroDto.Peso);
        Assert.Equal(heroRequest.Superpoderes.Count, heroDto.Superpowers.Count);
    }

    [Fact]
    public void ToResponse_ShouldMapHeroDTOToHeroResponse()
    {
        // Arrange
        var heroDto = new HeroDTO(1, "Hero Name", "Superhero Name", new DateTime(1990, 1, 1), 1.85f, 85.0f, new List<SuperpowerDTO>
        {
            new SuperpowerDTO(1, "Super Speed", "Super fast movement"),
            new SuperpowerDTO(2, "Flight", "Ability to fly")
        });

        // Act
        var heroResponse = heroDto.ToResponse();

        // Assert
        Assert.NotNull(heroResponse);
        Assert.Equal(heroDto.Id, heroResponse.Id);
        Assert.Equal(heroDto.Nome, heroResponse.Nome);
        Assert.Equal(heroDto.NomeHeroi, heroResponse.NomeHeroi);
        Assert.Equal(heroDto.DataNascimento, heroResponse.DataNascimento);
        Assert.Equal(heroDto.Altura, heroResponse.Altura);
        Assert.Equal(heroDto.Peso, heroResponse.Peso);
        Assert.Equal(heroDto.Superpowers.Count, heroResponse.Superpoderes.Count);
    }

    [Fact]
    public void ToResponse_ShouldMapHeroiToHeroResponse()
    {
        // Arrange
        var heroEntity = new Heroi
        {
            Id = 1,
            Nome = "Hero Name",
            NomeHeroi = "Superhero Name",
            DataNascimento = new DateTime(1990, 1, 1),
            Altura = 1.85f,
            Peso = 85.0f,
            HeroisSuperpoderes = new List<HeroiSuperpoder>
            {
                new HeroiSuperpoder(1, 1)
                {
                    Superpoderes = new Superpoder
                    {
                        Id = 1,
                        SuperpoderNome = "Super Speed",
                        Descricao = "Super fast movement"
                    }
                },
                new HeroiSuperpoder(1, 2)
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

        // Act
        var heroResponse = heroEntity.ToResponse();

        // Assert
        Assert.NotNull(heroResponse);
        Assert.Equal(heroEntity.Id, heroResponse.Id);
        Assert.Equal(heroEntity.Nome, heroResponse.Nome);
        Assert.Equal(heroEntity.NomeHeroi, heroResponse.NomeHeroi);
        Assert.Equal(heroEntity.DataNascimento, heroResponse.DataNascimento);
        Assert.Equal(heroEntity.Altura, heroResponse.Altura);
        Assert.Equal(heroEntity.Peso, heroResponse.Peso);
        Assert.Equal(heroEntity.HeroisSuperpoderes.Count, heroResponse.Superpoderes.Count);
    }
}
