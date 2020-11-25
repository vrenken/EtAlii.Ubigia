namespace EtAlii.xTechnology.MicroContainer.Tests
{
    using Xunit;

    public class ContainerTests
    {
        // Cycle tests.
        
        [Fact]
        public void Container_ViewModel_With_2_Commands_Initialized_With_ViewModel()
        {
            // Arrange.
            var container = new Container();
            container.Register<IFirstChild, FirstChild>();
            container.RegisterInitializer<IFirstChild>(c =>
            {
                var p = container.GetInstance<IParent>();
                ((IInitializable<IParent>)c).Initialize(p);
            });
            container.Register<ISecondChild, SecondChild>();
            container.RegisterInitializer<ISecondChild>(c =>
            {
                var p = container.GetInstance<IParent>();
                ((IInitializable<IParent>)c).Initialize(p);
            });
            container.Register<IParent, Parent>();

            // Act.
            var firstChild = container.GetInstance<IFirstChild>();
            var secondChild = container.GetInstance<ISecondChild>();
            var parent = container.GetInstance<IParent>();
            
            Assert.Equal(1, firstChild.Counter);
            Assert.Equal(1, secondChild.Counter);
            Assert.Same(firstChild.Parent, secondChild.Parent);
            Assert.Equal(1, firstChild.Parent.Counter);
            Assert.Equal(1, secondChild.Parent.Counter);
            Assert.NotNull(parent);
        }

        [Fact]
        public void Container_ViewModel_Initializes_2_Commands()
        {
            // Arrange.
            var container = new Container();
            container.Register<IFirstChild, FirstChild>();
            container.Register<ISecondChild, SecondChild>();
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p =>
            {
                var c1 = container.GetInstance<IFirstChild>();
                ((IInitializable<IParent>)c1).Initialize(p);

                var c2 = container.GetInstance<ISecondChild>();
                ((IInitializable<IParent>)c2).Initialize(p);
            });

            // Act.
            var firstChild = container.GetInstance<IFirstChild>();
            var secondChild = container.GetInstance<ISecondChild>();
            // Functions because the parent is being explicitly requested.
            var viewModel = container.GetInstance<IParent>();

            // Assert.
            Assert.Equal(1, firstChild.Counter);
            Assert.Equal(1, secondChild.Counter);
            Assert.Same(firstChild.Parent, secondChild.Parent);
            Assert.Equal(1, firstChild.Parent.Counter);
            Assert.Equal(1, secondChild.Parent.Counter);
            Assert.NotNull(viewModel);
        }
    }
}
