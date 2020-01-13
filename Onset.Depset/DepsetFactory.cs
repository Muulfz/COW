using System;
using System.Collections.Generic;
using System.Text;
using Onset.Depset.Sorters;

namespace Onset.Depset
{
    /// <summary>
    /// The base class for creating depset instances.
    /// </summary>
    public sealed class DepsetFactory
    {
        /// <summary>
        /// Creates the wanted sorting with the given algorithm.
        /// </summary>
        /// <typeparam name="T">The depset handle type</typeparam>
        /// <param name="algorithm">The algorithm of the sorting</param>
        /// <returns>The created sorter</returns>
        public static ISorter<T> CreateSorter<T>(Algorithm algorithm) where T : IDepsetHandle
        {
            switch (algorithm)
            {
                case Algorithm.PriorValuing:
                    return new PriorValuingSorter<T>();
                case Algorithm.WaitForParent:
                    return new WaitForParentSorter<T>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }
        }
    }
}
