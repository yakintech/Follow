using AutoMapper;
using Follow.API.DTO.WebUser;
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
    public class WebUserController : ControllerBase
    {

        private IGenericRepository<WebUser> webUserRepository;
        private readonly IMapper _mapper;

        public WebUserController(IGenericRepository<WebUser> webUserRepository, IMapper mapper)
        {
            this.webUserRepository = webUserRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(CreateWebUserRequestDTO model)
        {
            //WebUser webUser = new WebUser
            //{
            //    Name = model.Name,
            //    Surname = model.Surname,
            //    Email = model.Email,
            //    Phone = model.Phone,
            //    Address = model.Address,
            //    City = model.City,
            //    Country = model.Country
            //};

            var webUser = _mapper.Map<WebUser>(model);

            webUserRepository.Create(webUser);

            return Created("",webUser.Id);
        }
    }
}
