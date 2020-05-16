using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarsRover.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddImplementedTypes<T>(this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime, params Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new DevelopmentException($"Assemblies should not be null");
            }

            IEnumerable<TypeInfo> typesFromAssemblies = assemblies.SelectMany(assembly => assembly.DefinedTypes.Where(x => typeof(T).IsAssignableFrom(x)
                                                                                                                        && x.IsClass
                                                                                                                        && !x.IsAbstract));

            foreach (TypeInfo type in typesFromAssemblies)
            {
                serviceCollection.Add(new ServiceDescriptor(typeof(T), type, serviceLifetime));
            }

            return serviceCollection;
        }
    }
}