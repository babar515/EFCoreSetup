using Microsoft.EntityFrameworkCore;

namespace EFCoreSetupApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }   
    }
}
