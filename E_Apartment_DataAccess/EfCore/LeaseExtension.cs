using System;
using System.Collections.Generic;

namespace E_Apartment_DataAccess.EfCore;

public partial class LeaseExtension
{
    public Guid Id { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public bool? IsDelete { get; set; }

    public Guid? LeaseDetailsId { get; set; }

    public short? LeaseStatusId { get; set; }

    public virtual LeaseDetail? LeaseDetails { get; set; }

    public virtual LeaseStatus? LeaseStatus { get; set; }
}
