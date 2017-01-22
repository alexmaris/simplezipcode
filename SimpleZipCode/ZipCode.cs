using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleZipCode
{
    public class ZipCode
    {
        public string PostalCode { get; protected internal set; }
        public string PlaceName { get; protected internal set; }
        public string State { get; protected internal set; }
        public string StateAbbreviation { get; protected internal set; }
        public string County { get; protected internal set; }
        public double Latitude { get; protected internal set; }
        public double Longitude { get; protected internal set; }
    }
}
