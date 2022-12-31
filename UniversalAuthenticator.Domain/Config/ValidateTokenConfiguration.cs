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
    public class ValidateTokenConfiguration : IEntityTypeConfiguration<ValidationToken>
    {
        public void Configure(EntityTypeBuilder<ValidationToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Token).IsRequired().HasMaxLength(250);

            builder.Property(x => x.TokenType).IsRequired().HasMaxLength(50);

            builder.Property(x => x.DateGenerated).IsRequired();

            builder.Property(x => x.ExpiryDate).IsRequired();

            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
