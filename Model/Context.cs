using DreamStuffApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamStuffApp.Models
{
    public class Context : DbContext
    {
        public DbSet<GoodsModel> Goods { get; set; }
        public DbSet<TypesModel> Types { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<StachModel> Staches { get; set; }
        public DbSet<LocalStachModel> LocalStaches { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventClientListModel> EventClients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseName = "DreamStuffDB.db";
            string databasePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, databaseName);
            optionsBuilder.UseSqlite($@"Data Source={databasePath}");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodsModel>(entity =>
            {
                entity.HasKey(k => k.Good_ID);
                entity.HasOne(t => t.Types).WithMany(o => o.Goods).HasForeignKey(k => k.TypeOf);
                entity.Property(k => k.Good_ID).HasColumnName("Good_ID");
                entity.Property(k => k.Name).HasColumnName("Name");
                entity.Property(k => k.TypeOf).HasColumnName("TypeOf");
                entity.Property(k => k.Price).HasColumnName("Price");
                entity.Property(k => k.Amount).HasColumnName("Amount");
                entity.Property(k => k.Weigth).HasColumnName("Weigth").HasColumnType("INTEGER");
                entity.Property(k => k.MadeIn).HasColumnName("MadeIn");
                entity.Property(k => k.Description).HasColumnName("Description");
                entity.Property(k => k.ImageURL).HasColumnName("ImageURL");
            });

            modelBuilder.Entity<TypesModel>(entity =>
            {
                entity.HasKey(k => k.Key);
                entity.Property(k => k.Key).HasColumnName("Key");
                entity.Property(k => k.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<ClientModel>(entity =>
            {
                entity.HasKey(k => k.PhoneNumber);
                entity.Property(p => p.Name).HasColumnName("Name");
                entity.Property(p => p.SecondName).HasColumnName("SecondName");
                entity.Property(p => p.SureName).HasColumnName("SureName");
                entity.Property(p => p.Points).HasColumnName("Points");
                entity.Property(p => p.RegDate).HasColumnName("RegDate").HasColumnType("DATE");
                entity.Property(p => p.Age).HasColumnName("Age");
                entity.Property(p => p.Email).HasColumnName("Email");
                entity.Property(p => p.Password).HasColumnName("Password");
            });

            modelBuilder.Entity<StachModel>(entity =>
            {
                entity.HasKey(k => k.StachID);
                entity.HasOne(t => t.Clients).WithMany(o => o.Stachs).HasForeignKey(k => k.ClientID);
                entity.Property(p => p.TotalPriceWithSell).HasColumnName("TotalPriceWithSell");
            });

            modelBuilder.Entity<LocalStachModel>(entity =>
            {
                entity.HasKey(bc => new { bc.StachID, bc.Goods_ID });

                entity.HasOne(t => t.Stachs).WithMany(o => o.LocalStachs).HasForeignKey(k => k.StachID);

                entity.HasOne(t => t.Goods).WithMany(o => o.LocalStachs).HasForeignKey(k => k.Goods_ID);

            });

            modelBuilder.Entity<EventModel>(entity =>
            {
                entity.HasKey(k => k.ID);
                entity.Property(k => k.Data).HasColumnType("DATE").HasPrecision(0);
            });

            modelBuilder.Entity<EventClientListModel>(entity =>
            {
                entity.HasKey(bc => new { bc.ClientID, bc.Event_ID });
                entity.HasOne(t => t.Clients_Navigation).WithMany(o => o.Events).HasForeignKey(k => k.ClientID);
                entity.HasOne(t => t.Events_Navigation).WithMany(o => o.ClientList).HasForeignKey(k => k.Event_ID);

            });
        }
    }
}
