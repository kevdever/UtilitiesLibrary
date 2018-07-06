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
        /// Split a collection into {numChunks} as a collection of subsets.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An IEnumerable</param>
        /// <param name="numChunks">The desired number of batches/chunks. Must be positive.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitIntoNChunks<T>(this IEnumerable<T> source, int numChunks)
        {
            if (numChunks <= 0) throw new ArgumentOutOfRangeException("batchSize", "The batchSize parameter must be a positive value.");

            var numItems = source.Count();

            //get the chunksize NumItems/NumChunks, but force floating-point math so that the truncation and result is explicit.
            //If numItems/numChunks is not a whole number, ensure that the number of batches is respected by forcing the chunkSize to the ceiling
            //e.g., for a collection of size 7 with a desired batch size of 2, you want collections of size 3,3,1. (If you floor it, then items are lost or the number of batches grows)
            var chunkSize = (int)(Math.Ceiling(numItems / (double)numChunks));
            //As a failsafe, if the chunkSize is less than one, then set it to one so that the loop in the called method below can complete normally.
            if (chunkSize < 1)
                chunkSize = 1;

            return source.SplitIntoBatchesOfSize(chunkSize);
        }

        /// <summary>
        /// Split a collection into subsets of size {batchSize}.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="batchSize">The desired number of items per batch. Must be a positive integer.</param>
        /// <returns>A collection of collections of max size batchSize</returns>
        public static IEnumerable<IEnumerable<T>> SplitIntoBatchesOfSize<T>(this IEnumerable<T> source, int batchSize)
        {
            if (batchSize < 1)
                throw new ArgumentOutOfRangeException("batchSize", "batchSize must be a positive integer.");

            var numItems = source.Count();

            var numTaken = 0;
            while (numTaken + batchSize < numItems) //If taking the next {chunkSize} elements will exceed the size of the source collection, then end the loop and return the remainder
            {
                yield return source.Skip(numTaken).Take(batchSize);
                numTaken += batchSize;
            }
            yield return source.Skip(numTaken);
        }

        #region Shuffle
        //source: https://stackoverflow.com/a/5807238/2655263
        //The license for the methods in this Shuffle region flow through from the source linked above.
        /// <summary>
        /// Shuffle a collection randomly, returning a new collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>A new shuffled collection</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        /// <summary>
        /// Shuffle a collection randomly, returning a new collection. This overload allows you to provide your own instance of Random.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="rng">An instance of Random</param>
        /// <returns>A new shuffled collection</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        /// <summary>
        /// Shuffle a collection randomly using the provided Random instance, returning a new collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="rng"></param>
        /// <returns>A new shuffled collection</returns>
        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                //get a random (unused) index (between the counter i and the end of the array)
                int j = rng.Next(i, buffer.Count);
                //return the item at that index
                yield return buffer[j];
                //since j >= i, and j was just spent and counter i will be incremented (so the item at i might never otherwise be hit), place whatever was at i into j's slot.
                buffer[j] = buffer[i];
            }
        }
        #endregion
    }
}
