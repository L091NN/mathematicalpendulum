using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathpen
{
    public class Functions
    {
        public double g { set; get; }
        public double L { set; get; }

        public double f1(double _x, double _u1, double _u2)
        {
            return _u2;
        }

        public double f2(double _x, double _u1, double _u2)
        {
            return -(g / L) * Math.Sin(_u1);
        }
    }
}
