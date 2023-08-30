using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class RequestObject
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
