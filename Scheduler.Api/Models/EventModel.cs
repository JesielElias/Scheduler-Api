using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Scheduler.Api.Models
{
    public class EventModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? Id { get; set; }
        public string Title { get; set; }
        [ForeignKey("Room")]
        public int IdRoom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual RoomModel Room { get; set; } = null!;
    }
}
