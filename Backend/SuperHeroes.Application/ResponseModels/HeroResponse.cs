using SuperHeroes.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperHeroes.Application.ResponseModels
{
    public class HeroResponse
    {
        public HeroResponse(int? id, string nome, string nomeHeroi, DateTime? dataNascimento, float altura, float peso, ICollection<SuperpowerResponse> superpoderes)
        {
            Id = id;
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
            Superpoderes = superpoderes;
        }
        public HeroResponse()
        {

        }

        public int? Id { get; set; } = null;
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.MinValue;

        public float Altura { get; set; } = 0.00F;

        public float Peso { get; set; } = 0.00F;
        public ICollection<SuperpowerResponse> Superpoderes { get; set; }
    }
}
