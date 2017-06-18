using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HearthCollect
{
    class RarityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Rarity rarity = (Rarity)value;
            if (rarity == Rarity.COMMON)
                return Brushes.Black;
            if (rarity == Rarity.RARE)
                return Brushes.Blue;
            if (rarity == Rarity.EPIC)
                return Brushes.Purple;
            if (rarity == Rarity.LEGENDARY)
                return Brushes.DarkOrange;
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ClassToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CardClass @class = (CardClass)value;
            switch (@class)
            {
                case CardClass.DRUID:
                    return Brushes.Goldenrod;
                case CardClass.HUNTER:
                    return Brushes.PaleGreen;
                case CardClass.MAGE:
                    return Brushes.LightSkyBlue;
                case CardClass.NEUTRAL:
                    return Brushes.LightGray;
                case CardClass.PALADIN:
                    return Brushes.LightPink;
                case CardClass.PRIEST:
                    return Brushes.White;
                case CardClass.ROGUE:
                    return Brushes.LightYellow;
                case CardClass.SHAMAN:
                    return Brushes.Cyan;
                case CardClass.WARLOCK:
                    return new SolidColorBrush(Color.FromRgb(0x99,0x66,0xff));
                case CardClass.WARRIOR:
                    return Brushes.Tan;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
