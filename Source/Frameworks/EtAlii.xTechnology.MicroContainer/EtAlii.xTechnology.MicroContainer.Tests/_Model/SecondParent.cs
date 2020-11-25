namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class SecondParent : ISecondParent
    {
        public object Instance { get; }
        
        public SecondParent()
        {
            Instance = new object();
        }
    }
}
