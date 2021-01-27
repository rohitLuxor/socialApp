using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.DAL.Tables
{
    public class EntityTable : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EntityName { get; set; }
        [Required]
        public string EntityType { get; set; }
        [Required]
        public bool IsShown { get; set; }
    }
}
