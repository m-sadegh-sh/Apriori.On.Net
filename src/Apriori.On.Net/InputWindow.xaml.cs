namespace Apriori.On.Net {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Apriori.On.Net.Core;

    public partial class InputWindow : Window {
        private Point _startPoint;

        public InputWindow() {
            InitializeComponent();
        }

        private void PanelsPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            _startPoint = e.GetPosition(null);
        }

        private void PanelsPreviewMouseMove(object sender, MouseEventArgs e) {
            var diff = _startPoint - e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed && Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance) {
                var item = FindAnchestor<Ellipse>((DependencyObject) e.OriginalSource);

                if (item == null)
                    return;

                var dragData = new DataObject("item", item);
                DragDrop.DoDragDrop(item, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject {
            do {
                if (current is T)
                    return (T) current;
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }

        private void PanelsDragEnter(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent("item") || sender == e.Source)
                e.Effects = DragDropEffects.None;
        }

        private void PanelsDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent("item")) {
                var item = e.Data.GetData("item") as Ellipse;

                if (item == null)
                    return;

                var sourcePanel = (WrapPanel) item.Parent;
                var destinationPanel = sender as WrapPanel;

                if (destinationPanel == null || ReferenceEquals(sourcePanel, destinationPanel))
                    return;

                sourcePanel.Children.Remove(item);
                destinationPanel.Children.Add(item);
            }
        }

        private void ResetItems() {
            foreach (var child in newTransPanel.Children.Cast<UIElement>().ToList()) {
                newTransPanel.Children.Remove(child);
                sourcePanel.Children.Add(child);
            }
        }

        private void clearTransButton_Click(object sender, RoutedEventArgs e) {
            generatedTransPanel.Children.Clear();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e) {
            if (newTransPanel.Children.Count == 0)
                return;

            var newTransItemIdentities = newTransPanel.Children.Cast<Ellipse>().Select(i => i.Fill).ToList();

            //AprioriManager.SaveItems(newTransItemIdentities.Select(Item.Create));

            foreach (var trans in generatedTransPanel.Children.Cast<Border>().ToList()) {
                var itemIdentities = ((WrapPanel) trans.Child).Children.Cast<Ellipse>().Select(i => i.Fill).ToList();
                if (itemIdentities.Except(newTransItemIdentities).Count() == 0 && newTransItemIdentities.Except(itemIdentities).Count() == 0) {
                    ResetItems();
                    return;
                }
            }

            var border = new Border {Margin = new Thickness(0, 0, 10, 10), Padding = new Thickness(10), BorderBrush = new SolidColorBrush(Colors.Gainsboro), BorderThickness = new Thickness(1)};
            var newTransWrapPanel = new WrapPanel();

            border.Child = newTransWrapPanel;

            foreach (var child in newTransPanel.Children.Cast<Ellipse>().ToList()) {
                var newItem = new Ellipse {Width = child.Width, Height = child.Height, Stroke = child.Stroke, Fill = child.Fill, Margin = child.Margin};
                newTransWrapPanel.Children.Add(newItem);
            }

            generatedTransPanel.Children.Add(border);

            ResetItems();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void computeButton_Click(object sender, RoutedEventArgs e) {
            int minimumSuppprt;
            if (!int.TryParse(minimumSupprtTextBox.Text, out minimumSuppprt)) {
                minimumSupprtTextBox.Focus();
                return;
            }

            if (generatedTransPanel.Children.Count == 0)
                return;

            var originalItemSet = new Dictionary<Collection<Brush>, int>();
            var allTransItems = generatedTransPanel.Children.Cast<Border>().Select(x => ((WrapPanel) x.Child).Children.Cast<Ellipse>().Select(i => i.Fill)).SelectMany(x => x).ToList();

            foreach (var trans in generatedTransPanel.Children.Cast<Border>().ToList()) {
                var itemIdentities = ((WrapPanel) trans.Child).Children.Cast<Ellipse>().Select(i => i.Fill).ToList();
                foreach (var itemIdentity in itemIdentities) {
                    if (!originalItemSet.Keys.Any(i => i.Any(b => b == itemIdentity))) {
                        var support = allTransItems.Count(i => i == itemIdentity);
                        if (support >= minimumSuppprt)
                            originalItemSet.Add(new Collection<Brush> {itemIdentity}, support);
                    }
                }
            }

            var scanResults = new Collection<Dictionary<Collection<Brush>, int>> {originalItemSet};

            while (scanResults.Any(p => p.Count > 1)) {
                var newItemSet = new Dictionary<Collection<Brush>, int>();

                foreach (var thisItem in originalItemSet.Keys) {
                    var items = new Collection<Brush>();
                    foreach (var otherItem in originalItemSet.Keys) {
                        if (thisItem.Except(otherItem).Count() == 0)
                            continue;

                        // items.Add(thisItem.);
                    }
                }
            }

            new OutputWindow().BuildOutput(scanResults).ShowDialog();
        }
    }
}