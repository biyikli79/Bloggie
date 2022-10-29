using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

       
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {

           var existingBlog = await _bloggieDbContext.BlogPosts.FindAsync(Id);

            if(existingBlog != null)
            {
               _bloggieDbContext.BlogPosts.Remove(existingBlog);
               await _bloggieDbContext.SaveChangesAsync();
               return true;

            }

            return false;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieDbContext.BlogPosts.ToListAsync();


        }

        public async Task<BlogPost> GetAsync(Guid Id)
        {
            return await _bloggieDbContext.BlogPosts.FindAsync(Id);

            
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(blogPost.Id);

            if(existingBlogPost != null)
            {

                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;

            }

            await _bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;

        }
    }
}
