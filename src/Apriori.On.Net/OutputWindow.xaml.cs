namespace Apriori.On.Net {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class OutputWindow : Window {
        public OutputWindow() {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        public OutputWindow BuildOutput(Collection<Dictionary<Collection<Brush>, int>> scanResults) {
            foreach (var scanResult in scanResults) {
                var wrapContainer = new WrapPanel();

                foreach (var dic in scanResult) {
                    var border = new Border {Margin = new Thickness(0, 0, 10, 10), Padding = new Thickness(10), BorderBrush = new SolidColorBrush(Colors.Gainsboro), BorderThickness = new Thickness(1)};
                    var itemsWrapPanel = new WrapPanel();

                    border.Child = itemsWrapPanel;

                    foreach (var item in dic.Key) {
                        var ellipse = new Ellipse {Fill = item, Width = 48, Height = 48, Margin = new Thickness(5)};

                        itemsWrapPanel.Children.Add(ellipse);
                    }

                    wrapContainer.Children.Add(border);
                }

                scanResultsPanel.Children.Add(wrapContainer);

                //if (scanResult != scanResults.Last())
                //scanResultsPanel.Children.Add();
            }

            return this;
        }
    }
}