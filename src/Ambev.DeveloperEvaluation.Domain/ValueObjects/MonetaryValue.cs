namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with operations for addition, subtraction, and multiplication.
/// Ensures that monetary values cannot be negative and provides comparison operators.
/// </summary>
public sealed class MonetaryValue : IEquatable<MonetaryValue>
{
    /// <summary>
    /// Gets the amount of the monetary value.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonetaryValue"/> class with the specified amount.
    /// </summary>
    /// <param name="amount">The monetary amount. Must be non-negative.</param>
    /// <exception cref="ArgumentException">Thrown when the amount is negative.</exception>
    public MonetaryValue(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        Amount = amount;
    }

    /// <summary>
    /// Adds the specified monetary value to the current instance.
    /// </summary>
    /// <param name="other">The monetary value to add.</param>
    /// <returns>A new <see cref="MonetaryValue"/> representing the sum.</returns>
    public MonetaryValue Add(MonetaryValue other)
    {
        return new MonetaryValue(Amount + other.Amount);
    }

    /// <summary>
    /// Subtracts the specified monetary value from the current instance.
    /// </summary>
    /// <param name="other">The monetary value to subtract.</param>
    /// <returns>A new <see cref="MonetaryValue"/> representing the difference.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the resulting amount would be negative.</exception>
    public MonetaryValue Subtract(MonetaryValue other)
    {
        if (Amount < other.Amount)
            throw new InvalidOperationException("Resulting amount cannot be negative.");

        return new MonetaryValue(Amount - other.Amount);
    }

    /// <summary>
    /// Multiplies the current monetary value by the specified multiplier.
    /// </summary>
    /// <param name="multiplier">The multiplier. Must be non-negative.</param>
    /// <returns>A new <see cref="MonetaryValue"/> representing the product.</returns>
    /// <exception cref="ArgumentException">Thrown when the multiplier is negative.</exception>
    public MonetaryValue Multiply(decimal multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative.", nameof(multiplier));

        return new MonetaryValue(Amount * multiplier);
    }

    /// <summary>
    /// Returns a string representation of the monetary value formatted to two decimal places.
    /// </summary>
    /// <returns>A string representation of the monetary value.</returns>
    public override string ToString()
    {
        return $"{Amount:F2}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        return obj is MonetaryValue other && Equals(other);
    }

    /// <summary>
    /// Determines whether the specified <see cref="MonetaryValue"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The monetary value to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified monetary value is equal to the current instance; otherwise, <c>false</c>.</returns>
    public bool Equals(MonetaryValue? other)
    {
        if (other is null) return false;
        return Amount == other.Amount;
    }

    /// <summary>
    /// Returns the hash code for the current instance.
    /// </summary>
    /// <returns>The hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="MonetaryValue"/> instances are equal.
    /// </summary>
    /// <param name="left">The first monetary value to compare.</param>
    /// <param name="right">The second monetary value to compare.</param>
    /// <returns><c>true</c> if the two monetary values are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MonetaryValue left, MonetaryValue right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="MonetaryValue"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first monetary value to compare.</param>
    /// <param name="right">The second monetary value to compare.</param>
    /// <returns><c>true</c> if the two monetary values are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MonetaryValue left, MonetaryValue right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether one <see cref="MonetaryValue"/> is less than another.
    /// </summary>
    /// <param name="left">The first monetary value to compare.</param>
    /// <param name="right">The second monetary value to compare.</param>
    /// <returns><c>true</c> if the first monetary value is less than the second; otherwise, <c>false</c>.</returns>
    public static bool operator <(MonetaryValue left, MonetaryValue right)
    {
        return left.Amount < right.Amount;
    }

    /// <summary>
    /// Determines whether one <see cref="MonetaryValue"/> is greater than another.
    /// </summary>
    /// <param name="left">The first monetary value to compare.</param>
    /// <param name="right">The second monetary value to compare.</param>
    /// <returns><c>true</c> if the first monetary value is greater than the second; otherwise, <c>false</c>.</returns>
    public static bool operator >(MonetaryValue left, MonetaryValue right)
    {
        return left.Amount > right.Amount;
    }

    /// <summary>
    /// Gets a <see cref="MonetaryValue"/> instance representing zero.
    /// </summary>
    public static MonetaryValue Zero => new(0);
}

