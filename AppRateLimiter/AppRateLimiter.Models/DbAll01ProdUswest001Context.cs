using Microsoft.EntityFrameworkCore;

namespace AppRateLimiter.Models;

public partial class DbAll01ProdUswest001Context : DbContext
{
    private readonly string _connectionString;

    public DbAll01ProdUswest001Context()
    {
        _connectionString = Environment.GetEnvironmentVariable("cs-urlshortener") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public virtual DbSet<GeneratedKey> GeneratedKeys { get; set; }

    public virtual DbSet<UrlMapping> UrlMappings { get; set; }

    public virtual DbSet<UserBucket> UserBuckets { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneratedKey>(entity =>
        {
            entity.ToTable("generated_keys", "url_shortener");

            entity.Property(e => e.Id)
                .HasComment("pk incremental int")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.HashValue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hash_value");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
            entity.Property(e => e.UrlId).HasColumnName("url_id");
        });

        modelBuilder.Entity<UrlMapping>(entity =>
        {
            entity.ToTable("url_mapping", "url_shortener");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.HashValue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hash_value");
            entity.Property(e => e.KeyId).HasColumnName("key_id");
            entity.Property(e => e.LastAccessed).HasColumnName("last_accessed");
            entity.Property(e => e.LongUrl).HasColumnName("long_url");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<UserBucket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_app_rate_limiter_user_bucket");

            entity.ToTable("user_bucket", "app_rate_limiter");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BucketCount).HasColumnName("bucket_count");
            entity.Property(e => e.BucketLimit).HasColumnName("bucket_limit");
            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.LastAccessed).HasColumnName("last_accessed");
            entity.Property(e => e.RefillRateSeconds).HasColumnName("refill_rate_seconds");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("user_info", "url_shortener");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleInitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("middle_initial");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
