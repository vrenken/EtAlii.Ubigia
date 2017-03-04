using EtAlii.xTechnology.Structure;

namespace EtAlii.xTechnology.Tests
{
    internal class StringCommand : Command<string>
    {
        public StringCommand(string parameter) 
            : base(parameter)
        {
        }

        public string String => ((IParams<string>)this).Parameter;
    }
}