using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
   public class TagsRequest
    {
        public int Id { get; set; }

        public int IdTag { get; set; }

        public int IdRequest { get; set; }

        public virtual Request Request { get; set; }
        
        public virtual Tag Tag { get; set; }
    }
}
