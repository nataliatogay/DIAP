using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public  class Organization
    {
        public Organization()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int IdOrganizationRole { get; set; }

        public virtual OrganizationRole OrganizationRole { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
