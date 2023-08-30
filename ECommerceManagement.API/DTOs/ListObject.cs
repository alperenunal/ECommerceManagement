using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class ListObject<T>
    {
        [Required]
        public int Offset { get; set; }
        [Required]
        public int Limit { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public List<T> Items { get; set; } = null!;
    }
}
