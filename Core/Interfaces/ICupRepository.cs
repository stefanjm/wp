using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Core.Interfaces
{
    public interface ICupRepository
    {
        Task<IEnumerable<CupModel>> GetAll();

        Task<CupModel> GetById(int id);

        Task<bool> Insert(CupModel cup);

        Task<CupModel> Delete(CupModel cup);
    }
}
