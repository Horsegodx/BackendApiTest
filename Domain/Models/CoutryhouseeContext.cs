using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class CoutryhouseeContext : DbContext
    {
        public CoutryhouseeContext()
        {
        }

        public CoutryhouseeContext(DbContextOptions<CoutryhouseeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<FeedingSchedule> FeedingSchedules { get; set; } = null!;
        public virtual DbSet<Fertilization> Fertilizations { get; set; } = null!;
        public virtual DbSet<Harvest> Harvests { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<Poll> Polls { get; set; } = null!;
        public virtual DbSet<PollAnswer> PollAnswers { get; set; } = null!;
        public virtual DbSet<PollOption> PollOptions { get; set; } = null!;
        public virtual DbSet<ProductInShop> ProductInShops { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<Snt> Snts { get; set; } = null!;
        public virtual DbSet<SntEvent> SntEvents { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAnimal> UserAnimals { get; set; } = null!;
        public virtual DbSet<UserPlant> UserPlants { get; set; } = null!;
        public virtual DbSet<UserShop> UserShops { get; set; } = null!;
        public virtual DbSet<WateringSchedule> WateringSchedules { get; set; } = null!;

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.AnimalsId)
                    .HasName("PK__Animals__9DF80BFFC9C87EB7");

                entity.Property(e => e.AnimalsId).HasColumnName("animals_id");

                entity.Property(e => e.AnimalBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("animal_birth_date");

                entity.Property(e => e.AnimalName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("animal_name")
                    .IsFixedLength();

                entity.Property(e => e.AnimalType)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("animal_type")
                    .IsFixedLength();
            });

            modelBuilder.Entity<FeedingSchedule>(entity =>
            {
                entity.HasKey(e => e.FeedingId)
                    .HasName("PK__Feeding___6DA499DC68F51064");

                entity.ToTable("Feeding_schedule");

                entity.Property(e => e.FeedingId).HasColumnName("feeding_id");

                entity.Property(e => e.AnimalsId).HasColumnName("animals_id");

                entity.Property(e => e.FeedingDate)
                    .HasColumnType("date")
                    .HasColumnName("feeding_date");

                entity.Property(e => e.FeedingTime).HasColumnName("feeding_time");

                entity.Property(e => e.FeedingType)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("feeding_type")
                    .IsFixedLength();

                entity.HasOne(d => d.Animals)
                    .WithMany(p => p.FeedingSchedules)
                    .HasForeignKey(d => d.AnimalsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feeding_s__anima__5441852A");
            });

            modelBuilder.Entity<Fertilization>(entity =>
            {
                entity.ToTable("Fertilization");

                entity.Property(e => e.FertilizationId).HasColumnName("fertilization_id");

                entity.Property(e => e.FertilizationDate)
                    .HasColumnType("date")
                    .HasColumnName("fertilization_date");

                entity.Property(e => e.FertilizerName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("fertilizer_name")
                    .IsFixedLength();

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Fertiliza__plant__2F10007B");
            });

            modelBuilder.Entity<Harvest>(entity =>
            {
                entity.ToTable("Harvest");

                entity.Property(e => e.HarvestId).HasColumnName("harvest_id");

                entity.Property(e => e.HarvestDate)
                    .HasColumnType("date")
                    .HasColumnName("harvest_date");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Harvests)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Harvest__plant_i__31EC6D26");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.MessageDate)
                    .HasColumnType("date")
                    .HasColumnName("message_date");

                entity.Property(e => e.MessageText)
                    .HasColumnType("text")
                    .HasColumnName("message_text");

                entity.Property(e => e.MessageTime).HasColumnName("message_time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__user_id__403A8C7D");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.NewsDate)
                    .HasColumnType("date")
                    .HasColumnName("news_date");

                entity.Property(e => e.NewsText)
                    .HasColumnType("text")
                    .HasColumnName("news_text");

                entity.Property(e => e.NewsTitle)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("news_title")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__News__user_id__3D5E1FD2");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentsId)
                    .HasName("PK__Payments__C71B310F53909D3D");

                entity.Property(e => e.PaymentsId).HasColumnName("payments_id");

                entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("payment_type")
                    .IsFixedLength();

                entity.Property(e => e.PenaltyAmount).HasColumnName("penalty_amount");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments__user_i__4316F928");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToTable("Plant");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.PlantName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("plant_name")
                    .IsFixedLength();

                entity.Property(e => e.PlantType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("plant_type")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                entity.ToTable("Poll");

                entity.Property(e => e.PollId).HasColumnName("poll_id");

                entity.Property(e => e.PollDate)
                    .HasColumnType("date")
                    .HasColumnName("poll_date");

                entity.Property(e => e.PollQuestion)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("poll_question")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PollAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK__PollAnsw__3372431832505C10");

                entity.ToTable("PollAnswer");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.PollAnswers)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PollAnswe__optio__3A81B327");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PollAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PollAnswe__user___398D8EEE");
            });

            modelBuilder.Entity<PollOption>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__PollOpti__F4EACE1B4E2B0475");

                entity.ToTable("PollOption");

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.OptionText)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("option_text")
                    .IsFixedLength();

                entity.Property(e => e.PollId).HasColumnName("poll_id");

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.PollOptions)
                    .HasForeignKey(d => d.PollId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PollOptio__poll___36B12243");
            });

            modelBuilder.Entity<ProductInShop>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Product___47027DF5D760F7C6");

                entity.ToTable("Product_in_shop");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("product_name")
                    .IsFixedLength();

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ProductInShops)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_i__shop___47DBAE45");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.City)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("city")
                    .IsFixedLength();

                entity.Property(e => e.HouseNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("house_number")
                    .IsFixedLength();

                entity.Property(e => e.ShopName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("shop_name")
                    .IsFixedLength();

                entity.Property(e => e.Street)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("street")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Snt>(entity =>
            {
                entity.ToTable("SNT");

                entity.Property(e => e.SntId).HasColumnName("snt_id");

                entity.Property(e => e.City)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("city")
                    .IsFixedLength();

                entity.Property(e => e.HouseNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("house_number")
                    .IsFixedLength();

                entity.Property(e => e.ManagerFirstName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("manager_first_name")
                    .IsFixedLength();

                entity.Property(e => e.ManagerLastName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("manager_last_name")
                    .IsFixedLength();

                entity.Property(e => e.ManagerPhone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("manager_phone")
                    .IsFixedLength();

                entity.Property(e => e.SntName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("snt_name")
                    .IsFixedLength();

                entity.Property(e => e.Street)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("street")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Snts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SNT__user_id__571DF1D5");
            });

            modelBuilder.Entity<SntEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__SNT_even__2370F72787641CB7");

                entity.ToTable("SNT_event");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("event_date");

                entity.Property(e => e.EventLocation)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("event_location")
                    .IsFixedLength();

                entity.Property(e => e.EventName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("event_name")
                    .IsFixedLength();

                entity.Property(e => e.SntId).HasColumnName("snt_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Snt)
                    .WithMany(p => p.SntEvents)
                    .HasForeignKey(d => d.SntId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SNT_event__snt_i__59FA5E80");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SntEvents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SNT_event__user___5AEE82B9");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasColumnName("first_name")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasColumnName("last_name")
                    .IsFixedLength();

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasColumnName("middle_name")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserAnimal>(entity =>
            {
                entity.Property(e => e.UserAnimalId).HasColumnName("user_animal_id");

                entity.Property(e => e.AnimalsId).HasColumnName("animals_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Animals)
                    .WithMany(p => p.UserAnimals)
                    .HasForeignKey(d => d.AnimalsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAnima__anima__5165187F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnimals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAnima__user___5070F446");
            });

            modelBuilder.Entity<UserPlant>(entity =>
            {
                entity.Property(e => e.UserPlantId).HasColumnName("user_plant_id");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.UserPlants)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserPlant__plant__29572725");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserPlant__user___286302EC");
            });

            modelBuilder.Entity<UserShop>(entity =>
            {
                entity.Property(e => e.UserShopId).HasColumnName("user_shop_id");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.UserShops)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserShops__shop___4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserShops)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserShops__user___4AB81AF0");
            });

            modelBuilder.Entity<WateringSchedule>(entity =>
            {
                entity.ToTable("WateringSchedule");

                entity.Property(e => e.WateringScheduleId).HasColumnName("watering_schedule_id");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.WateringDate)
                    .HasColumnType("date")
                    .HasColumnName("watering_date");

                entity.Property(e => e.WateringTime).HasColumnName("watering_time");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.WateringSchedules)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WateringS__plant__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
