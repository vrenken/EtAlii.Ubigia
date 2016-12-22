using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class IntegerToArrayCommand : Command<int>
    {
        public IntegerToArrayCommand(int parameter) 
            : base(parameter)
        {
        }

        public int Integer { get { return ((IParams<int>)this).Parameter; } }
    }
}