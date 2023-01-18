using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class ApartmentStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Apartment> Apartments { get; } = new List<Apartment>();
}
