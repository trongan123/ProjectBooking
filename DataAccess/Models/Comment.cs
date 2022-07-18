using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Comment
    {
        [Key]
        public string Idcomment { get; set; } = null!;
        public int? Rate { get; set; }
        public string? Description { get; set; }
        public string? Idacc { get; set; }

        public virtual Account? IdaccNavigation { get; set; }
    }
}
