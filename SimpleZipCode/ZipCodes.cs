using SimpleZipCode.Properties;
using System;
using System.Collections.Generic;

namespace SimpleZipCode
{
    public class ZipCodes
    {
        private LocalResourceZipCodeReader _localResourceZipCodeReader;

        private List<Func<IZipCodeRepo, IZipCodeRepo>> _steps =
            new List<Func<IZipCodeRepo, IZipCodeRepo>>();

        public ZipCodes()
        {
            _localResourceZipCodeReader =
                new LocalResourceZipCodeReader(Resources.us_postal_codes);
        }

        public ZipCodes InMemory
        {
            get
            {
                _steps.Add((zipcodeRepo) =>
                {
                    zipcodeRepo = new LocalZipCodeRepo(_localResourceZipCodeReader.LoadZipCodes()                        );
                    return zipcodeRepo;
                });

                return this;
            }
        }

        public IZipCodeRepo GetRepo()
        {
            IZipCodeRepo repo = null;

            foreach (var step in _steps)
            {
                repo = step(repo);
            }

            return repo;
        }
    }
}
