using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Register.Options.Asp.Net.Core
{
    namespace Register.Options.Asp.Net.Core
    {
        public static class OptionsRegistrationServiceCollectionExtensions
        {
            private static readonly MethodInfo ConfigureMethodInfo;

            static OptionsRegistrationServiceCollectionExtensions() => ConfigureMethodInfo = GetConfigureMethodInfo();

            private static MethodInfo GetConfigureMethodInfo()
                => typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethod(
                        nameof(OptionsConfigurationServiceCollectionExtensions.Configure),
                        BindingFlags.Static | BindingFlags.Public,
                        null,
                        new[] { typeof(IServiceCollection), typeof(IConfiguration) },
                        null);

            private static IReadOnlyList<Type> GetTypesWithParameterlessConstructor(Assembly assembly)
                => assembly
                    .DefinedTypes
                    .Where(it => it.IsClass)
                    .Where(type => type.GetConstructor(Type.EmptyTypes) != null)
                    .ToList();


            /// <summary>
            ///     Configures Types from entry assemblies as option with value retrieved from configuration via name of the type
            /// </summary>
            public static IServiceCollection ConfigureOptionsFromEntyAssembly(
                this IServiceCollection services,
                IConfiguration configuration)
            {
                ConfigureOptionsFromAssembly(services, configuration, Assembly.GetEntryAssembly());
                return services;
            }

            /// <summary>
            ///     Configures Types from specified assemblies as option with value retrieved from configuration via name of the type
            /// </summary>
            public static IServiceCollection ConfigureOptionsFromAssemblies(
                this IServiceCollection services,
                IConfiguration configuration,
                IReadOnlyList<Assembly> assemblies)
            {
                foreach (var assembly in assemblies)
                {
                    ConfigureOptionsFromAssembly(services, configuration, assembly);
                }

                return services;
            }


            /// <summary>
            ///     Configures Types from specified assembly as option with value retrieved from configuration via name of the type
            /// </summary>
            public static IServiceCollection ConfigureOptionsFromAssembly(
                this IServiceCollection services,
                IConfiguration configuration,
                Assembly assembly)
            {
                var definedTypesWithParameterlessConstructor = GetTypesWithParameterlessConstructor(assembly);

                configuration
                    .GetChildren()
                    .Select(section =>
                    {
                        var sectionCorrespondingType =
                            definedTypesWithParameterlessConstructor
                                .FirstOrDefault(type => type.Name == section.Key);

                        return new
                        {
                            section,
                            sectionCorrespondingType
                        };
                    })
                    .Where(it => it.sectionCorrespondingType != null)
                    .ToList()
                    .ForEach(
                        it => ConfigureOption(services, configuration, it.sectionCorrespondingType, it.section.Key));

                return services;
            }


            /// <summary>
            ///     Configures option of type TConfig with value retrieved from configuration via name of TConfig type
            /// </summary>
            public static void ConfigureOption<TConfig>(this IServiceCollection services, IConfiguration configuration)
                where TConfig : class
                => services.Configure<TConfig>(configuration.GetSection(typeof(TConfig).Name));


            private static void ConfigureOption(
                this IServiceCollection services,
                IConfiguration configuration,
                Type optionType,
                string configurationSectionKey)
            {
                var genericConfigureMethodInfo = ConfigureMethodInfo.MakeGenericMethod(optionType);
                genericConfigureMethodInfo.Invoke(null,
                    new object[] { services, configuration.GetSection(configurationSectionKey) });
            }
        }
    }
}