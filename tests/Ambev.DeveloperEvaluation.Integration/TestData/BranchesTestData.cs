using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Repositories.TestData
{
    public class BranchesTestData
    {
        private static readonly Faker<Branch> createBranchesFaker = new Faker<Branch>()
            .RuleFor(u => u.Id, f => f.Random.Int(1, 1000))
            .RuleFor(u => u.Name, f => f.Company.CompanyName()
       );

        public static Branch GenerateValidEntity()
        {
            return createBranchesFaker.Generate();
        }
    }
}
