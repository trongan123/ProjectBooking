using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Transport
    {
        public Transport()
        {
            BookingTransportDetails = new HashSet<BookingTransportDetail>();
        }
        [Key]
        public string Idtransport { get; set; } = null!;
        public string IdtypeTransport { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public virtual TypeTransport? IdtypeTransportNavigation { get; set; } = null!;
        public virtual ICollection<BookingTransportDetail>? BookingTransportDetails { get; set; }
    }
}
