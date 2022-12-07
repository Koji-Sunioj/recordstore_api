using Microsoft.EntityFrameworkCore;

namespace api;

public partial class RecordStoreContext : DbContext
{
    public RecordStoreContext()
    {
    }

    public RecordStoreContext(DbContextOptions<RecordStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=my_user;Password=root;Database=record_store");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("albums_pkey");

            entity.ToTable("albums");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.Artist)
                .HasColumnType("character varying")
                .HasColumnName("artist");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
