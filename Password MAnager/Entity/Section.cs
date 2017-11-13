using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }
        public User user { get; set; }

        [StringLength(maximumLength: 100), Required]
        public string Name { get; set; }


        public ICollection<Service> Services { get; set; }
    }
}
