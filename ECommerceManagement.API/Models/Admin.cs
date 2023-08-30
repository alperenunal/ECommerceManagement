using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Admin
{
    public Guid Id { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
