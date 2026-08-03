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
    public IActionResult Index()
    {
        return View(new PostsViewModel
        {
            Posts = _postRepository.Posts.ToList()
            
        });
    }

    public async Task<IActionResult> Details(int? id)
    {
        return View(await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostID == id));
    }
}