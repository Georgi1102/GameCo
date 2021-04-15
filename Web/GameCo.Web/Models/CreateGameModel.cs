using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Models
{
    public class CreateGameModel
    {
        public string Id { get; set; }

        [Required]
        public string GameName { get; set; }
    }
}
