namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    public class KinkyOperation : IOperation<string, string>
    {
        public string Process(string input)
        {
            return input.ToUpper();
        }
    }
}