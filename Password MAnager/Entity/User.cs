using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MAnager.Entity
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength:100),Required]
        public string Login { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [StringLength(maximumLength: 100), Required]
        public string Password { get; set; }


        public ICollection<Section> Sections { get; set; }
    }
}
