namespace EtAlii.Servus.Api.Data
{
    public class SeparatorComponent : PathComponent
    {
        public string Name { get; private set; }

        public SeparatorComponent(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
