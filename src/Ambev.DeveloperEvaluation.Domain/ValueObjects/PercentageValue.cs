namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents a percentage value between 0 and 100.
    /// Provides functionality to apply the percentage to a given amount.
    /// </summary>
    public sealed class PercentageValue : IEquatable<PercentageValue>
    {
        /// <summary>
        /// Gets the percentage value.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentageValue"/> class.
        /// </summary>
        /// <param name="value">The percentage value, which must be between 0 and 100.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than 0 or greater than 100.</exception>
        public PercentageValue(decimal value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be between 0 and 100.");

            Value = value;
        }

        /// <summary>
        /// Applies the percentage value to a given amount.
        /// </summary>
        /// <param name="amount">The amount to which the percentage will be applied. Must be non-negative.</param>
        /// <returns>The result of applying the percentage to the amount.</returns>
        /// <exception cref="ArgumentException">Thrown when the amount is negative.</exception>
        public decimal ApplyTo(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            return amount * (Value / 100);
        }

        /// <summary>
        /// Returns a string representation of the percentage value in the format "XX.XX%".
        /// </summary>
        /// <returns>A string representation of the percentage value.</returns>
        public override string ToString()
        {
            return $"{Value:F2}%";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return obj is PercentageValue other && Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="PercentageValue"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="PercentageValue"/> to compare with the current instance.</param>
        /// <returns>True if the specified <see cref="PercentageValue"/> is equal to the current instance; otherwise, false.</returns>
        public bool Equals(PercentageValue? other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }

        /// <summary>
        /// Returns the hash code for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Determines whether two <see cref="PercentageValue"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="PercentageValue"/> to compare.</param>
        /// <param name="right">The second <see cref="PercentageValue"/> to compare.</param>
        /// <returns>True if the two instances are equal; otherwise, false.</returns>
        public static bool operator ==(PercentageValue left, PercentageValue right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="PercentageValue"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="PercentageValue"/> to compare.</param>
        /// <param name="right">The second <see cref="PercentageValue"/> to compare.</param>
        /// <returns>True if the two instances are not equal; otherwise, false.</returns>
        public static bool operator !=(PercentageValue left, PercentageValue right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Gets a <see cref="PercentageValue"/> instance representing 0%.
        /// </summary>
        public static PercentageValue Zero => new(0);
    }
}
