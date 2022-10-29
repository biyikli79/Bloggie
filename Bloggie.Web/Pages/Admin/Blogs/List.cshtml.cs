using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Bloggie.Web.Repositories;
using Bloggie.Web.Models.ViewModels;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository IblogPostRepository;

        public List<BlogPost> blogPosts{ get; set; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
            //_bloggieDbContext = bloggieDbContext;
            IblogPostRepository = blogPostRepository;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
           
            if(notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson.ToString());

            }

            blogPosts = (await IblogPostRepository.GetAllAsync()).ToList();

        }
    }
}
