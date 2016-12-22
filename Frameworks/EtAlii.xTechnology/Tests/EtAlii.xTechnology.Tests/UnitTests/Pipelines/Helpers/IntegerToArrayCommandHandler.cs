namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayCommandHandler : ICommandHandler<IntegerToArrayCommand, int>
    {
        public IntegerToArrayCommandHandler()
        {
        }

        public IntegerToArrayCommand Create(int parameter)
        {
            return new IntegerToArrayCommand(parameter);
        }

        public void Handle(IntegerToArrayCommand command)
        {
        }
    }
}