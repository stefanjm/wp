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

            // algorithm
            return SortCups(cups);
        }

        private IList<SortedCupsModel> SortCups(IEnumerable<CupModel> cups)
        {
            var cupsList = cups.ToList();
            var sortedCupsList = new List<SortedCupsModel>();

  
            while(cupsList.Any())
            {
                // Cup Package
                var cupPackage = new SortedCupsModel();
                cupPackage.Id = sortedCupsList.Any() ? sortedCupsList.Last().Id++ : 1;


                // Find biggest one
                var biggestCup = new CupModel();
                foreach (var cup in cupsList)
                {
                    if (cup.Diameter > biggestCup.Diameter && cup.Height > biggestCup.Height)
                    {
                        biggestCup = cup;
                    }
                }

                // Add the biggest cup as the first one in the list where other cups will fit into
                cupPackage.SortedCups.Add(biggestCup);
                // Remove it from the available cups list to loop through 
                cupsList.Remove(biggestCup);

                // Fit cups into the biggest cup
                for(var i = 0; i < cupsList.Count(); i++)
                {
                    // start fitting cups into the biggest cup
                    var mostSuitableCup = new CupModel();

                    foreach (var cup in cupsList)
                    {
                        if (DoesCupFitCup(biggestCup, cup))
                        {
                            // Check if the current iteration cup would have less leftover volume, if yes then assign that to the most suitable cup
                            if (biggestCup.Volume() - cup.Volume() < (biggestCup.Volume() - mostSuitableCup.Volume()))
                            {
                                mostSuitableCup = cup;
                            }
                        }
                    }
                    // Check if there are any cups that fit, if not then the default cup has id 0
                    if (mostSuitableCup.Id == 0)
                        break;

                    // fit the most suitable cup / append to the list of this cup package
                    cupPackage.SortedCups.Add(mostSuitableCup);
                    // Assign the most suitable cup to be the biggest one (start fitting cups inside it)
                    biggestCup = mostSuitableCup;
                    // Remove it from the list since it's used now
                    cupsList.Remove(mostSuitableCup);
                }

                sortedCupsList.Add(cupPackage);
            }


            return sortedCupsList;
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
            else if(DoesCupFitSideways(cup1,cup2))
            {
                return true;
            }
            else
                return false;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cup1">Cup to fit into</param>
        /// <param name="cup2">Cup to fit</param>
        /// <returns></returns>
        private bool DoesCupFitSideways(CupModel cup1, CupModel cup2)
        {
            /*
             * 
             * d^2 + h^2 = pyth^2

                sqrt(pyth) < diameter?
             * 
             */
            if (Math.Sqrt((cup2.Diameter * cup2.Diameter) + (cup2.Height * cup2.Height)) < cup1.Diameter && cup1.Height > cup2.Diameter)
                return true;
            else
                return false;
        }
    }
}
