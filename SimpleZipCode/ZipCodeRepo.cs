using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleZipCode
{
    public class ZipCodeRepo : IZipCodeRepository
    {
        private readonly List<ZipCode> _zipCodes;

        public ZipCodeRepo(List<ZipCode> zipCodes)
        {
            _zipCodes = zipCodes;
        }

        public IEnumerable<ZipCode> Search(Expression<Func<ZipCode, bool>> predicate)
        {
            return _zipCodes.Where(predicate.Compile());
        }

        public IEnumerable<ZipCode> RadiusSearch(ZipCode zipCode, double mileRadius)
        {
            return _zipCodes.Where(z =>
                (3963 * Math.Acos(
                    Math.Sin(z.Latitude / 57.2958) * Math.Sin(zipCode.Latitude / 57.2958) +
                    Math.Cos(z.Latitude / 57.2958) * Math.Cos(zipCode.Latitude / 57.2958) *
                    Math.Cos(zipCode.Longitude / 57.2958 - z.Longitude / 57.2958)
                    )
                    ) <= mileRadius);
        }

        public ZipCode Get(string postalCode)
        {
            return _zipCodes.FirstOrDefault(z => z.PostalCode == postalCode);
        }
    }
}
