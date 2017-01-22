using System;
using System.Collections.Generic;

namespace SimpleZipCode
{
    public class ZipCodeSource
    {
        private List<Func<IZipCodeRepository, IZipCodeRepository>> _steps =
            new List<Func<IZipCodeRepository, IZipCodeRepository>>();

        public ZipCodeSource() { }

        public ZipCodeSource FromMemory()
        {
            _steps.Add((zipcodeRepo) =>
            {
                zipcodeRepo = new ZipCodeRepo(LocalResourceZipCodeLoader.Instance().LoadZipCodes());
                return zipcodeRepo;
            });

            return this;
        }

        public IZipCodeRepository GetRepository()
        {
            IZipCodeRepository repo = null;

            foreach (var step in _steps)
            {
                repo = step(repo);
            }

            return repo;
        }
    }
}
