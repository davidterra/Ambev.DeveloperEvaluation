﻿using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Sales.Services;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        builder.Services.AddTransient<IDiscountService, DiscountService>();
        builder.Services.AddTransient<ISaleNumberGeneratorService, SaleNumberGeneratorService>();
    }
}