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

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskPlannerEntities"/> class.
        /// constructor for the class
        /// </summary>
        public TaskPlannerEntities()
        {
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Configure the details
        /// </summary>
        /// <param name="optionsBuilder">options builder </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
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

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Epic> Epics { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ProjectPermission> ProjectPermissions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
    }
}
