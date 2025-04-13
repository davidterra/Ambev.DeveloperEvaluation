namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents a geographical location with latitude and longitude values.
    /// This class is immutable and provides value equality semantics.
    /// </summary>
    public sealed class GeoLocationValue : IEquatable<GeoLocationValue>
    {
        /// <summary>
        /// Gets the latitude of the geographical location.
        /// </summary>
        public string Latitude { get; }

        /// <summary>
        /// Gets the longitude of the geographical location.
        /// </summary>
        public string Longitude { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocationValue"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location. Cannot be null or empty.</param>
        /// <param name="longitude">The longitude of the location. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="latitude"/> or <paramref name="longitude"/> is null, empty, or whitespace.
        /// </exception>
        public GeoLocationValue(string latitude, string longitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
                throw new ArgumentException("Latitude cannot be null or empty.", nameof(latitude));

            if (string.IsNullOrWhiteSpace(longitude))
                throw new ArgumentException("Longitude cannot be null or empty.", nameof(longitude));

            Latitude = latitude.Trim();
            Longitude = longitude.Trim();
        }

        /// <summary>
        /// Returns a string representation of the geographical location.
        /// </summary>
        /// <returns>A string in the format "Lat: {Latitude}, Long: {Longitude}".</returns>
        public override string ToString() => $"Lat: {Latitude}, Long: {Longitude}";

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj) => obj is GeoLocationValue other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="GeoLocationValue"/> is equal to the current object.
        /// </summary>
        /// <param name="other">The <see cref="GeoLocationValue"/> to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="GeoLocationValue"/> is equal to the current object; otherwise, <c>false</c>.</returns>
        public bool Equals(GeoLocationValue? other) =>
            other is not null && Latitude == other.Latitude && Longitude == other.Longitude;

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => HashCode.Combine(Latitude, Longitude);

        /// <summary>
        /// Determines whether two <see cref="GeoLocationValue"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="GeoLocationValue"/> to compare.</param>
        /// <param name="right">The second <see cref="GeoLocationValue"/> to compare.</param>
        /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(GeoLocationValue left, GeoLocationValue right) => Equals(left, right);

        /// <summary>
        /// Determines whether two <see cref="GeoLocationValue"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="GeoLocationValue"/> to compare.</param>
        /// <param name="right">The second <see cref="GeoLocationValue"/> to compare.</param>
        /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(GeoLocationValue left, GeoLocationValue right) => !Equals(left, right);
    }
}
