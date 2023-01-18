using E_Apartment_DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = E_Apartment_DataAccess.EfCore;

namespace E_Apartment_Logic.Logic
{
    public interface ILoginLogic
    {
        Task Add(User user);
        Task<dbCore.User> FindByUserName(String userName);
        Task<List<dbCore.UsersType>> GetAllTypes();
    }
}
