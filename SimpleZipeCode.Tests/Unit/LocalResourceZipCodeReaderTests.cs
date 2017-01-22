using NUnit.Framework;
using Should;
using SimpleZipCode;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipeCode.Tests.Unit
{
    [TestFixture]
    public class LocalResourceZipCodeReaderTests
    {
        private readonly string _localResourceCsv = @"Postal Code,Place Name,State,State Abbreviation,County,Latitude,Longitude,
                                                     12345,Springfield,TV Land,ZZ,Cook,41.8868,-87.6386,
                                                     ,,,,,,,
                                                     
                                                     ,No Postal Code,TV Land,ZZ,Cook,41.8864,-87.6382,";

        private LocalResourceZipCodeReader _localResourceZipCodeReader;

        [SetUp]
        public void Setup()
        {
            _localResourceZipCodeReader = new LocalResourceZipCodeReader(_localResourceCsv, header: true);
        }

        [Test]
        public void Should_skip_header_line()
        {
            LoadZipCodes()
                .Any(z => z.PostalCode == "Postal Code")
                .ShouldBeFalse();
        }

        [Test]
        public void Should_skip_empty_lines()
        {
            LoadZipCodes()
                .Any(z => string.IsNullOrEmpty(z.PostalCode))
                .ShouldBeFalse();
        }

        [Test]
        public void Should_skip_lines_with_no_postal_code()
        {
            LoadZipCodes()
                .Any(z => z.PlaceName == "No Postal Code")
                .ShouldBeFalse();
        }

        [Test]
        public void Should_load_zip_codes()
        {
            var zipCodes = LoadZipCodes();
            var result = zipCodes.First();

            result.PostalCode.ShouldEqual("12345");
            result.PlaceName.ShouldEqual("Springfield");
            result.State.ShouldEqual("TV Land");
            result.StateAbbreviation.ShouldEqual("ZZ");
            result.County.ShouldEqual("Cook");
            result.Latitude.ShouldEqual(41.8868);
            result.Longitude.ShouldEqual(-87.6386);
        }

        private List<ZipCode> LoadZipCodes()
        {
            return _localResourceZipCodeReader.LoadZipCodes().ToList();
        }
    }
}
