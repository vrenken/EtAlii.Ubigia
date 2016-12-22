namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    public class StringActionBasedOperation : IOperation<string, string>
    {
        public string Process(string input)
        {
            return input.ToUpper();
        }
    }
}