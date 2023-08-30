using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class User
{
    public Guid Id { get; set; }

    public int Role { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string IdentityNumber { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;
}
