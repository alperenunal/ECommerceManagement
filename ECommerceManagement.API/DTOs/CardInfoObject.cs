using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class CardInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string CardName { get; set; } = null!;
        [Required]
        public string LastFourDigit { get; set; } = null!;
    }
}
