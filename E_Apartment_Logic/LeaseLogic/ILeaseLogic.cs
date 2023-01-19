using E_Apartment_DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = E_Apartment_DataAccess.EfCore;

namespace E_Apartment_Logic.LeaseLogic
{
    public interface ILeaseLogic
    {
        Task AddAsync(LeaseDetail leaseDetail);
        Task ApproveLeaseNote(Guid? notApprovedLeaseDetailId);
        IList<LeaseDetail> FilterByDate(DateTime value1, DateTime value2);
        IList<dbCore.LeaseDetail> FindAllNotApprovedLeaseNotes();
        IList<dbCore.LeaseDetail> FindApprovedLeaseNotes();

        dbCore.LeaseDetail FindApprovedLeaseDetailsById(Guid id);
        Task Update(Guid? id, LeaseDetail leaseDetail);
        IList<dbCore.LeaseExtension> FindAllExtendRequest();
        Task UpdateExtentionStatusAsync(Guid? extentionLeaseId);
        IList<dbCore.LeaseExtension> FindAllExtendedLease();
        void AddNewLeaseExtension(LeaseExtension extension);
    }
}
