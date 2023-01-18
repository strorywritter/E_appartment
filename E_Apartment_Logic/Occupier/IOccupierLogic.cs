using E_Apartment_DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.Occupier
{
    public interface IOccupierLogic
    {
        Task<OccupierDetail> Add(OccupierDetail occupierDetail);
        List<OccupierDetail> FilterByCode(string text);
        int getCount();
        Task<List<OccupierDetail>> GetOccupiers();
        Task Update(Guid? id, OccupierDetail occupierDetail);

        
    }
}
