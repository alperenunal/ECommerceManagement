using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerceManagement.API.Commands.Category
{
    public class CreateCategoryCommand : IRequest<Models.Category>
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
