using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskPlanner.Entity
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using System.Reflection;

    public partial class TaskPlannerEntities : DbContext
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        public static string ConnectionString { get; set; }

        public TaskPlannerEntities(DbContextOptions<TaskPlannerEntities> options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskPlannerEntities"/> class.
        /// constructor for the class
        /// </summary>
        public TaskPlannerEntities()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CreatedBy) 
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCreatedBy");

                entity.HasOne(d => d.AspNetUser1)
                    .WithMany(p => p.Projects1)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectOwner");
            });

            builder.Entity<Story>(entity =>
            {
                entity.HasKey(e => e.StoryId);


                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoryCreatedBy");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoryProjectId");

                entity.HasOne(d => d.Epic)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.EpicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoryEpicId");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoryThemeId");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoryPriorityId");
            });

            builder.Entity<Epic>(entity =>
            {
                entity.HasKey(e => e.EpicId);


                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Epics)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EpicCreatedBy");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Epics)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EpicProjectId");
            });

            builder.Entity<Favourite>(entity =>
            {
                entity.HasKey(e => e.FavouriteId);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteProjectId");
            });

            builder.Entity<Priority>(entity =>
            {
                entity.HasKey(e => e.PriorityId);



                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Priorities)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriorityCreatedBy");
            });
            builder.Entity<ProjectPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId);


                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectPermissions)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPermissionProjectId");
            });

            builder.Entity<Theme>(entity =>
            {
                entity.HasKey(e => e.ThemeId);


                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Themes)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThemesCreatedBy");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Themes)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThemesProjectId");
            });
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<Epic> Epics { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ProjectPermission> ProjectPermissions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<TaskPlannerEntities>
    {
        public TaskPlannerEntities CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TaskPlannerEntities>(); builder.UseNpgsql(TaskPlannerEntities.ConnectionString,
             optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TaskPlannerEntities).GetTypeInfo().Assembly.GetName().Name));

            return new TaskPlannerEntities(builder.Options);
        }
    }
}