namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    public class StringToIntegerOperation : IOperation<string, int>
    {
        public int Process(string input)
        {
            return input.Length;
        }
    }
}