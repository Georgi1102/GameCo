using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.WebModels.Binding
{
    public class CreateRatingBindingModel
    {
        //public string Id { get; set; }

        public int RatingValue { get; set; }

        public string GameId { get; set; }

        public string UserId { get; set; }

    }
}
