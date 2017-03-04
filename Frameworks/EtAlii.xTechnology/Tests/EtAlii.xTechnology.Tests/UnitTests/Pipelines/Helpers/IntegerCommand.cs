using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class IntegerCommand : Command<int>
    {
        public IntegerCommand(int parameter) 
            : base(parameter)
        {
        }

        public int Integer => ((IParams<int>)this).Parameter;
    }
}