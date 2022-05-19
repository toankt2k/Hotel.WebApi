using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace Hotel.WebApi.core.Entities
{
    public class User
    {
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
        public int? Active { get; set; }
        public string? Code { get; set; }

    }
}
