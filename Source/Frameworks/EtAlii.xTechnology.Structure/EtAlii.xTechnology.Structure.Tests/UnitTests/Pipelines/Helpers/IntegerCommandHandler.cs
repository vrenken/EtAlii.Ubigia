namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerCommandHandler : ICommandHandler<IntegerCommand, int>
    {
        public IntegerCommand Create(int parameter)
        {
            return new IntegerCommand(parameter);
        }

        public void  Handle(IntegerCommand command)
        {
        }
    }
}