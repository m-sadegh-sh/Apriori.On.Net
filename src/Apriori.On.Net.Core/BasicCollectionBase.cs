namespace Apriori.On.Net.Core {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class BasicCollectionBase<T> {
        private readonly ICollection<T> _items = new Collection<T>();

        public void Add(T value) {
            _items.Add(value);
        }

        public void Remove(T value) {
            _items.Remove(value);
        }

        public IEnumerable<T> Items {
            get { return _items; }
        }
    }
}