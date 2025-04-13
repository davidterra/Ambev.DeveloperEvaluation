namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents a value object for a person's name, consisting of a first name and a last name.
/// This class is immutable and ensures that both names are non-null, non-empty, and trimmed.
/// </summary>
public sealed class PersonNameValue : IEquatable<PersonNameValue>
{
    /// <summary>
    /// Gets the first name of the person.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name of the person.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonNameValue"/> class.
    /// Ensures that the first name and last name are not null, empty, or whitespace.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    /// <param name="lastName">The last name of the person.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="firstName"/> or <paramref name="lastName"/> is null, empty, or whitespace.
    /// </exception>
    public PersonNameValue(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    /// <summary>
    /// Returns the full name as a single string in the format "FirstName LastName".
    /// </summary>
    /// <returns>A string representation of the full name.</returns>
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the specified object is a <see cref="PersonNameValue"/> and has the same first and last names; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return obj is PersonNameValue other && Equals(other);
    }

    /// <summary>
    /// Determines whether the specified <see cref="PersonNameValue"/> is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="PersonNameValue"/> to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="PersonNameValue"/> has the same first and last names; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(PersonNameValue? other)
    {
        if (other is null) return false;
        return FirstName == other.FirstName && LastName == other.LastName;
    }

    /// <summary>
    /// Returns a hash code for the current instance, based on the first and last names.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }

    /// <summary>
    /// Determines whether two <see cref="PersonNameValue"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="PersonNameValue"/> to compare.</param>
    /// <param name="right">The second <see cref="PersonNameValue"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if both instances have the same first and last names; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(PersonNameValue left, PersonNameValue right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two <see cref="PersonNameValue"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="PersonNameValue"/> to compare.</param>
    /// <param name="right">The second <see cref="PersonNameValue"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the instances do not have the same first and last names; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(PersonNameValue left, PersonNameValue right)
    {
        return !Equals(left, right);
    }
}
