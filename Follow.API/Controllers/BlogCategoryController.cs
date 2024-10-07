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
            var data = followContext.BlogCategories.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // find veya firstordefault kullanılabilir. ikisiyle de ayni islemi yapiyoruz.
            //var data = followContext.BlogCategories.Find(id);
            var data = followContext.BlogCategories.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }
            return Ok(data);
        }


        [HttpPost]
        public IActionResult Create(BlogCategory blogCategory)
        {
            followContext.BlogCategories.Add(blogCategory);
            followContext.SaveChanges();

            return Created("" , blogCategory);
        }
    }


}
