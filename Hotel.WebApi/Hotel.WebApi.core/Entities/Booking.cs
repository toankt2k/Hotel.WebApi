using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace Hotel.WebApi.core.Entities
{
    public class Booking
    {
        public Guid? BookingId { get; set; }

        public DateTime? BookDay { get; set; }

        public float? TotalAmount { get; set; }  

        public string? Note { get; set; }

        [NotMap]
        public Client? Client { get; set; }

        [NotMap]
        public List<BookedRoom> Rooms { get; set; }

        [NotMap]
        public User? User { get; set; }
    }
}
