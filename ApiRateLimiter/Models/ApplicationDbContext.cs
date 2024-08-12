using Microsoft.EntityFrameworkCore;

namespace ApiRateLimiter.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApiRequest> ApiRequests { get; set; }
   
    }
}
