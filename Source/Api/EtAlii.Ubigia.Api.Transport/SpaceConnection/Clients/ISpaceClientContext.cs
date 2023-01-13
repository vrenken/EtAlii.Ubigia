// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

public interface ISpaceClientContext
{
    Task Open(ISpaceConnection spaceConnection);
    Task Close(ISpaceConnection spaceConnection);
}
