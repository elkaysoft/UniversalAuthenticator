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
    /// Class RefreshToken Configuration, allows configurations to be factored on a separate class rather than inline
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RefreshToken}"/>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{RefreshToken}"/>


    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(50);

            builder.Property(x => x.DateCreated).IsRequired();

            builder.Property(x => x.CreatedByIp).IsRequired();

            builder.Property(x => x.Expiration).IsRequired();

            builder.Property(x => x.Token).IsRequired();

            

            builder.HasOne(u => u.User)
                .WithMany(r => r.refreshTokens)
                .HasForeignKey(r => r.UserId);
        }
    }
}
