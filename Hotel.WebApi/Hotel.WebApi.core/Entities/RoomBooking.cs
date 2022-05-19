using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Entities
{
    public class RoomBooking
    {
        public User User { get; set; }
        
        public Guid RoomId { get; set; }
    }
}
