using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class adpq2adpqContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseNpgsql(@"Host=ec2-52-53-195-63.us-west-1.compute.amazonaws.com;Database=adpq2adpq;Username=adpq;Password=adpq2adpq");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address1)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);

                entity.Property(e => e.Address2)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);

                entity.Property(e => e.City)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Zipcode)
                    .HasColumnType("varchar")
                    .HasMaxLength(25);

                entity.Property(e => e.PasswordSalt)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);
            });
        }
    }
}