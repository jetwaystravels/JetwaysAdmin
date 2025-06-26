using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class SuppliersCredential
    {
        [Key]
        public int Id { get; set; }
        public string? CredentialsName { get; set; }
        public string? ClientId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? AgentName { get; set; }
        public string? OrganizationId { get; set; }
        public string? AssociatedFareTypes { get; set; }
        public string? TravelType { get; set; }
        public string? SupplierCode { get; set; }
        public string? CredentialsType { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
