using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroes.Application.RequestModels
{
    public class HeroRequest
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string NomeHeroi { get; set; } = string.Empty;
        [Required]
        public DateTime? DataNascimento { get; set; } = DateTime.MinValue;
        [Required]
        public float Altura { get; set; } = 0.00F;
        [Required]
        public float Peso { get; set; } = 0.00F;
        [Required]
        public ICollection<SuperpowerRequest> Superpoderes { get; set; }
    }
}
