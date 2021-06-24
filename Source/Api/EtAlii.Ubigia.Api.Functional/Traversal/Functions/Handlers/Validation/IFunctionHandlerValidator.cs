// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IFunctionHandlerValidator
    {
        void Validate(IFunctionHandlersProvider functionHandlersProvider);
    }
}
