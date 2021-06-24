// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class Parent : IParent
    {
        private readonly IModelCount _modelCount;
        
        public IFirstChild FirstChild { get; }
        public ISecondChild SecondChild { get; }

        /// <summary>
        /// The problem arises because the DronesToolPanelViewModel - which is requested when the two commands are initialized - 
        /// itself again requires the commands in the constructor. 
        /// The error only occurs when there are at least 2 objects (i.e. commands) injected.
        /// </summary>
        public Parent(IFirstChild firstChild, ISecondChild secondChild, IModelCount modelCount)
        {
            _modelCount = modelCount;
            _modelCount.ParentConstructorCount += 1;
            FirstChild = firstChild;
            SecondChild = secondChild;
        }

        public void Initialize()
        {
            _modelCount.ParentInitializeCount += 1;
        }
    }
}
