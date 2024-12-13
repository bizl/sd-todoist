namespace SeamlessDigital.Todo.Domain
{
    public class WeatherItem
    {
        public Current Current { get; set; }
    }
    public class Current
    {
        public decimal temp_c { get; set; }
        public decimal temp_f { get; set; }
        public Condition condition { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
    }

} 