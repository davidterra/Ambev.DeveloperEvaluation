namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents a value object for a rating, consisting of a rate and a count.
/// </summary>
public sealed class RatingValue : IEquatable<RatingValue>
{
    /// <summary>
    /// Gets the rate value, which must be between 0 and 5.
    /// </summary>
    public decimal Rate { get; }

    /// <summary>
    /// Gets the count value, which must be a non-negative integer.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RatingValue"/> class.
    /// </summary>
    /// <param name="rate">The rate value, between 0 and 5.</param>
    /// <param name="count">The count value, a non-negative integer.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the rate is not between 0 and 5, or when the count is negative.
    /// </exception>
    public RatingValue(decimal rate, int count)
    {
        if (rate < 0 || rate > 5)
            throw new ArgumentException("Rate must be between 0 and 5.", nameof(rate));

        if (count < 0)
            throw new ArgumentException("Count must be a non-negative integer.", nameof(count));

        Rate = rate;
        Count = count;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as RatingValue);
    }

    /// <summary>
    /// Determines whether the specified <see cref="RatingValue"/> is equal to the current object.
    /// </summary>
    /// <param name="other">The <see cref="RatingValue"/> to compare with the current object.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public bool Equals(RatingValue? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Rate == other.Rate && Count == other.Count;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Rate, Count);
    }

    /// <summary>
    /// Determines whether two <see cref="RatingValue"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="RatingValue"/> to compare.</param>
    /// <param name="right">The second <see cref="RatingValue"/> to compare.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(RatingValue? left, RatingValue? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="RatingValue"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="RatingValue"/> to compare.</param>
    /// <param name="right">The second <see cref="RatingValue"/> to compare.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(RatingValue? left, RatingValue? right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="RatingValue"/>.
    /// </summary>
    /// <returns>A string in the format "Rate: {Rate}, Count: {Count}".</returns>
    public override string ToString()
    {
        return $"Rate: {Rate}, Count: {Count}";
    }

    /// <summary>
    /// Gets a <see cref="RatingValue"/> instance representing a zero rate and count.
    /// </summary>
    public static RatingValue Zero => new(0, 0);
}
