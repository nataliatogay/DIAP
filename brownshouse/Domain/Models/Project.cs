using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public class Project
    {
        public Project()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
