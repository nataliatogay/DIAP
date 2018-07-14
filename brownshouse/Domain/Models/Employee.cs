using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Employee
    {
        public Employee()
        {
            RaisedPunches = new HashSet<Punch>();
            RaisedRequests = new HashSet<Request>();
            SubcontractorRequests = new HashSet<Request>();
            ContractorRequests = new HashSet<Request>();
            OwnerRequests = new HashSet<Request>();
            ThirdPartRequests = new HashSet<Request>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public int IdUser { get; set; }

        public virtual User User { get; set; }


        public virtual ICollection<Punch> RaisedPunches { get; set; }

        public virtual ICollection<Request> RaisedRequests { get; set; }

        public virtual ICollection<Request> ContractorRequests { get; set; }

        public virtual ICollection<Request> SubcontractorRequests { get; set; }

        public virtual ICollection<Request> OwnerRequests { get; set; }

        public virtual ICollection<Request> ThirdPartRequests { get; set; }
    }
}