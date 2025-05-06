using AiLingua.Core.Entities;
using AiLingua.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks; 

namespace AiLingua.Repository.Data
{
    public class StoreContext: IdentityDbContext<User>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Review> Reviews { get; set; }
   //     public DbSet<Subscription> Subscriptions { get; set; }
   //     public DbSet<CourseVideo> CourseVideos { get; set; }
    
        public DbSet<AvailableTimeSlot> AvailableTimeSlots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Student", NormalizedName = "STUDENT" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" }

    );
     /*       modelBuilder.Entity<User>()
       .Property(u => u.Id)
       .ValueGeneratedOnAdd();
            // Configure User-Subscription One-to-One
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithOne(s => s.Student)
                .HasForeignKey<Subscription>(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);*/

            // Configure User-Review One-to-Many
         /*   modelBuilder.Entity<User>()
                .HasMany(u => u.ReviewsGiven)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
         */
            // Configure Course-User One-to-Many
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CreatedCourses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Course-CourseVideo One-to-Many
       /*     modelBuilder.Entity<CourseVideo>()
                .HasOne(cv => cv.Course)
                .WithMany(c => c.CourseVideos)
                .HasForeignKey(cv => cv.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Course-Review One-to-Many
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Course-Lesson One-to-Many
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);*/

            // Configure PlacementTest-User One-to-One
          /*  modelBuilder.Entity<PlacementTest>()
                .HasOne(pt => pt.User)
                .WithMany()
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Gamification-User One-to-One
            modelBuilder.Entity<Gamification>()
                .HasOne(g => g.User)
                .WithMany()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlacementTest>(entity =>
            {

                entity.HasOne(pt => pt.User) // Foreign key to User
                      .WithMany()
                      .HasForeignKey(pt => pt.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });*/
         
         /*   modelBuilder.Entity<PlacementTestQuestion>(entity =>
            {

                entity.HasOne(ptq => ptq.PlacementTest) // Foreign key to PlacementTest
                      .WithMany(pt => pt.PlacementTestQuestions)
                      .HasForeignKey(ptq => ptq.PlacementTestId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ptq => ptq.Question) // Foreign key to Question
                      .WithMany(q => q.PlacementTestQuestions)
                      .HasForeignKey(ptq => ptq.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });*/
            modelBuilder.Entity<Course>().Property(c => c.CourseVideos)
             .HasConversion(
                 v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                 v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
             )
             .HasColumnType("nvarchar(max)"); // Use nvarchar(max) for SQL Server (or jsonb for PostgreSQL)

            modelBuilder.Entity<Course>().Property(c => c.Slides)
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                   )
                   .HasColumnType("nvarchar(max)");
     

            modelBuilder.Entity<Review>()
     .HasOne(r => r.Student)
     .WithMany(s => s.ReviewsGiven)
     .HasForeignKey(r => r.StudentId)
     .OnDelete(DeleteBehavior.Cascade);

            // 🔗 One Teacher -> Many Reviews Received
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Teacher)
                .WithMany(t => t.ReceivedReviews)
                .HasForeignKey(r => r.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

        /*    modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Student)
                .WithOne(u => u.Subscription)
                .HasForeignKey<Subscription>(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);*/
            modelBuilder.Entity<AvailableTimeSlot>()
    .HasOne(a => a.Teacher)
    .WithMany(t => t.AvailableTimeSlots)
    .HasForeignKey(a => a.TeacherId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvailableTimeSlot>()
       .HasOne(ts => ts.Teacher)
       .WithMany(t => t.AvailableTimeSlots)
       .HasForeignKey(ts => ts.TeacherId)
       .OnDelete(DeleteBehavior.Cascade); // Deleting a teacher deletes their slots

                  modelBuilder.Entity<Lesson>()
                      .HasOne(l => l.Student)
                      .WithMany(s => s.BookedLessons)
                      .HasForeignKey(l => l.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
         /*   modelBuilder.Entity<LessonStudent>()
          .HasKey(ls => new { ls.LessonId, ls.StudentId });

            modelBuilder.Entity<LessonStudent>()
                .HasOne(ls => ls.Lesson)
                .WithMany(l => l.LessonStudents)
                .HasForeignKey(ls => ls.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonStudent>()
                .HasOne(ls => ls.Student)
                .WithMany(s => s.LessonStudents)
                .HasForeignKey(ls => ls.StudentId)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)
                .WithMany(t => t.ScheduledLessons)
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.AvailableTimeSlot)
                .WithMany(ts => ts.Lessons) 
                .HasForeignKey(l => l.AvailableTimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);
            /*    modelBuilder.Entity<User>()
          .Property(t => t.PreferredLevels)
          .HasConversion(
              v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
              v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
          )
          .HasColumnType("nvarchar(max)");*/

            modelBuilder.Entity<User>()
                .Property(t => t.Interests)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                )
                .HasColumnType("nvarchar(max)");

        }
     

    }
}
