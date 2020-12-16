namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class StringCommandHandler : ICommandHandler<StringCommand, string>
    {
        public StringCommand Create(string parameter)
        {
            return new(parameter);
        }

        public void Handle(StringCommand command)
        {
        }
    }
}
