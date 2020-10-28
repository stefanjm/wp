using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoodPeckerAngular.Core.Models
{
    public class CupModel
    {
        public int Id { get; set; }
        public double Diameter { get; set; }
        public double Height { get; set; }

        public double Volume()
        {
            var radius = Diameter / 2;
            return Math.PI * (radius * radius) * Height;
        }
    }
}
