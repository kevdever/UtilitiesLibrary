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
    public static class Extensions
    {
        /// <summary>
        /// Split a collection into numChunks subsets
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitIntoNChunks<T>(this IEnumerable<T> source, int numChunks)
        {
            if (source is null) throw new ArgumentNullException("source");
            if (numChunks <= 0) throw new ArgumentOutOfRangeException("batchSize", "The batchSize parameter must be a positive value.");
            if (numChunks == 1)
            {
                yield return source;
                yield break;
            }

            var chunkSize = (int)Math.Ceiling(source.Count() / (double)numChunks);
            var numTaken = 0;
            while (source.Skip(numTaken).Any())
            {
                var subset = source.Skip(numTaken).Take(chunkSize);
                numTaken += subset.Count();
                yield return subset;
            }
        }

        /// <summary>
        /// Split a collection into subsets of size batchSize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitIntoBatchesOfSize<T>(this IEnumerable<T> source, int batchSize)
        {
            if (batchSize <= 0)
                throw new ArgumentOutOfRangeException("batchsize must be greater than zero");

            var numTaken = 0;
            while (source.Skip(numTaken).Any())
            {
                var subset = source.Skip(numTaken).Take(batchSize);
                numTaken += subset.Count();
                yield return subset;
            }
        }

        #region Shuffle
        //source: https://stackoverflow.com/a/5807238/2655263
        //The license for the methods in this Shuffle region flow through from the source linked above.
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
        #endregion
    }
}
