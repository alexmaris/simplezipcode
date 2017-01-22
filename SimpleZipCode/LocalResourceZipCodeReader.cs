using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleZipCode
{
    public class LocalResourceZipCodeReader
    {
        private readonly string _localResource;
        private readonly bool _header;

        public LocalResourceZipCodeReader(string localResource, bool header = true)
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
                    return new ZipCode
                    {
                        PostalCode = x[0].Trim(),
                        PlaceName = x[1],
                        State = x[2],
                        StateAbbreviation = x[3],
                        County = x[4],
                        Latitude = double.Parse(x[5]),
                        Longitude = double.Parse(x[6])
                    };
                }).ToList();
        }
    }
}
