namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    public class IntegerActionBasedOperation : IOperation<int, int>
    {
        public int Process(int input)
        {
            return input * 3;
        }
    }
}