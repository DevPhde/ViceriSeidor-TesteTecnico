using Microsoft.AspNetCore.SignalR;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperHeroes.API.HubConfig
{
    public class HeroesHub : Hub, IHeroesHub
    {
        private readonly IHubContext<HeroesHub> _context;
        private IGetAllHeroesHandler _getAllHeroesHandler;
        public HeroesHub(IHubContext<HeroesHub> context, IGetAllHeroesHandler getAllHeroesHandler)
        {
            _context = context;
            _getAllHeroesHandler = getAllHeroesHandler;
        }

        public async Task SendHeroes()
        {
            List<HeroResponse> response = await _getAllHeroesHandler.Handle();
            await _context.Clients.All.SendAsync("SendHeroes", response);
        }
    }
}
