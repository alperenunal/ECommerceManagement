using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ErrorObject
    {
        [Required]
        public int Status { get; set; }
        [Required]
        public string Message { get; set; } = null!;
    }
}
