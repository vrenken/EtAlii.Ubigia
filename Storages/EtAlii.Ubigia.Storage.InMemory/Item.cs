namespace EtAlii.Ubigia.Storage
{
    public abstract class Item
    {
        public string Name { get; private set; }

        protected Item(string name)
        {
            Name = name;
        }
    }
}
