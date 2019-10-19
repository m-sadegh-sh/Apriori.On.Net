namespace Apriori.On.Net.Core {
    public class Transaction : BasicCollectionBase<Item> {
        private Transaction() {}

        public static Transaction Create() {
            return new Transaction();
        }
    }
}