namespace RegisterOptionsDemo.Options
{
    public class AOptions
    {
        public string Value1 { get; set; }

        public int Value2 { get; set; }

        public ASubOption ASubOption { get; set; }
    }

    public class ASubOption
    {
        public string Value3 { get; set; }

        public int Value4 { get; set; }
    }
}