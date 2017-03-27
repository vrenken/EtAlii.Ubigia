namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    public class StringArrayToIntQuery : Query<string[]>
    {
        public StringArrayToIntQuery(string[] parameter) 
            : base(parameter)
        {
        }

        public string[] Array => ((IParams<string[]>)this).Parameter;
    }
}