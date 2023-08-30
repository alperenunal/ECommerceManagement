using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Shop
{
    public int Id { get; set; }

    public float Area { get; set; }

    public virtual Store IdNavigation { get; set; } = null!;

    public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
