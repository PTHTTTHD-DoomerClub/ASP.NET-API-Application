using Backend_APIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Diagnostics;

namespace Backend_APIs
{
    public partial class ModelContext : DbContext
    {

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SkillModel> Skills { get; set; }
        public DbSet<PrerequisiteCourseReference> PrerequisiteCourses { get; set; }
        public DbSet<SkillTrainedByCourse> SkillsTrainedByCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SkillTrainedByCourse>().HasKey(nameof(SkillTrainedByCourse.CourseId), nameof(SkillTrainedByCourse.SkillId));
            modelBuilder.Entity<CourseModel>()
                    .HasMany(course => course.PrerequisiteCourses)
                    .WithOne(prerequire => prerequire.Course)
                    .HasForeignKey(prerequire => prerequire.CourseID);

            modelBuilder.Entity<CourseModel>()
                    .HasMany(course => course.TrainingSkills)
                    .WithOne(skill => skill.Course)
                    .HasForeignKey(skill => skill.CourseId);

            modelBuilder.Entity<PrerequisiteCourseReference>()
                .HasOne(e => e.Prerequisite).WithMany().HasForeignKey(e => e.PrerequisiteID);

        }
    }
}
