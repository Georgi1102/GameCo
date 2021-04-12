using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Data.Models
{
    public class GameCoFavorites
    {
        public int Id { get; set; }

        public string GameId { get; set; }

        public string UserId { get; set; }
    }
}
