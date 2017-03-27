namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    public class StringToIntQuery : Query<string>
    {
        public StringToIntQuery(string parameter) 
            : base(parameter)
        {
        }

        public string String => ((IParams<string>)this).Parameter;
    }
}