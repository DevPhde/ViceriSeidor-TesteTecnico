namespace SuperHeroes.Application.DTOs
{
    public class SuperpowerDTO
    {
        public SuperpowerDTO()
        {

        }

        public SuperpowerDTO(int id, string superpoderNome, string descricao)
        {
            Id = id;
            SuperpoderNome = superpoderNome;
            Descricao = descricao;
        }

        public int Id { get; set; } = 0;
        public string SuperpoderNome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
