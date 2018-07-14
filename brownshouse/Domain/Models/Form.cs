using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Form
    {
        public Form()
        {
            InspectionTestPlans = new HashSet<InspectionTestPlan>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FilePath { get; set; }

        public virtual ICollection<InspectionTestPlan> InspectionTestPlans { get; set; }
    }
}
