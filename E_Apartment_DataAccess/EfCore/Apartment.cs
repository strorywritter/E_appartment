using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class Apartment
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? FlowNo { get; set; }

    public short? StatusId { get; set; }

    public bool? IsDelete { get; set; }

    public short? TypeId { get; set; }

    public virtual ICollection<LeaseDetail> LeaseDetails { get; } = new List<LeaseDetail>();

    public virtual ICollection<OccupierDetail> OccupierDetails { get; } = new List<OccupierDetail>();

    public virtual ApartmentStatus? Status { get; set; }

    public virtual ApartmentType? Type { get; set; }
}
