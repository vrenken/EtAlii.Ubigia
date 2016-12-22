﻿﻿namespace EtAlii.xTechnology.Structure
{
    using System;
    using System.Diagnostics;

    public partial class Selector<TCriteria, TOption> : ISelector<TCriteria, TOption>
    {
        private ISelectorRegistration<TCriteria, TOption> _firstRegistration;

        [DebuggerStepThrough]
        public ISelector<TCriteria, TOption> Register(Func<TCriteria, bool> predicate, TOption option)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (_firstRegistration == null)
            {
                _firstRegistration = new SelectorRegistration<TCriteria, TOption>(predicate, option);
            }
            else
            {
                _firstRegistration.Register(predicate, option);
            }

            return this;
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria criteria)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.Select(criteria);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria criteria)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.TrySelect(criteria);
        }
    }

    public partial class Selector2<TCriteria1, TCriteria2, TOption> : ISelector<TCriteria1, TCriteria2, TOption>
    {
        private ISelectorRegistration<TCriteria1, TCriteria2, TOption> _firstRegistration;

        [DebuggerStepThrough]
        public ISelector<TCriteria1, TCriteria2, TOption> Register(Func<TCriteria1, TCriteria2, bool> predicate, TOption option)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (_firstRegistration == null)
            {
                _firstRegistration = new SelectorRegistration<TCriteria1, TCriteria2, TOption>(predicate, option);
            }
            else
            {
                _firstRegistration.Register(predicate, option);
            }

            return this;
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria1 criteria1, TCriteria2 criteria2)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.Select(criteria1, criteria2);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.TrySelect(criteria1, criteria2);
        }
    }

    public partial class Selector2<TCriteria1, TCriteria2, TCriteria3, TOption> : ISelector<TCriteria1, TCriteria2, TCriteria3, TOption>
    {
        private ISelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption> _firstRegistration;

        [DebuggerStepThrough]
        public ISelector<TCriteria1, TCriteria2, TCriteria3, TOption> Register(Func<TCriteria1, TCriteria2, TCriteria3, bool> predicate, TOption option)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (_firstRegistration == null)
            {
                _firstRegistration = new SelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption>(predicate, option);
            }
            else
            {
                _firstRegistration.Register(predicate, option);
            }

            return this;
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.Select(criteria1, criteria2, criteria3);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3)
        {
            if (_firstRegistration == null)
            {
                var message = String.Format("This selector has not yet been configured.");
                throw new InvalidOperationException(message);
            }

            return _firstRegistration.TrySelect(criteria1, criteria2, criteria3);
        }
    }

}