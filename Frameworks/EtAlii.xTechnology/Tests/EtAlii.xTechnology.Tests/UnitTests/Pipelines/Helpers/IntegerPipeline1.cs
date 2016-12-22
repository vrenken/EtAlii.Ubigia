namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    internal class IntegerPipeline1 : Pipeline<int, int>
    {
        public IntegerPipeline1()
        {
            this.StartWith(i => i * 3)
                .ContinueWith(i => i * 2)
                .EndWith(i => i + 4);
        }
    }
}