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
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SystemConfiguration}"/>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SystemConfiguration}"/>
    public class SystemConfigConfiguration : IEntityTypeConfiguration<SystemConfiguration>
    {
        public void Configure(EntityTypeBuilder<SystemConfiguration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.MaximumPasswordTries).IsRequired();

            builder.Property(x => x.TempPasswordExpirationPeriod).IsRequired();

        }
    }
}
