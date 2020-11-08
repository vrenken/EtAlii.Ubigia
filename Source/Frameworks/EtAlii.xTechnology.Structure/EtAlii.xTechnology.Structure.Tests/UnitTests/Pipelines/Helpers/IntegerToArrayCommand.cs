namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayCommand : Command<int>
    {
        public IntegerToArrayCommand(int parameter) 
            : base(parameter)
        {
        }

        public int Integer => ((IParams<int>)this).Parameter;
    }
}