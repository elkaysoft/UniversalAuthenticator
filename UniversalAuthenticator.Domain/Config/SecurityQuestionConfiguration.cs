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
    /// Security QuestionConfiguration, allows configurations to be factored on a separate class rather than inline
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SecurityQuestion}"/>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{SecurityQuestion}"/>

    public class SecurityQuestionConfiguration : IEntityTypeConfiguration<SecurityQuestion>
    {
        public void Configure(EntityTypeBuilder<SecurityQuestion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
