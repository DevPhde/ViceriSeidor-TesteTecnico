using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.DTOs
{
    public class HeroDTO
    {
        public HeroDTO(int? id, string nome, string nomeHeroi, DateTime? dataNascimento, float altura, float peso)
        {
            Id = Id;
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }

        public int? Id { get; set; } = null;
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.MinValue;

        public float Altura { get; set; } = 0.00F;

        public float Peso { get; set; } = 0.00F;
    }
}
