﻿namespace SeamlessDigital.Todo.Domain
{
    public class Location
    { 
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; } 
        public WeatherItem? Weather { get; set; }
    }
}
