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
        [Required(Msg = "Tài khoản")]
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        [Required(Msg = "Email")]
        public string? Email { get; set; }
        [NotMap]
        public int? Active { get; set; }
        [NotMap]
        public string? IdentifyCode { get; set; }

    }
}
