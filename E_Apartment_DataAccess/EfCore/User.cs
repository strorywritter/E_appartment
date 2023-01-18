using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class User
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public string? Description { get; set; }

    public string? Password { get; set; }

    public bool? IsDelete { get; set; }

    public short? TypeId { get; set; }

    public virtual UsersType? Type { get; set; }
}
