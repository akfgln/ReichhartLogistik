using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Models
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,
           WebApplicationBuilder builder)
        {
            AutoMapper.InitializeAutomapper();

            var mvcBuilder = services.AddControllersWithViews();
            //add fluent validation
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            //register all available validators from assemblies
            var assemblies = mvcBuilder.PartManager.ApplicationParts
                .OfType<AssemblyPart>()
                .Where(part => part.Name.StartsWith("ReichhartLogistik", StringComparison.InvariantCultureIgnoreCase))
                .Select(part => part.Assembly);
            services.AddValidatorsFromAssemblies(assemblies);
        }

    }
}
