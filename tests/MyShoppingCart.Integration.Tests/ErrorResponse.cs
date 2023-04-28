﻿namespace MyShoppingCart.Integration.Tests
{
    public class ErrorResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string Detail { get; set; } = string.Empty;
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
