using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BookingRoomDetails = new HashSet<BookingRoomDetail>();
            BookingSeviceDetails = new HashSet<BookingSeviceDetail>();
            BookingTransportDetails = new HashSet<BookingTransportDetail>();
        }
        [Key]
        public string Idbill { get; set; } = null!;
        public string Idacc { get; set; } = null!;
        public string? Idadmin { get; set; }
        public string Phone { get; set; } = null!;
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public decimal? Price { get; set; }

        public virtual Account? IdaccNavigation { get; set; }
        public virtual ICollection<BookingRoomDetail>? BookingRoomDetails { get; set; }
        public virtual ICollection<BookingSeviceDetail>? BookingSeviceDetails { get; set; }
        public virtual ICollection<BookingTransportDetail>? BookingTransportDetails { get; set; }
    }
}
