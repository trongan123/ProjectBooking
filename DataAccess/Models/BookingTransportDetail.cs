using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class BookingTransportDetail
    {

        [Key]
        public string IdbookingTransportDetail { get; set; } = null!;
        public string Idbill { get; set; } = null!;
        public string Idtransport { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int? St { get; set; }

        public virtual Bill? IdbillNavigation { get; set; } = null!;
        public virtual Transport? IdtransportNavigation { get; set; } = null!;
    }
}
