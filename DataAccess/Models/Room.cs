using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Room
    {
        public Room()
        {
            BookingRoomDetails = new HashSet<BookingRoomDetail>();
        }
        [Key]
        public string Idroom { get; set; } = null!;
        public string NumberRoom { get; set; } = null!;
        public string IdroomType { get; set; } = null!;
        public int Stroom { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public virtual RoomType? IdroomTypeNavigation { get; set; } = null!;
        public virtual ICollection<BookingRoomDetail>? BookingRoomDetails { get; set; }
    }
}
