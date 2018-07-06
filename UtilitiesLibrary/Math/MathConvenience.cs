using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary.MathConvenience
{
    public class MathConvenience
    {
        /// <summary>
        /// A method wrapper for Enumerable.Max accepting an arbitrary number of values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The maximum value from the provided arguments</returns>
        public static double Max(params double[] values)
        {
            if (values is null) throw new ArgumentNullException("values");
            if (!values.Any()) throw new ArgumentException("no items provided.");
            return values.Max();
        }

        /// <summary>
        /// A method wrapper for Enumerable.Min accepting an arbitrary number of values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The maximum value from the provided arguments</returns>
        public static double Min(params double[] values)
        {
            if (values is null) throw new ArgumentNullException("values");
            if (!values.Any()) throw new ArgumentException("no items provided.");
            return values.Min();
        }
    }
}
