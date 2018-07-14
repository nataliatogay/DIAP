using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class Punch
    {
        public int Id { get; set; }

        public int IdTag { get; set; }

        public int IdPriority { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public int IdRaisedByEmployee { get; set; }

        public virtual PunchPriority PunchPriority { get; set; }

        public virtual Tag Tag { get; set; }
       
        public virtual Employee RaisedByEmployee { get; set; }
    }

    public class PunchReport
    {
        public int PunchACount { get; set; }
        public int PunchBCount { get; set; }
        public int PunchCCount { get; set; }
        public int PunchAllCount { get; set; }
    }
}
