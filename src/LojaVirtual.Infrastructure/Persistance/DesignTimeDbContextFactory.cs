using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LojaVirtual.Infrastructure.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LojaVirtualContext>
    {
        public LojaVirtualContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LojaVirtualContext>();
            
            // Usar MySQL para design time (migrations)
            var connectionString = "Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=root;Password=;SslMode=None;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));
            
            return new LojaVirtualContext(optionsBuilder.Options);
        }
    }
}
