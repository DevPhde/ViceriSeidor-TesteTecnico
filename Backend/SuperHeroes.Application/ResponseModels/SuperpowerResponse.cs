namespace SuperHeroes.Application.ResponseModels
{
    public class SuperpowerResponse
    {
        public SuperpowerResponse(int id, string superpoderNome, string descricao)
        {
            Id = id;
            SuperpoderNome = superpoderNome;
            Descricao = descricao;
        }
        public SuperpowerResponse()
        {

        }
        public int Id { get; set; } = 0;
        public string SuperpoderNome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
