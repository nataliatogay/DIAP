using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Unit
    {
        public Unit()
        {
            Requests = new HashSet<Request>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Code { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
