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
        private const string wwwrootFolder = "wwwroot\\DownloadableFiles";
        private bool isUploaded;
        private string filePath;


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
            
            gameCoDbContext.Entry(await gameCoDbContext.Ratings.FirstOrDefaultAsync(x => x.GameId == ratingServiceModel.GameId))
                .CurrentValues.SetValues(ratingServiceModel);
            await this.gameCoDbContext.SaveChangesAsync();
            return true;
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

        public async Task<bool> UnZipIt(string someFile)
        {
            if (Directory.Exists(wwwrootFolder))
            {
                string exractDirName = @"E:\Project_2020_2021\ZipProject\TestZipFile\WebApplication1\UploadedFiles\CubyWeb.zip";
                ZipFile.ExtractToDirectory(exractDirName, wwwrootFolder);

                return true;
            }

            return false;
        }

        public async Task<bool> Upload(IFormFile someFile)
        {
            filePath = "";
            isUploaded = false;

            try
            {
                if (someFile.Length > 0)
                {
                    string fileName = Path.GetFileName(someFile.FileName);
                    filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), wwwrootFolder));

                    //Create folder in wwwroot
                    //TODO: Specifie in wwwroot > downloadableFiles > currentFolder ; (.Combine?)
                    //Idea: We create a directory in this method because we want to take the name of the initial file. 
                    string folderName = someFile.FileName.Remove(someFile.FileName.Length - 4);
                    System.IO.Directory.CreateDirectory("\\wwwroot\\Games"+folderName);

                    //DO NOT FORGET TO CLOSE THE STREAM!!!
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await someFile.CopyToAsync(fileStream);
                        fileStream.Close();
                    }

                    isUploaded = true;

                    await this.UnZipIt(filePath);
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
