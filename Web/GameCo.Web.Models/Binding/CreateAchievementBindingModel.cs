using System;
using System.Collections.Generic;
using System.Text;

namespace GameCo.WebModels.Binding
{
   public class CreateAchievementBindingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameId { get; set; }
    }
}
