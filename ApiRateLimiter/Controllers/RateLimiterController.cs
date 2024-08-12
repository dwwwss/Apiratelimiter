using Microsoft.AspNetCore.Mvc;
using ApiRateLimiter.Models;
using ApiRateLimiter.Services;

namespace ApiRateLimiter.Controllers
{
    public class RateLimiterController : Controller
    {
        private readonly RateLimiterService _rateLimiterService;

        public RateLimiterController(ApplicationDbContext context)
        {
            // सेटिंग: 5 अनुरोध प्रति मिनट
            _rateLimiterService = new RateLimiterService(context, 5, TimeSpan.FromMinutes(1));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogRequest(string apiKey, string endpoint)
        {
            if (_rateLimiterService.IsRequestAllowed(apiKey, endpoint))
            {
                return Ok("Request allowed.");
            }
            return StatusCode(429, "Rate limit exceeded.");
        }
    }
}
