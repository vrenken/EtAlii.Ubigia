// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    internal interface IContentCacheHelper
    {
        Content Get(in Identifier identifier);
        ContentPart Get(in Identifier identifier, ulong contentPartId);

        void Store(in Identifier identifier, Content content);
        void Store(in Identifier identifier, ContentPart contentPart);
    }
}