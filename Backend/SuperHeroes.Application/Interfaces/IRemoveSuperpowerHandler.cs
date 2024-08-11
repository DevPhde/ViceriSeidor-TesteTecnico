using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface IRemoveSuperpowerHandler
    {
        Task Handle(int superPowerId);
    }
}
