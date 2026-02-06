using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using wizardsoft_testtask.Constants;
using wizardsoft_testtask.Models;
using wizardsoft_testtask.Service.Auth;

namespace wizardsoft_testtask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreeNode>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    // TODO: решить проблему с кодировкой, что бы не пришлось экранировать строки с русскими символами. Через REST API русские символы нормально работают. При ошибках валидации ошибки на русском тоже работают. Но я не смог понять почему тут не работает.
                    new TreeNode { Id = 1, Name = "\u0413\u043B\u0430\u0432\u043D\u0430\u044F\u0020\u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0430\u0020\u0057\u0069\u007A\u0061\u0072\u0064\u0073\u006F\u0066\u0074" },
                    new TreeNode { Id = 2, Name = "\u041E\u0020\u043A\u043E\u043C\u043F\u0430\u043D\u0438\u0438", ParentId = 1 },
                    new TreeNode { Id = 3, Name = "\u041D\u043E\u0432\u043E\u0441\u0442\u0438", ParentId = 2 },
                    new TreeNode { Id = 4, Name = "\u0415\u0436\u0435\u043D\u0435\u0434\u0435\u043B\u044C\u043D\u044B\u0439\u0020\u0431\u044E\u043B\u043B\u0435\u0442\u0435\u043D\u044C\u0020\u00AB\u0410\u0441\u0441\u0438\u0441\u0442\u0435\u043D\u0442\u0020\u0441\u0442\u0440\u043E\u0438\u0442\u0435\u043B\u044F\u00BB", ParentId = 3 },
                    new TreeNode { Id = 5, Name = "\u0418\u0437\u043C\u0435\u043D\u0435\u043D\u0438\u044F\u0020\u0432\u0020\u0441\u043C\u0435\u0442\u043D\u043E\u002D\u043D\u043E\u0440\u043C\u0430\u0442\u0438\u0432\u043D\u043E\u0439\u0020\u0431\u0430\u0437\u0435\u0020\u041F\u041F\u0020\u0053\u006D\u0065\u0074\u0061\u0057\u0049\u005A\u0041\u0052\u0044", ParentId = 3 },
                    new TreeNode { Id = 6, Name = "\u041C\u0438\u043D\u0441\u0442\u0440\u043E\u0439\u0020\u0420\u0424\u0020\u043E\u043F\u0443\u0431\u043B\u0438\u043A\u043E\u0432\u0430\u043B\u0020\u0434\u043E\u043F\u043E\u043B\u043D\u0435\u043D\u0438\u044F\u0020\u043A\u0020\u0438\u043D\u0434\u0435\u043A\u0441\u0430\u043C\u0020\u0438\u0437\u043C\u0435\u043D\u0435\u043D\u0438\u044F\u0020\u0441\u043C\u0435\u0442\u043D\u043E\u0439\u0020\u0441\u0442\u043E\u0438\u043C\u043E\u0441\u0442\u0438\u0020\u0441\u0442\u0440\u043E\u0438\u0442\u0435\u043B\u044C\u0441\u0442\u0432\u0430", ParentId = 3 },

                    new TreeNode { Id = 7, Name = "IT" },
                    new TreeNode { Id = 8, Name = "C#", ParentId = 7 },
                    new TreeNode { Id = 9, Name = "Java", ParentId = 7 },
                    new TreeNode { Id = 10, Name = "Go", ParentId = 7 },
                    new TreeNode { Id = 11, Name = "ASP.NET", ParentId = 8 },
                    new TreeNode { Id = 12, Name = "Entity Framework", ParentId = 8 }
                );
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.Role).IsRequired().HasMaxLength(50);

                entity.HasData(
                    new User() { Id = 1, UserName = "admin", PasswordHash = AuthUtil.HashPassword("admin_password"), Role = AppRoles.ADMIN },
                    new User() { Id = 2, UserName = "maxsmg", PasswordHash = AuthUtil.HashPassword("qweqwe"), Role = AppRoles.USER }
                );
            });
        }
    }
}
