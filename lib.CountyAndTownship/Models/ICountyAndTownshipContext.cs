using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.CountyAndTownship.Models
{
    public interface ICountyAndTownshipContext
    {
        DbSet<Township> Township { get; set; }

        DbSet<County> County { get; set; }
    }
}
