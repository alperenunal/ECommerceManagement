using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class CardObject
    {
        [Required]
        public string CardName { get; set; } = null!;
        [Required]
        public string CardNumber { get; set; } = null!;
        [Required]
        public int Cvc { get; set; }
        [Required]
        public int ExpireMonth { get; set; }
        [Required]
        public int ExpireYear { get; set; }
    }
}
