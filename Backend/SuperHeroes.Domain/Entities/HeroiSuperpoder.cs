using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Domain.Entities
{
    public class HeroiSuperpoder
    {
        public int HeroiId { get; set; }
        public Heroi Heroi { get; set; }

        public int SuperpoderId { get; set; }
        public Superpoder Superpoderes { get; set; }
        public HeroiSuperpoder() { }
        public HeroiSuperpoder(int heroId, int superpoderId)
        {
            HeroiId = heroId;
            SuperpoderId = superpoderId;
        }
    }
}
