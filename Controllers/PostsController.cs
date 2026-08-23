using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    public PostsController(IPostRepository repository, ICommentRepository commentRepository)
    {
        _postRepository = repository;
        _commentRepository = commentRepository;
    }
    public async Task<IActionResult> Index(string tag)
    {
        var posts = _postRepository.Posts;
        if(!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
        }
        return View(new PostsViewModel
        {
            Posts = await posts.ToListAsync()
            
        });
    }

    public async Task<IActionResult> Details(string? url)
    {
        return View(await _postRepository.Posts.Include(p => p.Tags).Include(p => p.Comments).ThenInclude(c => c.User).FirstOrDefaultAsync(p => p.Url == url));
    }

    public IActionResult AddComment(int postId, string userName, string text, string postUrl)
    {
        var entity = new Comment
        {
            Text = text,
            PublishedOn = DateTime.Now,
            PostID = postId,
            User = new User
            {
                UserName = userName, Image = "avatar.jpg"
            }
        };
        _commentRepository.CreateComment(entity);
        // return Redirect("/posts/details/" + Url);
        return RedirectToRoute("post-details", new {Url = postUrl});
    }
}