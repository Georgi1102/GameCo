using GameCo.Services.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Services
{
    public interface IAchievementsService
    {
        Task<bool> CreateAchievement(AchievementServiceModel achievementServiceModel);
    }
}
