// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class MediaRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public MediaRootHandlerMapper()
        {
            Name = "Media";

            AllowedRootHandlers = new IRootHandler[]
            {
                // media:COMPANY/FAMILY/MODEL/NUMBER
                new MediaByCompanyFamilyModelNumberHandler(),
                // media:COMPANY/FAMILY/MODEL
                new MediaByCompanyFamilyModelHandler(),

                new MediaRootByEmptyHandler(), // Should be at the end.
            };
        }
    }
}
