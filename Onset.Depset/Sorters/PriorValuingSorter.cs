using System.Collections.Generic;
using System.Linq;

namespace Onset.Depset.Sorters
{
    internal class PriorValuingSorter<T> : ISorter<T> where T : IDepsetHandle
    {
        public List<T> Sort(List<T> input)
        {
            PrioritizedList list = new PrioritizedList(input);
            foreach (T handle in input)
            {
                if (handle.Dependencies.Count > 0)
                {
                    foreach (string parent in handle.Dependencies)
                    {
                        list.Increment(handle, list.GetRise(parent));
                    }
                }
            }
            return list.Convert();
        }

        internal class PrioritizedList
        {
            internal List<PrioritizedItem> Items { get; }

            internal PrioritizedList(List<T> input)
            {
                Items = new List<PrioritizedItem>();
                foreach (T handle in input)
                {
                    Items.Add(new PrioritizedItem(handle));
                }
            }

            internal void Increment(T handle, int value)
            {
                foreach (PrioritizedItem item in Items)
                {
                    if (item.Handle.Identifier == handle.Identifier)
                    {
                        item.Value += value;
                    }
                    else if (item.Handle.Dependencies.Contains(handle.Identifier))
                    {
                        Increment(item.Handle, value + 1);
                    }
                }
            }

            internal int GetRise(string parent)
            {
                foreach (PrioritizedItem item in Items)
                {
                    if (item.Handle.Identifier == parent)
                    {
                        return item.Value + 1;
                    }
                }
                return 1;
            }

            internal List<T> Convert()
            {
                return Items.OrderBy(o => o.Value).Select(item => item.Handle).ToList();
            }
        }

        internal class PrioritizedItem
        {
            internal int Value { get; set; }

            internal T Handle { get; }

            internal PrioritizedItem(T handle)
            {
                Value = 0;
                Handle = handle;
            }
        }
    }
}
