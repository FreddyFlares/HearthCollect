using System;

namespace HearthCollect
{
    class Card : ViewModelBase
    {
        public string ID { get; set; }
        int normal;
        public int Normal { get { return normal; } set { normal = value; OnPropertyChanged(); OnPropertyChanged("Total"); } }
        int golden;
        public int Golden { get { return golden; } set { golden = value; OnPropertyChanged(); OnPropertyChanged("Total"); } }
        public int Total { get { return Normal + Golden; } }
        public string Name { get; set; }
        public CardSet Set { get; set; }
        public CardClass CardClass { get; set; }
        Rarity rarity;
        public Rarity Rarity
        {
            get => rarity;
            set
            {
                rarity = value;
                switch (rarity)
                {
                    case Rarity.COMMON:
                        normalDustValue = 5; goldenDustValue = 50; maxInDeck = 2;
                        break;
                    case Rarity.RARE:
                        normalDustValue = 20; goldenDustValue = 100; maxInDeck = 2;
                        break;
                    case Rarity.EPIC:
                        normalDustValue = 100; goldenDustValue = 400; maxInDeck = 2;
                        break;
                    case Rarity.LEGENDARY:
                        normalDustValue = 400; goldenDustValue = 1600; maxInDeck = 1;
                        break;
                }
            }
        }
        public string Type { get; set; }
        public Race Race { get; set; }
        public int Cost { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public string Text { get; set; }
        public object Mechanics { get; set; }
        public object PlayRequirements { get; set; }
        public bool Collectible { get; set; }
        public string Artist { get; set; }
        public int DbfID { get; set; }
        public bool Elite { get; set; }
        public object Entourage { get; set; }
        public Faction Faction { get; set; }
        public string Flavor { get; set; }
        public string HowToEarn { get; set; }
        public string HowToEarnGolden { get; set; }
        public object ReferencedTags { get; set; }
        public string TargetingArrowText { get; set; }

        int normalDustValue;
        public int NormalDustValue => normalDustValue;
        int goldenDustValue;
        public int GoldenDustValue => goldenDustValue;
        int maxInDeck;
        public int MaxInDeck => maxInDeck;

        public int TotalDustValue => Normal * normalDustValue + Golden * goldenDustValue;

        public int ExcessDust => GetExcessDust(Normal, Golden, normalDustValue, goldenDustValue);

        public int ExcessDustPreserveGoldens => GetExcessDust(Golden, Normal, goldenDustValue, normalDustValue);

        public int ExcessDustIgnoreGoldens => Math.Max(0, Normal - maxInDeck) * normalDustValue;

        private int GetExcessDust(int preserve, int disFirst, int preserveValue, int disFirstValue)
        {
            return Math.Min(Math.Max(0, preserve + disFirst - maxInDeck), disFirst) * disFirstValue + (Math.Max(0, preserve - maxInDeck)) * preserveValue;
        }
    }
}
