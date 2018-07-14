using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Discipline
    {
        public Discipline()
        {
            Users = new HashSet<User>();
            Works = new HashSet<Work>();
        }

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Work> Works { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
