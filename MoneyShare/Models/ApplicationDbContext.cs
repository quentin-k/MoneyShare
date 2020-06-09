using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyStore.Models;

namespace MoneyStore.Data
{
    public class WListContext : IdentityDbContext
    {
        public WListContext (DbContextOptions<WListContext> options)
            : base(options)
        {
        }
        public DbSet<MemberModel> MemberModels { get; set; }
    }
}