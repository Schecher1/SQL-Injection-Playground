using Microsoft.EntityFrameworkCore;

namespace SQL_Injection_Playground.Class
{
    public class DbContext_EF : DbContext
    {
        //Table
#pragma warning disable CS8618 // A Non-Nullable field must contain a value other than NULL when the constructor exits. Consider declaring it as Nullable.
        public DbSet<UserModel> User { get; set; }
#pragma warning restore CS8618 // A Non-Nullable field must contain a value other than NULL when the constructor exits. Consider declaring it as Nullable.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(EnvironmentVariable.CONNECTION_STRING);
        }
    }
}
