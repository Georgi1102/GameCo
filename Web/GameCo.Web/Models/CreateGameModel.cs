using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Models
{
    public class CreateGameModel
    {
        [Required]
        public string GameName { get; set; }
    }
}
