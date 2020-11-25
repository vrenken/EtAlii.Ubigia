namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class SecondChild : ISecondChild, IInitializable<IParent>
    {
        private static int _counter;

        public SecondChild()
        {
            Counter = ++_counter;
        }

        public int Counter { get; }

        public IParent Parent { get; private set; }

        public void Initialize(IParent initial)
        {
            Parent = initial;
        }
    }
}
