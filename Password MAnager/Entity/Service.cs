using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class Service
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("section")]
        public int SectionId { get; set; }
        public Section section { get; set; }

        [StringLength(maximumLength: 100), Required]
        public string Name { get; set; }


        public ICollection<Account> Accountes { get; set; }
    }
}
