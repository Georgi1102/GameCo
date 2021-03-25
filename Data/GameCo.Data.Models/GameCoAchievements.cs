using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Data.Models
{
    public class GameCoAchievements
    {
       

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string GameId { get; set; }
    }
}
