using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Customer
{
    public Guid Id { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
