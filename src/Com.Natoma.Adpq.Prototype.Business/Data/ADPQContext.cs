using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class ADPQContext : DbContext
    {
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessageType> MessageType { get; set; }
        public virtual DbSet<UserMessage> UserMessage { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseNpgsql(@"Host=localhost;Database=ADPQ;Username=adpq_web;Password=password");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.EventTypeId)
                    .HasName("fki_fk_event_eventtype");

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar")
                    .HasMaxLength(5000);

                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(500);

                entity.Property(e => e.OccurrenceDate).HasColumnType("date");

                entity.Property(e => e.SourceUrl)
                    .HasColumnType("varchar")
                    .HasMaxLength(500);

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("fk_event_eventtype");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.EventId)
                    .HasName("fki_fk_message_event");

                entity.Property(e => e.Body)
                    .HasColumnType("varchar")
                    .HasMaxLength(5000);

                entity.Property(e => e.DateSent).HasColumnType("date");

                entity.Property(e => e.Subject)
                    .HasColumnType("varchar")
                    .HasMaxLength(500);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_message_event");
            });

            modelBuilder.Entity<MessageType>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.HasIndex(e => e.MessageId)
                    .HasName("fki_fk_usermessage_message");

                entity.HasIndex(e => e.UserProfileid)
                    .HasName("fki_fk_userprofile_usermessage");

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.UserMessage)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("fk_usermessage_message");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.UserMessage)
                    .HasForeignKey(d => d.UserProfileid)
                    .HasConstraintName("fk_userprofile_usermessage");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.AddressLine2)
                    .HasColumnType("varchar")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                entity.Property(e => e.Zipcode)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

            });

            modelBuilder.HasSequence("Message_MessageId_seq");

            modelBuilder.HasSequence("UserProfile_UserProfileId_seq").StartsAt(2);
        }
    }
}