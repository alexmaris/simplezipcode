using NUnit.Framework;
using Should;
using SimpleZipCode;
using SimpleZipCode.Repos;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipeCode.Tests.Unit
{
    [TestFixture]
    public class ZipCodeRepoTests
    {
        private ZipCodeRepo _zipCodeRepo;

        [OneTimeSetUp]
        public void Setup()
        {
            _zipCodeRepo = new ZipCodeRepo(GetZipCodes());
        }

        [Test]
        public void Should_get_zipCode_by_postalCode()
        {
            var zipCode = _zipCodeRepo.Get("81602");

            zipCode.PostalCode.ShouldEqual("81602");
            zipCode.PlaceName.ShouldEqual("Glenwood Springs");
            zipCode.State.ShouldEqual("Colorado");
            zipCode.StateAbbreviation.ShouldEqual("CO");
            zipCode.County.ShouldEqual("Garfield");
            zipCode.Latitude.ShouldEqual(39.5117);
            zipCode.Longitude.ShouldEqual(-107.3253);
        }

        [Test]
        public void Should_search_using_predicate()
        {
            var searchResults = _zipCodeRepo
                .Search(x => x.PlaceName == "Glenwood Springs")
                .ToList();

            searchResults.Count.ShouldEqual(1);
            searchResults[0].PostalCode.ShouldEqual("81602");
            searchResults[0].PlaceName.ShouldEqual("Glenwood Springs");
            searchResults[0].State.ShouldEqual("Colorado");
            searchResults[0].StateAbbreviation.ShouldEqual("CO");
            searchResults[0].County.ShouldEqual("Garfield");
            searchResults[0].Latitude.ShouldEqual(39.5117);
            searchResults[0].Longitude.ShouldEqual(-107.3253);
        }

        [Test]
        public void Should_perform_radius_search()
        {
            var zipCode = _zipCodeRepo.Get("81611");
            var searchResults = _zipCodeRepo
                .RadiusSearch(zipCode, 4)
                .ToList();

            searchResults.Count.ShouldEqual(2);
            searchResults[0].PostalCode.ShouldEqual("81611");
            searchResults[1].PostalCode.ShouldEqual("81612");
        }

        [Test]
        public void Should_get_default_value_for_zipCode_by_PostalCode()
        {
            _zipCodeRepo.Get("99999").ShouldBeNull();
        }

        private List<ZipCode> GetZipCodes()
        {
            return new List<ZipCode>
            {
                new ZipCode("81602","Glenwood Springs","Colorado","CO","Garfield",39.5117,-107.3253),
                new ZipCode("81610","Dinosaur","Colorado","CO","Moffat",40.2566,-108.9652),
                new ZipCode("81611","Aspen","Colorado","CO","Pitkin",39.1951,-106.8236),
                new ZipCode("81612","Aspen","Colorado","CO","Pitkin",39.2234,-106.8828),
                new ZipCode("81615","Snowmass Village","Colorado","CO","Pitkin",39.2212,-106.932)
            };
        }
    }
}
