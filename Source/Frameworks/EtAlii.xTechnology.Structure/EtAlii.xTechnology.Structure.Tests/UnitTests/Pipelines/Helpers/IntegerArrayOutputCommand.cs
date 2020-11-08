namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerArrayOutputCommand : Command<int[]>
    {
        public IntegerArrayOutputCommand(int[] parameter) 
            : base(parameter)
        {
        }

        public int[] Integers => ((IParams<int[]>)this).Parameter;
    }
}