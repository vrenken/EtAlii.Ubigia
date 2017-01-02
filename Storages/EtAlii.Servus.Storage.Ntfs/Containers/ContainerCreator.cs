namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ContainerCreator : IContainerCreator
    {
        private readonly IPathBuilder _pathBuilder;

        public ContainerCreator(IPathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
        }

        public void Create(ContainerIdentifier containerToCreate)
        {
            var folder = _pathBuilder.GetFolder(containerToCreate);
            LongPathHelper.Create(folder);
        }
    }
}
