using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Register.Options.Asp.Net.Core.Register.Options.Asp.Net.Core;
using Register.Options.Asp.Net.Core.Tests.Options;
using Shouldly;
using Xunit;

namespace Register.Options.Asp.Net.Core.Tests
{
    public class when_options_are_registered_from_assembly : register_options_asp_net_core_test_base
    {
        private readonly Assembly TestAssembly = typeof(IDEOptions).Assembly;
        private readonly ServiceProvider ServiceProvider;

        public when_options_are_registered_from_assembly()
        {
            ServiceCollection.ConfigureOptionsFromAssembly(Configuration, TestAssembly);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        [Fact]
        public void PersonOptions_should_be_registered_as_option_with_correct_values()
        {
            var personOptionsConfigurer = ServiceProvider.GetService<IConfigureOptions<PersonOptions>>();
            var personOptions = new PersonOptions();
            personOptionsConfigurer.Configure(personOptions);

            personOptions.FullName.ShouldBe("Bitchiko Tchelidze");
            personOptions.Age.ShouldBe(22);
            personOptions.AddressOptions.ShouldNotBeNull();
            personOptions.AddressOptions.City.ShouldBe("Tbilisi");
            personOptions.AddressOptions.Country.ShouldBe("Georgia");

        }

        [Fact]
        public void IDEOptions_should_be_registered_as_option_with_correct_values()
        {
            var personOptionsConfigurer = ServiceProvider.GetService<IConfigureOptions<IDEOptions>>();
            var ideOptions = new IDEOptions();
            personOptionsConfigurer.Configure(ideOptions);

            ideOptions.Name.ShouldBe("Visual Studio");
            ideOptions.Version.ShouldBe("15.6.6");
        }
    }
}