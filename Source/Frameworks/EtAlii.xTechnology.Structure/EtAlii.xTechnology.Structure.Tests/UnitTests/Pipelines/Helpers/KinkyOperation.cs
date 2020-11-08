namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    public class KinkyOperation : IOperation<string, string>
    {
        public string Process(string input)
        {
            return input.ToUpper();
        }
    }
}