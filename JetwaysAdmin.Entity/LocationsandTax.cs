using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class LocationsandTax
    {
        [Key]
        public int LocationID { get; set; }
        public string LegalEntityCode { get; set; }
        public string LocationName { get; set; }
        public string? LocationCode { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal? Longitude { get; set; }
        public bool? GSTRegistered { get; set; }
        public bool? UINRegistered { get; set; }
        public string? GSTNumber { get; set; }
        public string? GSTName { get; set; }
        [EmailAddress]
        public string? GSTEmail { get; set; }
        public string? MobileCountryCode { get; set; }
        public string? MobileNumber { get; set; }
        public bool? PersonalBooking { get; set; }
        public bool? IsSEZ { get; set; }
        public int? LocationHead { get; set; }
        public int? LocationManager1 { get; set; }
        public int? LocationManager2 { get; set; }
    }
}
