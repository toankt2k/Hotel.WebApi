using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Interfaces;
using MISA.Infrastructure.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Respository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public IEnumerable<Room> GetEmptyRooms()
        {
            throw new NotImplementedException();
        }
    }
}
