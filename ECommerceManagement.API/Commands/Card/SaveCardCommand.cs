using MediatR;

namespace ECommerceManagement.API.Commands.Card
{
    public class SaveCardCommand : IRequest
    {
        public Guid CustomerId { set; get; }
        public string CardName { set; get; } = null!;
        public string CardNumber { set; get; } = null!;
        public int Cvc { set; get;  }
        public int ExpireYear { set; get; }
        public int ExpireMonth { set; get; }
    }
}
