// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    using System;

    internal interface ISelectorRegistration<TCriteria, TOption>
    {
        void Register(Func<TCriteria, bool> predicate, TOption option);
        TOption Select(TCriteria criteria);
        TOption TrySelect(TCriteria criteria);
    }

    internal interface ISelectorRegistration<TCriteria1, TCriteria2, TOption>
    {
        void Register(Func<TCriteria1, TCriteria2, bool> predicate, TOption option);
        TOption Select(TCriteria1 criteria1, TCriteria2 criteria2);
        TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2);
    }

    internal interface ISelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption>
    {
        void Register(Func<TCriteria1, TCriteria2, TCriteria3, bool> predicate, TOption option);
        TOption Select(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3);
        TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3);
    }
}