using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyShare.Models;

namespace MoneyShare.Data
{
    public class MoneyShare : IdentityDbContext
    {
        public MoneyShare (DbContextOptions<MoneyShare> options)
            : base(options)
        {
        }
        public DbSet<MemberModel> MemberModels { get; set; }
    }
}