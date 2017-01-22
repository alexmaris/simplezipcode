using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleZipCode
{
    public interface IZipCodeRepository
    {
        /// <summary>
        /// Search the repo for ZipCode(s) that match the specified predicate
        /// </summary>
        IEnumerable<ZipCode> Search(Expression<Func<ZipCode, bool>> predicate);
        /// <summary>
        /// Return a collection of ZipCode(s) that are within the specified radius
        /// </summary>
        IEnumerable<ZipCode> RadiusSearch(ZipCode zipCode, double mileRadius);
        /// <summary>
        /// Get the first ZipCode that matches the specified postalCode
        /// </summary>
        ZipCode Get(string postalCode);
    }
}
