using E_Apartment_DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Apartment_DataAccess.EfCore;

namespace E_Apartment_Logic.Logic
{
    public class LoginLogic : AbstractLogic, ILoginLogic
    {
        public LoginLogic(EApartmentDbContext eApartmentDbContext) : base(eApartmentDbContext)
        {
        }

        public async Task Add(User user)
        {
            
            await eApartmentDbContext.Users.AddAsync(user);
            await eApartmentDbContext.SaveChangesAsync();
        }

        public async Task<User> FindByUserName(string userName)
        {            
            return eApartmentDbContext.Users.Where(x => x.Username == userName.Trim()).FirstOrDefault();
        }

        public async Task<List<UsersType>> GetAllTypes()
        {
            return await eApartmentDbContext.UsersTypes.ToListAsync();
        }
    }
}
