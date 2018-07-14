using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
  public  class Request
    {
        public Request()
        {
            TagsRequests = new HashSet<TagsRequest>();
        }

        public int Id { get; set; }

        public int Number { get; set; }

        public int IdProject { get; set; } 

        public int IdUnit { get; set; } 

        public int IdSubsyst { get; set; } 

        public int IdTestPlan { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOpen { get; set; }

        public int? IdAcceptanceSubcontractor { get; set; }

        public int? IdAcceptanceContractor { get; set; }

        public int? IdAcceptanceOwner { get; set; }

        public int? IdAcceptanceThirdParty { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateToDo { get; set; }

        public int? IdStatus { get; set; }

        public int IdRaisedByEmployee { get; set; }

        public int IdResponsibleSubcontractor { get; set; }

        public int IdResponsibleContractor { get; set; }

        public int? IdResponsibleOwner { get; set; }

        public int? IdResponsibleThirdParty { get; set; }

        public virtual AcceptanceResult AcceptanceSubcontractor { get; set; }

        public virtual AcceptanceResult AcceptanceContractor { get; set; }

        public virtual AcceptanceResult AcceptanceOwner { get; set; }
        
        public virtual AcceptanceResult AcceptanceThirdParty { get; set; }
        
        public virtual AcceptanceResult Status { get; set; }

        public virtual InspectionTestPlan InspectionTestPlan { get; set; }
        
        public virtual Project Project { get; set; }
        
        public virtual Subsyst Subsyst { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Employee RaisedByEmployee { get; set; }

        public virtual Employee ResponsibleSubcontractor { get; set; }

        public virtual Employee ResponsibleContractor { get; set; }

        public virtual Employee ResponsibleOwner { get; set; }

        public virtual Employee ResponsibleThirdParty { get; set; }

        public virtual ICollection<TagsRequest> TagsRequests { get; set; }
    }
}
