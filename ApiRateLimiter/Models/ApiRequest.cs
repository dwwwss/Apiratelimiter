using System;

namespace ApiRateLimiter.Models
{
    public class ApiRequest
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public DateTime Timestamp { get; set; }
        public string ApiKey { get; set; }
    }
}
