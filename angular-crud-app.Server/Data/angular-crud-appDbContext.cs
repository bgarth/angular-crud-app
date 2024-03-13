using angular_crud_app.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace angular_crud_app.Server.Data
{
    public class Angular_crud_appDbContext: DbContext
    {
        public Angular_crud_appDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tutorial> Tutorials { get; set; }
    }
}
