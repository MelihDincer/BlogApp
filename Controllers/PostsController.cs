using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    public PostsController(IPostRepository repository)
    {
        _postRepository = repository;
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
}