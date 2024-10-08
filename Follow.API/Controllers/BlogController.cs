using Follow.API.DTO.Blog;
using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Follow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        GenericRepository<BlogPost> blogPostRepository;

        public BlogController()
        {
            blogPostRepository = new GenericRepository<BlogPost>();
        }

        [HttpPost]
        public IActionResult Post(CreateBlogRequestDTO model)
        {
            BlogPost blog = new BlogPost
            {
                Title = model.Title,
                Content = model.Content,
                BlogCategoryId = model.CategoryId
            };

            blogPostRepository.Create(blog);

            return Ok(blog.Id);

        }


        [HttpGet]
        public IActionResult Get()
        {
            List<GetAllBlogPostResponseDTO> getAllBlogPostResponseDTOs = new List<GetAllBlogPostResponseDTO>();

            var data = blogPostRepository.GetAllWithIncludes(new string[] { "BlogCategory" });
            //var data = followContext.BlogPosts.Include("BlogCategory").ToList();
            // select * from BlogPosts inner join BlogCategories on BlogPosts.BlogCategoryId = BlogCategories.Id

            foreach (var item in data)
            {
                getAllBlogPostResponseDTOs.Add(new GetAllBlogPostResponseDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    Category = new BlogPostCategoryDTO
                    {
                        Id = item.BlogCategory.Id,
                        Name = item.BlogCategory.Name
                    }
                });
            }

            return Ok(getAllBlogPostResponseDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = blogPostRepository.GetByIdWitIncludes(id, new string[] { "BlogCategory" });

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }

            var response = new GetAllBlogPostResponseDTO
            {
                Id = data.Id,
                Title = data.Title,
                Content = data.Content,
                Category = new BlogPostCategoryDTO
                {
                    Id = data.BlogCategory.Id,
                    Name = data.BlogCategory.Name
                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = blogPostRepository.GetById(id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }

            blogPostRepository.Delete(id);

            return Ok();
        }

    }
}
