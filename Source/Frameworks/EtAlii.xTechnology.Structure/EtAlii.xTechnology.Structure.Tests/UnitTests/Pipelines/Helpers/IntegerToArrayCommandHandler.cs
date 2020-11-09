namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayCommandHandler : ICommandHandler<IntegerToArrayCommand, int>
    {
        public IntegerToArrayCommand Create(int parameter)
        {
            return new IntegerToArrayCommand(parameter);
        }

        public void Handle(IntegerToArrayCommand command)
        {
        }
    }
}