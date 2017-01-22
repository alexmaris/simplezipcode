using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipCode
{
    public sealed class LocalResourceZipCodeLoader
    {
        private readonly string _localResource;
        private readonly bool _header;

        public LocalResourceZipCodeLoader(string localResource, bool header = true)
        {
            _localResource = localResource;
            _header = header;
        }

        private string[] LoadZipCodesFromString(string csv)
        {
            return csv.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        public List<ZipCode> LoadZipCodes()
        {
            return LoadZipCodesFromString(_localResource)
                .Skip(_header ? 1 : 0)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split(','))
                .Where(x => !string.IsNullOrWhiteSpace(x[0]))
                .Select(x =>
                {
                    var postalCode = x[0].Trim();
                    var placeName = x[1];
                    var state = x[2];
                    var stateAbbreviation = x[3];
                    var county = x[4];
                    var latitude = double.Parse(x[5]);
                    var longitude = double.Parse(x[6]);

                    return new ZipCode(postalCode, placeName, state, stateAbbreviation, county, latitude, longitude);
                }).ToList();
        }
    }
}
