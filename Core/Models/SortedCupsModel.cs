using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoodPeckerAngular.Core.Models
{
    public class SortedCupsModel
    {
        public int Id { get; set; }
        public IList<CupModel> SortedCups { get; set; } = new List<CupModel>();
    }
}
