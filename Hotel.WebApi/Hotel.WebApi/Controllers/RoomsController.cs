using Hotel.WebApi.core.Entities;
using Hotel.WebApi.core.Exceptions;
using Hotel.WebApi.core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hotel.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : CustomBaseController<Room>
    {
        private IRoomService _roomService;
        public RoomsController(IRoomService roomService) : base(roomService)
        {
            _roomService = roomService;
        }


        [HttpGet("EmptyRoom")]
        public IActionResult GetEmptyRooms()
        {
            try
            {
                var res = _roomService.GetEmptyRooms();
                return Ok(res);
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
