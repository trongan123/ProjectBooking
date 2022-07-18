using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Service
    {
        public Service()
        {
            BookingSeviceDetails = new HashSet<BookingSeviceDetail>();
        }

        [Key] 
        public string Idservice { get; set; } = null!;
        public string NameService { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<BookingSeviceDetail>? BookingSeviceDetails { get; set; }
    }
}
