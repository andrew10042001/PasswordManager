using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class ExtraField
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("account")]
        public int AccountId { get; set; }
        public Account account { get; set; }

        [StringLength(maximumLength:100),Required]
        public string Name { get; set; }

        [StringLength(maximumLength:150),Required]
        public string Value { get; set; }
    }
}
