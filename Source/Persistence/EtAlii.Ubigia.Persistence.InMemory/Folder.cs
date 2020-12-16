namespace EtAlii.Ubigia.Persistence.InMemory
{
    using System.Collections.Generic;

    public class Folder : Item
    {
        public List<Item> Items { get; } = new();

        public Folder(string name)
            : base(name)
        {
        }

        public override string ToString()
        {
            return Name ?? "[Empty]";
        }
    }
}
