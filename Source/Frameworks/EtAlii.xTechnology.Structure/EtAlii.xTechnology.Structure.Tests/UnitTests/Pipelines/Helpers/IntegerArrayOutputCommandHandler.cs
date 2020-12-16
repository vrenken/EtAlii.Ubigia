namespace EtAlii.xTechnology.Structure.Tests
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
            return new(parameter);
        }

        public void Handle(IntegerArrayOutputCommand query)
        {
            _output(query.Integers);
        }
    }
}
