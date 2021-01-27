using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.DAL.Tables
{
    public class DynamicTable
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public int UserId { get; set; }
    }
}
