using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdate {

    /// <summary>
    /// 最近访问的历史记录
    /// </summary>
    public class PathHistoryCollection : ICollection<string> {
        private List<string> _items = new List<string>();

        public int Count {
            get {
                return Math.Min(_items.Count,_maxCapacity);
            }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        private int _maxCapacity = 10;
        public int MaxCapacity {
            get { return _maxCapacity; }
            set {
                if (_maxCapacity != value) {
                    if (value < 0) {
                        value = 0;
                    }
                    _maxCapacity = value;
                }
            }
        }

        public void Add(string path) {
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            path = RepairPath(path);
            var index = IndexOf(path);
            if (index < 0) {
                _items.Insert(0, path);
                if (_items.Count > _maxCapacity) {
                    _items.RemoveAt(_items.Count - 1);
                }
            }
            else {
                //重复
                if (index > 0) {
                    _items.RemoveAt(index);
                    _items.Insert(0, path);
                }
            }
        }

        public string this[int index] {
            get { return _items[index]; }
        }

        private string RepairPath(string path) {
            path = path.Trim();
            if (path.EndsWith(@"\")) {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        private int IndexOf(string path) {
            for (int i = 0; i < _items.Count; i++) {
                if (string.Equals(_items[i],path, StringComparison.CurrentCultureIgnoreCase)) {
                    return i;
                }
            }

            return -1;
        }

        public void Clear() {
            _items.Clear();
        }

        public bool Contains(string item) {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(string[] array, int arrayIndex) {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator() {
            var count = Math.Min(_items.Count, _maxCapacity);
            for (int i = 0; i < count; i++) {
                yield return _items[i];
            }
        }

        public bool Remove(string item) {
            var index = IndexOf(item);
            if (index >= 0) {
                _items.RemoveAt(index);
                return true;
            }
            else {
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public string[] ToArray() {
            string[] array = new string[Count];
            int i = 0;
            foreach (var item in this) {
                array[i++] = item;
            }

            return array;
        }
    }
}
