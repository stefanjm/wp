using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Interfaces;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Core.Services
{
    public class CupCalcService : ICupCalcService
    {

        private ICupRepository _cupRepository;

        public CupCalcService(ICupRepository cupRepository)
        {
            _cupRepository = cupRepository;
        }
        public async Task<IList<SortedCupsModel>> GetSortedCups()
        {
            // Get the cups
            var cups = await _cupRepository.GetAll();

            // Sort them / algorithm
            var sortedCupsList = new List<SortedCupsModel>();
            throw new NotImplementedException();
        }
    }
}
