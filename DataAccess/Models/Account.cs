using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
        }
        [Key]
        public string Idacc { get; set; } = null!;
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email is Wrong format")]
        public string Mail { get; set; } = null!;
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}", ErrorMessage = "Pass is >8 and have char and number")]
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;

        [RegularExpression(@"^(\+84|0[3|5|7|8|9])+([0-9]{8})", ErrorMessage = "Phone is Wrong format")]
        public string Phone { get; set; } = null!;
        public int? St { get; set; }

        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
