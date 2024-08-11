using System;
using System.Collections.Generic;

namespace SuperHeroes.Domain.Entities
{
    public class Heroi : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.MinValue;

        public float Altura { get; set; } = 0.00F;

        public float Peso { get; set; } = 0.00F;

        public ICollection<HeroiSuperpoder> HeroisSuperpoderes { get; set; }

        public Heroi(string nome, string nomeHeroi, DateTime? dataNascimento, float altura, float peso)
        {
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }
        public Heroi() { }
    }
}
