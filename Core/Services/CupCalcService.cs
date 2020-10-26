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

            // algorithm

            foreach (var cup in cups)
            {

            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cup1">Cup to fit into</param>
        /// <param name="cup2">Cup to fit</param>
        /// <returns></returns>
        private bool DoesCupFitCup(CupModel cup1, CupModel cup2)
        {
            if (cup1.Diameter > cup2.Diameter && cup1.Height > cup2.Height)
            {
                return true;
            }
            else if(cup1.Diameter > cup2.Height && cup1.Height > cup2.Diameter)
            {
                return true;
            }
            else
                return false;
        } 
    }
}
