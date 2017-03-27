namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    public class IntegerToStringOperation : IOperation<int, string>
    {
        public string Process(int input)
        {
            return input.ToString();
        }
    }
}