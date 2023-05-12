using Backend_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_APIs
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ModelContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ModelContext>>()))
            {
                // Look for any movies.
                if (context.Courses.Any())
                {
                    return;   // DB has been seeded
                }

               /* context.Courses.AddRange(
                    new CourseModel
                    {
                        //OnClassLesson = 16,
                        //OnClassPractice = 4,
                        //Trainer = "Đặng Nhật Hoàng",
                        Title = "Java Spring fundamental.",
                        //Prerequisite = 2,
                        //CourseTarget = "Java Spring cơ bản.",
                        StartDate = DateTime.Parse("2022-2-12"),
                        EndDate = DateTime.Parse("2022-2-12"),
                        Description = "sdfsfsdsdfsdfsd"
                    }
                );

                
                context.SaveChanges();*/
            }
        }
    }
}
