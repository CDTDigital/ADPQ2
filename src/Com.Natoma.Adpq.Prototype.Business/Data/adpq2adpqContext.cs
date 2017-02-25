using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class adpq2adpqContext : DbContext
    {
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }

        public adpq2adpqContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).ValueGeneratedOnAdd();

                entity.Property(e => e.Address1)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Address2)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.EmailMessage)
                    .HasColumnType("varchar")
                    .HasMaxLength(5000);

                entity.Property(e => e.EmailSubject)
                    .HasColumnType("varchar")
                    .HasMaxLength(250);

                entity.Property(e => e.SmsMessage)
                    .HasColumnType("varchar")
                    .HasMaxLength(450);

                entity.Property(e => e.State)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");

                entity.Property(e => e.Zipcode)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.HasOne(d => d.NotificationNavigation)
                    .WithOne(p => p.Notification)
                    .HasForeignKey<Notification>(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notification_notificationType");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
            });

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

                entity.Property(e => e.CreatedOn).HasColumnType("date");

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

                entity.Property(e => e.PasswordSalt)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");

                entity.Property(e => e.Zipcode)
                    .HasColumnType("varchar")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.HasIndex(e => e.NotificationId)
                    .HasName("fki_fk_notification_userNotification");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_fk_user_userNotification");

                entity.Property(e => e.NotificationDate).HasColumnType("date");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notification_userNotification");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_user_userNotification");
            });
        }
    }
}