using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    public JsonResult AddComment(int postId, string text, string postUrl)
    {   
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var avatar = User.FindFirstValue(ClaimTypes.UserData);
        var entity = new Comment
        {
            Text = text,
            PublishedOn = DateTime.Now,
            PostID = postId,
            UserID = int.Parse(userId ?? "")     
        };
        _commentRepository.CreateComment(entity);
        // return Redirect("/posts/details/" + Url);
        // return RedirectToRoute("post-details", new {Url = postUrl});
        return Json(new
        {
            success = true, userName, text, entity.PublishedOn, avatar
        });
    }

    public IActionResult Create()
    {
     return View();   
    }

    [HttpPost]
    public IActionResult Create(PostCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _postRepository.CreatePost(new Post
            {
               Title = model.Title,
               Content = model.Content,
               Url = model.Url,
               UserID = int.Parse(userId ?? ""),
               PublishedOn = DateTime.Now,
               Image = "1.jpg",
               IsActive = false
            });
            return RedirectToAction("Index");
        }
        return View(model);
    }
}