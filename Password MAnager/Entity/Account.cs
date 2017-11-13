using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("service")]
        public int ServiceId { get; set; }
        public Service service { get; set; }

        [StringLength(maximumLength:150),Required]
        public string Password { get; set; }

        [Required]
        public DateTime time { get; set; }

        public ICollection<ExtraField> extrafields { get; set; }
    }
}
