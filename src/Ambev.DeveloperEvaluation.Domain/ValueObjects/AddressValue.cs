namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents an immutable value object for an address.
    /// </summary>
    public sealed class AddressValue : IEquatable<AddressValue>
    {
        /// <summary>
        /// Gets the city of the address.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets the state of the address.
        /// </summary>
        public string State { get; }

        /// <summary>
        /// Gets the street of the address.
        /// </summary>
        public string Street { get; }

        /// <summary>
        /// Gets the number of the address.
        /// </summary>
        public string Number { get; }

        /// <summary>
        /// Gets the zip code of the address.
        /// </summary>
        public string ZipCode { get; }

        /// <summary>
        /// Gets the geographical location of the address.
        /// </summary>
        public GeoLocationValue GeoLocation { get; }

        /// <summary>
        /// Private constructor for EF Core usage.
        /// Initializes default values for the address properties.
        /// </summary>
        private AddressValue()
        {
            City = string.Empty;
            State = string.Empty;
            Street = string.Empty;
            Number = string.Empty;
            ZipCode = string.Empty;
            GeoLocation = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressValue"/> class with the specified address details.
        /// </summary>
        /// <param name="city">The city of the address.</param>
        /// <param name="state">The state of the address.</param>
        /// <param name="street">The street of the address.</param>
        /// <param name="number">The number of the address.</param>
        /// <param name="zipCode">The zip code of the address.</param>
        /// <param name="geoLocation">The geographical location of the address.</param>
        /// <exception cref="ArgumentException">Thrown when any string parameter is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="geoLocation"/> is null.</exception>
        public AddressValue(string city, string state, string street, string number, string zipCode, GeoLocationValue geoLocation)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be null or empty.", nameof(city));

            if (string.IsNullOrEmpty(state))
                throw new ArgumentException("State cannot be null or empty.", nameof(state));

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be null or empty.", nameof(street));

            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Number cannot be null or empty.", nameof(number));

            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("ZipCode cannot be null or empty.", nameof(zipCode));

            City = city.Trim();
            State = state.Trim();
            Street = street.Trim();
            Number = number;
            ZipCode = zipCode.Trim();
            GeoLocation = geoLocation ?? throw new ArgumentNullException(nameof(geoLocation));
        }

        /// <summary>
        /// Returns a string representation of the address.
        /// </summary>
        /// <returns>A string containing the address details.</returns>
        public override string ToString() =>
            $"{Street}, {Number}, {City}, {State}, {ZipCode} ({GeoLocation})";

        /// <summary>
        /// Determines whether the specified object is equal to the current address.
        /// </summary>
        /// <param name="obj">The object to compare with the current address.</param>
        /// <returns><c>true</c> if the specified object is equal to the current address; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj) => obj is AddressValue other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="AddressValue"/> is equal to the current address.
        /// </summary>
        /// <param name="other">The address to compare with the current address.</param>
        /// <returns><c>true</c> if the specified address is equal to the current address; otherwise, <c>false</c>.</returns>
        public bool Equals(AddressValue? other) =>
            other is not null &&
            City == other.City &&
            State == other.State &&
            Street == other.Street &&
            Number == other.Number &&
            ZipCode == other.ZipCode &&
            GeoLocation == other.GeoLocation;

        /// <summary>
        /// Returns the hash code for the current address.
        /// </summary>
        /// <returns>A hash code for the current address.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(City, State, Street, Number, ZipCode, GeoLocation);

        /// <summary>
        /// Determines whether two <see cref="AddressValue"/> instances are equal.
        /// </summary>
        /// <param name="left">The first address to compare.</param>
        /// <param name="right">The second address to compare.</param>
        /// <returns><c>true</c> if the two addresses are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(AddressValue left, AddressValue right) => Equals(left, right);

        /// <summary>
        /// Determines whether two <see cref="AddressValue"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first address to compare.</param>
        /// <param name="right">The second address to compare.</param>
        /// <returns><c>true</c> if the two addresses are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(AddressValue left, AddressValue right) => !Equals(left, right);
    }
}
