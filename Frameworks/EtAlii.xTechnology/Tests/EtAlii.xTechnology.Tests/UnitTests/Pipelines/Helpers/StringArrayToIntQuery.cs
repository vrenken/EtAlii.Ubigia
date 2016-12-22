namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    public class StringArrayToIntQuery : Query<string[]>
    {
        public StringArrayToIntQuery(string[] parameter) 
            : base(parameter)
        {
        }

        public string[] Array { get { return ((IParams<string[]>)this).Parameter; } }

    }
}