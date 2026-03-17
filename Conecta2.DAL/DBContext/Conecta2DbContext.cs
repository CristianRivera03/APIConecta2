using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Conecta2.Model;

namespace Conecta2.DAL.DBContext;

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

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolemodule> Rolemodules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

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

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modules_pkey");

            entity.ToTable("modules");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Path)
                .HasMaxLength(150)
                .HasColumnName("path");
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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.NameRol, "role_name_rol_key").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.NameRol)
                .HasMaxLength(100)
                .HasColumnName("name_rol");
        });

        modelBuilder.Entity<Rolemodule>(entity =>
        {
            entity.HasKey(e => new { e.Roleid, e.Moduleid }).HasName("rolemodules_pkey");

            entity.ToTable("rolemodules");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Moduleid).HasColumnName("moduleid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");

            entity.HasOne(d => d.Module).WithMany(p => p.Rolemodules)
                .HasForeignKey(d => d.Moduleid)
                .HasConstraintName("fk_rolemodules_modules");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolemodules)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("fk_rolemodules_roles");
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
            entity.Property(e => e.IdRole)
                .HasDefaultValue(3)
                .HasColumnName("id_role");
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

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
