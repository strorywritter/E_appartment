using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class LeaseDetail
{
    public Guid Id { get; set; }

    public decimal? MonthlyFee { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public bool? IsDelete { get; set; }

    public Guid? ApartmentId { get; set; }

    public Guid? OccupierId { get; set; }

    public short? LeaseStatusId { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual ICollection<LeaseExtension> LeaseExtensions { get; } = new List<LeaseExtension>();

    public virtual LeaseStatus? LeaseStatus { get; set; }

    public virtual OccupierDetail? Occupier { get; set; }
}
