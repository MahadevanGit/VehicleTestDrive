using Microsoft.EntityFrameworkCore;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.DBContext
{
    public class AccountDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {
        }
        protected AccountDBContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class AccountDBContextReadOnly : AccountDBContext
    {
        public AccountDBContextReadOnly(DbContextOptions<AccountDBContextReadOnly> options) : base(options)
        {
        }
    }
}
