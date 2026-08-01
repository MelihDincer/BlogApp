using BlogApp.Entity;

namespace BlogApp.Models;

public class PostsViewModel
{
    public List<Tag> Tags { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
}