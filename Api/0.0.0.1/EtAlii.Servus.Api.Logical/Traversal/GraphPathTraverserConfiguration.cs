namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Fabric;

    public class GraphPathTraverserConfiguration : IGraphPathTraverserConfiguration
    {
        public IFabricContext FabricContext { get { return _fabricContext; } }
        private IFabricContext _fabricContext;

        public IGraphPathTraverserExtension[] Extensions { get { return _extensions; } }
        private IGraphPathTraverserExtension[] _extensions;
        

        public GraphPathTraverserConfiguration()
        {
            _extensions = new IGraphPathTraverserExtension[0];
        }

        public IGraphPathTraverserConfiguration Use(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
            return this;
        }

        public IGraphPathTraverserConfiguration Use(IGraphPathTraverserExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

    }
}