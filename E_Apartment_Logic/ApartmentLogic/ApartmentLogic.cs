using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.ApartmentLogic
{
    public class ApartmentLogic : AbstractLogic, IApartmentLogic
    {
        public ApartmentLogic(EApartmentDbContext eApartmentDbContext) : base(eApartmentDbContext)
        {
        }

        public async Task<Apartment> AddApartment(Apartment apartment)
        {
            var recordCount = eApartmentDbContext.Apartments.Count();
            apartment.Code = GetCode(DocumentRef.Apartment, recordCount);

            await eApartmentDbContext.Apartments.AddAsync(apartment);
            await eApartmentDbContext.SaveChangesAsync();

            return apartment;
        }

        public int AvailableApartmentCount()
        {
            return eApartmentDbContext.Apartments.Where(x=>x.StatusId == 1).Count();
        }

        public List<Apartment> FilterByCode(string text)
        {
            return eApartmentDbContext.Apartments.Where(x => EF.Functions.Like(x.Code, text+"%")).ToList();
        }

        public IList<ApartmentType> GetAllApartmentTypes()
        {
            return eApartmentDbContext.ApartmentTypes.ToList();
        }

        public IList<ApartmentStatus> GetAllStauses()
        {
            return eApartmentDbContext.ApartmentStatuses.ToList();
        }

        public int GetCount()
        {
            return eApartmentDbContext.Apartments.Count();
        }

        public IList<Apartment> ListApartment()
        {
            return eApartmentDbContext.Apartments.ToList();
        }

        public async Task Update(Guid? apartmentId, Apartment apartment)
        {
            var existingApartment = eApartmentDbContext.Apartments.Where(x => x.Id == apartmentId).FirstOrDefault();

            existingApartment.Description = apartment.Description;
            existingApartment.FlowNo = apartment.FlowNo;
            existingApartment.StatusId = apartment.StatusId;
            existingApartment.IsDelete = apartment.IsDelete;
            existingApartment.TypeId = apartment.TypeId;
            existingApartment.BuildingId = apartment.BuildingId;

            eApartmentDbContext.Apartments.Update(existingApartment);
            eApartmentDbContext.SaveChanges();
                
        }
    }
}
