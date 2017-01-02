namespace EtAlii.Servus.Api.Data
{
    public class VariableComponent : PathComponent
    {
        public string Name { get; private set; }

        public VariableComponent(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
