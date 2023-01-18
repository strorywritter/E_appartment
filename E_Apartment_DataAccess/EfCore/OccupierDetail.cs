using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class OccupierDetail
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? AlternateAddress { get; set; }

    public string? ContactNo { get; set; }

    public string? NicOrPassportNo { get; set; }

    public bool? IsIncludeServant { get; set; }

    public Guid? ApartmentId { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual ICollection<LeaseDetail> LeaseDetails { get; } = new List<LeaseDetail>();
}
