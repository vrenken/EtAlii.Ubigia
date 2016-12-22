namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.Servus.Api.Logical;

    public class NodeTrackingReference
    {
        public bool IsAlive { get { return _nodeReference.IsAlive; } }

        public INode Node { get { return _nodeReference.Target as INode; } }

        private readonly WeakReference _nodeReference;

        internal NodeTrackingReference(INode node)
        {
            _nodeReference = new WeakReference(node);
        }

    }
}