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

using System.Collections.Generic;

namespace UtilitiesLibrary.Hashing
{
    /// <summary>
    /// Methods to hash a string.  Methods are not cryptographically secure unless otherwise noted.
    /// </summary>
    public static class StringHasher
    {
        private static readonly List<int> PrimesList = new List<int> { 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 49, 53, 59, 61, 67, 71, 73, 77, 79, 83, 89, 91, 97, 101, 103, 107, 109, 113, 119, 121, 127, 131, 133, 137, 139, 143, 149, 151, 157, 161, 163, 167, 169, 173, 179 };
        private static readonly int NumPrimes = PrimesList.Count;

        /// <summary>
        /// Hashes a string into an integer that will be consistent between calls, but not necessarily between systems.  Uniqueness is not guaranteed, nor is this suitable for cryptographic uses.
        /// It is designed to be useful in HashSets<T> to filter out duplicates.
        /// If there is no tolerance for collisions, do not use this method.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int HashStringInsecure(string str)
        {
            int nextPrimeIndex = 0;
            unchecked
            {
                int hash = 0;
                foreach (char c in str)
                {
                    hash *= c * PrimesList[nextPrimeIndex];
                    if (++nextPrimeIndex >= NumPrimes)
                        nextPrimeIndex = 0;
                }
                return hash;
            }
        }
    }
}
