namespace Register.Options.Asp.Net.Core.Tests.Options
{
    public class PersonOptions
    {
        public string FullName { get; set; }

        public int Age { get; set; }

        public AddressOptions AddressOptions { get; set; }
    }

    public class AddressOptions
    {
        public string City { get; set; }

        public string Country { get; set; }
    }
}