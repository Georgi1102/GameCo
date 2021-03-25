using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameCo.Web.Models
{
    public class AchievementViewModel
    {
        [DisplayName("GameId")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameId { get; set; }
        public List<SelectListItem> ListofAchievements { get; set; }

    }
}
