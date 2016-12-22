namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    public class StringToIntQuery : Query<string>
    {
        public StringToIntQuery(string parameter) 
            : base(parameter)
        {
        }

        public string String { get { return ((IParams<string>)this).Parameter; } }

    }
}