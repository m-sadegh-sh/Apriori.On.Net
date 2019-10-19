namespace Apriori.On.Net.Core {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class AprioriManager {
        private static readonly ICollection<Item> _items = new Collection<Item>();
        private static readonly ScanCollection _scanCollection = ScanCollection.Create();

        public static ScanCollection GetScans() {
            return _scanCollection;
        }

        public static void SaveItems(IEnumerable<Item> items) {
            foreach (var item in items)
                _items.Add(item);
        }

        public static Scan OpenNewScan() {
            var scan = Scan.Create();

            _scanCollection.Add(scan);

            return scan;
        }
    }
}