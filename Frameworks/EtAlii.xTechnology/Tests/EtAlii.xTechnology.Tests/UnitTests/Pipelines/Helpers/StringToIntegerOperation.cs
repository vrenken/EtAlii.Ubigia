namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    public class StringToIntegerOperation : IOperation<string, int>
    {
        public int Process(string input)
        {
            return input.Length;
        }
    }
}