using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
  public  class Subactivity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Number { get; set; }

        public int IdActivity { get; set; }
        
        public virtual Activity Activity { get; set; }

        public virtual InspectionTestPlan InspectionTestPlan { get; set; }
    }
}
