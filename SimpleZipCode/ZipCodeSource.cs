using SimpleZipCode.Repos;
using SimpleZipCode.Sources;
using System;
using System.Collections.Generic;

namespace SimpleZipCode
{
    public class ZipCodeSource
    {
        private List<Func<IZipCodeRepository, IZipCodeRepository>> _steps =
            new List<Func<IZipCodeRepository, IZipCodeRepository>>();

        public ZipCodeSource() { }
        public ZipCodeSource(Func<IZipCodeRepository, IZipCodeRepository> step)
        {
            _steps.Add(step);
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

        public static ZipCodeSource FromMemory()
        {
            return new ZipCodeSource((zipcodeRepo) =>
            {
                zipcodeRepo = new ZipCodeRepo(LocalResourceZipCodeLoader.Instance().LoadZipCodes());
                return zipcodeRepo;
            });
        }
    }
}
