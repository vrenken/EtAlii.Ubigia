namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerCommand : Command<int>
    {
        public IntegerCommand(int parameter) 
            : base(parameter)
        {
        }

        public int Integer => ((IParams<int>)this).Parameter;
    }
}