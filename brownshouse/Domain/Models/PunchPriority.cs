using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class PunchPriority
    {
        public PunchPriority()
        {
            Punches = new HashSet<Punch>();
        }

        public int Id { get; set; }

        [Required]
        public string Priority { get; set; }

        public virtual ICollection<Punch> Punches { get; set; }
    }
}
