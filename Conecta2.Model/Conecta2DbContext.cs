using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Conecta2.Model;

public partial class Conecta2DbContext : DbContext
{
    public Conecta2DbContext()
    {
    }

    public Conecta2DbContext(DbContextOptions<Conecta2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Conecta2DB;Username=postgres;Password=pixel10");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.NameCategory, "categories_name_category_key").IsUnique();

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.IsEnable)
                .HasDefaultValue(true)
                .HasColumnName("is_enable");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(100)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.HasIndex(e => e.IdCategory, "idx_posts_category");

            entity.HasIndex(e => e.IdPost, "idx_posts_not_deleted").HasFilter("(is_deleted = false)");

            entity.HasIndex(e => e.IdUser, "idx_posts_user");

            entity.Property(e => e.IdPost)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_post");
            entity.Property(e => e.ContentPost).HasColumnName("content_post");
            entity.Property(e => e.DeleteAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("delete_at");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PublishedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("published_at");
            entity.Property(e => e.TitlePost)
                .HasMaxLength(255)
                .HasColumnName("title_post");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("posts_id_category_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("posts_id_user_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.IdUser)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id_user");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("create_at");
            entity.Property(e => e.DeleteAt).HasColumnName("delete_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastnameUser)
                .HasMaxLength(100)
                .HasColumnName("lastname_user");
            entity.Property(e => e.NameUser)
                .HasMaxLength(100)
                .HasColumnName("name_user");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
