using Crito.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crito.Context
{
    public class ContactFormContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ContactFormContext(DbContextOptions<ContactFormContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public required DbSet<ContactEntity> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<ContactEntity>(entity =>
            {
                entity.ToTable("contacts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ContactUmbracoKey).HasColumnName("contactUmbracoKey");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Message).HasColumnName("message");
            });


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("umbracoDbDSN"));
        }
    }
}
