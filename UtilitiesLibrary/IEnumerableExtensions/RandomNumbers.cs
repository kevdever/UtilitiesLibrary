/*
 * Copyright (c) 2018 FichterApps, LLC

 MIT License
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary.IEnumerableExtensions
{
    public class RandomNumbers
    {
        /// <summary>
        /// Returns a sequence of {numValues} between 0 and maxVal. Whether duplicates are allowed in the resultset is determined by the allowRepetition flag.
        /// </summary>
        /// <param name="numValues"></param>
        /// <param name="maxVal"></param>
        /// <param name="allowRepetition"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetRandomNumbers(int numValues, int maxVal, bool allowRepetition = false)
        {
            if (allowRepetition)
                return GetRandomNumbersRepetitionOk(numValues, maxVal);
            else
                return GetUniqueRandomNumbers(numValues, maxVal);
        }

        /// <summary>
        /// Returns a sequence of {numValues} between 0 and maxVal. The output will not contain duplicates.
        /// </summary>
        /// <param name="numValues"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        private static IEnumerable<int> GetUniqueRandomNumbers(int numValues, int maxVal)
        {
            var rand = new Random();
            var yieldedValues = new HashSet<int>();

            int counter = 0;
            while (counter < numValues)
            {
                var r = rand.Next(maxVal);
                //if the test below returns false, then the item was already returned, so skip it and return to the loop.
                if (yieldedValues.Add(r))
                {
                    counter++;
                    yield return r;
                }
            }
        }
        /// <summary>
        /// Returns a sequence of {numValues} between 0 and maxVal. The output may contain duplicates.
        /// </summary>
        /// <param name="numValues"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        private static IEnumerable<int> GetRandomNumbersRepetitionOk(int numValues, int maxVal)
        {
            var rand = new Random();
            for (int i = 0; i < numValues; i++)
            {
                yield return rand.Next(maxVal);
            }
        }
    }
}
