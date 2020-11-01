using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Algorithms;
using WoodPeckerAngular.Core.Interfaces;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Core.Services
{
    public class CupCalcService : ICupCalcService
    {

        private ICupRepository _cupRepository;
        private CupSortingAlgorithm _cupSortingAlgorithm;

        public CupCalcService(ICupRepository cupRepository, CupSortingAlgorithm cupSortingAlgorithm)
        {
            _cupRepository = cupRepository;
            _cupSortingAlgorithm = cupSortingAlgorithm;
        }
        public async Task<IList<SortedCupsModel>> GetSortedCups()
        {
            // Get the cups
            var cups = await _cupRepository.GetAll();

            // Run the algorithm to fit the cups
            return _cupSortingAlgorithm.SortCups(cups.ToList());
        }

        
    }
}
