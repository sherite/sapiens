namespace HorusDL.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions<BaseDatos> options) : base(options)
        { }

        public virtual DbSet<vwDynamicMenuSideBar> vwDynamicMenuSideBar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vwDynamicMenuSideBar>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                .HasColumnName("ID")
               .ValueGeneratedNever();
            });
        }
    }

    public class vwDynamicMenuSideBar
    {
        public int? ID { get; set; }
        public int? Modulo { get; set; }
        public int? Padre { get; set; }
        public int? Orden { get; set; }
        public int? Tipo { get; set; }
        public string Accion { get; set; }
        public string Texto { get; set; }
    }
}