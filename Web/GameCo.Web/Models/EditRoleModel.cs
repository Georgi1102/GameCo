using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Models
{
    public class EditRoleModel
    {
        public EditRoleModel()
        {
            //Before this the collection was null, 2 hours of my life are gone <3
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
