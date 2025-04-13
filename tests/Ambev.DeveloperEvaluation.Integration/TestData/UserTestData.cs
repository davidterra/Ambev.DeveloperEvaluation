using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.TestData
{
    public static class UserTestData
    {
        private static readonly Faker<User> createUserFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
            .RuleFor(u => u.Name, f => new PersonNameValue(f.Name.FirstName(), f.Name.LastName()))
            .RuleFor(u => u.Address, f => new AddressValue
            (
                f.Address.City(),
                f.Address.StateAbbr(),
                f.Address.StreetName(),
                f.Random.Number(1, 9999).ToString(),
                f.Address.ZipCode("#####-###"),
                new GeoLocationValue
                (
                    f.Address.Latitude().ToString(),
                    f.Address.Longitude().ToString()
                )
            ));

        public static User GenerateValidEntity()
        {
            return createUserFaker.Generate();
        }
    }
}
