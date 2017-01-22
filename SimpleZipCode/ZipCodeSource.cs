using SimpleZipCode.Properties;
using System;
using System.Collections.Generic;

namespace SimpleZipCode
{
    public class ZipCodeSource
    {
        private LocalResourceZipCodeReader _localResourceZipCodeReader;

        private List<Func<IZipCodeRepo, IZipCodeRepo>> _steps =
            new List<Func<IZipCodeRepo, IZipCodeRepo>>();

        public ZipCodeSource()
        {
            _localResourceZipCodeReader =
                new LocalResourceZipCodeReader(Resources.us_postal_codes);
        }

        public ZipCodeSource FromMemory()
        {
            _steps.Add((zipcodeRepo) =>
            {
                zipcodeRepo = new ZipCodeRepo(_localResourceZipCodeReader.LoadZipCodes());
                return zipcodeRepo;
            });

            return this;
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
