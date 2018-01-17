using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

namespace HearthCollect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ICollectionView view;
        int dustValue = 0;
        public int DustValue { get { return dustValue; } set { dustValue = value; OnPropertyChanged(); } }
        int excessDust = 0;
        public int ExcessDust { get { return excessDust; } set { excessDust = value; OnPropertyChanged(); } }
        int excessDustPreserveGoldens = 0;
        public int ExcessDustPreserveGoldens { get { return excessDustPreserveGoldens; } set { excessDustPreserveGoldens = value; OnPropertyChanged(); } }
        int excessDustIgnoreGolden = 0;
        public int ExcessDustIgnoreGolden { get { return excessDustIgnoreGolden; } set { excessDustIgnoreGolden = value; OnPropertyChanged(); } }
        string filterString = "";
        public string FilterString { get { return filterString; } set { filterString = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.UnselectAll();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the custom view that was created in the xaml, not the default view. Page 395 wpf4u.
            CollectionViewSource viewSource = (CollectionViewSource)Resources["viewSource"];
            view = viewSource.View;
            view.Filter = Filter;
            FilterRefresh();
        }

        private void txtSearch_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            // Note the Binding.Delay property set in the xaml
            // which causes the binding update to happen 'Delay' ms after the last property change
            FilterRefresh();
        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).SynchronizeCollectionCommand.Execute(null);
            FilterRefresh();
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            ButtonBase b = (ButtonBase)sender;
            foreach (ToggleButton t in GetLeafElements<ToggleButton>((Panel)b.Parent))
            {
                t.IsChecked = true;
            }
            FilterRefresh();
        }

        private void btnNone_Click(object sender, RoutedEventArgs e)
        {
            ButtonBase b = (ButtonBase)sender;
            foreach (ToggleButton t in GetLeafElements<ToggleButton>((Panel)b.Parent))
            {
                t.IsChecked = false;
            }
            FilterRefresh();
        }

        IEnumerable<T> GetLeafElements<T>(Panel panel) where T : UIElement
        {
            foreach (UIElement element in panel.Children)
            {
                if (element is T)
                    yield return element as T;
                else
                if (element is Panel)
                {
                    foreach (UIElement elem in GetLeafElements<T>(element as Panel))
                    {
                        yield return elem as T;
                    }
                }
            }
        }

        bool Filter(object o)
        {
            Card c = o as Card;
            if (!(btnOwned.IsChecked ?? false) && c.Total > 0)
                return false;
            if (!(btnMissing.IsChecked ?? false) && c.Total == 0)
                return false;
            if (!(btnNaxx.IsChecked ?? false) && c.Set == CardSet.NAXX)
                return false;
            if (!(btnTGT.IsChecked ?? false) && c.Set == CardSet.TGT)
                return false;
            if (!(btnBRM.IsChecked ?? false) && c.Set == CardSet.BRM)
                return false;
            if (!(btnCore.IsChecked ?? false) && c.Set == CardSet.CORE)
                return false;
            if (!(btnGangs.IsChecked ?? false) && c.Set == CardSet.GANGS)
                return false;
            if (!(btnExpert.IsChecked ?? false) && c.Set == CardSet.EXPERT1)
                return false;
            if (!(btnGvG.IsChecked ?? false) && c.Set == CardSet.GVG)
                return false;
            if (!(btnKara.IsChecked ?? false) && c.Set == CardSet.KARA)
                return false;
            if (!(btnHoF.IsChecked ?? false) && c.Set == CardSet.HOF)
                return false;
            if (!(btnHero.IsChecked ?? false) && c.Set == CardSet.HERO_SKINS)
                return false;
            if (!(btnLoE.IsChecked ?? false) && c.Set == CardSet.LOE)
                return false;
            if (!(btnOG.IsChecked ?? false) && c.Set == CardSet.OG)
                return false;
            if (!(btnUngoro.IsChecked ?? false) && c.Set == CardSet.UNGORO)
                return false;
            if (!(btnIcecrown.IsChecked ?? false) && c.Set == CardSet.ICECROWN)
                return false;
            if (!(btnLootapalooza.IsChecked ?? false) && c.Set == CardSet.LOOTAPALOOZA)
                return false;
            if (FilterString != "" && !c.Name.ToLowerInvariant().Contains(FilterString.ToLowerInvariant()))
                return false;
            return true;
        }

        private void UpdateDustValue()
        {
            DustValue = 0;
            ExcessDust = 0;
            ExcessDustPreserveGoldens = 0;
            ExcessDustIgnoreGolden = 0;
            foreach (Card c in view)
            {
                DustValue += c.TotalDustValue;
                ExcessDust += c.ExcessDust;
                ExcessDustPreserveGoldens += c.ExcessDustPreserveGoldens;
                ExcessDustIgnoreGolden += c.ExcessDustIgnoreGoldens;
            }
        }

        void FilterRefresh()
        {
            view.Refresh();
            UpdateDustValue();
        }

        private void FilterRefresh(object sender, RoutedEventArgs e)
        {
            FilterRefresh();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
