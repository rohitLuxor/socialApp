using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApp.DAL
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
