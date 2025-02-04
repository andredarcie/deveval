using DevEval.Common;

namespace DevEval.Domain.ValueObjects
{
    /// <summary>
    /// Represents the geolocation details of an address.
    /// </summary>
    public class Geolocation : ValueObject
    {
        /// <summary>
        /// Gets the latitude of the location.
        /// </summary>
        public double Lat { get; }

        /// <summary>
        /// Gets the longitude of the location.
        /// </summary>
        public double Long { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        public Geolocation(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentException(nameof(latitude), "Latitude must be between -90 and 90.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentException(nameof(longitude), "Longitude must be between -180 and 180.");

            Lat = latitude;
            Long = longitude;
        }

        private Geolocation()
        {
            Lat = 0.0;
            Long = 0.0;
        }

        /// <summary>
        /// Represents an empty geolocation instance.
        /// </summary>
        public static Geolocation Empty => new Geolocation(0.0, 0.0);

        /// <summary>
        /// Returns a string representation of the geolocation.
        /// </summary>
        public override string ToString() => $"Latitude: {Lat}, Longitude: {Long}";

        /// <summary>
        /// Defines the components of equality for the value object.
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Lat;
            yield return Long;
        }
    }
}