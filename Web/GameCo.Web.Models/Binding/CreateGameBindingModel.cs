using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameCo.WebModels.Binding
{
    public class CreateGameBindingModel
    {
      //  [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile file { get; set; }
    }
}
