using NUnit.Framework;
using Should;
using SimpleZipCode;
using System.Linq;

namespace SimpleZipeCode.Tests.Integration
{
    [TestFixture]
    public class ZipCodeSourceTests
    {
        private readonly ZipCodeSource _zipCodeSource;
        private IZipCodeRepository _zipCodeRepo;

        [SetUp]
        public void Setup()
        {
            _zipCodeRepo = ZipCodeSource.FromMemory().GetRepository();
        }

        [Test]
        public void Should_search_within_radius()
        {
            var lisle = _zipCodeRepo.Search(x => x.PlaceName.Contains("Lisle"));
            var zipCode = _zipCodeRepo.Get("60606");
            var results = _zipCodeRepo.RadiusSearch(zipCode, 1).ToList();

            results.Count.ShouldEqual(9);
        }
    }
}
