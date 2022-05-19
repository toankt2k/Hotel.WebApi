using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Interfaces;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Services
{
    public class RoomService : BaseService<Room>, IRoomService
    {
        IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository) : base(roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<Room> GetEmptyRooms()
        {
            var res = _roomRepository.GetEmptyRooms();
            return res;
        }
    }
}
