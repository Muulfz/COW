using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Depset
{

    /// <summary>
    /// Represents a Depset Handle sorter.
    /// </summary>
    /// <typeparam name="T">The depset handle to be sorted</typeparam>
    public interface ISorter<T> where T : IDepsetHandle
    {
        /// <summary>
        /// Sorts the given input list via the given algorithm.
        /// </summary>
        /// <param name="input">The depset handle list</param>
        /// <returns>The sorted depset handle list</returns>
        List<T> Sort(List<T> input);
    }
}
