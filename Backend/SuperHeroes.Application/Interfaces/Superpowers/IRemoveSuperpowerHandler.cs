using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.Superpowers
{
    public interface IRemoveSuperpowerHandler
    {
        Task Handle(int superpowerId);
    }
}
