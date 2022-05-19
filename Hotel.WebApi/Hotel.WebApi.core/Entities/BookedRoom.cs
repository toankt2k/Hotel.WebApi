using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hotel.WebApi.core.Attributies.CustomAttribute;

namespace Hotel.WebApi.core.Entities
{
    public class BookedRoom
    {
        public Guid? BookedRoomId { get; set; }
        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public float? Price { get; set; }

        public string? Note { get; set; }

        public float? Amount { get; set; }

        public int IsCheckIn { get; set; }

        public Guid? RoomId { get; set; }

        public Guid? ClientId { get; set; }
        [NotMap]
        public Room? Room { get; set; }


    }
}
