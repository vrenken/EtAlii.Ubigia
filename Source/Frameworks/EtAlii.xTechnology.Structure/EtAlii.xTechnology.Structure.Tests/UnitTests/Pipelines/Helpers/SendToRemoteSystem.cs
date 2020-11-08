namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    internal class SendToRemoteSystem : IOperation<int, bool>
    {

        public bool Process(int input)
        {
            // Sure, we have send the data.
            return true;
        }
    }
}