#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityApplication.Models;

namespace UniversityApplication.Data
{
    public class UniversityApplicationContext : DbContext
    {
        public UniversityApplicationContext (DbContextOptions<UniversityApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<UniversityApplication.Models.Course> Course { get; set; }

        public DbSet<UniversityApplication.Models.Student> Student { get; set; }

        public DbSet<UniversityApplication.Models.Enrollment> Enrollment { get; set; }

        public DbSet<UniversityApplication.Models.Teacher> Teacher { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Enrollment>()
            .HasOne<Student>(p => p.student)
            .WithMany(p => p.Courses)
            .HasForeignKey(p => p.studentId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Enrollment>()
            .HasOne<Course>(p => p.course)
            .WithMany(p => p.Students)
            .HasForeignKey(p => p.courseId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Course>()
            .HasOne<Teacher>(p => p.firstTeacher)
            .WithMany(p => p.Courses1)
            .HasForeignKey(p => p.firstTeacherId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Course>()
           .HasOne<Teacher>(p => p.secondTeacher)
           .WithMany(p => p.Courses2)
           .HasForeignKey(p => p.secondTeacherId);
            //.HasPrincipalKey(p => p.Id);
        }

    }
}
