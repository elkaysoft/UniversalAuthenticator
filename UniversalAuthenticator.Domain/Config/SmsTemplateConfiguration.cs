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
    public class SmsTemplateConfiguration : IEntityTypeConfiguration<SMSTemplate>
    {
        public void Configure(EntityTypeBuilder<SMSTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.Property(x => x.SMSType).IsRequired();

            builder.Property(x => x.Body).IsRequired();

            builder.Property(x => x.Subject).IsRequired();

            builder.Property(x => x.DateCreated).IsRequired();

            builder.Property(x => x.DateCreated).IsRequired();

            builder.Property(x => x.CreatedByIp).IsRequired();
        }
    }
}
