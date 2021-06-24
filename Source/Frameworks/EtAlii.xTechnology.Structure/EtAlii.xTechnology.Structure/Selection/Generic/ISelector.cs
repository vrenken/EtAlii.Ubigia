// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    using System;

    public interface ISelector<TCriteria, TOption>
    {
        ISelector<TCriteria, TOption> Register(Func<TCriteria, bool> predicate, TOption option);
        TOption Select(TCriteria criteria);
        TOption TrySelect(TCriteria criteria);
    }

    public interface ISelector<TCriteria1, TCriteria2, TOption>
    {
        ISelector<TCriteria1, TCriteria2, TOption> Register(Func<TCriteria1, TCriteria2, bool> predicate, TOption option);
        TOption Select(TCriteria1 criteria1, TCriteria2 criteria2);
        TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2);
    }

    public interface ISelector<TCriteria1, TCriteria2, TCriteria3, TOption>
    {
        ISelector<TCriteria1, TCriteria2, TCriteria3, TOption> Register(Func<TCriteria1, TCriteria2, TCriteria3, bool> predicate, TOption option);
        TOption Select(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3);
        TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3);
    }
}