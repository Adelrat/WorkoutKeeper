using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayExercise>()
            .HasKey(t => new { t.DayId, t.ExerciseId });

            modelBuilder.Entity<DayExercise>()
            .HasOne(sc => sc.Day)
            .WithMany(s => s.DayExercise)
            .HasForeignKey(sc => sc.DayId);

            modelBuilder.Entity<DayExercise>()
            .HasOne(sc => sc.Exercise)
            .WithMany(c => c.DayExercise)
            .HasForeignKey(sc => sc.ExerciseId);
        }
        public DbSet<Day> Days { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        //old
        //modelBuilder.Entity<TrainingExercise>()
        //.HasKey(t => new { t.TrainingId, t.ExerciseId });

        //modelBuilder.Entity<TrainingExercise>()
        //.HasOne(sc => sc.Traning)
        //.WithMany(s => s.TrainingExercise)
        //.HasForeignKey(sc => sc.TrainingId);

        //modelBuilder.Entity<TrainingExercise>()
        //.HasOne(sc => sc.Exercise)
        //.WithMany(c => c.TrainingExercise)
        //.HasForeignKey(sc => sc.ExerciseId);
    }
}
