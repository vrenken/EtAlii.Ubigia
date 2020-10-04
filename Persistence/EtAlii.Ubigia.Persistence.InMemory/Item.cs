namespace EtAlii.Ubigia.Persistence.InMemory
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
