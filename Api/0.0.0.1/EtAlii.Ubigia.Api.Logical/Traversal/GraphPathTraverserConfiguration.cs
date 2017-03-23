namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;

    public class GraphPathTraverserConfiguration : IGraphPathTraverserConfiguration
    {
        public IFabricContext FabricContext { get; private set; }

        public IGraphPathTraverserExtension[] Extensions { get; private set; }


        public GraphPathTraverserConfiguration()
        {
            Extensions = new IGraphPathTraverserExtension[0];
        }

        public IGraphPathTraverserConfiguration Use(IFabricContext fabricContext)
        {
            FabricContext = fabricContext;
            return this;
        }

        public IGraphPathTraverserConfiguration Use(IGraphPathTraverserExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

    }
}