using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class UsersType
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
