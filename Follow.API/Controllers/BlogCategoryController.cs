﻿using Follow.API.DTO.BlogCategory;
using Follow.API.Filters;
using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Follow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogCategoryController : ControllerBase
    {
        private IGenericRepository<BlogCategory> blogCategoryRepository;

        public BlogCategoryController(IGenericRepository<BlogCategory> blogCategoryRepository)
        {
            this.blogCategoryRepository = blogCategoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<BlogCategory> data = blogCategoryRepository.GetAll();
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
            
            var data = blogCategoryRepository.GetById(id);

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

            blogCategoryRepository.Create(newBlogCategory);

            return Created("", newBlogCategory.Id);
        }


        [HttpDelete("{id}")]
        [RoleControl("Admin")]
        public IActionResult Delete(int id)
        {
            var data = blogCategoryRepository.GetById(id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + id);
            }

            blogCategoryRepository.Delete(id);

            return Ok();
        }


        [HttpPut]
        public IActionResult Update(UpdateBlogCategoryRequestDto blogCategory)
        {
            var data = blogCategoryRepository.GetById(blogCategory.Id);

            if (data == null)
            {
                return NotFound("Data not found. Id: " + blogCategory.Id);
            }

            data.Name = blogCategory.Name;
            data.Description = blogCategory.Description;

            blogCategoryRepository.Update(data);

            return Ok(data.Id);
        }

       
    
    }


}
