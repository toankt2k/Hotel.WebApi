﻿using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomsController : CustomBaseController<Room>
    {
        public RoomsController(IRoomService roomService) : base(roomService)
        {
        }
    }
}