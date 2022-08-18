using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Api.Models;
using Scheduler.Api.ObjectValue;
using Scheduler.Api.Repositories;

namespace Scheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventRepository _repositoy;

        public EventController(EventRepository repositoy)
        {
            _repositoy = repositoy;
        }

        [HttpGet]
        [Route("{room}")]
        public async Task<ActionResult<IEnumerable<EventModel>>> ListByRoom(int room)
        {
            return await _repositoy.EventByRoom(room);
        }

        [HttpGet]
        [Route("/room/{room}")]
        public async Task<ActionResult<RoomVO>> GetRoom(int room)
        {
            return await _repositoy.Room(room);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventModel>>> ListAll()
        {
            return await _repositoy.Events();
        }

        [HttpGet]
        [Route("rooms")]
        public async Task<ActionResult<IEnumerable<RoomVO>>> ListAllRooms()
        {
            return await _repositoy.Rooms();
        }

        [HttpPost]
        public async Task<ActionResult<EventModel>> PostEventModel([FromBody] EventModel eventModel)
        {

            if (eventModel.StartDate > eventModel.EndDate)
                throw new Exception("Data/hora inicial não pode ser maior que a data final");
            var dates = await _repositoy.EventRoomDate(eventModel.Id.GetValueOrDefault(), eventModel.IdRoom, eventModel.StartDate, eventModel.EndDate);
            if (dates.Count > 0)
                return StatusCode(500,
                    new {
                            StatusCode = 500, 
                            Message = "Já existem reservas para a data/hora selecionada:", 
                            Items = dates.Select(date => date.StartDate.ToString() + " à " + date.EndDate.ToString()).ToList() 
                    });
            return await _repositoy.Add(eventModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEventModel(int id)
        {
            if (!await _repositoy.Delete(id))
                return NotFound();
            return Ok();
        }


    }
}
