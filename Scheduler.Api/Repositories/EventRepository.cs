using Microsoft.EntityFrameworkCore;
using Scheduler.Api.Models;
using Scheduler.Api.ObjectValue;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Api.Repositories
{
    public class EventRepository
    {

        private readonly SchedulerDbContext _context;

        public EventRepository(SchedulerDbContext context)
        { 
            _context = context;
        }

        public async Task<EventModel> Add(EventModel ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<bool> Delete(int id)
        {
            var ev = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (ev == null)
                return false;
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EventModel>> EventByRoom(int room)
        {
            return await _context
                .Events
                .Where(e => e.IdRoom == room)
                .ToListAsync();
        }

        public async Task<EventModel> EventById(int id)
        {
            return await _context
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EventModel>> Events()
        {
            return await _context
                .Events
                .ToListAsync();
        }

        public async Task<List<RoomVO>> Rooms()
        {
            var rooms = await _context
                .Rooms
                .Include(r => r.Events)
                .ToListAsync();
            var ret = new List<RoomVO>();
            rooms.ForEach(room =>
                ret.Add(
                    new RoomVO()
                    {
                        Id = room.Id,
                        Name = room.Name,
                        Scheduling = room
                        .Events
                        .Count > 0
                    })
            );
            return ret;
        }

        public async Task<RoomVO> Room(int idRoom)
        {
            var teste = System.DateTime.UtcNow;
            var room = await _context
                .Rooms
                .Where(r=> r.Id == idRoom)
                .Include(r => r.Events)
                .FirstOrDefaultAsync();
            if (room != null)
            {
                return new RoomVO()
                {
                    Id = room.Id,
                    Name = room.Name,
                    Scheduling =
                        room
                        .Events
                        .Count > 0
                };
            }
            return null;
        }

        public async Task<bool> EventExists(int id)
        {
            return (await _context
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync()) != null;
        }

        public async Task<List<EventModel>> EventRoomDate(int id, int room, DateTime startDate, DateTime endDate)
        {
            return (await _context
                .Events
                .Where(e =>
                    e.Id != id && e.IdRoom == room &&
                    ((startDate > e.StartDate && startDate < e.EndDate) ||
                    (endDate > e.StartDate && endDate < e.EndDate)) ||
                    (startDate == e.StartDate && endDate == e.EndDate))
                .ToListAsync());
        }

    }
}
