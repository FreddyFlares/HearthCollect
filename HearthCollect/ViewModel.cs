using HearthMirror;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HearthCollect
{
    class ViewModel : ViewModelBase
    {
        string dataString;
        public string DataString { get { return dataString; } set { dataString = value; OnPropertyChanged(); } }
        public ObservableCollection<Card> cards;
        public ObservableCollection<Card> Cards { get { return cards; } set { cards = value; OnPropertyChanged(); } }
        public ICommand SynchronizeCollectionCommand { get; private set; }
        string collectionFilePath = "collection.json";

        public ViewModel()
        {
            SynchronizeCollectionCommand = new RelayCommand(SynchronizeCollection);
            ReadJSon();
        }

        public void ReadJSon()
        {
            string en = Environment.NewLine;
            string filePath = "cards.collectible.json";

            // return if cards file not present eg. in designer mode
            if (!File.Exists(filePath))
                return;

            // Read the collectible card pool
            JsonSerializer serializer = new JsonSerializer();
            using (JsonReader reader = new JsonTextReader(new StreamReader(filePath)))
            {
                Cards = serializer.Deserialize<ObservableCollection<Card>>(reader);
            }

            // Read collection from cached file
            if (File.Exists(collectionFilePath))
            {
                using (JsonReader reader = new JsonTextReader(new StreamReader(collectionFilePath)))
                {
                    var collection = serializer.Deserialize<IEnumerable<HearthMirror.Objects.Card>>(reader);
                    UpdateCounts(collection);
                }
            }
        }

        void SynchronizeCollection(object o)
        {
            // Get collection from Hearthstone using HearthMirror
            var mirrorCards = Reflection.GetCollection();
            if (mirrorCards == null)
            {
                throw new InvalidOperationException("Hearthstone is not running");
            }

            // Write collection to cached file
            JsonSerializer serializer = new JsonSerializer();
            using (JsonTextWriter writer = new JsonTextWriter(new StreamWriter(collectionFilePath)))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 1;
                serializer.Serialize(writer, mirrorCards);
            }
            UpdateCounts(mirrorCards);
        }

        void UpdateCounts(IEnumerable mirrorCards)
        {
            foreach (HearthMirror.Objects.Card mirrorCard in mirrorCards)
            {
                Card c = cards.First(o => o.ID == mirrorCard.Id);
                if (mirrorCard.Premium)
                {
                    c.Golden = mirrorCard.Count;
                }
                else
                {
                    c.Normal = mirrorCard.Count;
                }
            }
        }
    }
}
