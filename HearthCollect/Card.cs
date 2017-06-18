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
        public Rarity Rarity { get; set; }
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
    }
}
