using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Domain.Config;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Domain.Data
{
    public partial class UniversalAuthDbContext: DbContext
    {
        public UniversalAuthDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<ApplicationUserRole> UserRoles { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public virtual DbSet<ValidationToken> ValidationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());

            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());

            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

            modelBuilder.ApplyConfiguration(new SecurityQuestionConfiguration());

            modelBuilder.ApplyConfiguration(new SystemConfigConfiguration());
        }

    }
}
