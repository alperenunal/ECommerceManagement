using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Card
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string CardName { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public int Cvc { get; set; }

    public int ExpireYear { get; set; }

    public int ExpireMonth { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
