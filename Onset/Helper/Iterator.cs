using System.Collections.Generic;

namespace Onset.Helper
{
    /// <summary>
    /// The implementation of an java iterator. It iterates step by step through list selecting every item,
    /// so that the user can decide want to do with this item.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    public class Iterator<T>
    {
        private readonly IList<T> _list;
        private int _currentIndex;
        private T _lastItem;

        public Iterator(IList<T> list)
        {
            _list = list;
            _currentIndex = 0;
        }

        public T Next()
        {
            _lastItem = _list[_currentIndex];
            _currentIndex++;
            return _lastItem;
        }

        public bool HasNext()
        {
            return _list.Count > _currentIndex;
        }

        public void Remove()
        {
            if (_lastItem != null && _list.Contains(_lastItem))
            {
                _list.Remove(_lastItem);
            }
        }
    }
}
