using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.Model.Admin
{
    public class AddEntityModel
    {
        [Required]
        public string EntityName { get; set; }
        [Required]
        public string EntityType { get; set; }
    }
}
