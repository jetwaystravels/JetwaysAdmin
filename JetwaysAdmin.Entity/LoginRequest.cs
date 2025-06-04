using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CoprateLoginRequest
    {
        [Required]
        public string BusinessEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
