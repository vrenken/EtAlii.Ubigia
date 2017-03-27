namespace EtAlii.xTechnology.UnitTests
{
    using System;
    using EtAlii.xTechnology.Structure;

    internal class IntegerArrayOutputCommandHandler : ICommandHandler<IntegerArrayOutputCommand, int[]>
    {
        private readonly Action<int[]> _output;

        public IntegerArrayOutputCommandHandler(Action<int[]> output)
        {
            _output = output;
        }

        public IntegerArrayOutputCommand Create(int[] parameter)
        {
            return new IntegerArrayOutputCommand(parameter);
        }

        public void Handle(IntegerArrayOutputCommand query)
        {
            _output(query.Integers);
        }
    }
}