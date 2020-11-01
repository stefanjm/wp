using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Core.Algorithms
{
    public class CupSortingAlgorithm
    {
        /// <summary>
        /// Sort the cups to fit into each other in packages either upright or sideways
        /// </summary>
        /// <param name="cupsList">A list of cups to fit</param>
        /// <returns></returns>
        public IList<SortedCupsModel> SortCups(IList<CupModel> cupsList)
        {
            // 
            var sortedCupsList = new List<SortedCupsModel>();

            while (cupsList.Any())
            {
                // Define one package of cups and give id
                var cupPackage = new SortedCupsModel
                {
                    Id = sortedCupsList.Any() ? sortedCupsList.Last().Id++ : 1
                };


                // Find the current biggest cup
                var biggestCup = FindBiggestCup(cupsList);
                // Add the biggest cup as the first one in the list where other cups will fit into
                cupPackage.SortedCups.Add(biggestCup);
                // Remove it from the available cups list to loop through 
                cupsList.Remove(biggestCup);

                // Start fitting cups into the current biggest cup
                for (var i = 0; i < cupsList.Count(); i++)
                {
                    // Most perfect candidate that would fit the biggest cup
                    var mostSuitableCup = new CupModel();

                    foreach (var cup in cupsList)
                    {
                        if (DoesCupFitCup(biggestCup, cup))
                        {
                            // Check if the current iteration cup would have less leftover volume than the current most suitable cup, if yes then assign that to the most suitable cup
                            if (biggestCup.Volume() - cup.Volume() < (biggestCup.Volume() - mostSuitableCup.Volume()))
                            {
                                mostSuitableCup = cup;
                            }
                        }
                    }

                    // Check if there were any suitable cups found, if not then start again with a new package
                    if (mostSuitableCup.Id == 0)
                        break;

                    // fit the most suitable cup / append to the list of this cup package
                    cupPackage.SortedCups.Add(mostSuitableCup);
                    // Remove it from the list since it's used now
                    cupsList.Remove(mostSuitableCup);
                    // Assign the most suitable cup to be the biggest one (start fitting cups inside it)
                    biggestCup = mostSuitableCup;
                }

                sortedCupsList.Add(cupPackage);
            }


            return sortedCupsList;
        }

        /// <summary>
        /// Compares two cups to see if one fits inside another
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
            else if (DoesCupFitSideways(cup1, cup2))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// See if a cup fits into another cup sideways
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

        private CupModel FindBiggestCup(IList<CupModel> cups)
        {
            var biggestCup = new CupModel();
            foreach (var cup in cups)
            {
                if (cup.Diameter > biggestCup.Diameter && cup.Height > biggestCup.Height)
                {
                    biggestCup = cup;
                }
            }

            return biggestCup;
        }
    }
}
