using E_Apartment_DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace E_Apartment_Logic.LeaseLogic
{
    public class LeaseLogic : AbstractLogic, ILeaseLogic
    {
        public LeaseLogic(EApartmentDbContext eApartmentDbContext) : base(eApartmentDbContext)
        {
        }

        public async Task AddAsync(LeaseDetail leaseDetail)
        {
            leaseDetail.IsDelete = false;
            leaseDetail.LeaseStatusId = 2;

            await eApartmentDbContext.LeaseDetails.AddAsync(leaseDetail);

            var existingApartment = eApartmentDbContext.Apartments.Find(leaseDetail.ApartmentId);
            if (existingApartment != null)
            {
                existingApartment.StatusId = 2;
                eApartmentDbContext.Apartments.Update(existingApartment);
            }


            await eApartmentDbContext.SaveChangesAsync();
        }

        

        public void AddNewLeaseExtension(LeaseExtension extension)
        {
            eApartmentDbContext.LeaseExtensions.Add(extension);

            var existingLeaseDetail = eApartmentDbContext.LeaseDetails.Find(extension.LeaseDetailsId);

            if (existingLeaseDetail != null)
            {
                existingLeaseDetail.LeaseStatusId = 2;
                eApartmentDbContext.LeaseDetails.Update(existingLeaseDetail);
            }

            eApartmentDbContext.SaveChanges();


        }
        public async Task ApproveLeaseNote(Guid? notApprovedLeaseDetailId)
        {
            var result = eApartmentDbContext.LeaseDetails.Where(x => x.Id == notApprovedLeaseDetailId).FirstOrDefault();
            if (result != null)
            {
                result.LeaseStatusId = 1;

                eApartmentDbContext.LeaseDetails.Update(result);
                await eApartmentDbContext.SaveChangesAsync();
            }
        }

        public IList<LeaseDetail> FilterByDate(DateTime value1, DateTime value2)
        {
            return eApartmentDbContext.LeaseDetails.Where(x=>x.FromDate<value1.Date && x.ToDate>value2.Date && x.LeaseStatusId==1).ToList();
        }

        public IList<LeaseExtension> FindAllExtendedLease()
        {
            return eApartmentDbContext.LeaseExtensions.Where(x => x.LeaseStatusId == 1).ToList();
        }

        public IList<LeaseExtension> FindAllExtendRequest()
        {
            return eApartmentDbContext.LeaseExtensions.Where(x=>x.LeaseStatusId==2).ToList();
        }

        public IList<LeaseDetail> FindAllNotApprovedLeaseNotes()
        {
            return eApartmentDbContext.LeaseDetails.Where(x => x.LeaseStatusId == 2).ToList();
        }

        public LeaseDetail FindApprovedLeaseDetailsById(Guid id)
        {
            return eApartmentDbContext.LeaseDetails.Where(x => x.LeaseStatusId == 2 && x.Id==id).FirstOrDefault(); ;
        }

        public IList<LeaseDetail> FindApprovedLeaseNotes()
        {
            return eApartmentDbContext.LeaseDetails.Where(x => x.LeaseStatusId == 1).ToList();
        }

        public async Task Update(Guid? id, LeaseDetail leaseDetail)
        {
            var existingLeaseDetails = await eApartmentDbContext.LeaseDetails.FindAsync(id);

            if (existingLeaseDetails != null)
            {
                existingLeaseDetails.MonthlyFee = leaseDetail.MonthlyFee;
                existingLeaseDetails.FromDate = leaseDetail.FromDate;
                existingLeaseDetails.ToDate = leaseDetail.ToDate;
                existingLeaseDetails.ApartmentId= leaseDetail.ApartmentId;
                existingLeaseDetails.OccupierId = leaseDetail.OccupierId;

                eApartmentDbContext.LeaseDetails.Update(leaseDetail);
                await eApartmentDbContext.SaveChangesAsync();
            }            
        }

        public async Task UpdateExtentionStatusAsync(Guid? extentionLeaseId)
        {
            var existingLeaseExtentionDetail = await eApartmentDbContext.LeaseExtensions.Where(x=>x.LeaseDetailsId==extentionLeaseId).FirstOrDefaultAsync();

            if (existingLeaseExtentionDetail != null)
            {
                existingLeaseExtentionDetail.LeaseStatusId = 1;
                eApartmentDbContext.LeaseExtensions.Update(existingLeaseExtentionDetail);
            }


            var existingLeaseDetails = await eApartmentDbContext.LeaseDetails.FindAsync(extentionLeaseId);

            if (existingLeaseDetails != null)
            {
                existingLeaseDetails.LeaseStatusId = 1;
                eApartmentDbContext.LeaseDetails.Update(existingLeaseDetails);
            }

            await eApartmentDbContext.SaveChangesAsync();
        }
    }
}
