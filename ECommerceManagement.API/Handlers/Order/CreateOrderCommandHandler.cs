using ECommerceManagement.API.Commands.Order;
using ECommerceManagement.API.Data;
using ECommerceManagement.API.Exceptions;
using ECommerceManagement.API.Models;
using ECommerceManagement.API.Services;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ECommerceManagement.API.Handlers.Order
{
    public class CreateOrderCommandHandler : BaseHandler<CreateOrderCommand>
    {
        private readonly IConfiguration _configuration;
        private readonly Iyzipay.Options _options;

        public CreateOrderCommandHandler(EcommerceContext context, IConfiguration configuration) : base(context) {
            _configuration = configuration;
            _options = new Iyzipay.Options
            {
                BaseUrl = "https://sandbox-api.iyzipay.com",
                ApiKey = _configuration["Iyzipay:API_KEY"],
                SecretKey = _configuration["Iyzipay:API_SECRET"]
            };
        }

        public override async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.AsNoTracking()
                .Where(c => c.Id == request.CustomerId)
                .Include(c => c.IdNavigation)
                .Include(c => c.Addresses.Where(a => a.Id == request.AddressId))
                .Include(c => c.Cards.Where(c => c.Id == request.CardId))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new BadRequestException("Trying to pay without valid address or credit card");
            
            var card = customer.Cards.First();
            var address = customer.Addresses.First();
            var items = new List<BasketItem>();
            var prices = new List<float>();

            var totalPrice = 0f;
            request.Products.ForEach(product => {
                var p = _context.Products.AsNoTracking()
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == product.ProductId)
                    ?? throw new NotFoundException("Product not found");

                prices.Add(p.Price);
                var price = p.Price * product.Amount;
                totalPrice += price;

                items.Add(new BasketItem
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Category1 = p.Category.Name,
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = price.ToString(new CultureInfo("en-US")),
                });
            });

            var req = new CreatePaymentRequest
            {
                Price = totalPrice.ToString(new CultureInfo("en-US")),
                PaidPrice = totalPrice.ToString(new CultureInfo("en-US")),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
            };

            var paymentCard = new PaymentCard
            {
                CardHolderName = card.CardName,
                CardNumber = card.CardNumber,
                ExpireMonth = card.ExpireMonth.ToString(),
                ExpireYear = card.ExpireYear.ToString(),
                Cvc = card.Cvc.ToString()
            };

            var buyer = new Buyer
            {
                Id = customer.Id.ToString(),
                Name = customer.IdNavigation.Name,
                Surname = customer.IdNavigation.Surname,
                GsmNumber = customer.IdNavigation.PhoneNumber,
                Email = customer.IdNavigation.Email,
                IdentityNumber = customer.IdNavigation.IdentityNumber,
                City = address.City,
                Country = address.Country,
                RegistrationAddress = address.Address1,
                ZipCode = address.Zipcode.ToString(),
                Ip = "127.0.0.1",
            };

            var shipAddress = new Iyzipay.Model.Address
            {
                ContactName = $"{customer.IdNavigation.Name} {customer.IdNavigation.Surname}",
                City = address.City,
                Country = address.Country,
                Description = address.Address1,
                ZipCode = address.Zipcode.ToString(),
            };

            req.Buyer = buyer;
            req.PaymentCard = paymentCard;
            req.BillingAddress = shipAddress;
            req.ShippingAddress = shipAddress;
            req.BasketItems = items;

            var payment = Payment.Create(req, _options);
            
            if (payment.Status == Status.FAILURE.ToString())
            {
                throw new Exception(payment.ErrorMessage);
            }

            var shops = await _context.Stores.AsNoTracking()
                .Where(s => s.StoreType == false)
                .Include(s => s.AddressNavigation)
                .ToListAsync(cancellationToken);

            int closestShop = 0;
            var minDist = Double.PositiveInfinity;

            shops.ForEach(shop =>
            {
                var coordinate = shop.AddressNavigation.Coordinates;
                var dist = GeoService.GetDistance(address.Coordinates.Y, address.Coordinates.X,
                        shop.AddressNavigation.Coordinates.Y, shop.AddressNavigation.Coordinates.X);
                Console.WriteLine(dist);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestShop = shop.Id;
                }
            });

            var order = new Models.Order
            {
                CustomerId = customer.Id,
                AddressId = address.Id,
                CardId = card.Id,
                ShopId = closestShop,
                Price = totalPrice,
                OrderDate = DateTime.UtcNow,
                Status = false,
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            for (int i = 0; i < request.Products.Count; i++)
            {
                var product = request.Products[i];
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId,
                    Amount = product.Amount,
                    Price = prices[i],
                };
                await _context.OrderProducts.AddAsync(orderProduct, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
