namespace EtAlii.Ubigia.Storage
{
    public abstract class Item
    {
        public string Name { get; private set; }

        public Item(string name)
        {
            Name = name;
        }
    }
}
