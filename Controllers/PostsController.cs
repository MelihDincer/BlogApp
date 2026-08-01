using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    public PostsController(IPostRepository repository, ITagRepository tagRepository)
    {
        _postRepository = repository;
        _tagRepository = tagRepository;
    }
    public IActionResult Index()
    {
        return View(new PostsViewModel
        {
            Posts = _postRepository.Posts.ToList(),
            Tags = _tagRepository.Tags.ToList()
        });
    }
}