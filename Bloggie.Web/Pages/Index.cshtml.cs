using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IBlogPostRepository _IblogPostRepository;


        public List<BlogPost> Blogs { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlogPostRepository iblogPostRepository)
        {
            _logger = logger;
            _IblogPostRepository = iblogPostRepository;
        }

        
        public async Task<IActionResult> OnGet()
        {
            Blogs = (await _IblogPostRepository.GetAllAsync()).ToList();
            return Page();

        }
    }
}