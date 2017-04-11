namespace EtAlii.Ubigia.Storage
{
    public abstract class Item
    {
        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
        }
    }
}
