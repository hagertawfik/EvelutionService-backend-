using EvelutionServiceReceiver.Data_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvelutionServiceReceiver.DataContextEvelution
{
    public class DataContextEvelution1 : IdentityDbContext<Student>
    { 
        

            public DataContextEvelution1(DbContextOptions<DataContextEvelution1> options) : base(options)
            {

            }
            public DbSet<Exam> Exams { get; set; }
            public DbSet<Choices> Choices { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<ExamResult> ExamResults { get; set; }
            public DbSet<ExamQuestion> ExamQuestions { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // ExamQuestion many to many relation
                modelBuilder.Entity<ExamQuestion>()
                      .HasKey(pc => new { pc.ExamId, pc.QuestionId });
                modelBuilder.Entity<ExamQuestion>()
                    .HasOne(p => p.Exam)
                    .WithMany(pc => pc.ExamQuestions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(p => p.ExamId);


                modelBuilder.Entity<ExamQuestion>()
                   .HasOne(p => p.Question)
                   .WithMany(pc => pc.ExamQuestions)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasForeignKey(c => c.QuestionId);



                // one to many relation question and Choices
                modelBuilder.Entity<Question>()
                 .HasMany(s => s.Choices)            // question has many Choices
                 .WithOne(e => e.Question)          // Choices has one question
                 .HasForeignKey(e => e.QuestionId); // Define the foreign key property

                // one to many relation Exam and Result
                modelBuilder.Entity<Exam>()
                 .HasMany(s => s.ExamResults)            // Exam has many Result
                 .WithOne(e => e.Exam)          // Result has one Exam
                 .HasForeignKey(e => e.ExamId); // Define the foreign key property


                // one to many relation user and Result
                modelBuilder.Entity<Student>()
                 .HasMany(s => s.ExamResults)            // user has many Result
                 .WithOne(e => e.Student)          // Result has one user
                 .HasForeignKey(e => e.UserId); // Define the foreign key property

                base.OnModelCreating(modelBuilder);
            }
        }
    }


