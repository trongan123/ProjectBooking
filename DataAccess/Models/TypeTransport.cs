using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class TypeTransport
    {
        public TypeTransport()
        {
            Transports = new HashSet<Transport>();
        }
        [Key]
        public string IdtypeTransport { get; set; } = null!;
        public string NameTranspors { get; set; } = null!;

        public virtual ICollection<Transport>? Transports { get; set; }
    }
}
