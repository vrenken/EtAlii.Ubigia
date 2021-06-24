// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class FirstChild : IFirstChild, IInitializable<IParent>
    {
        private readonly IModelCount _modelCount;
        public IParent Parent { get; private set; }

        public FirstChild(IModelCount modelCount)
        {
            _modelCount = modelCount;
            _modelCount.FirstChildConstructorCount += 1;
        }
        public void Initialize(IParent initial)
        {
            Parent = initial;
            _modelCount.FirstChildInitializeCount += 1;
        }
    }
}
