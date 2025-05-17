using LearnNetCore.Basic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;

namespace LearnNetCore.Basic.DbContexts
{
    public class TestDbContext: DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options):base(options)
        {
            // 当跟踪的实体更改其状态时触发
            // ChangeTracker.StateChanged += AuditExtensions.UpdateTime;
            // 当上下文跟踪实体时触发
            // ChangeTracker.Tracked += AuditExtensions.UpdateTime;
        }
        public DbSet<BookEntity> Books { get; set; }
        // public DbSet<BookEntity> NewBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 只会在程序第一次访问EFCore时执行
            Console.WriteLine("OnModelCreating 被执行了");
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            // modelBuilder.Entity<BookEntity>().ToView("V_Books");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("OnConfiguring 被执行了");
            // 设置EFCore日志最低级别
            // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
            // 输出释放的EventId
            // optionsBuilder.LogTo(Console.WriteLine, new[] { CoreEventId.ContextDisposed });
            // optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name });
            optionsBuilder.LogTo(
            Console.WriteLine,
            (eventId, logLevel) => logLevel >= LogLevel.Information
                                   || eventId == RelationalEventId.ConnectionOpened
                                   || eventId == RelationalEventId.ConnectionClosed);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
