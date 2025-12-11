using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class BookingListDto
    {
        public string BookingID { get; set; }
        public string RecordLocator { get; set; }
        public DateTime? BookedDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
