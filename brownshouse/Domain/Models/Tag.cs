using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Tag
    {
        public Tag()
        {
            Punches = new HashSet<Punch>();
            TagsRequests = new HashSet<TagsRequest>();
        }

        public int Id { get; set; }
        
        [Required]
        public string Code { get; set; }

        public string Title { get; set; }

        public int IdSubsyst { get; set; }
        
        public int IdUnit { get; set; }

        public int IdDiscipline { get; set; }

        public virtual ICollection<Punch> Punches { get; set; }

        public virtual Subsyst Subsyst { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual ICollection<TagsRequest> TagsRequests { get; set; }
    }
}
