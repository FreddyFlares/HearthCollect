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
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x7D, 0x0B));
                case CardClass.HUNTER:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0xD8, 0x73));
                case CardClass.MAGE:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0x6A, 0xCC, 0xF1));
                case CardClass.NEUTRAL:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xE4, 0xE4, 0xE4));
                case CardClass.PALADIN:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xF6, 0x8C, 0xBA));
                case CardClass.PRIEST:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                case CardClass.ROGUE:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xFE, 0xF6, 0x67));
                case CardClass.SHAMAN:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x71, 0xDB));
                case CardClass.WARLOCK:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0x93, 0x82, 0xC8));
                case CardClass.WARRIOR:
                    return new SolidColorBrush(Color.FromArgb(0xFF, 0xC7, 0x9C, 0x6F));
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IntToDurabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameer, CultureInfo culture)
        {
            int v = (int)value;
            return v >= 0 ? v.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
