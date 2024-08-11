using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroes.Domain.Entities
{
    public class Superpoder : Entity
    {
        [Column("Superpoder")]
        public string SuperpoderNome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public ICollection<HeroiSuperpoder> HeroisSuperpoderes { get; set; }

        public Superpoder() { }

        public Superpoder(string superpoderValue, string descricao)
        {
            SuperpoderNome = superpoderValue;
            Descricao = descricao;
        }
    }
}
