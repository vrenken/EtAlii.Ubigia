namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    public class IntegerToStringOperation : IOperation<int, string>
    {
        public string Process(int input)
        {
            return input.ToString();
        }
    }
}