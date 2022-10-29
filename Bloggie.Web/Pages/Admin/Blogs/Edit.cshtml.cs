using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {

        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository IblogPostRepository;

        [BindProperty]
        public BlogPost blogPost { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public EditModel(IBlogPostRepository blogPostRepository)
        {
            //_bloggieDbContext = bloggieDbContext;
            IblogPostRepository = blogPostRepository;
        }
        public async Task OnGet(Guid id)
        {
            blogPost = await IblogPostRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {

            try
            {

                await IblogPostRepository.UpdateAsync(blogPost);

                ViewData["Notification"] = new Notification()
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Record updated succesfully!",

                };
            }
            catch (Exception Ex)
            {
                ViewData["Notification"] = new Notification()
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Something went wrong!",

                };

            }

            return Page();

        }

        public async Task<IActionResult> OnPostDelete()
        {
            
            var deleted = await IblogPostRepository.DeleteAsync(blogPost.Id);

            if(deleted == true)
            {

                var notification = new Notification()
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Blog was deleted!",

                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");

            }
           
            return Page();

        }

    }
}
