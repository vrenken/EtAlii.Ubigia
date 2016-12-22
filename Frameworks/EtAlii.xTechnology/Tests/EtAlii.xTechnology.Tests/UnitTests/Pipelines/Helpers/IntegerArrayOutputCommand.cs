using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class IntegerArrayOutputCommand : Command<int[]>
    {
        public IntegerArrayOutputCommand(int[] parameter) 
            : base(parameter)
        {
        }

        public int[] Integers { get { return ((IParams<int[]>)this).Parameter; } }
    }
}