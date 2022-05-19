using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Entities
{
    public class Room
    {
        public Guid? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomType { get; set; }
        public string? RoomPrice { get; set; }

        public string? RoomImg { get; set; }
        public string? RoomDescription { get; set; }
    }
}
