namespace EtAlii.xTechnology.MicroContainer.Tests
{
    using Xunit;

    public class ContainerTests
    {
        // Cycle tests.
        
        [Fact(Skip = "// TODO: Fix weird container issue")]
        public void Container_Parent_With_2_Children_Initialized_Using_Parent()
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
            container.RegisterInitializer<IParent>(p => p.Initialize());

            // Act.
            var firstChild = container.GetInstance<IFirstChild>();
            var secondChild = container.GetInstance<ISecondChild>();
            var parent = container.GetInstance<IParent>();
            
            Assert.Equal(1, parent.Counter);
            Assert.Equal(1, firstChild.Counter);
            Assert.Equal(1, secondChild.Counter);
            Assert.Equal(1, firstChild.Parent.Counter);
            Assert.Equal(1, secondChild.Parent.Counter);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);
        }

        [Fact]
        public void Container_Parent_Initializes_2_Children_1()
        {
            // Arrange.
            var container = new Container();
            container.Register<IFirstChild, FirstChild>();
            container.Register<ISecondChild, SecondChild>();
            
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p => p.Initialize());
            
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
            var parent = container.GetInstance<IParent>();

            // Assert.
            Assert.Equal(1, parent.Counter);
            Assert.Equal(1, firstChild.Counter);
            Assert.Equal(1, secondChild.Counter);
            Assert.Equal(1, firstChild.Parent.Counter);
            Assert.Equal(1, secondChild.Parent.Counter);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);
        }
        
        [Fact]
        public void Container_Parent_Initializes_2_Children_2()
        {
            // Arrange.
            var container = new Container();
            container.Register<IFirstChild, FirstChild>();
            container.Register<ISecondChild, SecondChild>();
            
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p => p.Initialize());
            
            container.RegisterInitializer<IParent>(p =>
            {
                var c1 = container.GetInstance<IFirstChild>();
                ((IInitializable<IParent>)c1).Initialize(p);

                var c2 = container.GetInstance<ISecondChild>();
                ((IInitializable<IParent>)c2).Initialize(p);
            });

            // Functions because the parent is being explicitly requested.
            var parent = container.GetInstance<IParent>();

            // Assert.
            Assert.Equal(1, parent.Counter);
        }
                
        [Fact]
        public void Container_Parent_Initialize_Chain_01()
        {
            // Arrange.
            var container = new Container();
            
            container.Register<ISecondParent, SecondParent>();
            container.Register<IThirdParent, ThirdParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var secondParent = container.GetInstance<ISecondParent>();
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.Same(secondParent.Instance, thirdParent.Instance);
        }
        [Fact]
        public void Container_Parent_Initialize_Chain_02()
        {
            // Arrange.
            var container = new Container();
            
            container.Register<ISecondParent, SecondParent>();
            container.Register<IThirdParent, ThirdParent>();
            container.Register<IFourthParent, FourthParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));

            // Act.
            var secondParent = container.GetInstance<ISecondParent>();
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.Same(secondParent.Instance, thirdParent.Instance);
        }

        [Fact]
        public void Container_Parent_Initialize_Chain_03()
        {
            // Arrange.
            var container = new Container();
            
            container.Register<IThirdParent, ThirdParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<ISecondParent, SecondParent>();
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var secondParent = container.GetInstance<ISecondParent>();
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.Same(secondParent.Instance, thirdParent.Instance);
        }

        [Fact]
        public void Container_Parent_Initialize_Chain_04()
        {
            // Arrange.
            var container = new Container();
            
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<IThirdParent, ThirdParent>();
            container.Register<ISecondParent, SecondParent>();
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var secondParent = container.GetInstance<ISecondParent>();
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.Same(secondParent.Instance, thirdParent.Instance);
        }
    }
}
