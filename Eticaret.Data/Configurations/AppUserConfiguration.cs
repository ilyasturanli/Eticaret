using Eticaret.Core.Entities;
using Eticaret.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
    
namespace Eticaret.Data.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //Appuser classının veritabanında olusucak olan tablosunun kolonlarının tiplerini ve özelliklerini belirliyoruz.
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            // bu kolon bos bırakılamaz.. not null ifadesi olur isrequired ile ve hascolumtype = Veritabanı türünü belirler..
            builder.Property(x => x.Surname).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired(false).HasColumnType("varchar(15)").HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);
            builder.Property(x => x.UserName).IsRequired(false).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "Admin",
                Email = "ilyas@gmail.com",
                IsActive = true,
                IsAdmin = true,
                Name = "Ilyas",
                Surname = "Turanli",
                Password = PasswordHelper.HashPassword("deneme123"),
                
            });

        }
    }
}
