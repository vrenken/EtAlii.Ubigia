// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;

    internal class NextContainerIdentifierFromTimeAlgorithm : INextContainerIdentifierAlgorithm
    {
        private readonly INextContainerIdentifierAlgorithm _nextContainerIdentifierAlgorithm;
        private readonly IContainerProvider _containerProvider;

        private int _year;
        private int _day;

        private bool _isInitialized;

        private string _storageFolder;
        private string _accountFolder;
        private string _spaceFolder;

        private ulong _eraId;
        private ulong _periodId;
        private ulong _momentId;

        private readonly object _lockObject = new();

        public NextContainerIdentifierFromTimeAlgorithm(
            INextContainerIdentifierAlgorithm nextContainerIdentifierAlgorithm,
            IContainerProvider containerProvider)
        {
            _nextContainerIdentifierAlgorithm = nextContainerIdentifierAlgorithm;
            _containerProvider = containerProvider;
        }

        public ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier)
        {
            lock (_lockObject)
            {
                ContainerIdentifier nextContainerIdentifier;
                var now = DateTime.Now;

                if (!_isInitialized)
                {
                    nextContainerIdentifier = _nextContainerIdentifierAlgorithm.Create(currentContainerIdentifier);
                    _storageFolder = nextContainerIdentifier.Paths[1];
                    _accountFolder = nextContainerIdentifier.Paths[2];
                    _spaceFolder = nextContainerIdentifier.Paths[3];

                    _eraId = nextContainerIdentifier.Paths.Length > 4
                        ? ulong.Parse(nextContainerIdentifier.Paths[4])
                        : 0;
                    _periodId = nextContainerIdentifier.Paths.Length > 5
                        ? ulong.Parse(nextContainerIdentifier.Paths[5])
                        : 0;
                    _momentId = nextContainerIdentifier.Paths.Length > 6
                        ? ulong.Parse(nextContainerIdentifier.Paths[6])
                        : 0;

                    _year = now.Year;
                    _day = now.DayOfYear;

                    _isInitialized = true;
                }
                else
                {
                    if (now.Year != _year)
                    {
                        _year = now.Year;
                        _eraId += 1;
                        _periodId = 0;
                    }
                    else if (now.DayOfYear != _day)
                    {
                        _day = now.DayOfYear;
                        _periodId += 1;
                        _momentId = 0;
                    }
                    else
                    {
                        _momentId += 1;
                    }

                    nextContainerIdentifier = _containerProvider.ForEntry(_storageFolder, _accountFolder, _spaceFolder, _eraId, _periodId, _momentId);
                }

                return nextContainerIdentifier;
            }
        }
    }
}
