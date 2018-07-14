using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public int IdOrganization { get; set; }

        public int IdDiscipline { get; set; }

        public int IdPosition { get; set; }

        [Required]
        public string Abbreviation { get; set; }
        
        public virtual Discipline Discipline { get; set; }
        
        public virtual Organization Organization { get; set; }

        public virtual Position Position { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        
    }
}
