// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class SecondChild : ISecondChild, IInitializable<IParent>
    {
        private readonly IModelCount _modelCount;

        public SecondChild(IModelCount modelCount)
        {
            _modelCount = modelCount;
            _modelCount.SecondChildConstructorCount += 1;
        }

        public IParent Parent { get; private set; }

        public void Initialize(IParent initial)
        {
            Parent = initial;
            _modelCount.SecondChildInitializeCount += 1;
        }
    }
}
