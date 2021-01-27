using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.Model.Admin
{
    public class GetEntityModel
    {
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        public bool IsShown { get; set; }
    }
}
