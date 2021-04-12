using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Data.Models
{
    public class GameCoRating
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int RatingValue { get; set; }

        public string GameId { get; set; }
       
        public string UserId { get; set; }
       
    }
}
