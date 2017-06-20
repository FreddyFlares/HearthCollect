using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HearthCollect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ICollectionView view;
        int dustValue=0;
        public int DustValue { get { return dustValue; } set { dustValue = value; OnPropertyChanged(); } }
        int excessDust = 0;
        public int ExcessDust { get { return excessDust; } set { excessDust = value; OnPropertyChanged(); } }
        int excessDustPreserveGoldens = 0;
        public int ExcessDustPreserveGoldens { get { return excessDustPreserveGoldens; } set { excessDustPreserveGoldens = value; OnPropertyChanged(); } }
        int excessDustIgnoreGolden =0;
        public int ExcessDustIgnoreGolden { get { return excessDustIgnoreGolden; } set { excessDustIgnoreGolden = value; OnPropertyChanged(); } }

        DispatcherTimer timer;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.UnselectAll();
            // Get the custom view that was created in the xaml, not the default view. Page 395 wpf4u.
            view = ((CollectionViewSource)Resources["viewSource"]).View;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            view.Filter = Filter;
            FilterRefresh();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            FilterRefresh();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            timer.Stop();
            timer.Start();
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
            if (txtSearch.Text != "" && !c.Name.ToLowerInvariant().Contains(txtSearch.Text.ToLowerInvariant()))
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
                switch (c.Rarity)
                {
                    case Rarity.COMMON:
                        DustValue += c.Normal * 5 + c.Golden * 50;
                        ExcessDust += Math.Min(Math.Max(0, c.Total - 2), c.Golden) * 50 + (Math.Max(0, c.Normal - 2)) * 5;
                        ExcessDustPreserveGoldens += Math.Min(Math.Max(0, c.Total - 2), c.Normal) * 5 + (Math.Max(0, c.Golden - 2)) * 50;
                        ExcessDustIgnoreGolden += Math.Max(0, c.Normal - 2) *5;
                        break;
                    case Rarity.RARE:
                        DustValue += c.Normal * 20 + c.Golden * 100;
                        ExcessDust += Math.Min(Math.Max(0, c.Total - 2), c.Golden) * 100 + (Math.Max(0, c.Normal - 2)) * 20;
                        ExcessDustPreserveGoldens += Math.Min(Math.Max(0, c.Total - 2), c.Normal) * 20 + (Math.Max(0, c.Golden - 2)) * 100;
                        ExcessDustIgnoreGolden += Math.Max(0, c.Normal - 2) * 20;
                        break;
                    case Rarity.EPIC:
                        DustValue += c.Normal * 100 + c.Golden * 400;
                        ExcessDust += Math.Min(Math.Max(0, c.Total - 2), c.Golden) * 400 + (Math.Max(0, c.Normal - 2)) * 100;
                        ExcessDustPreserveGoldens += Math.Min(Math.Max(0, c.Total - 2), c.Normal) * 100 + (Math.Max(0, c.Golden - 2)) * 400;
                        ExcessDustIgnoreGolden += Math.Max(0, c.Normal - 2) * 100;
                        break;
                    case Rarity.LEGENDARY:
                        DustValue += c.Normal * 400 + c.Golden * 1600;
                        ExcessDust += Math.Min(Math.Max(0, c.Total - 1), c.Golden) * 1600 + (Math.Max(0, c.Normal - 1)) * 400;
                        ExcessDustPreserveGoldens += Math.Min(Math.Max(0, c.Total - 1), c.Normal) * 400 + (Math.Max(0, c.Golden - 1)) * 1600;
                        ExcessDustIgnoreGolden += Math.Max(0, c.Normal - 1) * 400;
                        break;
                }
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
