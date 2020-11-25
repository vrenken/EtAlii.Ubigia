namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class Parent : IParent
    {
        private static int _counter;

        /// <summary>
        /// The problem arises because the DronesToolPanelViewModel - which is requested when the two commands are initialized - 
        /// itself again requires the commands in the constructor. 
        /// The error only occurs when there are at least 2 objects (i.e. commands) injected.
        /// </summary>
        public Parent(IFirstChild firstChild, ISecondChild secondChild)
        {
            Counter = ++_counter;
            FirstChild = firstChild;
            SecondChild = secondChild;
        }

        public int Counter { get; }
        public IFirstChild FirstChild { get; }
        public ISecondChild SecondChild { get; }
    }
}
