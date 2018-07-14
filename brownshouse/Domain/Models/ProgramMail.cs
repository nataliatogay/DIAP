using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class ProgramMail
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
