using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Work
    {
        public Work()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int IdDiscipline { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        
        public virtual Discipline Discipline { get; set; }
    }
}
