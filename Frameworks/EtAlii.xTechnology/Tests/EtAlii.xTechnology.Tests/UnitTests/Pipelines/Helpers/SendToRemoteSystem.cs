namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;

    internal class SendToRemoteSystem : IOperation<int, bool>
    {

        public bool Process(int input)
        {
            // Sure, we have send the data.
            return true;
        }
    }
}