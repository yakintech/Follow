using Follow.API.DTO.BlogCategory;
using Follow.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Follow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private FollowContext followContext;

        public BlogCategoryController()
        {
            followContext = new FollowContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<BlogCategory> data = followContext.BlogCategories.ToList();
            List<GetAllBlogCategoriesResponseDTO> response = new List<GetAllBlogCategoriesResponseDTO>();

            foreach (var item in data)
            {
                response.Add(new GetAllBlogCategoriesResponseDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                });
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // find veya firstordefault kullanılabilir. ikisiyle de ayni islemi yapiyoruz.
            //var data = followContext.BlogCategories.Find(id);
            var data = followContext.BlogCategories.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }

            var response = new GetBlogCategoryByIdResponseDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
            return Ok(response);
        }


        [HttpPost]
        public IActionResult Create(CreateBlogCategoryRequestDTO blogCategory)
        {
            var newBlogCategory = new BlogCategory
            {
                Name = blogCategory.Name,
                Description = blogCategory.Description
            };

            followContext.BlogCategories.Add(newBlogCategory);
            followContext.SaveChanges();

            return Created("", newBlogCategory.Id);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = followContext.BlogCategories.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }

            followContext.BlogCategories.Remove(data);
            followContext.SaveChanges();

            return Ok();
        }


        [HttpPut]
        public IActionResult Update(UpdateBlogCategoryRequestDto blogCategory)
        {
            var data = followContext.BlogCategories.FirstOrDefault(x => x.Id == blogCategory.Id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + blogCategory.Id);
            }

            data.Name = blogCategory.Name;
            data.Description = blogCategory.Description;

            followContext.SaveChanges();

            return Ok(data.Id);
        }

       
    
    }


}
