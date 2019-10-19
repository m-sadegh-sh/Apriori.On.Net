namespace Apriori.On.Net.Core {
    public class Scan : BasicCollectionBase<Transaction> {
        private Scan() {}

        public static Scan Create() {
            return new Scan();
        }
    }
}