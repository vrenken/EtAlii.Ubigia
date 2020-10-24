﻿namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Threading.Tasks;

    public interface ISpaceResolver
    {
        Task<Space> Get(ISpaceInfoProvider spaceInfoProvider, Space currentSpace, Account currentAccount);
    }
}