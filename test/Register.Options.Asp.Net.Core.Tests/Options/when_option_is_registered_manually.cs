using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Register.Options.Asp.Net.Core.Register.Options.Asp.Net.Core;
using Shouldly;
using Xunit;

namespace Register.Options.Asp.Net.Core.Tests.Options
{
    public class when_option_is_registered_manually : register_options_asp_net_core_test_base
    {
        [Fact]
        public void PersonOptions_should_be_registered_as_option_with_correct_values()
        {
            ServiceCollection.ConfigureOption<PersonOptions>(Configuration);
            var serviceProvider = ServiceCollection.BuildServiceProvider();

            var personOptionsConfigurer = serviceProvider.GetService<IConfigureOptions<PersonOptions>>();
            var personOptions = new PersonOptions();
            personOptionsConfigurer.Configure(personOptions);

            personOptions.FullName.ShouldBe("Bitchiko Tchelidze");
            personOptions.Age.ShouldBe(22);
            personOptions.AddressOptions.ShouldNotBeNull();
            personOptions.AddressOptions.City.ShouldBe("Tbilisi");
            personOptions.AddressOptions.Country.ShouldBe("Georgia");
        }
    }
}