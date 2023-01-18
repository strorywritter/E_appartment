using E_Apartment_DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.ApartmentLogic
{
    public interface IApartmentLogic
    {
        
        Task<Apartment> AddApartment(Apartment apartment);
        int AvailableApartmentCount();
        List<Apartment> FilterByCode(string text);
        IList<ApartmentType> GetAllApartmentTypes();
        IList<ApartmentStatus> GetAllStauses();
        int GetCount();
        IList<Apartment> ListApartment();
        Task Update(Guid? apartmentId, Apartment apartment);
    }
}
