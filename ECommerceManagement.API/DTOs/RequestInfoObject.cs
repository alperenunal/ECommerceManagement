using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class RequestInfoObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ManagerId { get; set; }
        [Required]
        public int ShopId { get; set; }
        [Required]
        public string ShopName { get; set; } = null!;
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public int Amount { get; set; }
        [Required]
        public bool? Status { get; set; }
    }
}
