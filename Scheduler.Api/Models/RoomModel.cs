using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Scheduler.Api.Models
{
    public class RoomModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EventModel> Events { get; set; }
        [NotMapped]
        public bool Scheduling
        {
            get
            {
                if (Events == null)
                    return false;
                try
                {
                    return this.Events
                        .Where(e=> e.StartDate > System.DateTime.Now).ToList().Count > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
