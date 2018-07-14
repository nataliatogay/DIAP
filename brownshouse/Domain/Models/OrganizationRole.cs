using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public class OrganizationRole
    {
        public OrganizationRole()
        {
            Organizations = new HashSet<Organization>();
        }

        public int Id { get; set; }

        [Required]
        public string Role { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
