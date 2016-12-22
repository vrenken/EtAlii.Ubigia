using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class StringToArrayQuery : Query<string>
    {
        public StringToArrayQuery(string parameter) 
            : base(parameter)
        {
        }

        public string String { get { return ((IParams<string>)this).Parameter; } }
    }
}