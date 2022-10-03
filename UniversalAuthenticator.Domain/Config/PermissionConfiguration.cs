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
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Permission}"/>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{Permission}"/>


    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedBy).IsRequired();

            builder.Property(x => x.DateCreated).IsRequired();

            builder.Property(x => x.CreatedByIp).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.ShortName).IsRequired();

            builder.Property(x => x.GroupName).IsRequired();
        }
    }
}
