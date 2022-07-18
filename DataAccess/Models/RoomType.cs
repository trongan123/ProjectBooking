using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }
        [Key]
        public string IdroomType { get; set; } = null!;
        public string NameRoomType { get; set; } = null!;

        public virtual ICollection<Room>? Rooms { get; set; }
    }
}
