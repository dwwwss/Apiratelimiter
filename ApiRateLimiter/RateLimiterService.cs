using System;
using System.Collections.Generic;
using System.Linq;
using ApiRateLimiter.Models;

namespace ApiRateLimiter.Services
{
    public class RateLimiterService
    {
        private readonly ApplicationDbContext _context;
        private readonly int _limit;
        private readonly TimeSpan _timeWindow;

        public RateLimiterService(ApplicationDbContext context, int limit, TimeSpan timeWindow)
        {
            _context = context;
            _limit = limit;
            _timeWindow = timeWindow;
        }

        public bool IsRequestAllowed(string apiKey, string endpoint)
        {
            var now = DateTime.UtcNow;
            var startTime = now - _timeWindow;

            var requestCount = _context.ApiRequests
                .Count(r => r.ApiKey == apiKey && r.Endpoint == endpoint && r.Timestamp >= startTime);

            if (requestCount < _limit)
            {
                // Log the request
                _context.ApiRequests.Add(new ApiRequest
                {
                    Endpoint = endpoint,
                    Timestamp = now,
                    ApiKey = apiKey
                });
                _context.SaveChanges();
                return true; // Request is allowed
            }

            return false; // Rate limit exceeded
        }
    }
}
