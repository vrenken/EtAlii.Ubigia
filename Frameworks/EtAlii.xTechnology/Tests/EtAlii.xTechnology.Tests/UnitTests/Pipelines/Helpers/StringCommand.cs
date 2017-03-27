namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    internal class StringCommand : Command<string>
    {
        public StringCommand(string parameter) 
            : base(parameter)
        {
        }

        public string String => ((IParams<string>)this).Parameter;
    }
}