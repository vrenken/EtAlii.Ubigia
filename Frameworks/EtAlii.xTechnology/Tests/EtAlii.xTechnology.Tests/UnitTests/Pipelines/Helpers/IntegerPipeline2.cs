namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    internal class IntegerPipeline2 : Pipeline<int, int>
    {
        public IntegerPipeline2()
        {
            this.StartWith(i => i + 1)
                .ContinueWith(i => i + 2)
                .EndWith(i => i * 2);
        }
    }
}