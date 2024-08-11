using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.ResponseModels;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.Superpowers
{
    public interface ICreateSuperpowerHandler
    {
        Task<SuperpowerResponse> Handle(SuperpowerDTO dto);
    }
}
