using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
  public  class InspectionTestPlan
    {
        public InspectionTestPlan()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSubactivity { get; set; }
        
        public int IdForm { get; set; }

        public int IdSubcontractorRole { get; set; }

        public int IdContractorRole { get; set; }
        
        public int? IdOwnerRole { get; set; }
        
        public int? IdThirdPartRole { get; set; }

        public virtual Form Form { get; set; }

        public virtual InspectionRole SubcontractorRole { get; set; }

        public virtual InspectionRole ContractorRole { get; set; }

        public virtual InspectionRole OwnerRole { get; set; }

        public virtual InspectionRole ThirdPartRole { get; set; }

        public virtual Subactivity Subactivity { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}


