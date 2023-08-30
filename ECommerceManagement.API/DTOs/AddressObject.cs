using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class AddressObject
    {
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public int Zipcode { get; set; }
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
    }
}
