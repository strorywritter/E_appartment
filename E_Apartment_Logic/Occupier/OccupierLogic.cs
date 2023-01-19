using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.Occupier
{
    public class OccupierLogic : AbstractLogic, IOccupierLogic
    {
        public OccupierLogic(EApartmentDbContext eApartmentDbContext) : base(eApartmentDbContext)
        {
        }

        public async Task<OccupierDetail> Add(OccupierDetail occupierDetail)
        {
            var recordCount = eApartmentDbContext.OccupierDetails.Count();
            occupierDetail.Code = GetCode(DocumentRef.OccupierDetail,recordCount);

            await eApartmentDbContext.OccupierDetails.AddAsync(occupierDetail);
            await eApartmentDbContext.SaveChangesAsync();

            return occupierDetail;
        }

        public List<OccupierDetail> FilterByCode(string text)
        {
            return eApartmentDbContext.OccupierDetails.Where(x => EF.Functions.Like(x.Code, text+"%")).ToList();
        }

        public int getCount()
        {
            return eApartmentDbContext.OccupierDetails.Count();
        }

        public IList<OccupierDetail> GetOccupiers()
        {
            return eApartmentDbContext.OccupierDetails.ToList();
        }

        public async Task Update(Guid? id, OccupierDetail occupierDetail)
        {
            var existingOccupier = eApartmentDbContext.OccupierDetails.Where(x => x.Id == id).FirstOrDefault();

            existingOccupier.Name = occupierDetail.Name;
            existingOccupier.NicOrPassportNo = occupierDetail.NicOrPassportNo;
            existingOccupier.ApartmentId = occupierDetail.ApartmentId;
            existingOccupier.IsIncludeServant = occupierDetail.IsIncludeServant;
            existingOccupier.ContactNo = occupierDetail.ContactNo;
            existingOccupier.AlternateAddress= occupierDetail.AlternateAddress;

            eApartmentDbContext.OccupierDetails.Update(existingOccupier);
            eApartmentDbContext.SaveChanges();
        }
    }
}
