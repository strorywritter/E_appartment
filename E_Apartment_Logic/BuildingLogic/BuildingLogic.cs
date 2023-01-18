using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Apartment_Logic.BuildingLogic
{
    public class BuildingLogic : AbstractLogic, IBuildingLogic
    {
        public BuildingLogic(EApartmentDbContext eApartmentDbContext) : base(eApartmentDbContext)
        {

        }
        
        public Building Add(Building building)
        {
            var recordCount = eApartmentDbContext.Buildings.Count();
            building.Code = GetCode(DocumentRef.Building, recordCount);

            eApartmentDbContext.Add(building);
            eApartmentDbContext.SaveChanges();

            return building;
        }

        public List<Building> FilterByCode(string text)
        {
            return eApartmentDbContext.Buildings.Where(x => EF.Functions.Like(x.Code,  text+"%")).ToList();
        }

        public async Task<List<Building>> GetBuildings()
        {
            return eApartmentDbContext.Buildings.ToList();
        }

        public int GetCount()
        {
            return eApartmentDbContext.Buildings.Count();
        }

        public async Task Update(Guid? buildingId, Building building)
        {
            var existingBuilding = eApartmentDbContext.Buildings.Where(x=>x.Id == buildingId).FirstOrDefault();

            existingBuilding.Description = building.Description;
            existingBuilding.Address = building.Address;
            existingBuilding.Name = building.Name;

            eApartmentDbContext.Buildings.Update(existingBuilding);
            eApartmentDbContext.SaveChanges();
        }
    }
}
