using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface ICreateSuperpowerHandler
    {
        Task<SuperpowerResponse> Handle(SuperpowerDTO dto);
    }
}
