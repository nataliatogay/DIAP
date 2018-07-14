using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public  class Position
    {
        public Position()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
