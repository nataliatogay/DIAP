using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class AcceptanceResult
    {
        public AcceptanceResult()
        {
            ContractorRequests = new HashSet<Request>();
            SubcontractorRequests = new HashSet<Request>();
            OwnerRequests = new HashSet<Request>();
            ThirdPartRequests = new HashSet<Request>();
            StatusRequests = new HashSet<Request>();
        }

        public int Id { get; set; }

        [Required]
        public string Result { get; set; }

        public virtual ICollection<Request> ContractorRequests { get; set; }

        public virtual ICollection<Request> SubcontractorRequests { get; set; }

        public virtual ICollection<Request> OwnerRequests { get; set; }

        public virtual ICollection<Request> ThirdPartRequests { get; set; }

        public virtual ICollection<Request> StatusRequests { get; set; }
    }
}
