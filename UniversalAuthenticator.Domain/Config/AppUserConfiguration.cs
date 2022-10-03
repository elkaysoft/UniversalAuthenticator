using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Domain.Config
{
    /// <summary>
    /// Class System Config Configuration, allows configurations to be factored on a separate class rather than inline
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{ApplicationUser}"/>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{ApplicationUser}"/>

    public class AppUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedBy).IsRequired();

            builder.Property(x => x.DateCreated).IsRequired();

            builder.Property(x => x.Email).IsRequired();
            
            builder.Property(x => x.PhoneNumber).IsRequired();

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            
            builder.Property(x => x.OtherName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.SaltedHashedPassword).IsRequired();

            builder.Property(x => x.Gender).IsRequired();

            builder.HasOne(u => u.UserRole)
                .WithOne(ur => ur.User)
                .HasForeignKey<ApplicationUserRole>(au => au.ApplicationUserId);    
                        
        }
    }
}
