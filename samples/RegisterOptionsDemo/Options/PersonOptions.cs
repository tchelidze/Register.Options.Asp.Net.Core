namespace RegisterOptionsDemo.Options
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

        public int Country { get; set; }
    }
}