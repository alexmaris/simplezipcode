namespace SimpleZipCode
{
    public class ZipCode
    {
        public string PostalCode { get; private set; }
        public string PlaceName { get; private set; }
        public string State { get; private set; }
        public string StateAbbreviation { get; private set; }
        public string County { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public ZipCode(
            string postalCode, 
            string placeName, 
            string state, 
            string stateAbbreviation, 
            string county, 
            double latitude, 
            double longitude)
        {
            PostalCode = postalCode;
            PlaceName = placeName;
            State = state;
            StateAbbreviation = stateAbbreviation;
            County = county;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
