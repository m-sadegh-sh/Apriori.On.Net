namespace Apriori.On.Net.Core {
    public class ScanCollection : BasicCollectionBase<Scan> {
        private ScanCollection() {}

        public static ScanCollection Create() {
            return new ScanCollection();
        }
    }
}