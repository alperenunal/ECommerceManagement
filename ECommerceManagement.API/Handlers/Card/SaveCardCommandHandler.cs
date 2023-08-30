using ECommerceManagement.API.Commands.Card;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;

namespace ECommerceManagement.API.Handlers.Card
{
    public class SaveCardCommandHandler : BaseHandler<SaveCardCommand>
    {
        public SaveCardCommandHandler(EcommerceContext context) : base(context)
        {
        }

        public override async Task Handle(SaveCardCommand request, CancellationToken cancellationToken)
        {
            if (request.Cvc.ToString().Length != 3 ||
                request.CardNumber.Length != 16 ||
                request.ExpireMonth < 0 || request.ExpireMonth > 12)
            {
                throw new BadRequestException("Invalid card format");
            }

            var card = new Models.Card
            {
                CustomerId = request.CustomerId,
                CardName = request.CardName,
                CardNumber = request.CardNumber,
                Cvc = request.Cvc,
                ExpireMonth = request.ExpireMonth,
                ExpireYear = request.ExpireYear,
            };

            await _context.Cards.AddAsync(card, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
