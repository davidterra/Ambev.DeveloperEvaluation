using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

/// <summary>
/// Represents a command to list product categories.
/// This command does not require any input parameters and returns a <see cref="ListCategoriesResult"/>.
/// </summary>
public record ListCategoriesCommand : IRequest<ListCategoriesResult> { }
