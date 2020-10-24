namespace EtAlii.Ubigia.Persistence.InMemory
{
    public class File : Item
    {
        public byte[] Content { get; set; }

        public File(string name)
            : base(name)
        {
        }

        public override string ToString()
        {
            return Name ?? "[Empty]";
        }
    }
}
