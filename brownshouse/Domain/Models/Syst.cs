using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public class Syst
    {
        public Syst()
        {
            Subsysts = new HashSet<Subsyst>();
        }

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Subsyst> Subsysts { get; set; }
    }
}
