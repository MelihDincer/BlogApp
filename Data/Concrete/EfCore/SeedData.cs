using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void FillTestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag{Text = "web programlama"},
                        new Tag{Text = "backend"},
                        new Tag{Text = "frontend"},
                        new Tag{Text = "fullstack"},
                        new Tag{Text = "php"}
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User{UserName="sadikturan"},
                        new User{UserName="melihdincer"}
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = "Asp.net core",
                            Content = "Asp.net core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            UserID = 1
                        },
                        new Post
                        {
                            Title = "PHP",
                            Content = "PHP dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            UserID = 1
                        },
                        new Post
                        {
                            Title = "Python",
                            Content = "Python dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            UserID = 1
                        }
                    );
                    context.SaveChanges();
                }
                
            }
        }
    }
}