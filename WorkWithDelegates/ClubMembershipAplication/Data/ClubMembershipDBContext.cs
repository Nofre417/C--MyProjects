using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClubMembershipAplication.Data
{
    public class ClubMembershipDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClubMembershipDb.db");
            base.OnConfiguring(optionsBuilder); 
        }

        public DbSet<User> Users { get; set; }
    }
}
