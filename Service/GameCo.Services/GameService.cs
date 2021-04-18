using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services.Models.Games;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;

namespace GameCo.Services
{
    public class GameService : IGamesService
    {
        private readonly GameCoDbContext gameCoDbContext;
        private readonly IMappingService mappingService;
        private const string wwwrootFolder = @"D:\Projects\Web\GameCo\Web\GameCo.Web\wwwroot\Games\";
        private const string imagesRoot = @"D:\Projects\Web\GameCo\Web\GameCo.Web\wwwroot\Images\";
        private bool isUploaded;
        private string gamefilePath;
        private string imagefilePath;


        public GameService(GameCoDbContext gameCoDbContext, IMappingService mappingService)
        {
            this.gameCoDbContext = gameCoDbContext;
            this.mappingService = mappingService;

        }

        public async Task<bool> CreateGame(GameServiceModel gameServiceModel)
        {
            GameCoGames game = this.mappingService.MapOject<GameCoGames>(gameServiceModel);

            game.Id = Guid.NewGuid().ToString();

            bool result = await this.gameCoDbContext.AddAsync(game) != null;
            await this.gameCoDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> CreateRating(RatingServiceModel ratingServiceModel)
        {
            GameCoRating rating = this.mappingService.MapOject<GameCoRating>(ratingServiceModel);

            rating.Id = Guid.NewGuid().ToString();
            bool result = await this.gameCoDbContext.AddAsync(rating) != null;
            await this.gameCoDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateRating(RatingServiceModel ratingServiceModel)
        {
            var wantedRating = gameCoDbContext.Ratings.Where(x => x.GameId == ratingServiceModel.GameId);

            var rating = wantedRating.FirstOrDefault(x => x.UserId == ratingServiceModel.UserId);


            if (rating.RatingValue != ratingServiceModel.RatingValue && rating.UserId == ratingServiceModel.UserId)
            {
                rating.RatingValue = ratingServiceModel.RatingValue;

                this.gameCoDbContext.Update(rating);
                await gameCoDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public string FindGameById(string id)
        {
            var wantedId = gameCoDbContext.Games.FirstOrDefault(x => x.Id == id);

            return wantedId.Id;
        }

        public List<GameServiceModel> GetAllGameEntities()
        {
            var result = gameCoDbContext.Games.Select(x => mappingService.MapOject<GameServiceModel>(x));

            List<GameServiceModel> list = new List<GameServiceModel>();

            foreach (var rating in result)
            {
                list.Add(rating);
            }

            return list;
        }

        public List<RatingServiceModel> GetAllRatingEntities(string userId)
        {
            var ratings = gameCoDbContext.Ratings.Select(x => mappingService.MapOject<RatingServiceModel>(x));
            var wanted = ratings.Where(x => x.UserId == userId);
            List<RatingServiceModel> list = new List<RatingServiceModel>();
            
 
            foreach (var rating in ratings)
            {
                if (rating.UserId == userId)
                {
                    list.Add(rating);
                }

            }
            return list;
        }

        public async Task<bool> UnZipIt(IFormFile someFile)
        {
            if (Directory.Exists(wwwrootFolder))
            {

                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), wwwrootFolder));
                string extractDir = filePath + someFile.FileName;
               
                ZipFile.ExtractToDirectory(extractDir, wwwrootFolder);

                return true;
            }

            return false;
        }

        public async Task<bool> Upload(IFormFile gameFile, IFormFile imageFile)
        {
            gamefilePath = "";
            imagefilePath = "";
            isUploaded = false;

            try
            {
                if (gameFile.Length > 0 && imageFile.Length > 0)
                {
                    string gameFileName = Path.GetFileName(gameFile.FileName);
                    string imageFileName = Path.GetFileName(imageFile.FileName);

                    gamefilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), wwwrootFolder));
                    imagefilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), imagesRoot));

                    //Create folder in wwwroot
                    //TODO: Specifie in wwwroot > downloadableFiles > currentFolder ; (.Combine?)
                    //Idea: We create a directory in this method because we want to take the name of the initial file. 

                   

                    //DO NOT FORGET TO CLOSE THE STREAM!!!
                    //fileStream for the game
                    using (var fileStream = new FileStream(Path.Combine(gamefilePath, gameFileName), FileMode.Create))
                    {
                        await gameFile.CopyToAsync(fileStream);
                        fileStream.Close();
                    }


                    //fileStream for the image
                    using (var fileStream = new FileStream(Path.Combine(imagefilePath, imageFileName), FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                        fileStream.Close();
                    }

                    isUploaded = true;

                    await this.UnZipIt(gameFile);
                }

                else
                {
                    isUploaded = false;
                }
            }
            catch (Exception)
            {

                throw new FileNotFoundException();
            }

            return isUploaded;
        }


    }


}
