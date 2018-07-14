using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class InspectionRole
    {
        public InspectionRole()
        {
            ITPsContractor = new HashSet<InspectionTestPlan>();
            ITPsSubcontractor = new HashSet<InspectionTestPlan>();
            ITPsOwner = new HashSet<InspectionTestPlan>();
            ITPsThirdPart = new HashSet<InspectionTestPlan>();
        }

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public bool RFIIsRequired { get; set; }

        public virtual ICollection<InspectionTestPlan> ITPsContractor { get; set; }

        public virtual ICollection<InspectionTestPlan> ITPsSubcontractor { get; set; }

        public virtual ICollection<InspectionTestPlan> ITPsOwner { get; set; }

        public virtual ICollection<InspectionTestPlan> ITPsThirdPart { get; set; }
    }
}
