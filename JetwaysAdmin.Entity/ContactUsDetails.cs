using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class ContactUsDetails
    {
        [Key]
        public int Id { get; set; }
        public bool UseHtml { get; set; }
        public string? HtmlContent { get; set; }
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? Url { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
    }
}
