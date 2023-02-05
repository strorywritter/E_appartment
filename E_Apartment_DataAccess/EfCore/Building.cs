using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class Building
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public bool? IsDelete { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Apartment> Apartments { get; } = new List<Apartment>();
}
