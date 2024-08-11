using System.ComponentModel.DataAnnotations;

namespace SuperHeroes.Application.RequestModels
{
    public class SuperpowerRequest
    {
        public int Id { get; set; } = 0;
        [Required]
        public string SuperpoderNome { get; set; } = string.Empty;
        [Required]
        public string Descricao { get; set; } = string.Empty;

    }
}
