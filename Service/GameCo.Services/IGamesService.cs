using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameCo.Data.Models;
using GameCo.Services.Models.Games;
using Microsoft.AspNetCore.Http;

namespace GameCo.Services
{
    public interface IGamesService
    {
        Task<bool> CreateGame(GameServiceModel gameServiceModel);
        Task<bool> CreateRating(RatingServiceModel ratingServiceModel);
        Task<bool> UpdateRating(RatingServiceModel ratingServiceModel);
        string FindGameById(string id);
        public List<GameServiceModel> GetAllGameEntities();
        public List<RatingServiceModel> GetAllRatingEntities(string userId);

        Task<bool> UnZipIt(IFormFile someFile);

        Task<bool> Upload(IFormFile gameFile, IFormFile imageFile);
    }
}
