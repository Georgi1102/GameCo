using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Data.Models
{
    public class GameCoGames
    {        
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
