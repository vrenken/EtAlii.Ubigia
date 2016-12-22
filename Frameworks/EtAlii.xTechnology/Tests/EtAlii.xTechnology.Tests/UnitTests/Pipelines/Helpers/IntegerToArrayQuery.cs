using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class IntegerToArrayQuery : Query<int>
    {
        public IntegerToArrayQuery(int parameter) 
            : base(parameter)
        {
        }

        public int Integer { get { return ((IParams<int>)this).Parameter; } }
    }
}