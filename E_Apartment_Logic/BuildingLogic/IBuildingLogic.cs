using E_Apartment_DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.BuildingLogic
{
    public interface IBuildingLogic
    {
        Building Add(Building building);
        List<Building> FilterByCode(string text);
        Task<List<Building>> GetBuildings();
        int GetCount();
        Task Update(Guid? buildingId, Building building);
    }
}
