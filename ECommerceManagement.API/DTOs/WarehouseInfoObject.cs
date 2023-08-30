using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.DTOs
{
    public class WarehouseInfoObject
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public AddressInfoObject AddressInfo { get; set; } = null!;
        [Required]
        public ListObject<StockInfoObject> StockInfo { get; set; } = null!;
    }
}
