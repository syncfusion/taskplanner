using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskPlanner.Entity
{    
    using System;
    using Microsoft.EntityFrameworkCore;

    public partial class TaskPlannerEntities : DbContext
    {
        public TaskPlannerEntities(DbContextOptions<TaskPlannerEntities> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
         .Property(e => e.Email)
         .IsUnicode(false);

            modelBuilder.Entity<Project>()
         .Property(e => e.ProjectName)
         .IsUnicode(false);

            modelBuilder.Entity<Theme>()
         .Property(e => e.ThemeName)
         .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Themes)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Epics)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Favourites)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Priorities)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);


            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Stories)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);


            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectPermissions)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Project>()
               .HasMany(e => e.Favourites)
               .WithOne(e => e.Project)
               .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Project>()
               .HasMany(e => e.Themes)
               .WithOne(e => e.Project)
               .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Project>()
               .HasMany(e => e.Epics)
               .WithOne(e => e.Project)
               .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Project>()
               .HasMany(e => e.Stories)
               .WithOne(e => e.Project)
               .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<Theme>()
               .HasMany(e => e.Stories)
               .WithOne(e => e.Theme)
               .HasForeignKey(e => e.ThemeId);

            modelBuilder.Entity<Epic>()
   .HasMany(e => e.Stories)
   .WithOne(e => e.Epic)
   .HasForeignKey(e => e.EpicId);

            modelBuilder.Entity<Priority>()
   .HasMany(e => e.Stories)
   .WithOne(e => e.Priority)
   .HasForeignKey(e => e.PriorityId);
        }

        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Epic> Epics { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ProjectPermission> ProjectPermissions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
    }
}
