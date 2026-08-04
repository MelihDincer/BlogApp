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
                        new Tag{Text = "web programlama", Url = "web-programlama", Color = TagColors.primary},
                        new Tag{Text = "backend", Url = "backend", Color = TagColors.danger},
                        new Tag{Text = "frontend", Url = "frontend", Color = TagColors.warning},
                        new Tag{Text = "fullstack", Url = "fullstack", Color = TagColors.success},
                        new Tag{Text = "php", Url = "php", Color = TagColors.secondary}
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User{UserName="sadikturan", Image="p1.jpg"},
                        new User{UserName="melihdincer", Image="p2.jpg"}
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
                            Url = "aspnet-core",
                            IsActive = true,
                            Image = "1.jpg",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            UserID = 1,
                            Comments = new List<Comment>
                            {
                                new Comment{Text="asp.net core çok güzel bir framework",
                                PublishedOn = DateTime.Now.AddHours(-5),
                                 UserID=2},
                                new Comment{Text="asp.net core ile web uygulamaları geliştirebilirsiniz", PublishedOn = DateTime.Now.AddHours(-3), UserID=1}
                            }
                        },
                        new Post
                        {
                            Title = "PHP",
                            Content = "PHP dersleri",
                            Url = "php",
                            IsActive = true,
                            Image = "2.jpg",
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            UserID = 1
                        },
                        new Post
                        {
                            Title = "Python",
                            Content = "Python dersleri",
                            Url = "python",
                            IsActive = true,
                            Image = "3.jpg",
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            UserID = 1
                        },
                        new Post
                        {
                            Title = "React",
                            Content = "React dersleri",
                            Url = "react",
                            IsActive = true,
                            Image = "3.jpg",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(4).ToList(),
                            UserID = 1
                        },
                        new Post
                        {
                            Title = "Angular",
                            Content = "Angular dersleri",
                            Url = "angular",
                            IsActive = true,
                            Image = "3.jpg",
                            PublishedOn = DateTime.Now.AddDays(-40),
                            Tags = context.Tags.Take(4).ToList(),
                            UserID = 1,
                            Comments = new List<Comment>
                            {
                                new Comment{Text="Angular guzel bir framework",
                                PublishedOn = DateTime.Now.AddHours(-1),
                                 UserID=2},
                                new Comment{Text="Severek kullanıyorum bu frameworkü.", PublishedOn = DateTime.Now, UserID=1}
                            }
                        },
                        new Post
                        {
                            Title = "Web Design",
                            Content = "Web Design dersleri",
                            Url = "web-design",
                            IsActive = true,
                            Image = "3.jpg",
                            PublishedOn = DateTime.Now.AddDays(-60),
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