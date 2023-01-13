// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal interface IItemToPathSubjectConverter
{
    PathSubject Convert(object items);
    bool TryConvert(object items, out PathSubject pathSubject);
}
