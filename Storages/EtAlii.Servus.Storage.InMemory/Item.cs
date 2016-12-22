namespace EtAlii.Servus.Storage
{
    public abstract class Item
    {
        public string Name { get; private set; }

        public Item(string name)
        {
            this.Name = name;
        }
    }
}
