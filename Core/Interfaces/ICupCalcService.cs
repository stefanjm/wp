using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Core.Interfaces
{
    public interface ICupCalcService
    {
        Task<IList<SortedCupsModel>> GetSortedCups();
    }
}
