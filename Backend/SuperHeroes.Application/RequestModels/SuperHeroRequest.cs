using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.RequestModels
{
    public class SuperHeroRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.MinValue;

        public float Altura { get; set; } = 0.00F;

        public float Peso { get; set; } = 0.00F;

        public ICollection<SuperpowerRequest> Superpoderes { get; set; }
    }
}
