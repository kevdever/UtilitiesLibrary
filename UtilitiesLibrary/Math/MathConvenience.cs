using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary.MathConvenience
{
    public class MathConvenience
    {
        public static double Max(params double[] values)
        {
            if (values is null) throw new ArgumentNullException("values");
            if (!values.Any()) throw new ArgumentException("no items provided.");
            return values.Max();
        }

        public static double Min(params double[] values)
        {
            if (values is null) throw new ArgumentNullException("values");
            if (!values.Any()) throw new ArgumentException("no items provided.");
            return values.Min();
        }
    }
}
