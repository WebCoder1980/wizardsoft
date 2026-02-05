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
                    new TreeNode { Id = 1, Name = "Главная страница Wizardsoft" },
                    new TreeNode { Id = 2, Name = "О компании", ParentId = 1 },
                    new TreeNode { Id = 3, Name = "Новости", ParentId = 2 },
                    new TreeNode { Id = 4, Name = "Еженедельный бюллетень «Ассистент строителя»", ParentId = 3 },
                    new TreeNode { Id = 5, Name = "Изменения в сметно-нормативной базе ПП SmetaWIZARD", ParentId = 3 },
                    new TreeNode { Id = 6, Name = "Минстрой РФ опубликовал дополнения к индексам изменения сметной стоимости строительства", ParentId = 3 },
                    
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
