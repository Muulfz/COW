using System.Collections.Generic;

namespace Onset.Depset.Sorters
{
    internal class WaitForParentSorter<T> : ISorter<T> where T : IDepsetHandle
    {
        public List<T> Sort(List<T> input)
        {
            List<T> sorted = new List<T>();
            int idx = 0;
            while (input.Count > 0)
            {
                T current = input[idx];
                if (current.Dependencies.Count > 0)
                {
                    if (AreDependenciesLoaded(sorted, current))
                    {
                        input.Remove(current);
                        sorted.Add(current);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    input.Remove(current);
                    sorted.Add(current);
                }
                idx++;
                if (idx >= input.Count)
                {
                    idx = 0;
                }
            }

            return sorted;
        }

        private static bool AreDependenciesLoaded(List<T> sorted, T current)
        {
            int i = 0;
            foreach (string name in current.Dependencies)
            {
                if (IsNameExisting(sorted, name))
                    i++;
            }

            return i == current.Dependencies.Count;
        }

        private static bool IsNameExisting(List<T> sorted, string name)
        {
            foreach (T sort in sorted)
            {
                if (sort.Identifier == name)
                    return true;
            }

            return false;
        }
    }
}
