using System.Data.Entity;

namespace Computer_Accounting
{
    public class ComputerContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
    }
}