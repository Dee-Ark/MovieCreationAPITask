using Microsoft.EntityFrameworkCore;
using MovieCreationAPI.Model.Domain;

namespace MovieCreationAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Movie> movie { get; set; }
    }
}
