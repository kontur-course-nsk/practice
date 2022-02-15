namespace API.Mapping
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var mappingConfig = new MapperConfiguration(it => it.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingConfig.CreateMapper());

            return services;
        }
    }
}
