// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    using System;
    using System.Diagnostics;

    internal class SelectorRegistration<TCriteria, TOption> : ISelectorRegistration<TCriteria, TOption>
    {
        private readonly Func<TCriteria, bool> _predicate;
        private readonly TOption _option;

        private ISelectorRegistration<TCriteria, TOption> _nextRegistration;

        [DebuggerStepThrough]
        public SelectorRegistration(Func<TCriteria, bool> predicate, TOption option)
        {
            _predicate = predicate;
            _option = option;
        }

        [DebuggerStepThrough]
        public void Register(Func<TCriteria, bool> predicate, TOption option)
        {
            if (_nextRegistration == null)
            {
                _nextRegistration = new SelectorRegistration<TCriteria, TOption>(predicate, option);
            }
            else
            {
                _nextRegistration.Register(predicate, option);
            }
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria criteria)
        {
            if (_predicate(criteria))
            {
                return _option;
            }

            if (_nextRegistration != null)
            {
                return _nextRegistration.Select(criteria);
            }

            var message = $"Unable to select option for criteria: {(criteria != null ? criteria.ToString() : "[NULL]")}";
            throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria criteria)
        {
            if (_predicate(criteria))
            {
                return _option;
            }

            return _nextRegistration != null
                ? _nextRegistration.TrySelect(criteria)
                : default;
        }
    }

    internal class SelectorRegistration<TCriteria1, TCriteria2, TOption> : ISelectorRegistration<TCriteria1, TCriteria2, TOption>
    {
        private readonly Func<TCriteria1, TCriteria2, bool> _predicate;
        private readonly TOption _option;

        private ISelectorRegistration<TCriteria1, TCriteria2, TOption> _nextRegistration;

        [DebuggerStepThrough]
        public SelectorRegistration(Func<TCriteria1, TCriteria2, bool> predicate, TOption option)
        {
            _predicate = predicate;
            _option = option;
        }

        [DebuggerStepThrough]
        public void Register(Func<TCriteria1, TCriteria2, bool> predicate, TOption option)
        {
            if (_nextRegistration == null)
            {
                _nextRegistration = new SelectorRegistration<TCriteria1, TCriteria2, TOption>(predicate, option);
            }
            else
            {
                _nextRegistration.Register(predicate, option);
            }
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria1 criteria1, TCriteria2 criteria2)
        {
            if (_predicate(criteria1, criteria2))
            {
                return _option;
            }

            if (_nextRegistration != null)
            {
                return _nextRegistration.Select(criteria1, criteria2);
            }

            var message = $"Unable to select option for criteria: {(criteria1 != null ? criteria1.ToString() : "[NULL]")}, {(criteria2 != null ? criteria2.ToString() : "[NULL]")}";
            throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2)
        {
            if (_predicate(criteria1, criteria2))
            {
                return _option;
            }

            return _nextRegistration != null ? _nextRegistration.TrySelect(criteria1, criteria2) : default(TOption);
        }
    }

    internal class SelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption> : ISelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption>
    {
        private readonly Func<TCriteria1, TCriteria2, TCriteria3, bool> _predicate;
        private readonly TOption _option;

        private ISelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption> _nextRegistration;

        [DebuggerStepThrough]
        public SelectorRegistration(Func<TCriteria1, TCriteria2, TCriteria3, bool> predicate, TOption option)
        {
            _predicate = predicate;
            _option = option;
        }

        [DebuggerStepThrough]
        public void Register(Func<TCriteria1, TCriteria2, TCriteria3, bool> predicate, TOption option)
        {
            if (_nextRegistration == null)
            {
                _nextRegistration = new SelectorRegistration<TCriteria1, TCriteria2, TCriteria3, TOption>(predicate, option);
            }
            else
            {
                _nextRegistration.Register(predicate, option);
            }
        }

        [DebuggerStepThrough]
        public TOption Select(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3)
        {
            if (_predicate(criteria1, criteria2, criteria3))
            {
                return _option;
            }

            if (_nextRegistration != null)
            {
                return _nextRegistration.Select(criteria1, criteria2, criteria3);
            }

            var message = $"Unable to select option for criteria: {(criteria1 != null ? criteria1.ToString() : "[NULL]")}, {(criteria2 != null ? criteria2.ToString() : "[NULL]")}, {(criteria3 != null ? criteria3.ToString() : "[NULL]")}";
            throw new InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        public TOption TrySelect(TCriteria1 criteria1, TCriteria2 criteria2, TCriteria3 criteria3)
        {
            if (_predicate(criteria1, criteria2, criteria3))
            {
                return _option;
            }

            return _nextRegistration != null ? _nextRegistration.TrySelect(criteria1, criteria2, criteria3) : default(TOption);
        }
    }
}
