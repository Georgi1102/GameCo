using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameCo.Services.Models.Games;

namespace GameCo.Services
{
    public interface IGamesService
    {
        Task<bool> CreateGame(GameServiceModel gameServiceModel);
    }
}
