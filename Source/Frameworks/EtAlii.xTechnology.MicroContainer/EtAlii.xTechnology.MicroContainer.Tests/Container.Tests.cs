// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    using Xunit;

    public class ContainerTests
    {
        // Cycle tests.

#if USE_ORIGINAL_CONTAINER
        [Fact]
        public void Container_Parent_With_Children_Initialized_Using_Parent()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);

            container.Register<IFirstChild, FirstChild>();
            container.RegisterLazyInitializer<IFirstChild>(c =>
            {
                var p = container.GetInstance<IParent>();
                ((IInitializable<IParent>)c).Initialize(p);
            });
            container.Register<ISecondChild, SecondChild>();
            container.RegisterLazyInitializer<ISecondChild>(c =>
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

            Assert.Equal(1, modelCount.ParentConstructorCount);
            Assert.Equal(1, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);
        }
#endif
        [Fact]
        public void Container_Parent_Initializes_2_Children_1()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);

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
            Assert.Equal(1, modelCount.ParentConstructorCount);
            Assert.Equal(1, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);
        }

        [Fact]
        public void Container_Parent_Initializes_2_Children_2()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);

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
            Assert.NotNull(parent);
            Assert.Equal(1, modelCount.ParentConstructorCount);
            Assert.Equal(1, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
        }

        [Fact]
        public void Container_Parent_Initialize_Chain_01()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);

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
        public void Container_Parent_Initialize_Chain_05()
        {
            // Arrange.
            var container = new Container();

            container.Register<ISecondParent, SecondParent>();
            container.Register<IThirdParent, ThirdParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.NotNull(thirdParent.Instance);
        }

        [Fact]
        public void Container_Parent_Initialize_Chain_06()
        {
            // Arrange.
            var container = new Container();

            container.Register<IThirdParent, ThirdParent>();
            container.Register<ISecondParent, SecondParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.NotNull(thirdParent.Instance);
        }

        [Fact]
        public void Container_Parent_Initialize_Chain_07()
        {
            // Arrange.
            var container = new Container();

            container.Register<ISecondParent, SecondParent>();
            container.Register<IThirdParent, ThirdParent>();
            container.RegisterInitializer<IThirdParent>(parent => parent.Initialize(container.GetInstance<ISecondParent>().Instance));
            container.Register<IFourthParent, FourthParent>();

            // Act.
            var thirdParent = container.GetInstance<IThirdParent>();

            // Assert.
            Assert.NotNull(thirdParent.Instance);
        }



        [Fact]
        public void Container_Parent_With_Children_Initialize_Children_First()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);
            container.Register<ISecondChild, SecondChild>();
            container.Register<IFirstChild, FirstChild>();
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p =>
            {
                var child2  = container.GetInstance<ISecondChild>();
                var child1 = container.GetInstance<IFirstChild>();

                ((IInitializable<IParent>)child2).Initialize(p);
                ((IInitializable<IParent>)child1).Initialize(p);
            });

            // Act.
            var secondChild = container.GetInstance<ISecondChild>();
            var firstChild = container.GetInstance<IFirstChild>();
            var parent = container.GetInstance<IParent>();

            // Assert.
            Assert.Equal(0, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);
        }

        [Fact]
        public void Container_Parent_Initialize_Parent_With_Parent_First()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);
            container.Register<ISecondChild, SecondChild>();
            container.Register<IFirstChild, FirstChild>();
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p =>
            {
                var child2  = container.GetInstance<ISecondChild>();
                var child1 = container.GetInstance<IFirstChild>();

                ((IInitializable<IParent>)child2).Initialize(p);
                ((IInitializable<IParent>)child1).Initialize(p);
            });


            // Act.
            var parent = container.GetInstance<IParent>();
            var secondChild = container.GetInstance<ISecondChild>();
            var firstChild = container.GetInstance<IFirstChild>();

            // Assert.
            Assert.Equal(0, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);

        }

        [Fact]
        public void Container_Parent_Initialize_Parent_With_Children_First()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);
            container.Register<ISecondChild, SecondChild>();
            container.Register<IFirstChild, FirstChild>();
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p =>
            {
                var child2  = container.GetInstance<ISecondChild>();
                var child1 = container.GetInstance<IFirstChild>();

                ((IInitializable<IParent>)child2).Initialize(p);
                ((IInitializable<IParent>)child1).Initialize(p);
            });


            // Act.
            var secondChild = container.GetInstance<ISecondChild>();
            var firstChild = container.GetInstance<IFirstChild>();
            var parent = container.GetInstance<IParent>();

            // Assert.
            Assert.Equal(0, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);

        }

        [Fact]
        public void Container_Parent_Initialize_Children_With_Parent_First()
        {
            // Arrange.
            var container = new Container();
            var modelCount = new ModelCount();
            container.Register<IModelCount>(() => modelCount);
            container.Register<ISecondChild, SecondChild>();
            container.Register<IFirstChild, FirstChild>();
            container.Register<IParent, Parent>();
            container.RegisterInitializer<IParent>(p =>
            {
                var child2  = container.GetInstance<ISecondChild>();
                var child1 = container.GetInstance<IFirstChild>();

                ((IInitializable<IParent>)child2).Initialize(p);
                ((IInitializable<IParent>)child1).Initialize(p);
            });

            // Act.
            var parent = container.GetInstance<IParent>();
            var secondChild = container.GetInstance<ISecondChild>();
            var firstChild = container.GetInstance<IFirstChild>();

            // Assert.
            Assert.Equal(0, modelCount.ParentInitializeCount);
            Assert.Equal(1, modelCount.FirstChildConstructorCount);
            Assert.Equal(1, modelCount.FirstChildInitializeCount);
            Assert.Equal(1, modelCount.SecondChildConstructorCount);
            Assert.Equal(1, modelCount.SecondChildInitializeCount);
            Assert.Same(parent, firstChild.Parent);
            Assert.Same(parent, secondChild.Parent);

        }
    }
}
