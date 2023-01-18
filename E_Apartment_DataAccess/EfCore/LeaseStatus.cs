using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class LeaseStatus
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LeaseDetail> LeaseDetails { get; } = new List<LeaseDetail>();

    public virtual ICollection<LeaseExtension> LeaseExtensions { get; } = new List<LeaseExtension>();
}
