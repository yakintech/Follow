using Follow.API.DTO.Blog;
using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Follow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        IGenericRepository<BlogPost> blogPostRepository;

        public BlogController(IGenericRepository<BlogPost> blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
       

        [HttpPost]
        public IActionResult Post([FromForm]CreateBlogRequestDTO model)
        {
            string fileName = "";
            if (model.Image != null)
            {
                if (model.Image.ContentType != "image/jpeg" && model.Image.ContentType != "image/png" && model.Image.ContentType != "image/jpg")
                {
                    return BadRequest("Only jpeg, png, jpg files are allowed.");
                }

                //bu resmi projenin içine kaydetmek istiyorum
                fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
            }



            BlogPost blog = new BlogPost
            {
                Title = model.Title,
                Content = model.Content,
                BlogCategoryId = model.CategoryId,
                MainImage = fileName
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
