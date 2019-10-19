namespace Apriori.On.Net {
    using System.Windows.Media;

    public class Item {
        private Item() {}

        public static Item Create(Brush identifier) {
            return new Item {Identifier = identifier};
        }

        public Brush Identifier { get; private set; }
    }
}