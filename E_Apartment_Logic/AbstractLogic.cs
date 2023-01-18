using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace E_Apartment_Logic
{
    public class AbstractLogic
    {
        protected readonly EApartmentDbContext eApartmentDbContext;
        public AbstractLogic(EApartmentDbContext eApartmentDbContext)
        {
            this.eApartmentDbContext = eApartmentDbContext;
        }

        public string GetCode(DocumentRef documentRef, int recordCount)
        {
            var code = documentRef.ToString();

            code = code.Replace("@@YEAR@@", DateTime.Now.ToString("yy"));
            code = code.Replace("@@MONTH@@", DateTime.Now.Month.ToString("D2"));
            code = code.Replace("@@DAY@@", DateTime.Now.Day.ToString("D2"));
            code = code.Replace("@@NO@@", (recordCount + 1).ToString("D4"));
            code.Trim();

            return code;
        }        
    }
}
