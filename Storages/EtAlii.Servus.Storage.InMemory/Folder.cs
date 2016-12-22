namespace EtAlii.Servus.Storage
{
    using System.Collections.Generic;

    public class Folder : Item
    {
        public readonly List<Item> Items = new List<Item>();

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
