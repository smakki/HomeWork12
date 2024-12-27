namespace HomeWork12
{
    public class OtusDictionary
    {
        DictionaryItem[]? items;

        public OtusDictionary()
        {
            Initialize(32);
        }
        public void Add(int key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var index = getIndex(key, items.Length);
            if (items[index].IsOccupied && key != items[index].Key)
            {
                Resize();
                Add(key, value);
                return;
            }

            if (items[index].IsOccupied && key == items[index].Key)
            {
                throw new ArgumentException($"An item with key {key} has already exists");
            }
            items[index].Value = value;
            items[index].Key = key;
            items[index].IsOccupied = true;
        }

        private void Resize()
        {
            var newItems = new DictionaryItem[items.Length * 2];
            foreach (var item in items)
            {
                if (!item.IsOccupied) continue;
                var index = getIndex(item.Key, newItems.Length);
                if (newItems[index].IsOccupied && item.Key != newItems[index].Key)
                {
                    Resize();
                    break;
                }
                newItems[index] = item;
            }
            items = newItems;
        }

        private int getIndex(int key, int arrayLenght)
        {
            return Math.Abs(key % arrayLenght);
        }

        private void Initialize(int capacity)
        {
            if (capacity == 0)
            {
                capacity = 32;
            }
            items = new DictionaryItem[capacity];

        }
        public string this[int key]
        {
            get => Get(key);
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                Add(key, value);
            }
        }

        public string Get(int key)
        {
            var index = getIndex((int)key, items.Length);
            if (!items[index].IsOccupied || items[index].Key != key)
                return null;

            return items[index].Value;
        }

        private struct DictionaryItem
        {

            public bool IsOccupied;
            public int Key;
            public string Value;
        }
    }
}
