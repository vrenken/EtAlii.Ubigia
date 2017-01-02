namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class NextContainerIdentifierFromTimeAlgorithm : INextContainerIdentifierAlgorithm
    {
        private readonly INextContainerIdentifierAlgorithm _nextContainerIdentifierAlgorithm;

        private int _year;
        private int _day;

        private bool _isInitialized;

        private string _entriesFolder;
        private string _storageFolder;
        private string _accountFolder;
        private string _spaceFolder;

        private UInt64 _eraId;
        private UInt64 _periodId;
        private UInt64 _momentId;

        public NextContainerIdentifierFromTimeAlgorithm(INextContainerIdentifierAlgorithm nextContainerIdentifierAlgorithm)
        {
            _nextContainerIdentifierAlgorithm = nextContainerIdentifierAlgorithm;
        }

        public ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier)
        {
            var nextContainerIdentifier = (ContainerIdentifier)null;
            var now = DateTime.Now;

            if (!_isInitialized)
            {
                nextContainerIdentifier = _nextContainerIdentifierAlgorithm.Create(currentContainerIdentifier);
                _entriesFolder = nextContainerIdentifier.Paths[0];
                _storageFolder = nextContainerIdentifier.Paths[1];
                _accountFolder = nextContainerIdentifier.Paths[2];
                _spaceFolder = nextContainerIdentifier.Paths[3];

                _eraId = nextContainerIdentifier.Paths.Length > 4 ? UInt64.Parse(nextContainerIdentifier.Paths[4]) : 0;
                _periodId = nextContainerIdentifier.Paths.Length > 5 ? UInt64.Parse(nextContainerIdentifier.Paths[5]) : 0;
                _momentId = nextContainerIdentifier.Paths.Length > 6 ? UInt64.Parse(nextContainerIdentifier.Paths[6]) : 0;

                _year = now.Year;
                _day = now.DayOfYear;

                _isInitialized = true;
            }
            else
            {
                if(now.Year != _year)
                {
                    _year = now.Year;
                    _eraId += 1;
                    _periodId = 0;
                }
                else if(now.DayOfYear != _day)
                {
                    _day = now.DayOfYear;
                    _periodId += 1;
                    _momentId = 0;
                }
                else 
                {
                    _momentId += 1;
                }

                nextContainerIdentifier = ContainerIdentifier.FromPaths(_entriesFolder, _storageFolder, _accountFolder, _spaceFolder, _eraId.ToString(), _periodId.ToString(), _momentId.ToString());
            }

            return nextContainerIdentifier;
        }
    }
}
